using System;
using System.ComponentModel.DataAnnotations;

namespace DeucesWild.Models
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

        public static Notification TournamentCreated(Tournament tournament)
        {
            return new Notification(NotificationType.Created, tournament);
        }
        public static Notification TournamentUpdated(Tournament newTournament, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.Updated, newTournament);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification TournamentCanceled(Tournament tournament)
        {
            return new Notification(NotificationType.Canceled, tournament);
        }
    }
}