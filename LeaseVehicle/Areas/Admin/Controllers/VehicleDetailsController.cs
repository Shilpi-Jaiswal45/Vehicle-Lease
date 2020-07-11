using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaseVehicle.Areas.Admin.Controllers
{
   
    public class VehicleDetailsController : Controller
    {
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        // GET: VehicleDetails
        public ActionResult Index()
        {
      
            return View(db.VehicleDetails.Where(x=>x.IsDelete==false).ToList());
        }
        public ActionResult AddVehicle()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
            return View();
        }
        [HttpPost]
        public ActionResult AddVehicle(VMVehicle vMVehicle)
        {
            try
            {
                VehicleDetail vehicleDetail = new VehicleDetail();
                vehicleDetail.VehicleName = vMVehicle.VehicleName;
                vehicleDetail.VehicleNo = vMVehicle.VehicleNo;
                vehicleDetail.BrandId = vMVehicle.BrandId;
                vehicleDetail.CategoryId = vMVehicle.CategoryId;
                vehicleDetail.ColorId = vMVehicle.ColorId;
                vehicleDetail.Euronum = vMVehicle.Euronum;
                vehicleDetail.Kilometers = vMVehicle.Kilometers;
                vehicleDetail.RegistrationYear = vMVehicle.RegistrationYear;
                vehicleDetail.Price = vMVehicle.Price;
                vehicleDetail.Weight = vMVehicle.Weight;
                vehicleDetail.NoOfSeats = vMVehicle.NoOfSeats;
                vehicleDetail.IsSold = vMVehicle.IsSold;
                vehicleDetail.SoldDate = vMVehicle.SoldDate;
                vehicleDetail.IsActive = vMVehicle.IsActive;
                vehicleDetail.IsDelete = vMVehicle.IsDelete;
                vehicleDetail.CreatedDate = vMVehicle.CreatedDate;
                vehicleDetail.ModifiedDate = vMVehicle.ModifiedDate;
                vehicleDetail.CreatedBy = vMVehicle.CreatedBy;
                vehicleDetail.ModifiedBy = vMVehicle.ModifiedBy;
                db.VehicleDetails.Add(vehicleDetail);

                foreach (HttpPostedFileBase file in vMVehicle.VehicleImage1)
                {

                    VehicleImage vehicleImage = new VehicleImage();
                   vehicleImage.VehicleImage1 = vMVehicle.VehicleImage1;

                    string VFileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string VImageExtension = Path.GetExtension(file.FileName);
                    VFileName = VFileName + DateTime.Now.ToString("yymmssff") + VImageExtension;
                    vehicleImage.VehicleImgName = "~/VehicleImages/" + VFileName;
                    VFileName = Path.Combine(Server.MapPath("~/VehicleImages/"), VFileName);
                    file.SaveAs(VFileName);

                    vehicleDetail.VehicleId = vehicleImage.VehicleId;
                    db.VehicleImages.Add(vehicleImage);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
                ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
                return View();
            }
        }

        public ActionResult ViewVehicle()
        {
            //VehicleDetail vd = new VehicleDetail();
            //VehicleImage vi = new VehicleImage();

            var vd = db.VehicleDetails.Include("VehicleImages").FirstOrDefault(v => v.VehicleId == 1);

            VMVehicle vMVehicle = new VMVehicle();

            vMVehicle.VehicleId = vd.VehicleId;
            vMVehicle.VehicleName = vd.VehicleName;
            vMVehicle.VehicleNo = vd.VehicleNo;
            vMVehicle.BrandId = vd.BrandId;
            vMVehicle.CategoryId = vd.CategoryId;
            vMVehicle.ColorId = vd.ColorId;
            vMVehicle.Euronum = vd.Euronum;
            vMVehicle.Kilometers=vd.Kilometers;
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
            vMVehicle.VehicleImages = vd.VehicleImages.ToList();

            
            return View(vMVehicle);
        }

        // GET: VehicleDetails/Details/5
        public ActionResult Details(int id)
        {
            var vd = db.VehicleDetails.Include("VehicleImages").FirstOrDefault(v => v.VehicleId == id);

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
            vMVehicle.VehicleImages = vd.VehicleImages.ToList();

            return View(vMVehicle);
            
        }

        // GET: VehicleDetails/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
            return View();
        }

        // POST: VehicleDetails/Create
        [HttpPost]
        public ActionResult Create(VehicleDetail vehicleDetail)
        {

            //string VFileName = Path.GetFileNameWithoutExtension(vehicleDetail.VehicleImage.FileName);
            //string 
            try
            {
                db.VehicleDetails.Add(vehicleDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
                ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
                return View();
            }
        }

        // GET: VehicleDetails/Edit/5
        public ActionResult Edit(int id)
        {
            var vehicle = db.VehicleDetails.Single(m => m.VehicleId == id);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
            return View(vehicle);
        }

        // POST: VehicleDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var vehicle = db.VehicleDetails.Single(m => m.VehicleId == id);
                if (TryUpdateModel(vehicle))
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(vehicle);

            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
                ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
                return View();
            }
        }

        // GET: VehicleDetails/Delete/5
        public ActionResult Delete(int id)
        {
            VehicleDetail b = db.VehicleDetails.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        // POST: VehicleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            if (ModelState.IsValid)
            {
                var vd = db.VehicleDetails.Include("VehicleImages").FirstOrDefault(v => v.VehicleId == id);
                //VehicleDetail vehicle = db.VehicleDetails.Find(id);
                vd.IsDelete = true;
                db.Entry(vd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Record Not deleted");
            }
            return View();
        }

        public ActionResult DetailsDemo(int id)
        {
            var vd = db.VehicleDetails.Include("VehicleImages").FirstOrDefault(v => v.VehicleId == id);
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
            vMVehicle.VehicleImages = vd.VehicleImages.ToList();

            return View(vMVehicle);
        }
    }
}
