using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class Course
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public User Teacher { get; set; }

        public ICollection<User> Users { get; set; } //danger!!!!

        public ICollection<CourseDocument> Documents { get; set; }
    }
}
