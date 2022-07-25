﻿using AutoMapper;
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

            CreateMap<Coursetable, CourseDetailsDio>().ForMember(dest => dest.BaseofcoursesDto, act => act.MapFrom(src => src.Baseofcourses));
            CreateMap<Baseofcourse, BaseofcourseDto>().ForMember(dest => dest.Base, act => act.MapFrom(src => src.Base.BaseName));

        }
    }
}
