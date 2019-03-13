using System;
using System.ComponentModel.DataAnnotations;
using LexiconLMS.Models;

namespace LexiconLMS.ViewModels
{
    public class ActivityAddViewModel: IDateInterval, IParentDateInterval
    {

        [Required]
        [DataType(DataType.Date)]
        public DateTime ParentStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ParentEndDate { get; set; }

        public int Id { get; set; }

        [Required]
        public int ModuleId { get; set; }

        
        [Display(Name ="Module name")]
        public string ModuleName { get; set; }

        //[Required]
        //[Display(Name = "Activity name")]
        //public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [StartDateNotEarlierThanParentStartDate(ErrorMessage = "Activity Start Date {0} must be later or equal to Module Start Date {1}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [EndDateLaterThanStartDate]
        [EndDateNotLaterThanParentEndDate(ErrorMessage = "Activity End Date {0} must be earlier or equal to Module End Date {1}")]
        public DateTime EndDate { get; set; }

        //[Required]
        public ActivityType ActivityType { get; set; }

        public int ActivityTypeId { get; set; }

       

        //[Required]
        public Module Module { get; set; }
        public Course Course { get; set; }


    }
}