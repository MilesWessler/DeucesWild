using System;
using System.Collections.Generic;

namespace DeucesWild.Models
{
    public class MyTournament
    {
        public int Id { get; set; }
        public string PlayerId { get; set; }
        public ApplicationUser Player { get; set; }
        public byte CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string Name { get; set; }
        public double Buyin { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public int Place { get; set; }
        public double AmountWon { get; set; }
        public double AmountLost { get; set; }
        public Bankroll Bankroll { get; set; }

    }
}