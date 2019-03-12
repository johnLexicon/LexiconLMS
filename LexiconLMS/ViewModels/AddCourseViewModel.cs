using LexiconLMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.ViewModels
{
    public class AddCourseViewModel : IDateInterval
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Course name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [EndDateLaterThanStartDate]
        public DateTime EndDate { get; set; }

        //Teachers
        public string TeacherId { get; set; }

        public List<Tuple<string, string>> Teachers { get; set; }
        [Display(Name = "Teacher")]
        public IEnumerable<SelectListItem> FormatedTeachers
        { get => Teachers.Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 }); }

        ////Students
        //public IEnumerable<string> StudentIds { get; set; }

        //public List<Tuple<string, string>> Students { get; set; }
        //[Display(Name = "Students")]
        //public IEnumerable<SelectListItem> FormatedStudents
        //{ get => Students.Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 }); }
    }
}
