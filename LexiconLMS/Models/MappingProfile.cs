using AutoMapper;
using LexiconLMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Module, ModuleViewModel>();
            CreateMap<ModuleViewModel, Module>();
            CreateMap<AddCourseViewModel, Course>();
            CreateMap<User, CourseDetailsViewModel>()
                .ForMember(dest => dest.TeacherEmail, from => from.MapFrom(src => src.Email));
            CreateMap<Course, CourseDetailsViewModel > ().ForMember(a => a.Modules, opt => opt.Ignore());
            CreateMap<Course, CourseListViewModel>()
                .ForMember(dest => dest.TeacherName, from => from.MapFrom(src => src.Teacher.FullName));


            CreateMap<ActivityViewModel, Activityy>();
            CreateMap<Activityy, ActivityViewModel>();
        }

    }
}
