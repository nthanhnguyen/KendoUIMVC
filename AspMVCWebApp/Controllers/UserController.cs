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
        // GET: User
        UserContext db = new UserContext();


        // GET: User
        public ActionResult Index()
        {
            return View(db.GetUsers());
        }

        // GET: User/Details/5
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

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            UserModel user = db.GetUsersByID(id);
            return View(user);
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            UserModel us = db.GetUsersByID(id);
            return View(us);
        }

        // POST: User/Delete/5
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