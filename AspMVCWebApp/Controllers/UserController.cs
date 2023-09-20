using AspMVCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMVCWebApp.Controllers
{
    public class UserController : Controller
    {
        
        UserContext db = new UserContext();

        public ActionResult Index()
        {
            return View(db.GetUsers());
        }

      
        public ActionResult Details(string id)
        {
            return View(db.GetUsersByID(id));
        }


        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserModel users)
        {
            int i = db.SaveUser(users);
            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {

                return View();
            }

        }

        public ActionResult Edit(string id)
        {
            UserModel user = db.GetUsersByID(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserModel users)
        {
            int i = db.UpdateUser(users);
            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public ActionResult Delete(string id)
        {
            UserModel us = db.GetUsersByID(id);
            return View(us);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            int i = db.DeleteUser(id);
            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

    }
}