using ProgramPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
/// <summary>
/// Student number: 3179234
/// Author: Ryan Cunneen.
/// </summary>
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
            UniversityOptions();
            DegreeOptions();
            YearDegreeOptions();
            MajorOptions();
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        private void UniversityOptions() {
            // Extract all the Universities in the database.
            ViewBag.Universities = new SelectList(db.Universities, "UniversityID", "UniName");
        }
        /// <summary>
        /// 
        /// </summary>
        private void DegreeOptions() {
            // Extract the degrees from the database for the first University. 
            var degrees = db.Degrees.Where(y => y.UniversityID == db.Universities.FirstOrDefault().UniversityID);
            ViewBag.Degrees = new SelectList(degrees, "UniversityID", "DegreeName", 0);
        }
        /// <summary>
        /// 
        /// </summary>
        private void YearDegreeOptions() {
            // Extract the Associated Year Degrees with the first Degree; 
            var yearDegrees = db.YearDegrees.Where(yd => yd.DegreeID == db.Degrees.FirstOrDefault().DegreeID);
            ViewBag.YearDegrees = new SelectList(yearDegrees, "YearDegreeID", "Year", 0);
        }
        /// <summary>
        /// 
        /// </summary>
        private void MajorOptions() {
            // Extract the majors for the first Year Degree.
            var majors = db.Majors.Where(m => m.YearDegreeID == db.YearDegrees.FirstOrDefault().YearDegreeID);
            ViewBag.Majors = new SelectList(majors, "MajorID", "MajorName", 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="universityID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DegreeOptions(int universityID)
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
        public ActionResult YearDegreeOptions(int degreeID)
        {
            // Find the YearDegrees associated with the particular Degree (degreeID).
            var yearDegrees = db.YearDegrees.Where(yd => yd.DegreeID == degreeID);
            var sl = new SelectList(yearDegrees, "YearDegreeID", "Year", 0);
            return Json(sl);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yearDegreeID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MajorOptions(int yearDegreeID)
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
