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
        public string Result { get; set; }

        public Message(BlogMessage blog)
        {
            if (blog.Status == Inbox.Approve)
            {
                Result = $"{blog.BlogCode} blog is approved by admin.";
            }
            else
            {
                Result = $"{blog.BlogCode} blog is rejected by admin. ";
            }
        }

        public Message(CommentMessage comment)
        {
            Result = $"{comment.FirstName} {comment.LastName} added comment to your {comment.BlogCode} blog. ";
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


