using AuthenticationWithClie.Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Models
{
    public class Comment
    {

        public int RowNumber { get; set; }
        public static int RowCounter { get; set; } = 1;
        public string Content { get; set; }
        public DateTime CommentDateTime { get; set; }

        public User Owner { get; set; }

        public Comment(User owner, string content)
        {
            Owner = owner;
            Content = content;
            CommentDateTime = DateTime.Now;
            RowNumber = RowCounter++;
        }

    }
}
