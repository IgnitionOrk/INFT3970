using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPlanner.Models
{
    public class MajorSlot
    {
        public int MajorSlotID { get; set; }
        public int MajorID { get; set; }
        public virtual Major Major { get; set; }
        public virtual ICollection<DirectedSlot> Directeds { get; set; }
        public string Rule { get; set; }
    }
}