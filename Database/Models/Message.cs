using AuthenticationWithClie.Database.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Models
{
    public class Message
    {
        public string BlogCode { get; set; }
        public InboxEnum BlogStatus { get; set; }
        public List<Comment> CommentList { get; set; } = new List<Comment>();
    }
}
