using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Airlines.Data.Entities;
using Airlines.Data.Repository;
using Airlines.WebUI.Models;

namespace Airlines.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class PilotsController : Controller
    {
        IAirlineRepository<Pilot> repository;
        public PilotsController()
        {

        }
        public PilotsController(IAirlineRepository<Pilot> repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            AdminViewModel adminWiewModel = new AdminViewModel
            {
                Pilots = repository.GetList()
            };
            return PartialView(adminWiewModel);
        }

        // GET: Pilots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilot pilot = repository.GetByID(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        // GET: Pilots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pilots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName")] Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                repository.Create(pilot);
                repository.Save();
                return RedirectToAction("Index","Admin","");
            }

            return View(pilot);
        }

        // GET: Pilots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilot pilot = repository.GetByID(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        // POST: Pilots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName")] Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                repository.Update(pilot);
                repository.Save();
                return RedirectToAction("Index", "Admin", "");
            }
            return View(pilot);
        }

        // GET: Pilots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilot pilot = repository.GetByID(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        // POST: Pilots/Delete/5
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
