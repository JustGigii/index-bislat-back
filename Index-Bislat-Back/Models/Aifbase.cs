using System;
using System.Collections.Generic;

namespace index_bislatContext
{
    public partial class Aifbase
    {

        public int  Id { get; set; }
        public string? BaseName { get; set; }

        public virtual ICollection<Baseofcourse> Baseofcourses { get; set; }
    }
}
