using index_bislatContext;

namespace Index_Bislat_Back.Interfaces
{
    public interface ICourse
    {
        ICollection<Coursetable> GetAllCourses();
        Task<Coursetable> GetCourseById(string CourseNumber);
        bool IsExist(string CourseNumber);
        bool AddCourse(Coursetable course);
        bool DeleteCourse(int CourseNumber);
        bool UpdateCourse(Coursetable courset);
    }
}
