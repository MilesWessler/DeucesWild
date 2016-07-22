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
            var gig = _context.Tournaments
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.UserId == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Casino, viewModel.Category);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Tournaments");
        }
    }
}