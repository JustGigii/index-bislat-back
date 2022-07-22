using AutoMapper;
using Index_Bislat_Back.Dto;
using index_bislatContext;

namespace Index_Bislat_Back.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Aifbase, AifbaseDto>();
            CreateMap<AifbaseDto, Aifbase>();

            CreateMap<Coursetable, CoursesDto>();
            CreateMap<CoursesDto, Coursetable>();

            CreateMap<BaseofcourseDto, Baseofcourse>();
            CreateMap<Baseofcourse, BaseofcourseDto>();

            CreateMap<BaseofcourseDto, Baseofcourse>();
            CreateMap<Baseofcourse, BaseofcourseDto>();

            CreateMap<CourseDetailsDio, Coursetable>();
            CreateMap<Coursetable,CourseDetailsDio>();

        }
    }
}
