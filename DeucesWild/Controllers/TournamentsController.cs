using DeucesWild.Models;
using DeucesWild.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
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
                .Include(g => g.User)
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
                    g.UserId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Category)
                .ToList();

            return View(tournaments);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new TournamentFormViewModel()
            {
                Categories = _context.Categories.ToList(),
                Heading = "Add a Gig"
            };

            return View("TournamentForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TournamentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                return View("TournamentForm", viewModel);
            }

            var tournament = new Tournament
            {
                UserId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Casino = viewModel.Casino
            };

            _context.Tournaments.Add(tournament);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Tournaments");
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
                .Single(g => g.Id == viewModel.Id && g.UserId == userId);

            tournament.Modify(viewModel.GetDateTime(), viewModel.Casino, viewModel.Category);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Tournaments");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var tournament = _context.Tournaments.Single(g => g.Id == id && g.UserId == userId);

            var viewModel = new TournamentFormViewModel
            {
                Heading = "Edit a Tournament",
                Id = tournament.Id,
                Categories = _context.Categories.ToList(),
                Date = tournament.DateTime.ToString("d MMM yyyy"),
                Time = tournament.DateTime.ToString("HH:mm"),
                Category = tournament.CategoryId,
                Casino = tournament.Casino
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