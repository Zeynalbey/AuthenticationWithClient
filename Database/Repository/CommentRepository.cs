using AuthenticationWithClie.ApplicationLogic;
using AuthenticationWithClie.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Repository
{
    public class CommentRepository : Repository<User, int>
    {
        public static List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
