using index_bislatContext;

namespace Index_Bislat_Back.Interfaces
{
    public interface ICourse
    {
        Task<ICollection<Coursetable>> GetAllCourses();
        Task<Coursetable> GetCourseById(string CourseName);
        Task<bool> IsExist(string CourseName);
        Task<bool> AddCourse(Coursetable course, List<string> bases);
        Task<int> GetCourseIdByNumber(string CourseName);
        Task<bool> DeleteCourse(Coursetable CourseName);
        Task<bool> UpdateCourse(Coursetable course, List<string> bases);
        Task<bool> Save();
    }
}
