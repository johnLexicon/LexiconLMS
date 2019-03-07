using System;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class Activityy
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public String Description { get; set; }

        //fk
        public int ActivityTypeId { get; set; }
        public int ModuleId{ get; set; }

        //nav ref
        public ActivityType ActivityType{ get; set; }
        public Module Module { get; set; }


    }
}
