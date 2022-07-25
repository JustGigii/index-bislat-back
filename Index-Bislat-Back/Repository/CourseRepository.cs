using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Index_Bislat_Back.Repository
{
    public class CourseRepository : ICourse
    {
        private readonly indexbislatContext _context;

        public CourseRepository(indexbislatContext context)
        {
            this._context = context;
        }
        public bool AddCourse(Coursetable course,List<string> bases,IAifBase iabase)
        {
            int baseId, courseId;
            //  var pram = parameters(course);
            course.Baseofcourses = null;
            try
            {
                _context.Coursetables.Add(course);
                if(!Save()) return false;
                 courseId = GetCourseIdByNumber(course.CourseNumber);
                foreach  (var i in bases)
                {
                    if(!iabase.Isexsit(i))
                    {
                        baseId = iabase.AddBase(i);
                    }
                    baseId = iabase.getidofCourse(i);
                    var basofcourse = _context.Baseofcourses.FromSqlRaw("INSERT into baseofcourse (BASEID,courseId) VALUE ({0},{1})", baseId,courseId).FirstOrDefault();
                }

                return true;
            }
            catch (Exception ex) { throw ex; };
            
        }
        public bool DeleteCourse(int CourseNumber)
        {
            throw new NotImplementedException();
        }

        public ICollection<Coursetable> GetAllCourses()
        {
            return _context.Coursetables.ToList();
        }

        public async Task<Coursetable> GetCourseById(string CourseNumber)
        {
            return await  _context.Coursetables
                 .Include(p => p.Baseofcourses).ThenInclude(a => a.Base)
                 .Where(p => p.CourseNumber.Contains(CourseNumber))
                 .FirstOrDefaultAsync();
        }
        public int GetCourseIdByNumber(string CourseNumber)
        {
            var course =  _context.Coursetables
                 .Where(p => p.CourseNumber.Contains(CourseNumber))
                 .FirstOrDefault();
            return course.CourseId;
        }

        public bool IsExist(string CourseNumber)
        {
            return _context.Coursetables.Any(p => p.CourseNumber.Contains(CourseNumber));
        }

        public bool UpdateCourse(Coursetable courset)
        {
            throw new NotImplementedException();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
