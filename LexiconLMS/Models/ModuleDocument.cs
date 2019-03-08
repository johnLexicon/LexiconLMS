using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class ModuleDocument : GenericDocument
    {
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}
