using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.ViewModels
{
    public class ModuleAddViewModel : IDateInterval
    {
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Module name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
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

        public ICollection<ActivityAddViewModel> Activities{ get; set; }
    }
}
