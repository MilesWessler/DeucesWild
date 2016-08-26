using DeucesWild.Models;
using System.Collections.Generic;

namespace DeucesWild.ViewModels
{
    public class TournamentsViewModel
    {
        public int? Id { get; set; }
        public string Heading { get; set; }
        public bool ShowActions { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<Tournament> UpcomingTournaments { get; set; }
        public IEnumerable<ICollection<Attendance>> Attendances { get; set; }
        public IEnumerable<ApplicationUser> Attending { get; set; }

        public List<string> Attendees { get; set; }
        public int NumberOfAttendees { get; set; }
    }
}