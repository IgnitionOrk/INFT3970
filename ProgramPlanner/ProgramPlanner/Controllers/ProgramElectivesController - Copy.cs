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
    public class ProgramElectivesController : Controller
    {
        private ProgramPlannerContext db = new ProgramPlannerContext();

        // GET: ProgramElectives
        public ActionResult Index()
        {
            var programElectives = db.ProgramElectives.Include(p => p.Course).Include(p => p.ProgramStructure);
            return View(programElectives.ToList());
        }

        // GET: ProgramElectives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramElective programElective = db.ProgramElectives.Find(id);
            if (programElective == null)
            {
                return HttpNotFound();
            }
            return View(programElective);
        }

        // GET: ProgramElectives/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email");
            return View();
        }

        // POST: ProgramElectives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,ProgramStructureID,Completed")] ProgramElective programElective)
        {
            if (ModelState.IsValid)
            {
                db.ProgramElectives.Add(programElective);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", programElective.CourseID);
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email", programElective.ProgramStructureID);
            return View(programElective);
        }

        // GET: ProgramElectives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramElective programElective = db.ProgramElectives.Find(id);
            if (programElective == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", programElective.CourseID);
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email", programElective.ProgramStructureID);
            return View(programElective);
        }

        // POST: ProgramElectives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,ProgramStructureID,Completed")] ProgramElective programElective)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programElective).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", programElective.CourseID);
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email", programElective.ProgramStructureID);
            return View(programElective);
        }

        // GET: ProgramElectives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramElective programElective = db.ProgramElectives.Find(id);
            if (programElective == null)
            {
                return HttpNotFound();
            }
            return View(programElective);
        }

        // POST: ProgramElectives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramElective programElective = db.ProgramElectives.Find(id);
            db.ProgramElectives.Remove(programElective);
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
