using Index_Bislat_Back.Dto;
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

        public ICollection<CoursesDto> CoursesDtoNum()
        {
            List<CoursesDto> Courses = new List<CoursesDto>();
            Couseofsorts.ToList().ForEach(item => Courses.Add(ConverClass(item.Course)));
            return Courses;
        }

        public CoursesDto ConverClass(Coursetable course)
        {
            CoursesDto map = new CoursesDto();

            map.CourseNumber = course.CourseNumber;
            map.Gender = course.Gender;
            map.CourseName = course.CourseName;
            return map;
        }
        public ICollection<string> StringCourseNum()
        {
            List<string> Courses = new List<string>();
            Couseofsorts.ToList().ForEach(item => Courses.Add(item.Course.CourseNumber));
            return Courses;
        }
    }
}
