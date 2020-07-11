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
    public class QuotesController : Controller
    {
        private VehicleLeasingEntities db = new VehicleLeasingEntities();

        public VehicleLeasingEntities Db { get => db; set => db = value; }

        // GET: Admin/Quotes
        public ActionResult Index()
        {
            var quotes = db.Quotes.Include(q => q.Status).Include(q => q.UserDetail).Include(q => q.VehicleDetail);
            return View(quotes.ToList());
        }

        // GET: Admin/Quotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var qu = db.Quotes.Include("Status").FirstOrDefault(q => q.QuoteId == id);
            var doc = db.Documents.Where(d => d.QuoteId == qu.QuoteId).ToList();
            var VImage = db.VehicleDetails.Include("VehicleImages").FirstOrDefault(v => v.VehicleId == qu.VehicleId);
            var VDetail = db.UserDetails.FirstOrDefault(i => i.UserId == qu.UserId);

            VMQuote vMQuote = new VMQuote()
            {
                id = qu.QuoteId,
                VImage = VImage.VehicleImages.First().VehicleImgName,
                VName = VImage.VehicleName,
                Price = VImage.Price,
                Duration = qu.Duration,
                DownPayment = qu.DownPayment,
                TotalAmount = qu.TotalAmount,
                Details = qu.Details,
                Status = qu.Status.Status1,
                Documents = doc,
                FName = VDetail.FirstName.ToString(),
                vehicleId = VImage.VehicleId,
                userId = VDetail.UserId,
                statusId = qu.StatusId,
                
            };
            return View(vMQuote);
        }

        // GET: Admin/Quotes/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Status1");
            ViewBag.DocStatusId = new SelectList(db.Status, "StatusId", "Status1");
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName");
            ViewBag.VehicleId = new SelectList(db.VehicleDetails, "VehicleId", "VehicleName");
            return View();
        }

        // POST: Admin/Quotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuoteId,VehicleId,UserId,Duration,DownPayment,TotalAmount,StatusId,Details,IsActive,IsDelete,DocStatusId")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                Db.Quotes.Add(quote);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Status1", quote.StatusId);
           
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", quote.UserId);
            ViewBag.VehicleId = new SelectList(db.VehicleDetails, "VehicleId", "VehicleName", quote.VehicleId);
            return View(quote);
        }

        // GET: Admin/Quotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = Db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Status1", quote.StatusId);
            
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", quote.UserId);
            ViewBag.VehicleId = new SelectList(db.VehicleDetails, "VehicleId", "VehicleName", quote.VehicleId);
            return View(quote);
        }

        // POST: Admin/Quotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuoteId,VehicleId,UserId,Duration,DownPayment,TotalAmount,StatusId,Details,IsActive,IsDelete")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(quote).State = EntityState.Modified;
                Db.SaveChanges();
                if (quote.StatusId == 6)
                {
                    List<Document> doclist = Db.Documents.Where(doc => doc.QuoteId == quote.QuoteId).ToList();
                    foreach (Document item in doclist)
                    {
                        db.Documents.Remove(item);
                    }
                    db.SaveChanges();
                }
                if (quote.StatusId == 5)
                    CreatePaymentList(quote.QuoteId);
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Status1", quote.StatusId);
           
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", quote.UserId);
            ViewBag.VehicleId = new SelectList(db.VehicleDetails, "VehicleId", "VehicleName", quote.VehicleId);
            return View(quote);
        }

        // GET: Admin/Quotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = Db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Admin/Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quote quote = Db.Quotes.Find(id);
            var s = DeleteDoc(id);
            if (s)
            {
                Db.Quotes.Remove(quote);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        public bool DeleteDoc(int id)
        {
            List<Document> doclist = Db.Documents.Where(doc => doc.QuoteId == id).ToList();
            foreach (Document item in doclist)
            {
                db.Documents.Remove(item);
            }
            

            var b = db.SaveChanges();
            if (b >= 1)
                return true;
            else
                return false;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult DeleteDocument(int id)
        {
            Quote quote = Db.Quotes.Find(id);
            Db.Quotes.Remove(quote);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
        public bool CreatePaymentList(int id)
        {
            var qt = db.Quotes.FirstOrDefault(q => q.QuoteId == id);
            List<PaymentDetail> pd = new List<PaymentDetail>();
            var total = qt.TotalAmount;
            int dp = Convert.ToInt32(qt.DownPayment);
            var rem = total - dp;
            var monthly = rem / (qt.Duration - 1);
            List<DateTime> sDate = new List<DateTime>();
            List<DateTime> eDate = new List<DateTime>();
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            PaymentDetail downpaymentdetail = new PaymentDetail()
            {
                Amount = dp,
                QuoteId = id,
                StatusId = 10,
                StartDate = now,
                EndDate = endDate,
            };
            db.PaymentDetails.Add(downpaymentdetail);
            //pd.Add(downpaymentdetail);

            for (int i = 2; i <= qt.Duration; i++)
            {
                now = now.AddMonths(1);
                startDate = new DateTime(now.Year, now.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
                PaymentDetail paymentDetail = new PaymentDetail()
                {
                    Amount = Convert.ToInt32(monthly),
                    QuoteId = id,
                    TransactionId = "",
                    PaymentMode = "",
                    StatusId = 9,
                    StartDate = startDate,
                    EndDate = endDate,
                };
                //pd.Add(paymentDetail);
                db.PaymentDetails.Add(paymentDetail);
            }
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}
