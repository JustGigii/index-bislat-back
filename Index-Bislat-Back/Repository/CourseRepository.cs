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
        public async Task<bool> AddCourse(Coursetable course, List<string> bases)
        {
            course.Baseofcourses = null;
            try
            {
                _context.Coursetables.Add(course);
                if ( !await Save()) return false;
                    if (!await AddBaseToCourse(course, bases)) return false;
                return true;
            }
            catch (Exception ex) { throw ex; };

        }

        private async Task<bool> AddBaseToCourse(Coursetable course, List<string> bases)
        {
           try
            { 
            foreach (var iafBase in bases)
            {
                var idnum = MakePrameter("@ncourseNumber", System.Data.DbType.String, 45, course.CourseNumber);
                var baseofcourse = MakePrameter("@base", System.Data.DbType.String, 45, iafBase); 
                await _context.Database.ExecuteSqlRawAsync("call Add_base_to_course(@ncourseNumber,@base);", idnum, baseofcourse);

            }
            return true;
            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
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

        public async Task<bool> DeleteCourse(Coursetable CourseNumber)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("delete from baseofcourse where courseId = {0}", CourseNumber.CourseId);
                _context.Coursetables.Remove(CourseNumber);
                return await Save();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message);  return false; }
        }

        public async Task<ICollection<Coursetable>> GetAllCourses()
        {
            return await _context.Coursetables.ToListAsync();
        }

        public async Task<Coursetable> GetCourseById(string CourseNumber)
        {
            return await _context.Coursetables
                 .Include(p => p.Baseofcourses).ThenInclude(a => a.Base)
                 .Where(p => p.CourseNumber.Contains(CourseNumber))
                 .FirstOrDefaultAsync();
        }
        public async Task<int> GetCourseIdByNumber(string CourseNumber)
        {
            var course = await _context.Coursetables
                 .Where(p => p.CourseNumber.Contains(CourseNumber))
                 .FirstOrDefaultAsync();
            return  course.CourseId;
        }
        
        public async Task<bool> IsExist(string CourseNumber)
        {
            return await _context.Coursetables.AnyAsync(p => p.CourseNumber.Contains(CourseNumber));
        }

        public  async Task<bool> UpdateCourse(Coursetable course, List<string> bases)
        {
            try
            {

                var courseold =  _context.Coursetables
                    .Where(p => p.CourseNumber.Contains(course.CourseNumber))
                    .FirstOrDefault();
                return await DeleteCourse(courseold) && await AddCourse(course, bases);

            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
      
    }
}
