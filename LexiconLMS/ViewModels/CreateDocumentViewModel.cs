using LexiconLMS.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.ViewModels
{
    public class CreateDocumentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name="Document")]
        public IFormFile file { get; set; }
        public DateTime UploadTime { get; set; }
        public User UploadedBy { get; set; }

        public int EnitityId { get; set; }
    }
}
