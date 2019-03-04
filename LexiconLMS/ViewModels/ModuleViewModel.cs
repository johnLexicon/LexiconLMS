using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.ViewModels
{
    public class ModuleViewModel
    {
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string StartDateDisplay { get { return StartDate.ToShortDateString(); }  }
        public string EndDateDisplay { get { return EndDate.ToShortDateString(); } }

        public int DocumentId { get; set; }
        public int CourseId { get; set; }

        public ModuleViewModel(Module model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
        }
    }
}
