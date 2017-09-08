using ProgramPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPlanner.Controllers
{
    public static class Setup
    {
        // Initializes the data for the CourseCode for each Course in the database.
        // But doesn't store the data into the database.
        public static void InitializeCourseCode(ProgramPlannerContext db)
        {
            foreach (Course course in db.Courses)
            {
                course.CourseCode = course.Abbreviation.AbbrevName + course.Code;
            }
        }

        // This was just for testing purposes, but it will help if we are doing multiple joins
        // In a database.
        public static List<Test> InitializeReplacements(ProgramPlannerContext db)
        {
            List<Test> temp = new List<Test>();
            var replacements = (
               from c in db.Courses
               join r in db.Replacements
               on c.ReplacementID equals r.ReplacementID
               join y in db.YearDegrees
               on r.YearDegreeID equals y.YearDegreeID
               select new { c, r, y });
            foreach (var t in replacements)
            {
                temp.Add(new Test()
                {
                    Replaced = t.c,
                    Replacement = t.r,
                    YearDegree = t.y
                });
            }
            return temp;
        }
    }
}