using System;
using System.Collections.Generic;

namespace index_bislatContext
{
    /// <summary>
    /// מחזור מיון 
    /// </summary>
    public partial class SortCycle
    {
        public SortCycle()
        {
            Choisetables = new HashSet<Choisetable>();
            Couseofsorts = new HashSet<Couseofsort>();
        }

        public int Sortid { get; set; }
        /// <summary>
        /// שם מיון 
        /// </summary>
        public string Name { get; set; } = null!;

        public virtual ICollection<Choisetable> Choisetables { get; set; }
        public virtual ICollection<Couseofsort> Couseofsorts { get; set; }

        public ICollection<string> StringCourseNum()
        {
            List<string> Courses = new List<string>();
            Couseofsorts.ToList().ForEach(item => Courses.Add(item.Course.CourseNumber));
            return Courses;
        }
    }
}
