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

        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Casino { get; set; }

        public Category Category { get; set; }

        [Required]
        public byte CategoryId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

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

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.TournamentUpdated(this, DateTime, Casino);

            Casino = venue;
            DateTime = dateTime;
            CategoryId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}
