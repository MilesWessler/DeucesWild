using DeucesWild.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace DeucesWild.Controllers.Api
{
    [Authorize]
    public class TournamentsController : ApiController
    {
        private ApplicationDbContext _context;

        public TournamentsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var tournament = _context.Tournaments
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.MemberId == userId);

            if (tournament.IsCanceled)
                return NotFound();

            tournament.Cancel();

            _context.SaveChanges();

            return Ok();
        }
    }
}
