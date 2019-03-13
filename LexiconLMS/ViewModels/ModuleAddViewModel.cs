using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.ViewModels
{
    public class ModuleAddViewModel : IDateInterval, IParentDateInterval
    {

        [Required]
        [DataType(DataType.Date)]
        public DateTime ParentStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ParentEndDate { get; set; }




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
        [StartDateNotEarlierThanParentStartDate(ErrorMessage = "Module Start Date {0} must be later or equal to Course Start Date {1}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [EndDateLaterThanStartDate]
        [EndDateNotLaterThanParentEndDate(ErrorMessage = "Module End Date {0} must be earlier or equal to Course End Date {1}")]
        public DateTime EndDate { get; set; }

        public ICollection<ActivityAddViewModel> Activities{ get; set; }
    }
}
