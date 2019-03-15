using AutoMapper;
using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.ViewModels
{
    public class ActivityDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Display(Name = "Activity name")]
        public string ModuleName { get; set; }

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
        public ActivityType ActivityType { get; set; }

        public int ActivityTypeId { get; set; }

        //[Required]
        public Module Module { get; set; }
        public Course Course { get; set; }

        //[IgnoreMap]
        public List<DocumentListViewModel> Documents { get; set; }
    }
}
