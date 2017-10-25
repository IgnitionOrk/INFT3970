using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProgramPlanner.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public int Code { get; set; }
        public string CourseCode{ get; set; }
        public string CourseName { get; set; }
        public int Units { get; set; }
        // The recommended year the course should be take First, Second, Third, Fourth, etc. 
        public int RecommendedYear { get; set; }
        // Additional information about any complicated prerequisites e.g. Major Project Assumed Knowledge 
        // paragraph. 
        public string Information { get; set; }
        public int UniversityID { get; set; }
        public virtual University University { get; set; }
        public int AbbreviationID { get; set; }
        public virtual Abbreviation Abbreviation { get; set; }
        public int? ReplacementID { get; set; }
        public virtual Replacement Replacement { get; set; }
        // These are your 'AND' prerequisites. A Course can one-to-many mandatory prerequisites. 
        public virtual ICollection<Course> MandatoryPrerequisites { get; set; }
        // These are your 'OR' prerequisites. A Course can have zero-to-many optional prerequisites. 
        public virtual ICollection<Course> OptionalPrerequisites { get; set; }
        public virtual ICollection<ProgramElective> ProgramElectives { get; set; }
        public virtual ICollection<MajorCore> MajorCores { get; set; }
        public virtual ICollection<DegreeCore> DegreeCores { get; set; }
        public virtual ICollection<TrimesterCourse> TrimesterCourses { get; set; }
        public virtual ICollection<SemesterCourse> SemesterCourses { get; set; }
        public virtual ICollection<OptionalCoreCourse> OptionalCoreCourses { get; set; }
        public virtual ICollection<DirectedSlot> Directeds { get; set; }
    }
}