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
    public partial class DashboardService : Repository<User, int>
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


        public void AddBlog()
        {
            Console.Write("Please enter your blog title : ");
            string title = Console.ReadLine();
            Console.Write("Please enter your blog content : ");
            string content = Console.ReadLine();

            if (Validation.IsLengthBetween(title,5,35) && Validation.IsLengthBetween(content,5,100))
            {
                Random random = new Random();
                int num = random.Next(10000, 99999);
                string blogCode = $"BL{num}";
                
                Blog blog = new Blog(Authentication.Account,content, blogCode, title);

                BlogRepository.Blogs.Add(blog);
                Console.WriteLine("Blog added successfully. ");

                
            }
            
        }

        public void Metod()
        {
            blogRepository.ShowBlogs();
            Console.Write("Which id do you want to Approve or Reject : ");
            while (true)
            {
                try
                {
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Choose Approve ol Reject : ");
                    Blog blog = blogRepository.GetBlogbyId(id);
                    string command1 = Console.ReadLine();

                    if (command1 == BlogStatus.Pending.ToString())
                    {
                        blog.blogStatus = BlogStatus.Approved;
                        Console.WriteLine("Status approved. ");
                        break;
                    }
                    else if (command1 == BlogStatus.Rejected.ToString())
                    {
                        blog.blogStatus = BlogStatus.Rejected;
                        Console.WriteLine("Status rejected. ");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong command. ");
                    }
                }
                catch
                {
                    Console.WriteLine("Write only numbers for id. ");
                }
            }
        }
        public void ShowOwnBlogs()
        {
            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (Authentication.Account == blog.Owner)
                {
                    Console.WriteLine($"{blog.Id}.{blog.Owner.FirstName} {blog.BlogDateTime} {blog.BlogCode} {blog.Title} {blog.Content} {blog.blogStatus}.");
                }

            }
        }

        public void ShowAuditingBlogs()
        {
            foreach (Blog blog in BlogRepository.Blogs )
            {
                if ( blog.blogStatus == BlogStatus.Pending)
                {
                    Console.WriteLine($"{blog.BlogDateTime} {blog.BlogCode} {blog.blogStatus} {blog.Owner.FirstName} {blog.Owner.LastName}");
                    Console.WriteLine($"==={blog.Title}===");
                    Console.WriteLine(blog.Content);
                }
            }
        }








    }

      
    
}
