using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaseVehicle.Areas.User.Controllers
{
    public class InvoiceController : Controller
    {
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        // GET: User/Invoice
        public ActionResult Index(int id)
        {
            int userid = Convert.ToInt32(Session["userid"]);
            string address = Session["address"].ToString();
            string name = Session["username"].ToString();
            string email = Session["email"].ToString();
            ViewBag.Address = address;
            ViewBag.Name = name;
            ViewBag.Email = email;
            var quote = db.Quotes.Include("Status").FirstOrDefault(q => q.QuoteId == id);
           

           
            var Vehicledetails = db.VehicleDetails.FirstOrDefault(v => v.VehicleId == quote.VehicleId);
            string vehiclename = Vehicledetails.VehicleName;
            string brand = Vehicledetails.Brand.BrandName.ToString();
            string category = Vehicledetails.Category.CategoryName.ToString();
            double price = Vehicledetails.Price;
            int downpayment = Convert.ToInt32(quote.DownPayment);
         
            
            ViewBag.VName = vehiclename;
            ViewBag.Brand = brand;
            ViewBag.Category = category;
            ViewBag.Price = price;
            ViewBag.DownPayment = downpayment;
           

            return View();
        }
    }
}