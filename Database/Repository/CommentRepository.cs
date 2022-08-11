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

        public void AddComment()
        {


            Console.Write("Please enter your comment content : ");
            string content = Console.ReadLine();

            Comment comment = new Comment(Authentication.Account,content);
            Comments.Add(comment);

        }
    }
}
