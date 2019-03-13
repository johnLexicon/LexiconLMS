using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class UserDocument : GenericDocument
    {
        public string AssigneeUserId { get; set; }
        public User AssigneeUser { get; set; }
        public new string UserId { get; set; }
    }
}
