using DeucesWild.Models;
using DeucesWild.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
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


        //public ActionResult TestIndex(int? id, string query = null)
        //{
        //    var upcomingTournaments = _context.Tournaments
        //       .Include(g => g.Member)
        //       .Include(g => g.Category)
        //       .Include(l => l.Location)
        //       .Include(a => a.Attendances)
        //       .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);


        //    if (!String.IsNullOrWhiteSpace(query))
        //    {
        //        upcomingTournaments = upcomingTournaments
        //            .Where(g =>
        //                    g.Member.Name.Contains(query) ||
        //                    g.Category.Name.Contains(query) ||
        //                    g.Venue.Contains(query));
        //    }


        //    var viewModel = new TournamentsViewModel
        //    {
        //        UpcomingTournaments = upcomingTournaments,
        //        ShowActions = User.Identity.IsAuthenticated,
        //        Heading = "Featured Tournaments",
        //        //Count = count,
        //        SearchTerm = query
        //    };

        //    return View("TestIndex", viewModel);
        //}


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
    }
}