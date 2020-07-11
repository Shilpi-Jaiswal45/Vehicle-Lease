using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

namespace LeaseVehicle.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login model)
        {
            using (VehicleLeasingEntities db = new VehicleLeasingEntities())
            {
                var user = db.Logins.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault();
                if(user == null)
                {
                    ModelState.AddModelError("", "Invalid Username and Password");
                }
                else
                {
                    Session["username"] = user.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }
                return View();

        }
        
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}