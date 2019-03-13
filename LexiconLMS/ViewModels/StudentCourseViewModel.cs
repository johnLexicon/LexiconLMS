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

        public List<ModuleAddViewModel> Modules { get; set; }

        public List<List<User>> Students { get; set; }

        //Add Activities
        public List<ActivityAddViewModel> activities { get; set; }
    }
}
