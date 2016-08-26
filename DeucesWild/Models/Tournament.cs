using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DeucesWild.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public bool IsCanceled { get; private set; }
        public double EntryFee { get; set; }
        public double PrizePool { get; set; }
        public int NumberOfEntries { get; set; }
        public byte[] Image { get; set; }
        public int LocationId { get; set; }
        public string TournamentName { get; set; }
        public Location Location { get; set; }
        public ApplicationUser Member { get; set; }
        public string AttendeeId { get; set; }
        public IEnumerable<Attendance> Attendee { get; set; }

        [Required]
        public string MemberId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Category Category { get; set; }

        [Required]
        public byte CategoryId { get; set; }

        public double PlayerOfTheYearPoints { get; set; }


        public ICollection<Attendance> Attendances { get; set; }

        public Tournament()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {

            IsCanceled = true;

            var notification = Notification.TournamentCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);

            }

        }

        public void Modify(DateTime dateTime, string venue, byte category)
        {
            var notification = Notification.TournamentUpdated(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            CategoryId = category;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}