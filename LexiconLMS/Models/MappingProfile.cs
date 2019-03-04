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
            CreateMap<AddCourseViewModel, Course>();
            CreateMap<User, CourseDetailsViewModel>()
                .ForMember(dest => dest.TeacherEmail, from => from.MapFrom(src => src.Email));
        }
    }
}
