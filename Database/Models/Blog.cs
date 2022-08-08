using AuthenticationWithClie.Database.Common;
using AuthenticationWithClie.Database.Models.Enums;
using AuthenticationWithClie.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public static int IdCounter { get; set; } = 1;
        public User Owner { get; set; }
        public string Content { get; set; }
        public  DateTime BlogDateTime { get; set; }
        public BlogStatus blogStatus { get; set; }

        public Blog(User owner, string content)
        {
            Id = IdCounter++;
            Owner = owner;
            Content = content;
            BlogDateTime = DateTime.Now;
            blogStatus = BlogStatus.Pending;

            
        }


    }
}
