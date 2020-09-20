using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Labourproject.Models;

namespace Labourproject.Controllers
{
    public class LaboursController : Controller
    {
        private LabourDBContext db = new LabourDBContext();


        public ActionResult Index(string searchString)
        {
            var labours = from x in db.Labour
                           select x;

            if (!String.IsNullOrEmpty(searchString))
            {
                labours = labours.Where(s => s.LabourName.Contains(searchString));
            }

            return View(labours);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labour labour = db.Labour.Find(id);
            if (labour == null)
            {
                return HttpNotFound();
            }
            return View(labour);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Age,LabourName,Gender")] Labour labour)
        {
            if (ModelState.IsValid)
            {
                db.Labour.Add(labour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(labour);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labour labour = db.Labour.Find(id);
            if (labour == null)
            {
                return HttpNotFound();
            }
            return View(labour);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Age,LabourName,Gender")] Labour labour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(labour);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labour labour = db.Labour.Find(id);
            if (labour == null)
            {
                return HttpNotFound();
            }
            return View(labour);
        }

        // POST: Labours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Labour labour = db.Labour.Find(id);
            db.Labour.Remove(labour);
            db.SaveChanges();
            return RedirectToAction("Index");
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
