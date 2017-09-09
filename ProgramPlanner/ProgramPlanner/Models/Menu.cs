using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPlanner.Models
{
    public class Menu
    {
        public virtual ICollection<University> Universities { get; set; }
        public virtual ICollection<YearDegree> YearDegrees { get; set; }
        public virtual ICollection<Major> Majors { get; set; }
    }
}