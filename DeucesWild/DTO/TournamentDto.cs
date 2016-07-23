using System;

namespace DeucesWild.Dtos
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public UserDto User { get; set; }
        public DateTime DateTime { get; set; }
        public string Casino { get; set; }
        public CategoryDto Category { get; set; }
    }
}