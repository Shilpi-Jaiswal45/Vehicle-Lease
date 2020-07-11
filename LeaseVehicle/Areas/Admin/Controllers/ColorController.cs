
using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaseVehicle.Areas.Admin.Controllers
{
   
    public class ColorController : Controller
    {
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        // GET: Color
        public ActionResult Index()
        {
            return View(db.Colors.ToList());
            
        }
     
        
        // GET: Color/Details/5
        public ActionResult Details(int id)
        {
            var color = db.Colors.Single(m => m.ColorId == id);
            return View(color);
        }

        // GET: Color/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Color/Create
        [HttpPost]
        public ActionResult Create(Color colors)
        {

            try
            {
                db.Colors.Add(colors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }


        }

        // GET: Color/Edit/5
        public ActionResult Edit(int id)
        {
            var color = db.Colors.Single(m => m.ColorId == id);
            return View(color);
        }

        // POST: Color/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var color = db.Colors.Single(m => m.ColorId == id);
                if (TryUpdateModel(color))
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(color);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Color/Delete/5
        public ActionResult Delete(int id)
        {
            Color c = db.Colors.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);


        }

        // POST: Color/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            if (ModelState.IsValid)
            {
                Color c = db.Colors.Find(id);
                db.Colors.Remove(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Record Not deleted");
            }
            return View();
        }
    }
}
