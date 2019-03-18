using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.ViewModels
{
    public class StudentCourseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Course name")]
        public string Name { get; set; }

        [Display(Name = "Teacher name")]
        public string TeacherName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Teacher email")]
        public string TeacherEmail { get; set; }

        public List<ModuleDetailsViewModel> Modules { get; set; }

        public List<User> Students { get; set; }

        //aaaaand documents
        public ICollection<DocumentListViewModel> Documents { get; set; }

        public ICollection<AssignmentListViewModel> DueAssignments { get; set; }

        public ICollection<AssignmentListViewModel> MyAssignments { get; set; }
    }
}
