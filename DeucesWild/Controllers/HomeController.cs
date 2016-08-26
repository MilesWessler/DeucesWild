using DeucesWild.Models;
using DeucesWild.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DeucesWild.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(int? id, string query = null)
        {
            var upcomingTournaments = _context.Tournaments
                .Include(g => g.Member)
                .Include(g => g.Category)
                .Include(l => l.Location)
                .Include(a => a.Attendances)
               .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled).ToList();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingTournaments = upcomingTournaments
                    .Where(g =>
                            g.Member.Name.Contains(query) ||
                            g.Category.Name.Contains(query) ||
                            g.Venue.Contains(query)).ToList();
            }


            var viewModel = new TournamentsViewModel
            {
                UpcomingTournaments = upcomingTournaments,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Featured Tournaments",
                SearchTerm = query
            };


            return View("Tournaments", viewModel);
        }

        //public ActionResult Tournaments(int? id, Tournament tournament, string query = null)
        //{
        //    var upcomingTournaments = _context.Tournaments
        //      .Include(g => g.Member)
        //      .Include(g => g.Category)
        //      .Include(l => l.Location)
        //      .Include(a => a.Attendances)
        //     .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled).ToList();

        //    if (!String.IsNullOrWhiteSpace(query))
        //    {
        //        upcomingTournaments = upcomingTournaments
        //            .Where(g =>
        //                    g.Member.Name.Contains(query) ||
        //                    g.Category.Name.Contains(query) ||
        //                    g.Venue.Contains(query)).ToList();
        //    }


        //    var viewModel = new TournamentsViewModel
        //    {
        //        Id = tournament.Id,
        //        UpcomingTournaments = upcomingTournaments,
        //        ShowActions = User.Identity.IsAuthenticated,
        //        Heading = "Featured Tournaments",
        //        SearchTerm = query
        //    };


        //    return View("Tournaments", viewModel);

        //}
        public ActionResult TournamentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = _context.Tournaments.Find(id);
            var attendees = _context.Tournaments.Include(a => a.Attendances).Where(x => x.Id == tournament.Id);
            var firstOrDefault = attendees.FirstOrDefault();
            if (firstOrDefault != null) tournament.Attendances = firstOrDefault.Attendances;

            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }
    }
}