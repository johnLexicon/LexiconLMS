using LexiconLMS.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.ViewModels
{
    public class AssignmentListViewModel
    {
        public int Id { get; set; }
        [Display(Name="File name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name="Submitted")]
        public DateTime UploadTime { get; set; }
        [Display(Name="Student")]
        public User Owner { get; set; }
        public string CourseName { get; set; }
        public string ModuleName { get; set; }
        public DateTime Deadline { get; set; }
    }
}