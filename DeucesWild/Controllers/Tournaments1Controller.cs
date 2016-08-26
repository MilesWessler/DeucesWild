using DeucesWild.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DeucesWild.Controllers
{
    public class Tournaments1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tournaments1
        public ActionResult Index()
        {
            var tournaments = db.Tournaments.Include(t => t.Category).Include(t => t.Location).Include(t => t.Member);
            return View(tournaments.ToList());
        }

        // GET: Tournaments1/Details/5
        public ActionResult Details1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            //var att = db.Tournaments.Include(a => a.Attendances).Where(x => x.Id == tournament.Id);
            //tournament.Attendances = att.FirstOrDefault().Attendances;

            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }



        // GET: Tournaments1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }

        // POST: Tournaments1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            db.Tournaments.Remove(tournament);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
