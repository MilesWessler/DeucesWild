using DeucesWild.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class TournamentsViewModel
    {
        public string Heading { get; set; }
        public bool ShowActions { get; set; }
        public List<Tournament> UpcomingTournaments { get; set; }
    }
}