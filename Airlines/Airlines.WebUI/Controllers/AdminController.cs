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
        UnitOfWork unitOfWork;
        public AdminController()
        {
            unitOfWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            AdminViewModel admin = new AdminViewModel
            {
                Stuardesses = unitOfWork.Stuardesses.GetList(),
                Races = unitOfWork.Races.GetList(),
                Navigators = unitOfWork.Navigators.GetList(),
                RadioMen = unitOfWork.RadioMen.GetList(),
                Queries = unitOfWork.Queries.GetList(),
            };
            return View(admin);
        }
    }
}