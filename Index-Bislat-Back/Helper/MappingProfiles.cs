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

            CreateMap<Coursetable, CourseDetailsDto>().ForMember(dest => dest.Baseofcourses, act => act.MapFrom(src => src.stringbase()));
            CreateMap<CourseDetailsDto, Coursetable>();

            CreateMap<Baseofcourse, BaseofcourseDto>().ForMember(dest => dest.Base, act => act.MapFrom(src => src));
            CreateMap<BaseofcourseDto, Baseofcourse>();
        }
    }
}
