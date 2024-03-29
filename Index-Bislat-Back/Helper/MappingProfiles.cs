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

            CreateMap<Coursetable, CourseDetailsDto>().ForMember(dest => dest.CourseBases, act => act.MapFrom(src => src.stringbase()));
            CreateMap<CourseDetailsDto, Coursetable>();

            CreateMap<Baseofcourse, BaseofcourseDto>().ForMember(dest => dest.Base, act => act.MapFrom(src => src));
            CreateMap<BaseofcourseDto, Baseofcourse>();

            CreateMap<SortCycle, SortCycleDetailsDto>().ForMember(dest => dest.courses, act => act.MapFrom(src => src.StringCourseNum()));
            CreateMap<SortCycleDetailsDto, SortCycle>();

            CreateMap<SortCycle, SortCycleDto>();
            CreateMap<SortCycleDto, SortCycle>();

            CreateMap<Choisetable, ChoisetableDto>();
            CreateMap<ChoisetableDto, Choisetable>();
        }
    }
}
