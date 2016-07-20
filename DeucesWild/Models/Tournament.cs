using System;
using System.ComponentModel.DataAnnotations;

namespace DeucesWild.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Category Category { get; set; }

        [Required]
        public byte GenreId { get; set; }

        //public ICollection<Attendance> Attendances { get; private set; }

        //public Tournament()
        //{
        //    Attendances = new Collection<Attendance>();
        //}

        //public void Cancel()
        //{

        //    IsCanceled = true;

        //    var notification = Notification.GigCanceled(this);

        //    foreach (var attendee in Attendances.Select(a => a.Attendee))
        //    {
        //        attendee.Notify(notification);

        //    }

        //}

        //public void Modify(DateTime dateTime, string venue, byte genre)
        //{
        //    var notification = Notification.GigUpdated(this, DateTime, Venue);

        //    Venue = venue;
        //    DateTime = dateTime;
        //    GenreId = genre;

        //    foreach (var attendee in Attendances.Select(a => a.Attendee))
        //    {
        //        attendee.Notify(notification);
        //    }
    }
}
