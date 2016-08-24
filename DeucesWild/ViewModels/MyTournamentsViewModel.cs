using DeucesWild.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeucesWild.ViewModels
{
    public class MyTournamentsViewModel
    {
        public int NumberOfTournaments { get; set; }
        public int NumberOfTournamentsRegistered { get; set; }
        public int Top10Finishes { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "n/a", ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public double CurrentBankRoll { get; set; }
        public List<MyTournament> UpcomingTournaments { get; set; }
        public double[] Bankroll { get; set; }
        public double AmountLost { get; set; }

    }
}