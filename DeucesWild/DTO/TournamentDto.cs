using System;

namespace DeucesWild.Dtos
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public UserDto Member { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public CategoryDto Category { get; set; }
    }
}