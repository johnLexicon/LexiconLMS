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
            CreateMap<Course, CourseDetailsViewModel>().ForMember(a => a.Modules, opt => opt.Ignore());
        }
    }
}
