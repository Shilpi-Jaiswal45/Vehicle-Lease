using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaseVehicle.Models;

namespace LeaseVehicle.Areas.Admin.Controllers
{
    public class InvoiceController : Controller
    {
        private VehicleLeasingEntities db = new VehicleLeasingEntities();

        // GET: Admin/Invoice
        public ActionResult Index()
        {
            var quotes = db.Quotes.Where(s => s.StatusId == 12 || s.StatusId==13).Include(q => q.Status).Include(q => q.UserDetail).Include(q => q.VehicleDetail);
            return View(quotes.ToList());
        }
        public ActionResult ViewInvoice(int? id)
        {
            var plist = db.PaymentDetails.Include("Status").Include("Quote").Where(i => i.QuoteId == id).ToList();
            var qlist = db.Quotes.Include("Status").FirstOrDefault(q => q.QuoteId == id);
            var VImage = db.VehicleDetails.Include("VehicleImages").FirstOrDefault(v => v.VehicleId == qlist.VehicleId);
            var VDetail = db.UserDetails.FirstOrDefault(i => i.UserId == qlist.UserId);
            var doc = db.Documents.Where(d => d.QuoteId == qlist.QuoteId).ToList();

            VMInvoice vMInvoice = new VMInvoice();
            //vMInvoice.QID = plist.QuoteId;
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
        public ActionResult EditpaymentStatus(int id)
        {
            PaymentDetail paymentDetail = db.PaymentDetails.FirstOrDefault(p => p.Id == id);
            
            paymentDetail.StatusId = 10;
            db.Entry(paymentDetail).State = EntityState.Modified;
            db.SaveChanges();
            var qid = paymentDetail.QuoteId;
            //return View("ViewInvoice");
            return Redirect("../ViewInvoice/"+qid);
        }

     }
}