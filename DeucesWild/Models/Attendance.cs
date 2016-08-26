using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeucesWild.Models
{
    public class Attendance
    {
        public Tournament Tournament { get; set; }
        public ApplicationUser Attendee { get; set; }

        [Key]
        [Column(Order = 1)]
        public int TournamentId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }

        public string Name { get; set; }
    }
}