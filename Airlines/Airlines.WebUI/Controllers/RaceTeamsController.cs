using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Airlines.WebUI.Models;
using Airlines.Data.Entities;
using Airlines.Data.Repository;
using Airlines.Data.OtherActions;
using static Airlines.Data.OtherActions.Methods;

namespace Airlines.WebUI.Controllers
{
    public class RaceTeamsController : Controller
    {
        IAirlineRepository<RaceTeam> repository;
        public RaceTeamsController()
        {

        }
        public RaceTeamsController(IAirlineRepository<RaceTeam> repo)
        {
            repository = repo;
        }
        
        [Authorize(Roles = "admin, dispatcher")]
        public ActionResult Show(int id)
        {
            AdminViewModel adminViewModel = new AdminViewModel
            {
                RaceTeam = repository.GetByID(id)
            };
            return PartialView(adminViewModel);
        }
        [Authorize(Roles = "admin, dispatcher")]
        public ActionResult ShowAll()
        {
            return View(repository.GetList());
        }
        // GET: RaceTeams/Create
        [Authorize(Roles = "admin, dispatcher")]
        public ActionResult Create(int id)
        {
            ViewBag.PilotID = new SelectList(GetCustomPilots(), "ID", "Name" , "");
            ViewBag.NavigatorID = new SelectList(GetCustomNavigators(), "ID","Name", "");
            ViewBag.Stuardess1 = new SelectList(GetCustomStuardesses(), "ID", "Name", "");
            ViewBag.Stuardess2 = new SelectList(GetCustomStuardesses(), "ID", "Name", "2");
            ViewBag.RadioManID = new SelectList(GetCustomRadiomen(), "ID", "Name", "");
            return View();
        }
        [Authorize(Roles = "admin, dispatcher")]
        // POST: RaceTeams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, RaceTeam raceTeam, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                repository.Create(raceTeam);
                repository.Save();
                int? firstStuardess = 0;
                int? secondStuardess = 0;

                if (!string.IsNullOrEmpty(form["Stuardess1"]))
                {
                    firstStuardess = int.Parse(form["Stuardess1"]);
                }
                if (!string.IsNullOrEmpty(form["Stuardess2"]))
                {
                    secondStuardess = int.Parse(form["Stuardess2"]);
                }
                UpdateStuardesses(firstStuardess, secondStuardess, raceTeam.ID);
                UpdateRace(id, raceTeam.ID);
            }
            return RedirectToAction("Index","Dispatcher","");
        }
        [Authorize(Roles = "admin, dispatcher")]
        // GET: RaceTeams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaceTeam raceTeam = repository.GetByID(id);
            if (raceTeam == null)
            {
                return HttpNotFound();
            }
            return View(raceTeam);
        }

        [Authorize(Roles="dispatcher, admin")]
        // POST: RaceTeams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RaceTeam raceTeam, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                repository.Update(raceTeam);
                repository.Save();
                int? firstStuardess = 0;
                int? secondStuardess = 0;

                if (!string.IsNullOrEmpty(form["Stuardess1"]))
                {
                    firstStuardess = int.Parse(form["Stuardess1"]);
                }
                if (!string.IsNullOrEmpty(form["Stuardess2"]))
                {
                    secondStuardess = int.Parse(form["Stuardess2"]);
                }
                
                UpdateStuardesses(firstStuardess,secondStuardess,raceTeam.ID);
             
                return RedirectToAction("ShowAll");
            }
            return View(raceTeam);
        }
        [Authorize(Roles = "admin, dispatcher")]
        // GET: RaceTeams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaceTeam raceTeam = repository.GetByID(id);
            if (raceTeam == null)
            {
                return HttpNotFound();
            }
            return View(raceTeam);
        }
        [Authorize(Roles = "admin, dispatcher")]
        // POST: RaceTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            repository.Save();
            return RedirectToAction("ShowAll");
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
