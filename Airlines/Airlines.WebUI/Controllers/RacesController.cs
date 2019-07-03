using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Airlines.Data.Entities;
using Airlines.Data.OtherActions;
using Airlines.Data.Repository;
using PagedList;

namespace Airlines.WebUI.Controllers
{
    public class RacesController : Controller
    {

        private IAirlineRepository<Race> repository;

        public RacesController()
        {

        }
        public RacesController(IAirlineRepository<Race> repo)
        {
            repository = repo;
        }
        [AllowAnonymous]
        public ViewResult Index(string sortOrder, int? currentFilter, int? number, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "ID_desc" : "";
            var races = repository.GetList();
            if (number != null)
            {
                page = 1;
            }
            else
            {
                number = currentFilter;
            }
            ViewBag.CurrentFilter = number;
            if (number != null)
            {
                races = races.Where(r => r.ID == number);
            }

            switch (sortOrder)
            {
                case "name":
                    races = races.OrderBy(s => s.Name);
                    break;
                case "ID_desc":
                    races = races.OrderByDescending(s => s.ID);
                    break;
                case "name_desc":
                    races = races.OrderByDescending(s => s.Name);
                    break;
                default:
                    races = races.OrderBy(s => s.ID);
                    break;
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);


            return View(races.ToPagedList(pageNumber, pageSize));
            //return View(races);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Filter(string newDeparture, string newDestination, DateTime? newDate, int? page)
        {
            var races = repository.GetList();
            if (newDate == null || newDeparture == null || newDestination ==null)
            {
                    ModelState.AddModelError("", "Please make sure that all the parameters were used.");
            }            
            if (ModelState.IsValid)
            {
                races = races
                .Where(s => s.Departure == newDeparture)
                .Where(s => s.Destinaton == newDestination)
                .Where(s => s.Date == newDate);
                ViewBag.Date = newDate;
                ViewBag.Departure = newDeparture;
                ViewBag.Destination = newDestination;
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);


            return View("Index",races.ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles ="admin")]
        // GET: Races/Create
        public ActionResult Create()
        {
            ViewBag.RaceTeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID");
            return View();
        }

        [Authorize(Roles = "admin")]// POST: Races/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Departure,Destinaton,Date,IsDeparted,RaceTeamID")] Race race)
        {
            if (ModelState.IsValid)
            {
                repository.Create(race);
                repository.Save();
                return RedirectToAction("Index","Admin","");
            }

            ViewBag.RaceTeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID", race.RaceTeamID);
            return View(race);
        }

        // GET: Races/Edit/5
        [Authorize(Roles = "admin, dispatcher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = repository.GetByID(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            ViewBag.RaceTeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID", race.RaceTeamID);
            return View(race);
        }

        // POST: Races/Edit/5
        [Authorize(Roles ="admin, dispatcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Departure,Destinaton,Date,IsDeparted,RaceTeamID")] Race race)
        {
            if (ModelState.IsValid)
            {
                repository.Update(race);
                repository.Save();

                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Index", "Admin", "");
                }
                else
                {
                    return RedirectToAction("Index", "Dispatcher", "");
                }
            }
            ViewBag.RaceTeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID", race.RaceTeamID);
            return View(race);
        }
        [Authorize(Roles = "admin")]
        // GET: Races/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = repository.GetByID(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            return View(race);
        }
        [Authorize(Roles = "admin")]
        // POST: Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            repository.Save();
            return RedirectToAction("Index", "Admin", "");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
