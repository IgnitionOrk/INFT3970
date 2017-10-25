using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPlanner.Models
{
    public class DirectedSlot
    {
        public int DirectedSlotID { get; set; }
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
        public int MajorSlotID { get; set; }
        public virtual MajorSlot MajorSlot { get; set; }
        public virtual ICollection<ProgramDirected> ProgramDirecteds { get; set; }
    }
}