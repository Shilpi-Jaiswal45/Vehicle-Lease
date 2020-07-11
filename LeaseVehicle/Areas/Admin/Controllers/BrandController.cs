
using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LeaseVehicle.Areas.Admin.Controllers
{
    
    public class BrandController : Controller
    {
       
        // GET: Brand
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        public ActionResult Index()
        {
            return View();
        }
        // GET: Brand/Create
        [HttpGet]
        public ActionResult AddBrand()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        public ActionResult AddBrand(Brand brands)
        {

            try
            {
                db.Brands.Add(brands);
                db.SaveChanges();
                return RedirectToAction("ViewBrand");
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                return View();
            }
        }
        public ActionResult ViewBrand()
        {
            return View(db.Brands.ToList());
        }
        // GET: Brand/Details/5
        public ActionResult Details(int id)
        {
            var brand = db.Brands.Single(m => m.BrandId == id);
            return View(brand);
        }
        // GET: Vehicle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Brand/Edit/5
        public ActionResult Edit(int id)
        {
            var brand = db.Brands.Single(m => m.BrandId == id);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(brand);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var brand = db.Brands.Single(m => m.BrandId == id);
                if (TryUpdateModel(brand))
                {
                    db.SaveChanges();
                    return RedirectToAction("ViewBrand");
                }
                return View(brand);

            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                return View();
            }
        }

        // GET: Brand/Delete/5
        public ActionResult Delete(int id)
        {
            Brand b = db.Brands.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);

        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            if (ModelState.IsValid)
            {
                Brand c = db.Brands.Find(id);
                db.Brands.Remove(c);
                db.SaveChanges();
                return RedirectToAction("ViewBrand");
            }
            else
            {
                ModelState.AddModelError("", "Record Not deleted");
            }
            return View();
        }

    }
}
