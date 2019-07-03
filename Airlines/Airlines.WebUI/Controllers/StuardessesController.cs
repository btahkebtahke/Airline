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
    [Authorize(Roles = "admin")]
    public class StuardessesController : Controller
    {
        private IAirlineRepository<Stuardess> repository;

        public StuardessesController()
        {

        }
        public StuardessesController(IAirlineRepository<Stuardess> repo)
        {
            repository = repo;
        }

        // GET: Stuardesses
        public ActionResult Index()
        {
            var stuardesses = repository.GetList();
            return View(stuardesses.ToList());
        }

        // GET: Stuardesses/Create
        public ActionResult Create()
        {
            ViewBag.TeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID");
            return View();
        }

        // POST: Stuardesses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TeamID,Form,FirstName,LastName")] Stuardess stuardess)
        {
            if (ModelState.IsValid)
            {
                repository.Create(stuardess);
                repository.Save();
                return RedirectToAction("Index", "Admin", "");
            }

            ViewBag.TeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID", stuardess.TeamID);
            return View(stuardess);
        }

        // GET: Stuardesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stuardess stuardess = repository.GetByID(id);
            if (stuardess == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID", stuardess.TeamID);
            return View(stuardess);
        }

        // POST: Stuardesses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TeamID,Form,FirstName,LastName")] Stuardess stuardess)
        {
            if (ModelState.IsValid)
            {
                repository.Update(stuardess);
                repository.Save();
                return RedirectToAction("Index", "Admin", "");
            }
            ViewBag.TeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID", stuardess.TeamID);
            return View(stuardess);
        }

        // GET: Stuardesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stuardess stuardess = repository.GetByID(id);
            if (stuardess == null)
            {
                return HttpNotFound();
            }
            return View(stuardess);
        }

        // POST: Stuardesses/Delete/5
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
