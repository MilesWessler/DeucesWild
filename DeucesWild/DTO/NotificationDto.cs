using DeucesWild.Models;
using System;

namespace DeucesWild.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public TournamentDto Tournament { get; set; }
    }
}