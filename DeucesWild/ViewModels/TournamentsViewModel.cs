using DeucesWild.Models;
using System.Collections.Generic;

namespace DeucesWild.ViewModels
{
    public class TournamentsViewModel
    {
        public string Heading { get; set; }
        public bool ShowActions { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<Tournament> UpcomingTournaments { get; set; }
        public IEnumerable<ICollection<Attendance>> Attendances { get; set; }
        public IEnumerable<ApplicationUser> Attending { get; set; }
        public string Attendees { get; set; }
    }
}