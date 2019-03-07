using System;
using System.ComponentModel.DataAnnotations;
using LexiconLMS.Models;

namespace LexiconLMS.ViewModels
{
    public class ActivityViewModel: IDateInterval
    {

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
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [EndDateLaterThanStartDate]
        public DateTime EndDate { get; set; }

        //[Required]
        //public ActivityType ActivityType { get; set; }

        public int ActivityTypeId { get; set; }

        public string ActivityTypeType { get; set; }

        //[Required]
        public Module Module { get; set; }
        public Course Course { get; set; }


    }
}