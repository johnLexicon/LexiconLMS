using System;
using System.Collections.Generic;
using LexiconLMS.Models;

namespace LexiconLMS.ViewModels
{
    public class TeacherPageViewModel
    {
        public List<Course> OngoingCourses { get; set; }
        public List<AssignmentListViewModel> Assignments { get; set; }
    }
}
