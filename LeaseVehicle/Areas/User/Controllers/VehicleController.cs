using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaseVehicle.Areas.User.Controllers
{
    public class VehicleController : Controller
    {
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        // GET: User/Vehicle
        static private int payment_id;
        static private int Quote_id;
        public ActionResult SerachVehicle()
        {
            return View("~/User/Home/ListProduct");
        }
        [HttpPost]
        public ActionResult ListProductBySearch(VMVehicle vMSerach)
        {
            if (vMSerach.CategoryId == 0 && vMSerach.BrandId == 0 && vMSerach.VehicleName == null)
            {

                return RedirectToAction("../Home/ListProduct");
            }
            else if (vMSerach.VehicleName != null && vMSerach.CategoryId == 0 && vMSerach.BrandId == 0)
            {
            
                List<VMVehicle> vehiclesList = new List<VMVehicle>();
                var v = db.VehicleDetails.Include("VehicleImages").Where(x => (x.VehicleName == vMSerach.VehicleName)).ToList();
                foreach (var vd in v)
                {
                    VMVehicle vMVehicle = new VMVehicle();
                    vMVehicle.VehicleId = vd.VehicleId;
                    vMVehicle.VehicleName = vd.VehicleName;
                    vMVehicle.VehicleNo = vd.VehicleNo;
                    vMVehicle.BrandId = vd.BrandId;
                    vMVehicle.CategoryId = vd.CategoryId;
                    vMVehicle.ColorId = vd.ColorId;
                    vMVehicle.Euronum = vd.Euronum;
                    vMVehicle.Kilometers = vd.Kilometers;
                    vMVehicle.RegistrationYear = vd.RegistrationYear;
                    vMVehicle.Price = vd.Price;
                    vMVehicle.Weight = vd.Weight;
                    vMVehicle.NoOfSeats = vd.NoOfSeats;
                    vMVehicle.IsSold = vd.IsSold;
                    vMVehicle.SoldDate = vd.SoldDate;
                    vMVehicle.IsActive = vd.IsActive;
                    vMVehicle.IsDelete = vd.IsDelete;
                    vMVehicle.CreatedDate = vd.CreatedDate;
                    vMVehicle.ModifiedDate = vd.ModifiedDate;
                    vMVehicle.CreatedBy = vd.CreatedBy;
                    vMVehicle.ModifiedBy = vd.ModifiedBy;
                    vMVehicle.VehicleImgName = vd.VehicleImages.Select(d => d.VehicleImgName).FirstOrDefault();

                    vehiclesList.Add(vMVehicle);
               }
                return View(vehiclesList);
            }
            else
            {
                List<VMVehicle> vehiclesList = new List<VMVehicle>();
                var v = db.VehicleDetails.Include("VehicleImages").Where(x => (x.CategoryId == vMSerach.CategoryId) && (x.BrandId == vMSerach.BrandId)).ToList();
                foreach (var vd in v)
                {
                    VMVehicle vMVehicle = new VMVehicle();
                    vMVehicle.VehicleId = vd.VehicleId;
                    vMVehicle.VehicleName = vd.VehicleName;
                    vMVehicle.VehicleNo = vd.VehicleNo;
                    vMVehicle.BrandId = vd.BrandId;
                    vMVehicle.CategoryId = vd.CategoryId;
                    vMVehicle.ColorId = vd.ColorId;
                    vMVehicle.Euronum = vd.Euronum;
                    vMVehicle.Kilometers = vd.Kilometers;
                    vMVehicle.RegistrationYear = vd.RegistrationYear;
                    vMVehicle.Price = vd.Price;
                    vMVehicle.Weight = vd.Weight;
                    vMVehicle.NoOfSeats = vd.NoOfSeats;
                    vMVehicle.IsSold = vd.IsSold;
                    vMVehicle.SoldDate = vd.SoldDate;
                    vMVehicle.IsActive = vd.IsActive;
                    vMVehicle.IsDelete = vd.IsDelete;
                    vMVehicle.CreatedDate = vd.CreatedDate;
                    vMVehicle.ModifiedDate = vd.ModifiedDate;
                    vMVehicle.CreatedBy = vd.CreatedBy;
                    vMVehicle.ModifiedBy = vd.ModifiedBy;
                    vMVehicle.VehicleImgName = vd.VehicleImages.Select(d => d.VehicleImgName).FirstOrDefault();

                    vehiclesList.Add(vMVehicle);
                }

                return View(vehiclesList);
            }

        }

        //[HttpPost]
        public ActionResult SingleVehicle(int id)
        {
            //VMVehicle vehicle = new VMVehicle();
            var vd = db.VehicleDetails.Include("VehicleImages").FirstOrDefault(x =>x.VehicleId == id);
            var vMVehicle = new VMVehicle
            {
                VehicleId = vd.VehicleId,
                VehicleName = vd.VehicleName,
                VehicleNo = vd.VehicleNo,
                BrandId = vd.BrandId,
                CategoryId = vd.CategoryId,
                ColorId = vd.ColorId,
                Euronum = vd.Euronum,
                Kilometers = vd.Kilometers,
                RegistrationYear = vd.RegistrationYear,
                Price = vd.Price,
                Weight = vd.Weight,
                NoOfSeats = vd.NoOfSeats,
                IsSold = vd.IsSold,
                SoldDate = vd.SoldDate,
                IsActive = vd.IsActive,
                IsDelete = vd.IsDelete,
                CreatedDate = vd.CreatedDate,
                ModifiedDate = vd.ModifiedDate,
                CreatedBy = vd.CreatedBy,
                ModifiedBy = vd.ModifiedBy,
                VehicleImgName = vd.VehicleImages.Select(d => d.VehicleImgName).FirstOrDefault()
            };

            return View(vMVehicle);
        }

        [HttpPost]
        public ActionResult Quote(VMVehicle vMVehicle,FormCollection fc)
        {
            if (vMVehicle is null)
            {
                throw new ArgumentNullException(nameof(vMVehicle));
            }

            if (Session["userid"]!=null)
            {
                
                int VehicleId = Convert.ToInt32(fc["VehicleId"]);
                int uid = Convert.ToInt32(Session["userid"]);
                int downpayment = Convert.ToInt32 (fc["DownPayment"]);
                int duration = Convert.ToInt32(fc["Duration"]);
                int TotalPayment = Convert.ToInt32(fc["totalpayment"]);
                if (downpayment != 0 || duration != 0)
                {
                    int DeductDownpayment, PerMonth;

                    DeductDownpayment = TotalPayment - downpayment;
                    PerMonth = DeductDownpayment / duration;
                    if (PerMonth == 0)
                    {
                        ViewBag.BookingMessage = "Booking not Success!!!!";
                        return RedirectToAction("/SingleVehicle/" + VehicleId);
                    }
                    else
                    {
                        Quote quote = new Quote()
                        {
                            UserId = uid,
                            VehicleId = VehicleId,
                            DownPayment = downpayment,
                            Duration = duration,
                            TotalAmount = TotalPayment,
                            StatusId = 1,
                            IsActive = true,
                            IsDelete = false,

                        };

                        db.Quotes.Add(quote);
                        if (db.SaveChanges() > 0)
                        {
                            return RedirectToAction("QuoteList");
                        }
                        ViewBag.BookingMessage = "Booking not Success!!!!";
                        return RedirectToAction("/SingleVehicle/"+VehicleId);
                    }
                }
                else
                {
                    ViewBag.AmountMessage = "Amount must not be zero!!!!";
                    //return Redirect("/User/Vehicle/SingleVehicle/"+VehicleId);
                    return RedirectToAction("/SingleVehicle/"+VehicleId);
                   // return View("/Vehicle/SingleVehicle/"+VehicleId);
                }
            }
            return Redirect("/User/UserLogin/Login");
            
        }

        public ActionResult QuoteList()
        {


            int userid = Convert.ToInt32(Session["userid"]);
            var data = db.Quotes.Include("VehicleDetail").Where(x=>x.IsDelete == false && x.UserId == userid).ToList();
           
            List<VMQuote> quotes = new List<VMQuote>();
            foreach(var item in data)
            {
                VMQuote vMQuote = new VMQuote()
                {

                    id = item.QuoteId,
                    VImage = item.VehicleDetail.VehicleImages.First().VehicleImgName,
                    VName = item.VehicleDetail.VehicleName,
                    Price = item.VehicleDetail.Price,
                    Duration = item.Duration,
                    DownPayment = item.DownPayment,
                    TotalAmount = item.TotalAmount,
                    Details = item.Details,
                    Status = item.Status.Status1,
                    
                };
                    quotes.Add(vMQuote);
                
                
            }
            return View(quotes);
        }

        public ActionResult QuoteDetail(int Id)
        {
            var qu = db.Quotes.Include("Status").FirstOrDefault(q => q.QuoteId == Id);
            var doc = db.Documents.Where(d => d.QuoteId == qu.QuoteId).ToList();
            var VImage = db.VehicleDetails.Include("VehicleImages").FirstOrDefault(v => v.VehicleId == qu.VehicleId);
            var VDetail = db.UserDetails.FirstOrDefault(i => i.UserId == qu.UserId);
            VMQuote vmQuote = new VMQuote()
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
                userId=VDetail.UserId,
                vehicleId = VImage.VehicleId,
                statusId=qu.StatusId,

                
      };
            return View(vmQuote);
           
        }
        public ActionResult DeleteQuote(int id)
        {
            
            if (ModelState.IsValid)
            {
                Quote quote = db.Quotes.Find(id);
                
                quote.IsDelete = true;
                db.SaveChanges();
                return RedirectToAction("QuoteList");
            }
            else
            {
                ModelState.AddModelError("", "Record Not deleted");
            }
            return View();
            
        }

        [HttpPost]
        public ActionResult UploadDocument(HttpPostedFileBase []documents,[Bind(Include = "QuoteId,VehicleId,UserId,Duration,DownPayment,TotalAmount,StatusId,Details,IsActive,IsDelete")] Quote quote)
        {
            
            try
            {
                List<Document> doc = new List<Document>();
                foreach (var item in documents)
                {
                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(item.InputStream))
                    {
                        bytes = br.ReadBytes(item.ContentLength);
                    }
                    Document document = new Document()
                    {
                        DocName = item.FileName,
                        DocContentType = item.ContentType,
                        DocData = bytes,
                        QuoteId = quote.QuoteId

                    };
                    doc.Add(document);
                }
                db.Documents.AddRange(doc);
                var st = db.SaveChanges();
                if (st > 0 )
                {
                    quote.StatusId = 4;
                    db.Entry(quote).State = EntityState.Modified;
                    db.SaveChanges();
                   
                }
                return RedirectToAction("QuoteList");
            }
            catch (Exception e)
            {
               
                return Redirect("../QuoteDetails/"+quote.QuoteId);
            }
        }

        public FileContentResult DownloadDoc(int id)
        {
            var data = db.Documents.Find(id);
            var bytes = data.DocData;
            var content = data.DocContentType;
            var filename = data.DocName;

            return File(bytes, content, filename);
        }

        public ActionResult DeleteDocument(int id)
        {
            Document doc = db.Documents.Find(id);
            Document d = db.Documents.Include("Quote").FirstOrDefault(a =>a.DocId == id);
            var Id = d.QuoteId; 
            db.Documents.Remove(doc);
            db.SaveChanges();
            return RedirectToAction("../User/Vehicle/QuoteDetail/"+Id);
        }
        public ActionResult Payment()
        {
            return View();
        }

       
        public ActionResult CreatePayment(Payment pay, int Id)
        {
            payment_id = Id;
            Random r = new Random();
            var qu = db.PaymentDetails.FirstOrDefault(q => q.Id == Id);
            Quote_id = qu.QuoteId;
            var t = db.Quotes.Include("UserDetail").FirstOrDefault(q => q.QuoteId == qu.QuoteId);
            String merchantKey = Key.merchantKey;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", Key.merchantId);
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
             parameters.Add("EMAIL", t.UserDetail.Email);
            parameters.Add("MOBILE_NO", t.UserDetail.Contact);
            parameters.Add("CUST_ID", "1");
            parameters.Add("ORDER_ID", r.Next().ToString());
            parameters.Add("TXN_AMOUNT", qu.Amount.ToString());
            parameters.Add("CALLBACK_URL", "http://localhost:63105/User/Vehicle/Response"); //This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = paytm.CheckSum.generateCheckSum(merchantKey, parameters);

            string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            ViewBag.htmlData = outputHTML;
            System.Diagnostics.Debug.WriteLine(TempData["userid"]);
            return View("PaymentPage");
        }

        [HttpPost]
        public ActionResult PaytmResponse(PaytmResponse data)
        {

            return View("PaytmResponse", data);
        }
        public ActionResult Response(PaytmResponse data)
        {

            System.Diagnostics.Debug.WriteLine(Session["pid"]);
            if (data.STATUS == "TXN_SUCCESS")
            {

                int status = 11;
                
                PaymentDetail paymentDetail = db.PaymentDetails.FirstOrDefault(p => p.Id == payment_id);
                paymentDetail.TransactionId = data.TXNID;
                paymentDetail.StatusId = status;
                paymentDetail.PaymentDate = DateTime.Now.ToLocalTime();
                paymentDetail.PaymentMode = data.PAYMENTMODE; ;
                //for updating status in QuoteTable if payment is done
                // so now status is vehicle deliverd
                Quote quoteDetail = db.Quotes.FirstOrDefault(q => q.QuoteId == Quote_id);
                quoteDetail.StatusId = 12;

                var plist = db.PaymentDetails.Where(a => a.QuoteId == Quote_id).ToList();
                plist.Reverse();
                var d = plist.ElementAt(0);

                if (d.StatusId == 11)
                {
                    quoteDetail.StatusId = 13;
                }

                db.Entry(quoteDetail).State = EntityState.Modified;
                db.Entry(paymentDetail).State = EntityState.Modified;
                
                db.SaveChanges();

                System.Diagnostics.Debug.WriteLine("Success");
                ViewBag.success = "Payment Done Succesfully ";
                //return View();
                return RedirectToAction("/QuoteList/"+Quote_id);
            }
            else if(data.STATUS == "TXN_FAILURE")
            {
               var r = data.RESPMSG;
               ViewBag.fail = "Transaction Fail : "+ r;
               System.Diagnostics.Debug.WriteLine("Failure");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Pending");
            }
            return View();
        }




    }
}
