using System;

namespace DeucesWild.Models
{
    public class Bankroll
    {
        public int Id { get; set; }
        public string PlayerId { get; set; }
        public ApplicationUser Player { get; set; }
        public double BankrollStartAmount { get; set; }
        public double BankrollCurrentAmount { get; set; }
        public double WinAmount { get; set; }
        public double LoseAmount { get; set; }
        public DateTime Date { get; set; }

    }
}