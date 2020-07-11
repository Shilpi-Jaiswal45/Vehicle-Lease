using LeaseVehicle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LeaseVehicle.Areas.Admin.Controllers
{
    public class LeadController : Controller
    {
        // GET: Admin/Lead
        private VehicleLeasingEntities db = new VehicleLeasingEntities();

        // GET: Admin/Lead/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Lead/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDetail userDetail)
        {
            try
            {
                db.UserDetails.Add(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(userDetail);
            }
        }
        // GET: Admin/Lead
        public ActionResult Index()
        {
         return View(db.UserDetails.ToList());
        }

        // GET: Admin/Lead/Details/5
        public ActionResult Details(int id)
        {
            var lead = db.UserDetails.Single(m => m.UserId == id);
            return View(lead);

        }
        // GET: Admin/Lead/Edit/5
        public ActionResult Edit(int? id)
        {
            var lead = db.UserDetails.Single(m => m.UserId == id);
            return View(lead);
        }

        // POST: Admin/Lead/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var lead = db.UserDetails.Single(m => m.UserId == id);
                if (TryUpdateModel(lead))
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(lead);

            }
            catch
            {
                return View();
            }
        }


        // GET: Admin/Lead/Delete/5
        public ActionResult Delete(int id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }


        // POST: Admin/Lead/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            if (ModelState.IsValid)
            {
                UserDetail userDetail = db.UserDetails.Find(id);
                db.UserDetails.Remove(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Record Not deleted");
            }
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}