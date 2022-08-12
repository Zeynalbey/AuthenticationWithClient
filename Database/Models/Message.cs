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
        public string Body { get; set; }

        public Message(BlogMessage blog)
        {
            Body = blog.Status == Inbox.Approve
                ? $"{blog.BlogCode} blog is approved by admin. "
                : $"{blog.BlogCode} blog is rejected by admin. ";
        }

        public Message(CommentMessage comment)
        {
            Body = $"{comment.FirstName} {comment.LastName} added comment to your {comment.BlogCode} blog. ";
        }
    }

    public class CommentMessage
    {
        public string BlogCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class BlogMessage
    {
        public string BlogCode { get; set; }
        public Inbox Status { get; set; }
    }
 }


