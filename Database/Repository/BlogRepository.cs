using AuthenticationWithClie.ApplicationLogic;
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

        public Blog AddBlog(User owner, string content)
        {
            Blog blog = new Blog(owner, content);
            Blogs.Add(blog);
            return blog;
        }

        public static void ShowBlogs()
        {
            foreach (Blog blog in Blogs)
            {
                Console.WriteLine($"{blog.Id}. Owner: {Authentication.Account.FirstName}, Content: {blog.Content}, Date: {blog.BlogDateTime}, Blog status: {blog.blogStatus}.");
            }
        }

        public static Blog GetBlogbyId(int id)
        {
            foreach (Blog blog in Blogs)
            {
                if (blog.Id == 1)
                {
                    return blog;
                }

            }
            return null;
        }
    }
}
