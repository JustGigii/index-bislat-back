using index_bislatContext;

namespace Index_Bislat_Back.Dto
{
    public class SortCycleDetailsDto
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public CoursesDto[] courses { get; set; }
    }
}
