using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TH05.Models;

namespace TH05.Controllers
{
    public class LoginUserController : Controller
    {
        TH05Entities database = new TH05Entities();
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAccount(AdminUser user)
        {
            var check = database.AdminUsers.Where(s => s.NameUser == user.NameUser && s.PasswordUser == user.PasswordUser).FirstOrDefault();
            if(check == null)
            {
                ViewBag.ErrorInfo = "Sai";
                return View("Index");
            } else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = user.NameUser;
                Session["PasswordUser"] = user.PasswordUser;
                return RedirectToAction("Index", "Products");
            }
        }

        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginUser");
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(AdminUser user)
        {
            if(ModelState.IsValid)
            {
                var checkID = database.AdminUsers.Where(s => s.ID == user.ID).FirstOrDefault();
                if(checkID == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.AdminUsers.Add(user);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.RegisterError = "ID Ton Tai";
                    return View("RegisterUser");
                }

            }
            return View("RegisterUser");
        }
    }
}