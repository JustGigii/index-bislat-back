using index_bislatContext;

namespace Index_Bislat_Back.Dto
{
    public class CourseDetailsDio
    {
        public int CourseId { get; set; }
        public string? Category { get; set; }
        public string? CourseNumber { get; set; }
        public string? CourseName { get; set; }
        public string? CourseTime { get; set; }
        public string? CourseDescription { get; set; }
        public string? YouTubeUrl { get; set; }
        public string? ImgUrl { get; set; }
        public string? Note { get; set; }

        public ICollection<BaseofcourseDto> BaseofcoursesDto { get; set; }
    }
}
