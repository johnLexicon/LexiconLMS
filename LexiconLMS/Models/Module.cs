using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }


        public ICollection<Activityy> Activities { get; set; }

        public ICollection<ModuleDocument> Documents { get; set; }
    }
}
