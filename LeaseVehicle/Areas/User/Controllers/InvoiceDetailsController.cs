using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaseVehicle.Areas.User.Controllers
{
    public class InvoiceDetailsController : Controller
    {
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        // GET: User/InvoiceDetails
        public ActionResult Index(int id)
        {
            var plist = db.PaymentDetails.Include("Status").Include("Quote").Where(i => i.QuoteId == id).ToList();
            var qlist = db.Quotes.Include("Status").FirstOrDefault(q => q.QuoteId == id);
            var VImage = db.VehicleDetails.Include("VehicleImages").FirstOrDefault(v => v.VehicleId == qlist.VehicleId);
            var VDetail = db.UserDetails.FirstOrDefault(i => i.UserId == qlist.UserId);
            var doc = db.Documents.Where(d => d.QuoteId == qlist.QuoteId).ToList();
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

            VMInvoice vMInvoice = new VMInvoice();
            vMInvoice.QID = id;
            vMInvoice.VImage = VImage.VehicleImages.First().VehicleImgName;
            vMInvoice.VName = VImage.VehicleName;
            vMInvoice.Price = VImage.Price;
            vMInvoice.Duration = qlist.Duration;
            vMInvoice.DownPayment = qlist.DownPayment;
            vMInvoice.TotalAmount = qlist.TotalAmount;
            vMInvoice.Details = qlist.Details;
            vMInvoice.Documents = doc;
            vMInvoice.FName = VDetail.FirstName.ToString();
            vMInvoice.vehicleId = VImage.VehicleId;
            vMInvoice.paymentDetails = plist;
            return View(vMInvoice);

        }
        public ActionResult GetDownpayment(int qid)
        {
            PaymentDetail paymentDetail = new PaymentDetail();

            return null;

        }
        public  List<PaymentDetail> getPaymentDetail(int id)
        {

           var list= db.PaymentDetails.Where(x => x.QuoteId == id && (x.StatusId == 10 || x.StatusId == 11)).ToList();
            list.Reverse();
            return list;
        }
    }
}