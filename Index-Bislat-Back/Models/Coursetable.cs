using System;
using System.Collections.Generic;

namespace index_bislatContext
{
    public partial class Coursetable
    {
        public Coursetable()
        {
            Baseofcourses = new HashSet<Baseofcourse>();
            Couseofsorts = new HashSet<Couseofsort>();
        }

        public int CourseId { get; set; }
        public string? Category { get; set; }
        public string? Gender { get; set; }
        public string? CourseNumber { get; set; }
        public string? CourseName { get; set; }
        public string? CourseTime { get; set; }
        public string? CourseDescription { get; set; }
        public string? YouTubeUrl { get; set; }
        public string? ImgUrl { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<Baseofcourse> Baseofcourses { get; set; }
        public virtual ICollection<Couseofsort> Couseofsorts { get; set; }
        public ICollection<string> stringbase()
        {
            List<string> bases = new List<string>();
            Baseofcourses.ToList().ForEach(item => bases.Add(item.Base.BaseName));
            return bases;
        }
    }
}
