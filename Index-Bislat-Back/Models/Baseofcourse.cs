using System;
using System.Collections.Generic;

namespace index_bislatContext
{
    public partial class Baseofcourse
    {
        public int Id { get; set; }
        public int? Baseid { get; set; }
        public int? CourseId { get; set; }

        public virtual Aifbase? Base { get; set; }
        public virtual Coursetable? Course { get; set; }
    }
}
