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
    public class ProgramMajorsController : Controller
    {
        private ProgramPlannerContext db = new ProgramPlannerContext();

        // GET: ProgramMajors
        public ActionResult Index()
        {
            var programMajors = db.ProgramMajors.Include(p => p.Major).Include(p => p.ProgramStructure);
            return View(programMajors.ToList());
        }

        // GET: ProgramMajors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramMajor programMajor = db.ProgramMajors.Find(id);
            if (programMajor == null)
            {
                return HttpNotFound();
            }
            return View(programMajor);
        }

        // GET: ProgramMajors/Create
        public ActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email");
            return View();
        }

        // POST: ProgramMajors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramStructureID,MajorID")] ProgramMajor programMajor)
        {
            if (ModelState.IsValid)
            {
                db.ProgramMajors.Add(programMajor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", programMajor.MajorID);
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email", programMajor.ProgramStructureID);
            return View(programMajor);
        }

        // GET: ProgramMajors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramMajor programMajor = db.ProgramMajors.Find(id);
            if (programMajor == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", programMajor.MajorID);
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email", programMajor.ProgramStructureID);
            return View(programMajor);
        }

        // POST: ProgramMajors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgramStructureID,MajorID")] ProgramMajor programMajor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programMajor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", programMajor.MajorID);
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email", programMajor.ProgramStructureID);
            return View(programMajor);
        }

        // GET: ProgramMajors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramMajor programMajor = db.ProgramMajors.Find(id);
            if (programMajor == null)
            {
                return HttpNotFound();
            }
            return View(programMajor);
        }

        // POST: ProgramMajors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramMajor programMajor = db.ProgramMajors.Find(id);
            db.ProgramMajors.Remove(programMajor);
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
