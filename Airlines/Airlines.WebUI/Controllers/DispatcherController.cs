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
    [Authorize(Roles = "dispatcher")]
    public class DispatcherController : Controller
    {
        UnitOfWork unitOfWork;

        public DispatcherController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Dispatcher
        public ActionResult Index()
        {
            AdminViewModel a = new AdminViewModel
            {
                Races = unitOfWork.Races.GetList()
            };
            return View(a);
        }
        public ActionResult Queries()
        {
            return View(unitOfWork.Queries.GetList());
        }
    }
}