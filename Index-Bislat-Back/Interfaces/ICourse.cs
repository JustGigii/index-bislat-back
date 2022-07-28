using index_bislatContext;

namespace Index_Bislat_Back.Interfaces
{
    public interface ICourse
    {
        ICollection<Coursetable> GetAllCourses();
        Task<Coursetable> GetCourseById(string CourseNumber);
        bool IsExist(string CourseNumber);
        bool AddCourse(Coursetable course, List<string> bases);
        int GetCourseIdByNumber(string CourseNumber);
        bool DeleteCourse(Coursetable CourseNumber);
        bool UpdateCourse(Coursetable course, List<string> bases);
        public bool Save();
    }
}
