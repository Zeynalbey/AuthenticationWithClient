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

        public void ShowBlogs()
        {
            foreach (Blog blog in Blogs)
            {
                Console.WriteLine($"{blog.Id}. Owner: {blog.Owner.FirstName}, Content: {blog.Content}, Date: {blog.BlogDateTime}, Blog status: {blog.blogStatus}.");
            }
        }

        public Blog GetBlogbyId(int id)
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
        public void DeleteBlogs()
        {
            Console.Write("Which blog do you want delete, write id : ");
            int id = int.Parse(Console.ReadLine());
            foreach (Blog blog in Blogs)
            {
                if (blog.Id == id)
                {
                    Blogs.Remove(blog);
                    Console.WriteLine("Blog deleted. ");
                }
                else
                {
                    Console.WriteLine("This blog not found. ");
                }
            }

        }
    }
}
