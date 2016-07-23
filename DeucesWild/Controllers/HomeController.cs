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

        public ActionResult Index(string query = null)
        {
            var upcomingTournaments = _context.Tournaments
                .Include(g => g.User)
                .Include(g => g.Category)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingTournaments = upcomingTournaments
                    .Where(g =>
                            g.User.Name.Contains(query) ||
                            g.Category.Name.Contains(query) ||
                            g.Casino.Contains(query));
            }

            var viewModel = new TournamentsViewModel
            {
                UpcomingTournaments = upcomingTournaments,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query
            };

            return View("Tournaments", viewModel);
        }
    }
}