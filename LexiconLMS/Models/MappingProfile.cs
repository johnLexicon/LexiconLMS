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
            CreateMap<Module, ModuleAddViewModel>();
            CreateMap<ModuleAddViewModel, Module>();
            CreateMap<ModuleDetailsViewModel, Module>().ForSourceMember(a => a.Documents, opt => opt.DoNotValidate());
            CreateMap<Module, ModuleDetailsViewModel>();

            CreateMap<Module, ModuleDetailsViewModel>();
            CreateMap<ModuleDetailsViewModel, Module>();

            CreateMap<CourseAddViewModel, Course>();
            CreateMap<User, CourseDetailsViewModel>()
                .ForMember(dest => dest.TeacherEmail, from => from.MapFrom(src => src.Email));
            CreateMap<Course, CourseDetailsViewModel > ().ForMember(a => a.Modules, opt => opt.Ignore());

            CreateMap<ActivityAddViewModel, Activityy>();
            CreateMap<ActivityDetailsViewModel, Activityy>();
            CreateMap<Activityy, ActivityAddViewModel>();
            CreateMap<ActivityDetailsViewModel, Activityy>();
            CreateMap<Activityy, ActivityDetailsViewModel>();
            CreateMap<GenericDocument, DocumentListViewModel>();

            CreateMap<Bogus.Person, User>()
                .ForMember(dest => dest.UserName, from => from.MapFrom(src => src.Email));
        }

    }
}
