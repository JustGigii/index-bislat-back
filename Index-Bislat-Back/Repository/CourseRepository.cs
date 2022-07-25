using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.EntityFrameworkCore;

namespace Index_Bislat_Back.Repository
{
    public class CourseRepository : ICourse
    {
        private readonly indexbislatContext _context;

        public CourseRepository(indexbislatContext context)
        {
            this._context = context;
        }
        public bool AddCourse(Coursetable course,string bases)
        {
            _context.Add(course);

            return Save();
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
