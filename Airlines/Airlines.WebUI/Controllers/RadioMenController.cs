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
    public class RadioMenController : Controller
    {
        IAirlineRepository<RadioMan> repository;
        public RadioMenController()
        {

        }
        public RadioMenController(IAirlineRepository<RadioMan> repo)
        {
            repository = repo;
        }
        // GET: RadioMen
        public ActionResult Index()
        {
            return View(repository.GetList());
        }

        // GET: RadioMen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RadioMen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName")] RadioMan radioMan)
        {
            if (ModelState.IsValid)
            {
                repository.Create(radioMan);
                repository.Save();
                return RedirectToAction("Index", "Admin", "");
            }

            return View(radioMan);
        }

        // GET: RadioMen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadioMan radioMan = repository.GetByID(id);
            if (radioMan == null)
            {
                return HttpNotFound();
            }
            return View(radioMan);
        }

        // POST: RadioMen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName")] RadioMan radioMan)
        {
            if (ModelState.IsValid)
            {
                repository.Update(radioMan);
                repository.Save();
                return RedirectToAction("Index", "Admin", "");
            }
            return View(radioMan);
        }

        // GET: RadioMen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadioMan radioMan = repository.GetByID(id);
            if (radioMan == null)
            {
                return HttpNotFound();
            }
            return View(radioMan);
        }

        // POST: RadioMen/Delete/5
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
