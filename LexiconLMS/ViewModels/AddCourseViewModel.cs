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

        [Display(Name = "Course name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [EndDateLaterThanStartDate]
        public DateTime EndDate { get; set; }

        public string TeacherId { get; set; }

        //public IList<User> Teachers { get; set; }
        public List<Tuple<string, string>> Teachers { get; set; }

        [Display(Name = "Teachers")]
        public IEnumerable<SelectListItem> FormattedTeachers
        { get => Teachers.Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 }); }
    }
}
