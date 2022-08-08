using System;
using System.Collections.Generic;

namespace index_bislatContext
{
    public partial class Couseofsort
    {
        public int Id { get; set; }
        public int Sortid { get; set; }
        public int CourseId { get; set; }

        public virtual Coursetable Course { get; set; } = null!;
        public virtual SortCycle Sort { get; set; } = null!;
    }
}
