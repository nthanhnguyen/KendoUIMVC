using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using AspMVCWebApp.Models;

namespace AspMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //UserSvc svc = new UserSvc() ;
        public ActionResult Editing_Popup()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            var users = new UserService().Read();

            var dataSourceResult = users.ToDataSourceResult(request);

            return Json(dataSourceResult, JsonRequestBehavior.AllowGet);




        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, UserModel users)
        {
            var userSvc = new UserService();
            if (users != null && ModelState.IsValid)
            {
                userSvc.Create(users);
            }

            return Json(new[] { users }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        } 
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, UserModel users)
        {
            var userService = new UserService();
            if (users != null && ModelState.IsValid)
            {
                userService.Update(users);
            }

            return Json(new[] { users }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, UserModel users)
        {
            var userService = new UserService();
            if (users != null)
            {
                userService.Destroy(users);
            }

            return Json(new[] { users }.ToDataSourceResult(request, ModelState));
        }

    }
}
}