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
            CreateMap<ModuleDetailsViewModel, Module>();//.ForSourceMember(a => a.Documents, opt => opt.DoNotValidate());
            CreateMap<Module, ModuleDetailsViewModel>();//.ForMember(a => a.Documents, opt => opt.Ignore()).ReverseMap();

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

            CreateMap<ActivityDocument, AssignmentListViewModel>()
                .ForMember(dest => dest.Owner, from => from.MapFrom(src => src.User))
                .ForMember(dest => dest.CourseName, from => from.MapFrom(src => src.Activityy.Module.Course.Name))
                .ForMember(dest => dest.ModuleName, from => from.MapFrom(src => src.Activityy.Module.Name))
                .ForMember(dest => dest.Deadline, from => from.MapFrom(src => src.Activityy.EndDate));

            CreateMap<Bogus.Person, User>()
                .ForMember(dest => dest.UserName, from => from.MapFrom(src => src.Email));
        }

    }
}
