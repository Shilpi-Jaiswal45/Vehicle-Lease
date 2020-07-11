using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LeaseVehicle.Areas.User.Controllers

{
    public class HomeController : Controller
    {
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        public ActionResult Index(string categoryId)
        {
            return View();
        }

        public JsonResult GetCategory()
        {
            return Json(db.Categories.Select(x => new
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                IsActive=x.IsActive,
                IsDelete=x.IsDelete
            }).Where(x =>x.IsDelete == false).ToList(), JsonRequestBehavior.AllowGet);
          
        }

        public JsonResult GetBrandByCategoryId(int categoryId)
        {
           
            return Json(db.Brands.Select(x => new
            {
                BrandId = x.BrandId,
                BrandName = x.BrandName,
                CategoryId = x.CategoryId
            }).Where(x => x.CategoryId == categoryId).ToList(), JsonRequestBehavior.AllowGet);

        }

    
        public ActionResult ListProductByBrand(int categoryId)
        {

            return Json(db.VehicleDetails.Select(x => new
            {
                VehicleId = x.VehicleId,
                VehicleName = x.VehicleName,
                VehicleNo=x.VehicleNo,
                BrandId=x.BrandId,
                CategoryId = x.CategoryId,
                ColorId = x.ColorId,
                Euronum = x.Euronum,
                Kilometers = x.Kilometers,
                RegistrationYear = x.RegistrationYear,
                Price = x.Price,
                Weight = x.Weight,
                NoOfSeats = x.NoOfSeats,
                IsSold = x.IsSold,
                SoldDate = x.SoldDate,
                IsActive = x.IsActive,
                IsDelete = x.IsDelete,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
                VehicleImgName = x.VehicleImages.Select(d => d.VehicleImgName).FirstOrDefault()

            }).Where(x => x.CategoryId == categoryId).ToList(), JsonRequestBehavior.AllowGet);


           
        }
        [HttpPost]
        public ActionResult ListProductBySearch(VMSerach vMSerach)
        {

            return Json(db.VehicleDetails.Select(x => new
            {
                VehicleId = x.VehicleId,
                VehicleName = x.VehicleName,
                VehicleNo = x.VehicleNo,
                BrandId = x.BrandId,
                CategoryId = x.CategoryId,
                ColorId = x.ColorId,
                Euronum = x.Euronum,
                Kilometers = x.Kilometers,
                RegistrationYear = x.RegistrationYear,
                Price = x.Price,
                Weight = x.Weight,
                NoOfSeats = x.NoOfSeats,
                IsSold = x.IsSold,
                SoldDate = x.SoldDate,
                IsActive = x.IsActive,
                IsDelete = x.IsDelete,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
                VehicleImgName = x.VehicleImages.Select(d => d.VehicleImgName).FirstOrDefault()

            }).Where(x => x.CategoryId == vMSerach.CategoryId).ToList(), JsonRequestBehavior.AllowGet);


            
        }
        public ActionResult ListProduct()
        {
            List<VMVehicle> vehiclesList = new List<VMVehicle>();
            var v = db.VehicleDetails.Include("VehicleImages").Where(x => x.IsActive==true && x.IsDelete==false).ToList();

            foreach(var vd in v)
            {
                VMVehicle vMVehicle = new VMVehicle();
                vMVehicle.VehicleId = vd.VehicleId;
                vMVehicle.VehicleName = vd.VehicleName;
                vMVehicle.VehicleNo = vd.VehicleNo;
                vMVehicle.BrandId = vd.BrandId;
                //vMVehicle.BrandName = vd.BrandName;
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

        public ActionResult VehicleByCategory(int id)
        {
            if (id == 0)
            {
                // HomeController homeController = new HomeController();
                //VMVehicle vehiclesList = homeController.ListProduct();
                return RedirectToAction("../Home/ListProduct");
                //return View("../Home/ListProduct");
            }
            else
            {
                List<VMVehicle> vehiclesList = new List<VMVehicle>();
                var v = db.VehicleDetails.Include("VehicleImages").Where(x => x.CategoryId == id).ToList();
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
        public ActionResult Blogsingle()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Listings()
        {
            return View();
        }
        public ActionResult Listingssingle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFeedback(Feedback feedback)
        {
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}