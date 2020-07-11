using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaseVehicle.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult SearchProductByCategory(Category category)
        {
            return View();
        }
    }
}