using DeucesWild.Dtos;
using DeucesWild.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;


namespace DeucesWild.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            string userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Tournament.User);

            return notifications.Select(n => new NotificationDto()
            {
                DateTime = n.DateTime,
                TournamentDto = new TournamentDto
                {
                    User = new UserDto
                    {
                        Id = n.Tournament.User.Id,
                        Name = n.Tournament.User.Name
                    },
                    DateTime = n.Tournament.DateTime,
                    Id = n.Tournament.Id,
                    IsCanceled = n.Tournament.IsCanceled,
                    Casino = n.Tournament.Casino
                },
                OriginalDateTime = n.OriginalDateTime,
                OriginalVenue = n.OriginalVenue,
                Type = n.Type
            });
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
