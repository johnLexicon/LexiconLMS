using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class CourseDocument : GenericDocument
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
