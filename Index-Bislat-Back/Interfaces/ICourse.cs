using index_bislatContext;

namespace Index_Bislat_Back.Interfaces
{
    public interface ICourse
    {
        Task<ICollection<Coursetable>> GetAllCourses();
        Task<Coursetable> GetCourseById(string CourseNumber);
        Task<bool> IsExist(string CourseNumber);
        Task<bool> AddCourse(Coursetable course, List<string> bases);
        Task<int> GetCourseIdByNumber(string CourseNumber);
        Task<bool> DeleteCourse(Coursetable CourseNumber);
        Task<bool> UpdateCourse(Coursetable course, List<string> bases);
        Task<bool> Save();
    }
}
