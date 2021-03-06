﻿using DeucesWild.Controllers;
using DeucesWild.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace DeucesWild.ViewModels
{
    public class TournamentFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        //[FutureDate]
        public string Date { get; set; }

        [Required]
        //[ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Category { get; set; }

        public Location Location { get; set; }
        public double EntryFee { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<TournamentsController, ActionResult>> update =
                    (c => c.Update(this));
                Expression<Func<TournamentsController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));

        }

        //public GetDateTime GetDateTime => GetDateTime.Parse($"{Date} {Time}");
    }
}
