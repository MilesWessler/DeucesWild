using DeucesWild.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Tournament Tournament { get; private set; }

        public Notification()
        {

        }

        private Notification(NotificationType type, Tournament tournament)
        {
            if (tournament == null)
                throw new ArgumentNullException("tournament");

            Type = type;
            Tournament = tournament;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Tournament gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }
        public static Notification GigUpdated(Tournament newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification GigCanceled(Tournament gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}