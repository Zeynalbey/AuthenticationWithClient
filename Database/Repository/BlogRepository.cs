using AuthenticationWithClie.ApplicationLogic;
using AuthenticationWithClie.ApplicationLogic.Validations;
using AuthenticationWithClie.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Repository
{
    public class BlogRepository
    {
        public static List<Blog> Blogs { get; set; } = new List<Blog>();

        public void AddBlog()
        {
            BlogValidations blogValidations = new BlogValidations();
            Console.Write("Please enter your blog title : ");
            string title = Console.ReadLine();

            Console.Write("Please enter your blog content : ");
            string content = Console.ReadLine();

            Random random = new Random();
            int num = random.Next(10000, 99999);
            string blogCode = $"BL{num}";

            if (blogValidations.IsValidTitle(title) && blogValidations.IsValidContent(content))
            {
                Blog blog = new Blog(Authentication.Account, content, blogCode, title);

                Blogs.Add(blog);
                Console.WriteLine("Blog added successfully! ");
            }
            else
            {
                Console.WriteLine("Title or content is not correct! ");
            }
        }

    }
}
