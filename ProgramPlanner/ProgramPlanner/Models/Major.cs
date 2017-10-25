using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProgramPlanner.Models
{
    public class Major
    {
        public int MajorID { get; set; }
        public string MajorName { get; set; }
        public int YearDegreeID { get; set; }
        public virtual YearDegree YearDegree { get; set; }
        public virtual ICollection<MajorCore> MajorCores { get; set; }
        public virtual ICollection<ProgramMajor> ProgramMajors { get; set; }
        public virtual ICollection<MajorSlot> MajorSlots { get; set; }
    }
}