
using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaseVehicle.Areas.Admin.Controllers
{
    
    public class CategoryController : Controller
    {
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("ViewCategory");
        }
        public ActionResult ViewCategory()
        {
            Category c = new Category();

            var data = db.Categories.Where(x => x.IsDelete == false);
          
            return View(data.ToList());
        }
        // GET: Vehicle/Details/5
        public ActionResult Details(int id)
        {
            var vechicle = db.Categories.Single(m => m.CategoryId == id);
            return View(vechicle);
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

        // GET: Vehicle/Edit/5
        public ActionResult Edit(int id)
        {
            var vechicle = db.Categories.Single(m => m.CategoryId == id);
            return View(vechicle);
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var vechicle = db.Categories.Single(m => m.CategoryId == id);
                if(TryUpdateModel(vechicle))
                {
                    db.SaveChanges();
                    return RedirectToAction("ViewCategory");
                }
                return View(vechicle);
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(int id)
        {
            Category c = db.Categories.Find(id);
            if(c==null)
            {
                return HttpNotFound();
            }
            

            return View(c);
        }

        // POST: Vehicle/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            if (ModelState.IsValid)
            {
                Category c = db.Categories.Find(id);
                c.IsDelete = true;
                db.SaveChanges();
                return RedirectToAction("ViewCategory");
            }
            else
            {
                ModelState.AddModelError("","Record Not deleted");
            }
            return View();
        }
    }
}
