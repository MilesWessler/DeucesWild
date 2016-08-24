using DeucesWild.Models;
using DeucesWild.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DeucesWild.Controllers
{
    public class MyTournamentsController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: MyTournaments
        public ActionResult Index()
        {
            return View(_context.MyTournaments.ToList());
        }

        public ActionResult Dashboard()
        {

            var userId = User.Identity.GetUserId();

            List<MyTournament> myTournaments = _context.MyTournaments
                .Where(p => p.PlayerId == userId).ToList();

            var numberOfRegistrations = _context.Attendances.Count(p => p.AttendeeId == userId);

            var top10Finishes = myTournaments.Count(t => t.Place <= 10);

            var amountWon = myTournaments.Sum(x => x.AmountWon);
            var amountLost = myTournaments.Sum(x => x.AmountLost);
            double[] bankroll =
                _context.Bankrolls.Where(x => x.PlayerId == userId).Select(x => x.BankrollCurrentAmount).ToArray();

            var currentBankRoll =
                _context.Bankrolls.Where(x => x.PlayerId == userId)
                    .OrderByDescending(p => p.Id)
                    .Select(x => x.BankrollCurrentAmount)
                    .First();

            var mytournaments = new MyTournamentsViewModel()
            {

                NumberOfTournaments = myTournaments.Count(),
                NumberOfTournamentsRegistered = numberOfRegistrations,
                Top10Finishes = top10Finishes,
                CurrentBankRoll = currentBankRoll,
                Bankroll = bankroll,
            };

            return View(mytournaments);
        }

        // GET: MyTournaments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyTournament myTournament = _context.MyTournaments.Find(id);
            if (myTournament == null)
            {
                return HttpNotFound();
            }
            return View(myTournament);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new MyTournamentsFormViewModel()
            {
                Categories = _context.Categories.ToList(),
            };

            return View("Create", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyTournamentsFormViewModel formViewModel)
        {
            if (!ModelState.IsValid)
            {
                formViewModel.Categories = _context.Categories.ToList();
                return View("Create", formViewModel);
            }

            var tournament = new MyTournament()
            {
                PlayerId = User.Identity.GetUserId(),
                CategoryId = formViewModel.Category,
                Name = formViewModel.Name,
                AmountWon = formViewModel.AmountWon,
                AmountLost = formViewModel.AmountLost,
                Date = formViewModel.Date,
                Buyin = formViewModel.Buyin,
                Notes = formViewModel.Notes,
                Place = formViewModel.Place,
            };
            var userId = User.Identity.GetUserId();
            var startAmount = _context.Users.Where(x => x.Id == userId).Sum(x => x.Bankroll);
            var winAmount = _context.Bankrolls.Where(x => x.PlayerId == userId).Sum(x => x.WinAmount);
            var loseAmount = _context.Bankrolls.Where(x => x.PlayerId == userId).Sum(x => x.LoseAmount);
            var bankroll = new Bankroll()
            {
                PlayerId = User.Identity.GetUserId(),
                WinAmount = formViewModel.AmountWon,
                LoseAmount = formViewModel.AmountLost,
                BankrollStartAmount = startAmount,
                BankrollCurrentAmount = +(winAmount + formViewModel.AmountWon) - (formViewModel.AmountLost + loseAmount) + startAmount,
                Date = DateTime.Today
            };

            _context.Bankrolls.Add(bankroll);
            _context.MyTournaments.Add(tournament);
            _context.SaveChanges();


            return RedirectToAction("Dashboard");
        }

        // GET: MyTournaments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyTournament myTournament = _context.MyTournaments.Find(id);
            if (myTournament == null)
            {
                return HttpNotFound();
            }
            return View(myTournament);
        }

        // POST: MyTournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Buyin,Date,Notes,Place,AmountWon")] MyTournament myTournament)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(myTournament).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myTournament);
        }

        // GET: MyTournaments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyTournament myTournament = _context.MyTournaments.Find(id);
            if (myTournament == null)
            {
                return HttpNotFound();
            }
            return View(myTournament);
        }

        // POST: MyTournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyTournament myTournament = _context.MyTournaments.Find(id);
            _context.MyTournaments.Remove(myTournament);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
