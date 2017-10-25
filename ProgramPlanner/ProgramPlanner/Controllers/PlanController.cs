using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProgramPlanner.Models;
using System.Diagnostics;
using System.IO;

namespace ProgramPlanner.Controllers
{
    public class PlanController : Controller
    {
        private ProgramPlannerContext db = new ProgramPlannerContext();

        // GET: Plan
        public ActionResult Index() {
            Setup.InitializeCourseCode(db);
            ViewBag.UnitsPerDegree = 240;
            ViewBag.SubjectsPerSemester = 4;
            getCourseCodes();
            getDegreeCores();
            getMajorCores();
            getDegreeOptionalSlots();
            return View(db.StudyAreas.ToList());
        }
        // GET: Plan
        public ActionResult Create([Bind(Include ="yearDegreeID, majorID")] int yearDegreeID, int majorID)  {
            Setup.InitializeCourseCode(db);
            ViewBag.UnitsPerDegree = 240;
            ViewBag.SubjectsPerSemester = 4;
            getCourseCodes();
            getDegreeCores(yearDegreeID);
            getMajorCores(majorID);
            getDegreeOptionalSlots(yearDegreeID);
            getMajorSlots(majorID);
            return View(db.StudyAreas.ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        private void getCourseCodes() {
            //courses to be searched for in the search box
            var courseCodes = new List<String>();
            foreach (var item in db.Courses){
                courseCodes.Add(item.CourseCode);
            }
            ViewBag.CourseCodeList = courseCodes;
        }
        /// <summary>
        /// 
        /// </summary>
        private void getDegreeCores() {
            //pass in list of degree cores based on semester
            int yearDegreeID = 5;   //will be passed in from main menu
            var myYearDegree = db.YearDegrees.Find(yearDegreeID);
            var semester1Cores = new List<String>();
            var semester2Cores = new List<String>();
            foreach (var item in myYearDegree.DegreeCores) {
                IQueryable<SemesterCourse> temp = db.SemesterCourses.Where(i => i.CourseID == item.CourseID);
                foreach (var semCourse in temp){
                    //make sure the semester course we are dealing with is the one matching our yeardegree
                    if (semCourse.Year == myYearDegree.Year) {
                        if (semCourse.SemesterID == 1) {
                            semester1Cores.Add(item.Course.CourseCode);
                        }
                        else{
                            semester2Cores.Add(item.Course.CourseCode);
                        }
                    }
                }
            }
            ViewBag.Sem1Cores = semester1Cores;
            ViewBag.Sem2Cores = semester2Cores;
        }

        /// <summary>
        /// @override
        /// Populates the Viewbag with the Degrees core courses for both semester 1, and 2.
        /// </summary>
        /// <param name="yearDegreeID"></param>
        private void getDegreeCores(int yearDegreeID)
        {
            var myYearDegree = db.YearDegrees.Find(yearDegreeID);
            var semester1Cores = new List<String>();
            var semester2Cores = new List<String>();
            foreach (var item in myYearDegree.DegreeCores){
                IQueryable<SemesterCourse> temp = db.SemesterCourses.Where(i => i.CourseID == item.CourseID);
                foreach (var semCourse in temp){
                    //make sure the semester course we are dealing with is the one matching our yeardegree
                    if (semCourse.Year == myYearDegree.Year){
                        if (semCourse.SemesterID == 1){
                            semester1Cores.Add(item.Course.CourseCode);
                        }
                        else{
                            semester2Cores.Add(item.Course.CourseCode);
                        }
                    }
                }
            }
            ViewBag.Sem1Cores = semester1Cores;
            ViewBag.Sem2Cores = semester2Cores;
        }
        /// <summary>
        /// 
        /// </summary>
        private void getMajorCores() {
            //pass in list of major cores based on semester
            int majorID = 2;   //will be passed in from main menu
            var myMajor = db.Majors.Find(majorID);
            var semester1MajorCores = new List<String>();
            var semester2MajorCores = new List<String>();
            foreach (var item in myMajor.MajorCores){
                IQueryable<SemesterCourse> temp = db.SemesterCourses.Where(i => i.CourseID == item.CourseID);
                foreach (var semCourse in temp){
                    //make sure the semester course we are dealing with is the one matching our major's yeardegree
                    if (semCourse.Year == myMajor.YearDegree.Year){
                        if (semCourse.SemesterID == 1){
                            semester1MajorCores.Add(item.Course.CourseCode);
                        }
                        else
                        {
                            semester2MajorCores.Add(item.Course.CourseCode);
                        }
                    }
                }
            }
            ViewBag.Sem1MajorCores = semester1MajorCores;
            ViewBag.Sem2MajorCores = semester2MajorCores;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="majorID"></param>
        private void getMajorCores(int majorID){
            ViewBag.Test1 = majorID;
            var myMajor = db.Majors.Find(majorID);
            var semester1MajorCores = new List<String>();
            var semester2MajorCores = new List<String>();
            foreach (var item in myMajor.MajorCores) {
                IQueryable<SemesterCourse> temp = db.SemesterCourses.Where(i => i.CourseID == item.CourseID);
                foreach (var semCourse in temp)   {
                    //make sure the semester course we are dealing with is the one matching our major's yeardegree
                    if (semCourse.Year == myMajor.YearDegree.Year) {
                        if (semCourse.SemesterID == 1){
                            semester1MajorCores.Add(item.Course.CourseCode);
                        }
                        else {
                            semester2MajorCores.Add(item.Course.CourseCode);
                        }
                    }
                }
            }
            ViewBag.Sem1MajorCores = semester1MajorCores;
            ViewBag.Sem2MajorCores = semester2MajorCores;
        }

        private void getDegreeOptionalSlots() {
            //pass in list of degree cores based on semester
            int yearDegreeID = 5;   //will be passed in from main menu
            var myYearDegree = db.YearDegrees.Find(yearDegreeID);
            var degreeSlots = new List<string[]>();

            foreach (var coreSlot in myYearDegree.DegreeCoreSlots) {
                //get all the course codes for this slot and add them to the first element of the string array
                string[] strArr = new string[2];
                Course firstCourse = null; //course we get semester from
                Boolean firstCodeRetrieved = false;
                foreach (var optionalCore in coreSlot.OptionalCoreCourses){
                    strArr[0] += optionalCore.Course.CourseCode + " ";
                    //if code we are getting semester from hasn't been fetched yet, get it now
                    if (!firstCodeRetrieved) {
                        firstCourse = optionalCore.Course;
                        firstCodeRetrieved = true;
                    }
                }

                //get the semester for the first item in the array, we will use that
                IQueryable<SemesterCourse> tempSemCourse = db.SemesterCourses.Where(i => i.CourseID == firstCourse.CourseID);
                foreach (var semCourse in tempSemCourse)   {
                    //make sure the semester course we are dealing with is the one matching our major's yeardegree
                    if (semCourse.Year == myYearDegree.Year) {
                        strArr[1] = semCourse.SemesterID.ToString();
                    }
                }
                degreeSlots.Add(strArr);
            }
            ViewBag.DegreeSlots = degreeSlots;
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yearDegreeID"></param>
        private void getDegreeOptionalSlots(int yearDegreeID)
        {
            var myYearDegree = db.YearDegrees.Find(yearDegreeID);
            var degreeSlots = new List<string[]>();
            foreach (var coreSlot in myYearDegree.DegreeCoreSlots) {
                //get all the course codes for this slot and add them to the first element of the string array
                string[] strArr = new string[2];      
                Course firstCourse = null; //course we get semester from
                Boolean firstCodeRetrieved = false;
                foreach (var optionalCore in coreSlot.OptionalCoreCourses)  {
                    strArr[0] += optionalCore.Course.CourseCode + " ";
                    //if code we are getting semester from hasn't been fetched yet, get it now
                    if (!firstCodeRetrieved) {
                        firstCourse = optionalCore.Course;
                        firstCodeRetrieved = true;
                    }
                }

                //get the semester for the first item in the array, we will use that
                IQueryable<SemesterCourse> tempSemCourse = db.SemesterCourses.Where(i => i.CourseID == firstCourse.CourseID);
                foreach (var semCourse in tempSemCourse)  {
                    //make sure the semester course we are dealing with is the one matching our major's yeardegree
                    if (semCourse.Year == myYearDegree.Year)   {
                        strArr[1] = semCourse.SemesterID.ToString();
                    }
                }
                degreeSlots.Add(strArr);
            }
            ViewBag.DegreeSlots = degreeSlots;
        }



        private void getMajorSlots(int majorID)
        {
            var myMajor = db.Majors.Find(majorID);

            //2d array - each stores an array containing 2 elements:
            //1st dimension is the rule. 2nd is the list of courses that go in that slot
            List<string[]> majorSlots = new List<string[]>();

            //all directeds for this major
            List<String> allDirecteds = new List<String>();

            foreach (var majorSlot in myMajor.MajorSlots)
            {
                string[] strArr = new string[2];

                strArr[0] = majorSlot.Rule; //store the rule

                if (strArr[0].Equals("Any"))
                {
                    strArr[1] = ""; 
                }

                //store all the directeds for this slot
                foreach (var directed in majorSlot.Directeds)
                {
                    string CourseCode = directed.Course.CourseCode;

                    strArr[1] += CourseCode + " "; //because of the way it's passed to javascript, need a non-space or comma delimiter 

                    if (!allDirecteds.Contains(CourseCode)) //if course isn't already in the list of all directeds, add it
                    {
                        allDirecteds.Add(CourseCode);
                    }

                }

                majorSlots.Add(strArr);

            }
            ViewBag.MajorSlots = majorSlots;
            ViewBag.AllDirecteds = allDirecteds;
        }
        [HttpPost]
        public ActionResult isRunningInSemester(int semesterID, string courseCode) {
            //Need to check if that course was running in the same year as the program planner. 
            int count = 0;
            var abbr = courseCode.Substring(0, 4);
            var abbrID = (db.Abbreviations.Where(
                ar => ar.AbbrevName == abbr).SingleOrDefault()).AbbreviationID;

            var code = Convert.ToInt32(courseCode.Substring(4, 4));     // e.g. 1004
            var course = db.Courses.Where(
                c => c.Code == code && 
                c.AbbreviationID == abbrID).SingleOrDefault();

            if (course != null){
                count = (db.SemesterCourses.Where(
                    sc => sc.SemesterID == semesterID && 
                    sc.CourseID == course.CourseID)).Count();
            }
            else {
                throw new NullReferenceException();
            }
            Debug.WriteLine("COUNT: " + count + " : " + courseCode);
            return Json(new { iCount = count });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "InputData")]Plan plan) {
            string blah = plan.InputData;
            return View();
        }
    }
}