using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class GenericDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] DocumentData { get; set; }
        public DateTime UploadTime { get; set; }
        public string UserId { get; set; }
    }
}
