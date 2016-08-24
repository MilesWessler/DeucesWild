using DeucesWild.Models;
using System;
using System.Collections.Generic;

namespace DeucesWild.ViewModels
{
    public class MyTournamentsFormViewModel
    {
        public int Id { get; set; }
        public ApplicationUser Player { get; set; }
        public string Name { get; set; }
        public double Buyin { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public int Place { get; set; }
        public double AmountWon { get; set; }
        public double AmountLost { get; set; }
        public byte Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}