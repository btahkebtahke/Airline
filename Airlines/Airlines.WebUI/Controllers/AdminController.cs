using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airlines.Data.Entities;
using Airlines.WebUI.Models;
using Airlines.Data.Repository;
using System.Data.Entity;

namespace Airlines.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        AirlineContext db = new AirlineContext();
        //IAirlineRepository<Race> raceRepo;
        //IAirlineRepository<RaceTeam> raceTRepo;
        //IAirlineRepository<Navigator> navRepo;
        //IAirlineRepository<Stuardess> stdRepo;
        //IAirlineRepository<RadioMan> radRepo;
        //IAirlineRepository<Query> queRepo;
        public AdminController()
        {

        }
        //public AdminController(
        //    RaceRepository raceRepo,
        //    RaceTeamRepository raceTRepo,
        //    StuardessRepository stdRepo,
        //    NavigatorRepository navRepo,
        //    RadioManRepository radRepo,
        //    QueryRepository queRepo
        //    )
        //{
        //    this.raceRepo = raceRepo;
        //    this.raceTRepo = raceTRepo;
        //    this.stdRepo = stdRepo;
        //    this.navRepo = navRepo;
        //    this.radRepo = radRepo;
        //    this.queRepo = queRepo;
        //}
       


       //AirlineContext db = new AirlineContext();
        public ActionResult Index()
        {
            AdminViewModel admin = new AdminViewModel{
                Stuardesses = db.Stuardesses.Include(t => t.Team).ToList(),
                //Races = raceRepo.GetList(),
                Races  = db.Races.Include(t=>t.RaceTeam).ToList(),
            //Navigators = navRepo.GetList(),
                Navigators = db.Navigators.ToList(),
                //RadioMen = radRepo.GetList(),
                RadioMen = db.RadioMen.ToList(),
                //Queries = queRepo.GetList()
                Queries = db.Queries.Include(r=>r.RaceTeam).Include(r=>r.Race).ToList(),
            };
            return View(admin);
        }
    }
}