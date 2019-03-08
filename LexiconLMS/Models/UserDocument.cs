using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class UserDocument : GenericDocument
    {
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
