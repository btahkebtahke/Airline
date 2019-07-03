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
using Airlines.Data.OtherActions;

namespace Airlines.WebUI.Controllers
{
    [Authorize(Roles = "admin, dispatcher")]
    public class QueriesController : Controller
    {
        
        IAirlineRepository<Query> repository;
        public QueriesController()
        {

        }
        public QueriesController(IAirlineRepository<Query> repo)
        {
            repository = repo;
        }

        // GET: Queries
        public ActionResult Index()
        { 
            return View(repository.GetList());
        }
        //Accept button in admin
        public ActionResult Update(int id)
        {
            repository.Update(repository.GetByID(id));
            repository.Save();
            return RedirectToAction("Index", "Admin", "");
        }

        // GET: Queries/Create
        public ActionResult Create(int id)
        {
            ViewBag.RaceTeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID");
            return View();
        }

        // POST: Queries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Query query) 
        {
            if (ModelState.IsValid)
            {
                query.RaceID = id;
                repository.Create(query);
                repository.Save();
                return RedirectToAction("Queries", "Dispatcher", "");
            }
            ViewBag.RaceTeamID = new SelectList(Methods.PopulateRaceTeamDropDownList(), "ID", "ID", query.RaceTeamID);
            return View(query);
        }

        //Decline button in Admin
      public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repository.Delete(id);
            repository.Save();
            if (repository.GetByID(id) == null)
            {
                return HttpNotFound();
            }
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
