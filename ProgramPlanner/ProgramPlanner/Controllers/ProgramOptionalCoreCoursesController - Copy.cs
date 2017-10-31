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
    public class ProgramOptionalCoreCoursesController : Controller
    {
        private ProgramPlannerContext db = new ProgramPlannerContext();

        // GET: ProgramOptionalCoreCourses
        public ActionResult Index()
        {
            var programOptionalCoreCourses = db.ProgramOptionalCoreCourses.Include(p => p.OptionalCoreCourse).Include(p => p.ProgramStructure);
            return View(programOptionalCoreCourses.ToList());
        }

        // GET: ProgramOptionalCoreCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramOptionalCoreCourse programOptionalCoreCourse = db.ProgramOptionalCoreCourses.Find(id);
            if (programOptionalCoreCourse == null)
            {
                return HttpNotFound();
            }
            return View(programOptionalCoreCourse);
        }

        // GET: ProgramOptionalCoreCourses/Create
        public ActionResult Create()
        {
            ViewBag.OptionalCoreID = new SelectList(db.OptionalCoreCourses, "OptionalCoreCourseID", "OptionalCoreCourseID");
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email");
            return View();
        }

        // POST: ProgramOptionalCoreCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramStructureID,OptionalCoreID,Completed")] ProgramOptionalCoreCourse programOptionalCoreCourse)
        {
            if (ModelState.IsValid)
            {
                db.ProgramOptionalCoreCourses.Add(programOptionalCoreCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OptionalCoreID = new SelectList(db.OptionalCoreCourses, "OptionalCoreCourseID", "OptionalCoreCourseID", programOptionalCoreCourse.OptionalCoreID);
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email", programOptionalCoreCourse.ProgramStructureID);
            return View(programOptionalCoreCourse);
        }

        // GET: ProgramOptionalCoreCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramOptionalCoreCourse programOptionalCoreCourse = db.ProgramOptionalCoreCourses.Find(id);
            if (programOptionalCoreCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.OptionalCoreID = new SelectList(db.OptionalCoreCourses, "OptionalCoreCourseID", "OptionalCoreCourseID", programOptionalCoreCourse.OptionalCoreID);
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email", programOptionalCoreCourse.ProgramStructureID);
            return View(programOptionalCoreCourse);
        }

        // POST: ProgramOptionalCoreCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgramStructureID,OptionalCoreID,Completed")] ProgramOptionalCoreCourse programOptionalCoreCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programOptionalCoreCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OptionalCoreID = new SelectList(db.OptionalCoreCourses, "OptionalCoreCourseID", "OptionalCoreCourseID", programOptionalCoreCourse.OptionalCoreID);
            ViewBag.ProgramStructureID = new SelectList(db.ProgramStructures, "ProgramStructureID", "Email", programOptionalCoreCourse.ProgramStructureID);
            return View(programOptionalCoreCourse);
        }

        // GET: ProgramOptionalCoreCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramOptionalCoreCourse programOptionalCoreCourse = db.ProgramOptionalCoreCourses.Find(id);
            if (programOptionalCoreCourse == null)
            {
                return HttpNotFound();
            }
            return View(programOptionalCoreCourse);
        }

        // POST: ProgramOptionalCoreCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramOptionalCoreCourse programOptionalCoreCourse = db.ProgramOptionalCoreCourses.Find(id);
            db.ProgramOptionalCoreCourses.Remove(programOptionalCoreCourse);
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
