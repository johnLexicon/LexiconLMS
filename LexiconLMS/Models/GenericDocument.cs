using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public abstract class GenericDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] DocumentData { get; set; }
        [DataType(DataType.Date)]
        public DateTime UploadTime { get; set; }
        public string UserId { get; set; }
    }
}
