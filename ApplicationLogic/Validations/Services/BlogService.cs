using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationWithClie.Database.Repository;
using AuthenticationWithClie.Database.Models;
using AuthenticationWithClie.Database.Models.Enums;

namespace AuthenticationWithClie.ApplicationLogic.Validations.Services
{
    public class BlogService
    {

        public static List<Comment> Comments { get; set; } = new List<Comment>();

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
            foreach (Blog blog in BlogRepository.Blogs)
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
            var NewList = from blog in BlogRepository.Blogs
                          where Authentication.Account != blog.Owner
                          select blog;
            BlogRepository.Blogs = NewList.ToList();
            /*internetde arasdirdim, yazdim*/
        }

        public void ShowBlogWithComments()
        {
            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (blog.blogStatus==BlogStatus.Approved)
                {
                    Console.WriteLine($"{ blog.BlogDateTime} {blog.BlogCode} {blog.Owner.FirstName} {blog.Owner.LastName}");
                    Console.WriteLine(blog.Title);
                    Console.WriteLine(blog.Content);
                    Console.WriteLine();

                    foreach (Comment comment in blog.Comments)
                    {
                    
                        Console.WriteLine($"{comment.RowNumber}. {comment.CommentDateTime} {comment.Owner.FirstName} {comment.Owner.LastName} - {comment.Content}.");
                    }
   
                }
            }
        }

        public void Approve()
        {
            Console.Write("Please enter blogcode : ");
            string blogCode = Console.ReadLine();

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (blogCode == blog.BlogCode)
                {
                    blog.blogStatus = BlogStatus.Approved;
                    Message message = new Message();
                    message.BlogCode = blog.BlogCode;
                    message.BlogStatus = InboxEnum.Approve;
                    blog.Owner.Inbox.Add(message);
                    Console.WriteLine("Status approved. ");
                    break;
                }
            }
        }

        public void Reject()
        {
            Console.Write("Please enter blogcode : ");
            string blogCode = Console.ReadLine();

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (blogCode == blog.BlogCode)
                {
                    blog.blogStatus = BlogStatus.Rejected;
                    Message message = new Message();
                    message.BlogCode = blog.BlogCode;
                    message.BlogStatus = InboxEnum.Approve;
                    blog.Owner.Inbox.Add(message);
                    Console.WriteLine("Status rejected. ");
                    break;
                }
            }

        }

        public void AddCommit()
        {
            Console.Write("Please enter your blogcode : ");
            string blogCode = Console.ReadLine();

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (blogCode == blog.BlogCode)
                {
                    string content = Console.ReadLine();

                    Comment comment = new Comment(Authentication.Account, content);
                 
                  Message message = new Message();
                    message.BlogCode = blog.BlogCode;
                    message.BlogStatus = InboxEnum.Approve;

                    message.CommentList.Add(comment);
                    blog.Owner.Inbox.Add(message);

                    blog.Comments.Add(comment);
                    blog.Owner.Comments.Add(comment);


                }
            }
        }

        public void ShowFilteredBlogs()
        {
            Console.WriteLine("With which method do you want search blog: ");
            Console.WriteLine("a) Title");
            Console.WriteLine("b) Firstname");
            string command = Console.ReadLine();
            if (command == "Title")
            {
                ShowBlogWithCommentsByTitle();
            }
            else if (command == "Firstname")
            {
                ShowFilteredBlogWithCommentsByFirstname();
            }
            else
            {
                Console.WriteLine("Command not found");
            }
        }
        public void ShowBlogWithCommentsByTitle()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (title == blog.Title)
                {
                    Console.WriteLine($"{ blog.BlogDateTime} {blog.BlogCode} {blog.Owner.FirstName} {blog.Owner.LastName}");
                    Console.WriteLine(blog.Title);
                    Console.WriteLine(blog.Content);
                    Console.WriteLine();

                    foreach (Comment comment in blog.Comments)
                    {

                        Console.WriteLine($"{comment.RowNumber}. {comment.CommentDateTime} {comment.Owner.FirstName} {comment.Owner.LastName} - {comment.Content}.");
                    }
                }
            }
        }
        public void ShowFilteredBlogWithCommentsByFirstname()
        {
            Console.Write("Enter firstname:  ");
            string firstName = Console.ReadLine();

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (firstName == blog.Owner.FirstName)
                {
                    Console.WriteLine($"{ blog.BlogDateTime} {blog.BlogCode} {blog.Owner.FirstName} {blog.Owner.LastName}");
                    Console.WriteLine(blog.Title);
                    Console.WriteLine(blog.Content);
                    Console.WriteLine();

                    foreach (Comment comment in blog.Comments)
                    {

                        Console.WriteLine($"{comment.RowNumber}. {comment.CommentDateTime} {comment.Owner.FirstName} {comment.Owner.LastName} - {comment.Content}.");
                    }
                }
            }
        }

    }
}
