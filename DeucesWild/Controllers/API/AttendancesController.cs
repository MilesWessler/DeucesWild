using DeucesWild.Dtos;
using DeucesWild.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;




namespace DeucesWild.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.TournamentId == dto.TournamentId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance
            {
                TournamentId = dto.TournamentId,
                AttendeeId = userId,
                Name = User.Identity.Name
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }

        //public IEnumerable<Attendance> GetAllAttendees(int id)
        //{

        //    var attendances = _context.Attendances
        //        .Where(a => a.TournamentId == 1)
        //        .Include(a => a.Attendee.Name)
        //        .Include(a => a.Tournament);

        //    return attendances.ToList();
    }
}

