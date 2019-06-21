using Airlines.Data.Authentication;
using Airlines.Data.Repository;
using Airlines.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Airlines.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthentication auth;
        public AccountController()
        {

        }
        public AccountController(IAuthentication auth)
        {
            this.auth = auth;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                if (auth.CheckUser(model.UserName,model.Password) != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return RedirectToAction("Index", "Races");
                }
                else
                {
                    ModelState.AddModelError("", "The username or password is incorrect, please try again");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        { 
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (auth.CheckUser(model.UserName, model.Password) == null)
                {
                    auth.AddUser(model.UserName, model.Email, model.Password);
                    if (auth.CheckUser(model.UserName, model.Password) != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("Index", "Races");
                    }
                }
                else
                    ModelState.AddModelError("", "There is the user with the same username, please try again");
            }
            return View(model);
        }
    }
}
