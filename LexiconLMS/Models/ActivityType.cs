using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class ActivityType
    {
        public int Id { get; set; }

        [Display(Name ="Activity Type")]
        public string Type { get; set; }

        //nav collection
        //public ICollection<Activityy> Activities { get; set; }

    }
}