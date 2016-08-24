using DeucesWild.Models;
using DeucesWild.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeucesWild.Controllers
{
    public class TournamentsController : Controller
    {
        private ApplicationDbContext _context;

        public TournamentsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var tournaments = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Tournament)
                .Include(g => g.Member)
                .Include(g => g.Category)
                .ToList();

            var viewModel = new TournamentsViewModel()
            {
                UpcomingTournaments = tournaments,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Tournaments I'm Attending"
            };

            return View("Tournaments", viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var tournaments = _context.Tournaments
                .Where(g =>
                    g.MemberId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Category)
                .ToList();

            return View(tournaments);
        }


        [Authorize]
        public ActionResult Create(TournamentFormViewModel model)
        {
            var viewModel = new TournamentFormViewModel()
            {
                Categories = _context.Categories.ToList(),
                Heading = "Add a tournament"
            };

            return View("TournamentForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TournamentFormViewModel viewModel, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                //viewModel.States = _context.States.ToList();

                return View("TournamentForm", viewModel);
            }

            byte[] data = GetBytesFromFile(file);

            var tournament = new Tournament()
            {
                MemberId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Venue = viewModel.Venue,
                EntryFee = viewModel.EntryFee,
                Image = data,
                Location = viewModel.Location
            };

            _context.Tournaments.Add(tournament);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Tournaments");
        }

        public byte[] GetBytesFromFile(HttpPostedFileBase file)
        {
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                return memoryStream.ToArray();
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TournamentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                return View("TournamentForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var tournament = _context.Tournaments
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.MemberId == userId);

            tournament.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Category);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Tournaments");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var tournament = _context.Tournaments.Single(g => g.Id == id && g.MemberId == userId);

            var viewModel = new TournamentFormViewModel
            {
                Heading = "Edit a Tournament",
                Id = tournament.Id,
                Categories = _context.Categories.ToList(),
                Date = tournament.DateTime.ToString("d MMM yyyy"),
                Time = tournament.DateTime.ToString("HH:mm"),
                Category = tournament.CategoryId,
                Venue = tournament.Venue
            };

            return View("TournamentForm", viewModel);
        }

        [HttpPost]
        public ActionResult Search(TournamentsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }
    }
}