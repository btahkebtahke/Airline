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
    [Authorize(Roles = "admin")]
    public class NavigatorsController : Controller
    {
        

        IAirlineRepository<Navigator> repository;
        public NavigatorsController()
        {

        }
        public NavigatorsController(IAirlineRepository<Navigator> repo)
        {
            repository = repo;
        }

        // GET: Navigators
        public ActionResult Index()
        {
            return View(repository.GetList());
        }

        // GET: Navigators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Navigators/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName")] Navigator navigator)
        {
            if (ModelState.IsValid)
            {
                repository.Create(navigator);
                repository.Save();
                return RedirectToAction("Index", "Admin", "");
            }

            return View(navigator);
        }

        // GET: Navigators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navigator navigator = repository.GetByID(id);
            if (navigator == null)
            {
                return HttpNotFound();
            }
            return View(navigator);
        }

        // POST: Navigators/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName")] Navigator navigator)
        {
            if (ModelState.IsValid)
            {
                repository.Update(navigator);
                repository.Save();
                return RedirectToAction("Index", "Admin", "");
            }
            return View(navigator);
        }

        // GET: Navigators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navigator navigator = repository.GetByID(id);
            if (navigator == null)
            {
                return HttpNotFound();
            }
            return View(navigator);
        }

        // POST: Navigators/Delete/5
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
