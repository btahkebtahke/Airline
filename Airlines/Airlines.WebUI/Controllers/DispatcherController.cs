using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airlines.Data.Repository;
using System.Data.Entity;
using Airlines.Data.Entities;
using Airlines.WebUI.Models;

namespace Airlines.WebUI.Controllers
{
    public class DispatcherController : Controller
    {
        AirlineContext db = new AirlineContext();
        [Authorize(Roles="dispatcher")]
        // GET: Dispatcher
        public ActionResult Index()
        {
            AdminViewModel a = new AdminViewModel
            {
                Races = db.Races.Include(p => p.RaceTeam).ToList()
            };
            return View(a);
        }

        public ActionResult Queries()
        {
            return View(db.Queries.Include(r=>r.Race).Include(r=>r.RaceTeam));
        }
    }
}