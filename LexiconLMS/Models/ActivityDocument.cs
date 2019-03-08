using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class ActivityDocument : GenericDocument
    {
        public int ActivityId { get; set; }
        public Activityy Activityy { get; set; }
    }
}
