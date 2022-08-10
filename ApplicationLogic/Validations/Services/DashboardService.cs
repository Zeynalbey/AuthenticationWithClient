using AuthenticationWithClie.Database.Models;
using AuthenticationWithClie.Database.Models.Enums;
using AuthenticationWithClie.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuthenticationWithClie.ApplicationLogic.Validations.Services
{
    public class DashboardService : Repository<User, int>
    {
        BlogRepository blogRepository = new BlogRepository();


        public void ShowUsers()
        {
            foreach (User user in DbContext)
            {
                if (user is not Admin)
                {
                    Console.WriteLine($"Id: {user.Id}, Name: {user.FirstName}, Lastname: {user.LastName}, Email: {user.Email}, Date: {user.CreatedAt}");
                }
            }
        }

        public void ShowAdmins()
        {
            foreach (User user in DbContext)
            {
                if (user is Admin)
                {
                    Console.WriteLine($"Id: {user.Id}, Name: {user.FirstName}, Lastname: {user.LastName}, Email: {user.Email}, Date: {user.CreatedAt}");
                }
            }
        }

        public void ShowOwnBlogs()
        {
            foreach (Blog blog in blogRepository.Blogs)
            {
                if (Authentication.Account == blog.Owner)
                {
                    Console.WriteLine($"{blog.Id}.{blog.Owner.FirstName} {blog.BlogDateTime} {blog.BlogCode} {blog.Title} {blog.Content} {blog.blogStatus}.");
                }
            }
        }

        public void ShowAuditingBlogs()
        {
            foreach (Blog blog in blogRepository.Blogs)
            {
                if (blog.blogStatus == BlogStatus.Pending)
                {
                    Console.WriteLine($"{blog.BlogDateTime} {blog.BlogCode} {blog.blogStatus} {blog.Owner.FirstName} {blog.Owner.LastName}");
                    Console.WriteLine($"==={blog.Title}===");
                    Console.WriteLine(blog.Content);
                }
            }
        }

        public void DeleteOwnBlog()
        {
            var NewList = from blog in blogRepository.Blogs
                          where Authentication.Account != blog.Owner
                          select blog;
            blogRepository.Blogs = NewList.ToList();
            /*internetde arasdirdim, yazdim*/
        }

        public void Approve()
        {
            Console.Write("Please enter blogcode : ");
            string blogCode = Console.ReadLine();

            foreach (Blog blog in blogRepository.Blogs)
            {
                if (blogCode == blog.BlogCode)
                {
                    blog.blogStatus = BlogStatus.Approved;
                    Console.WriteLine("Status approved. ");
                    break;
                }
            }
        }

        public void Reject()
        {
            Console.Write("Please enter blogcode : ");
            string blogCode = Console.ReadLine();

            foreach (Blog blog in blogRepository.Blogs)
            {
                if (blogCode == blog.BlogCode)
                {
                    blog.blogStatus = BlogStatus.Rejected;
                    Console.WriteLine("Status rejected. ");
                    break;
                }
            }

        }


    }



}
