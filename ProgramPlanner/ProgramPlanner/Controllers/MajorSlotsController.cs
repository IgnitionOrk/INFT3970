using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProgramPlanner.Models;

namespace ProgramPlanner.Controllers
{
    public class MajorSlotsController : Controller
    {
        private ProgramPlannerContext db = new ProgramPlannerContext();

        // GET: MajorSlots
        public ActionResult Index()
        {
            var majorSlots = db.MajorSlots.Include(m => m.Major);
            return View(majorSlots.ToList());
        }

        // GET: MajorSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorSlot majorSlot = db.MajorSlots.Find(id);
            if (majorSlot == null)
            {
                return HttpNotFound();
            }
            return View(majorSlot);
        }

        // GET: MajorSlots/Create
        public ActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        // POST: MajorSlots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MajorSlotID,MajorID,Rule")] MajorSlot majorSlot)
        {
            if (ModelState.IsValid)
            {
                db.MajorSlots.Add(majorSlot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorSlot.MajorID);
            return View(majorSlot);
        }

        // GET: MajorSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorSlot majorSlot = db.MajorSlots.Find(id);
            if (majorSlot == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorSlot.MajorID);
            return View(majorSlot);
        }

        // POST: MajorSlots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MajorSlotID,MajorID,Rule")] MajorSlot majorSlot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(majorSlot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorSlot.MajorID);
            return View(majorSlot);
        }

        // GET: MajorSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorSlot majorSlot = db.MajorSlots.Find(id);
            if (majorSlot == null)
            {
                return HttpNotFound();
            }
            return View(majorSlot);
        }

        // POST: MajorSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MajorSlot majorSlot = db.MajorSlots.Find(id);
            db.MajorSlots.Remove(majorSlot);
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
