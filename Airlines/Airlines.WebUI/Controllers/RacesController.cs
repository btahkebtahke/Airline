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

        private AirlineContext db = new AirlineContext();
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "ID_desc" : "";
            var races = repository.GetList();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                races = races.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
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

            //int pageSize = 3;
            //int pageNumber = (page ?? 1);
            return View(races);
        }
        [HttpPost]
        public ActionResult Filter(string newDeparture, string newDestination, DateTime? newDate)
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

            return View("Index", races);
        }


    

        // GET: Races/Create
        public ActionResult Create()
        {
            ViewBag.RaceTeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID");
            return View();
        }

        // POST: Races/Create
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
