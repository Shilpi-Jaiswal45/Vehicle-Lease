
using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LeaseVehicle.Areas.User.Controllers
{
    public class FeedbackController : Controller
    {
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        public ActionResult AddFeedback(Feedback feedback)
        {
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
            TempData["msg"] = "<script>alert('Thank You!!');</script>";
            return RedirectToAction("Index");
        }
    }
}
