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

namespace Airlines.WebUI.Controllers
{
    public class QueriesController : Controller
    {

        IAirlineRepository<Query> repository;
        private AirlineContext db = new AirlineContext();
        public QueriesController()
        {

        }
        QueriesController(IAirlineRepository<Query> repo)
        {
            repository = repo;
        }

        // GET: Queries
        public ActionResult Index()
        {
            var queries = db.Queries.Include(q => q.Race).Include(q => q.RaceTeam);
            return View(queries.ToList());
        }
        //Accept button in admin
        public ActionResult Update(int id)
        {
            Query query = db.Queries.Include(q => q.Race).Include(q => q.RaceTeam).FirstOrDefault(r => r.ID == id);
            query.IsAccepted = true;
            query.Race.RaceTeamID = query.RaceTeamID;
            db.SaveChanges();
            return RedirectToAction("Index", "Admin", "");
        }

        // GET: Queries/Create
        public ActionResult Create(int id)
        {
            ViewBag.RaceTeamID = new SelectList(db.RaceTeams, "ID", "ID");
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
                db.Queries.Add(query);
                db.SaveChanges();
                return RedirectToAction("Queries", "Dispatcher", "");
            }
            ViewBag.RaceTeamID = new SelectList(db.RaceTeams, "ID", "ID", query.RaceTeamID);
            return View(query);
        }

        //Decline button in Admin
      public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Query query = db.Queries.Find(id);
            if (query == null)
            {
                return HttpNotFound();
            }
            return View(query);
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
