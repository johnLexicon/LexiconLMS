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
    public class ModuleDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Module Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Module starts")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Module ends")]
        [DataType(DataType.Date)]
        [EndDateLaterThanStartDate]
        public DateTime EndDate { get; set; }

        //TODO: make sure this doesn't break anything
        public ICollection<ActivityDetailsViewModel> Activities { get; set; }


        public ICollection<DocumentListViewModel> Documents { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime ParentStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ParentEndDate { get; set; }
    }
}
