using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
//{
//    "category": "משהו",
//  "courseNumber": "69420",
//  "courseName": "בדיקה",
//  "courseTime": "בידקה",
//  "courseBases": [
//    "בח''א 8"
//  ],
//  "courseDescription": "בדיקה",
//  "youTubeURL": "בדיקה",
//  "imgURL": "בדיקה",
//  "note": "בדיקה"
//}
namespace Index_Bislat_Back.Repository
{
    public class CourseRepository : ICourse
    {
        private readonly indexbislatContext _context;

        public CourseRepository(indexbislatContext context)
        {
            this._context = context;
        }
        public bool AddCourse(Coursetable course, List<string> bases)
        {
            course.Baseofcourses = null;
            try
            {
                _context.Coursetables.Add(course);
                if (!Save().Result) return false;
                AddBaseToCourse(course, bases);
                return true;
            }
            catch (Exception ex) { throw ex; };

        }

        private void AddBaseToCourse(Coursetable course, List<string> bases)
        {
            foreach (var iafBase in bases)
            {
                var idnum = MakePrameter("@ncourseNumber", System.Data.DbType.String, 45, course.CourseNumber);
                var baseofcourse = MakePrameter("@base", System.Data.DbType.String, 45, iafBase); 
                _context.Database.ExecuteSqlRaw("call Add_base_to_course(@ncourseNumber,@base);", idnum, baseofcourse);
            }
        }

        private MySqlParameter MakePrameter(string name, System.Data.DbType type, int size, object? value)
        {
            var pram = new MySqlParameter();
            pram.ParameterName = name;
            pram.DbType = type;
            pram.Size = size;
            pram.Value = value;
            pram.Direction = System.Data.ParameterDirection.Input;
            return pram;
        }

        public bool DeleteCourse(Coursetable CourseNumber)
        {
            _context.Database.ExecuteSqlRaw("delete from baseofcourse where courseId = {0}", CourseNumber.CourseId);
            _context.Coursetables.Remove(CourseNumber);
            return Save().Result;
        }

        public ICollection<Coursetable> GetAllCourses()
        {
            return _context.Coursetables.ToList();
        }

        public async Task<Coursetable> GetCourseById(string CourseNumber)
        {
            return await _context.Coursetables
                 .Include(p => p.Baseofcourses).ThenInclude(a => a.Base)
                 .Where(p => p.CourseNumber.Contains(CourseNumber))
                 .FirstOrDefaultAsync();
        }
        public int GetCourseIdByNumber(string CourseNumber)
        {
            var course = _context.Coursetables
                 .Where(p => p.CourseNumber.Contains(CourseNumber))
                 .FirstOrDefault();
            return course.CourseId;
        }

        public bool IsExist(string CourseNumber)
        {
            return _context.Coursetables.Any(p => p.CourseNumber.Contains(CourseNumber));
        }

        public bool UpdateCourse(Coursetable course, List<string> bases)
        {
            try
            {
                return DeleteCourse(course) && AddCourse(course, bases);
     
            }
            catch(Exception err) { throw err; }
        }
        public async Task<bool> Save()
        {
             var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
      
    }
}
