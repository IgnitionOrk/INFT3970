using ProgramPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ProgramPlanner.Controllers
{
    public class MainMenuController : Controller
    {
        private ProgramPlannerContext db = new ProgramPlannerContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            // Extract all the Universities in the database.
            ViewBag.Universities = new SelectList(db.Universities, "UniversityID", "UniName");

            // Extract the degrees from the database for the first University. 
            var degrees = db.Degrees.Where(y => y.UniversityID == db.Universities.FirstOrDefault().UniversityID);

            // Extract the Associated Year Degrees with the first Degree; 
            var yDegrees = db.YearDegrees.Where(yd => yd.DegreeID == degrees.FirstOrDefault().DegreeID);

            var years = (from y in yDegrees select new { y.YearDegreeID, y.Year});

            // Extract the majors for the first Year Degree.
            var majors = db.Majors.Where(m => m.YearDegreeID == yDegrees.FirstOrDefault().YearDegreeID);

            ViewBag.Degrees = new SelectList(degrees, "UniversityID", "DegreeName", 0);
            ViewBag.Years = new SelectList(years,"YearDegreeID", "Year" ,0);
            ViewBag.Majors = new SelectList(majors, "MajorID", "MajorName", 0);    
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="universityID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDegrees(int universityID)
        {
            // Find all the Degrees associated with a University
            var uDegrees = db.Degrees.Where(degree => degree.UniversityID == universityID);
            SelectList sl = new SelectList(uDegrees, "DegreeID", "DegreeName");
            return Json(sl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="degreeID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetYears(int degreeID)
        {
            // Find the YearDegrees associated with the particular Degree (degreeID).
            var yDegrees = db.YearDegrees.Where(yd => yd.DegreeID == degreeID);

            var years = (from y in yDegrees select new { y.YearDegreeID, y.Year});

            SelectList sl = new SelectList(years, "YearDegreeID", "Year", 0);
            return Json(sl);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="yearDegreeID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMajors(int yearDegreeID)
        {          
            // Find the YearDegree associated with the particular Degree (degreeID).
            var yDegree = db.YearDegrees.FirstOrDefault(yd => yd.YearDegreeID == yearDegreeID);


            // Find the majors that were held for that particular Year Degree. 
            var dMajors = db.Majors.Where(major => major.YearDegreeID == yDegree.YearDegreeID);

            SelectList sl = new SelectList(dMajors, "MajorID", "MajorName");
            return Json(sl);
        }
    }
}
