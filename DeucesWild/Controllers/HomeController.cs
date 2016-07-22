using DeucesWild.Models;
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

        public ActionResult Index()
        {
            //Include used to eagerload
            var upcomingTournaments = _context.Tournaments.Include(g => g.User)
                .Include(g => g.Category)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            return View(upcomingTournaments);
        }
    }
}