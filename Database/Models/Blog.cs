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
    public class Blog : Entity<int>
    {
        public int IdCounter = 1;
        public User Owner { get; set; }
        public string Content { get; set; }
        public  DateTime BlogDateTime { get; set; }
        public BlogStatus blogStatus { get; set; }

        public Blog(User owner, string content, int? id = null)
        {
            Owner = owner;
            Content = content;
            BlogDateTime = DateTime.Now;
            blogStatus = BlogStatus.Pending;
            Id = IdCounter++;
        }


    }
}
