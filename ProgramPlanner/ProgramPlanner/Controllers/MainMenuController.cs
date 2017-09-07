using ProgramPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramPlanner.Controllers
{
    public class MainMenuController : Controller
    {
        private ProgramPlannerContext db = new ProgramPlannerContext();

        public ActionResult Menu() {

            // If you want to combine YearDegrees and Degrees together use this code
            /*
              new SelectList((
                from d in db.Degrees
                join yd in db.YearDegrees
                on d.DegreeID equals yd.DegreeID
                select new {yd.YearDegreeID, FullName = d.DegreeName + ", " + yd.Year }
                ), "YearDegreeID", "FullName");
             */
            ViewBag.Universities = new SelectList(db.Universities, "UniversityID", "UniName");
            ViewBag.Degrees = new SelectList(db.Degrees, "UniversityID", "DegreeName");
            ViewBag.Years = new SelectList(Setup.uniqueYearList(db));
            ViewBag.Majors = new SelectList(db.Majors, "MajorID", "MajorName");    
            return View();
        }
    }
}
