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
        public void ShowOwnBlogs()
        {
            if (BlogRepository.Blogs.Count == 0)
            {
                Console.WriteLine("There are not any blogs yet!");
            }
            else
            {
                foreach (Blog blog in BlogRepository.Blogs)
                {
                    if (Authentication.Account == blog.Owner)
                    {
                        Console.WriteLine($"{blog.Id}.{blog.Owner.FirstName} {blog.BlogDateTime} {blog.BlogCode} {blog.Title} {blog.Content} {blog.blogStatus}.");
                    }
                }
            }
        }

        public void ShowAuditingBlogs()
        {
            if (BlogRepository.Blogs.Count == 0)
            {
                Console.WriteLine("There are not any blogs yet!");
            }
            else
            {
                foreach (Blog blog in BlogRepository.Blogs)
                {
                    if (blog.blogStatus == BlogStatus.Pending)
                    {
                        Console.WriteLine($"[{blog.BlogDateTime}] [{blog.BlogCode}] [{blog.blogStatus}] [{blog.Owner.FirstName} {blog.Owner.LastName}]");
                        Console.WriteLine($"======{blog.Title}======");
                        Console.WriteLine(blog.Content);
                    }
                }
            }
        }

        public void DeleteOwnBlog()
        {
            if (BlogRepository.Blogs.Count == 0)
            {
                Console.WriteLine("There are not any blogs yet!");
            }
            else
            {
                var NewList = from blog in BlogRepository.Blogs
                              where Authentication.Account != blog.Owner
                              select blog;
                BlogRepository.Blogs = NewList.ToList();
                /*internetde arasdirdim, yazdim*/
            }
        }

        public void ShowBlogWithComments()
        {
            if (BlogRepository.Blogs.Count == 0)
            {
                Console.WriteLine("There are not any blogs yet.");
            }
            else
            {
                foreach (Blog blog in BlogRepository.Blogs)
                {
                    if (blog.blogStatus == BlogStatus.Approved)
                    {
                        BlogDetails(blog);

                    }
                    else
                    {
                        Console.WriteLine("There are not any approved blogs.");
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
                    message.BlogStatus = Inbox.Approve;
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
                if (blogCode == blog.BlogCode )
                {
                    blog.blogStatus = BlogStatus.Rejected;
                    Message message = new Message();
                    message.BlogCode = blog.BlogCode;
                    message.BlogStatus = Inbox.Approve;
                    blog.Owner.Inbox.Add(message);
                    Console.WriteLine("Status rejected. ");
                    break;
                }
            }
        }

        public void AddComment()
        {
            if (BlogRepository.Blogs.Count == 0)
            {
                Console.WriteLine("There are not any blogs yet.");
            }
            else
            {
                Console.Write("Please enter your blogcode : ");
                string blogCode = Console.ReadLine();
                Console.Write("Please enter your comment : ");
                string content = Console.ReadLine();

                foreach (Blog blog in BlogRepository.Blogs)
                {
                    BlogValidations blogValidations = new BlogValidations();

                    if (blogCode == blog.BlogCode & blog.blogStatus == BlogStatus.Approved & blogValidations.IsValidContent(content))
                    {
                        Comment comment = new Comment(Authentication.Account, content);
                        Console.WriteLine("Comment added.");

                        Message message = new Message();
                        message.BlogCode = blog.BlogCode;
                        message.BlogStatus = Inbox.Approve;

                        message.CommentList.Add(comment);
                        blog.Owner.Inbox.Add(message);

                        blog.Comments.Add(comment);
                        blog.Owner.Comments.Add(comment);
                    }
                }
            }
        }

        public void ShowFilteredBlogs()
        {
            if (BlogRepository.Blogs.Count == 0)
            {
                Console.WriteLine("There are not any blogs yet.");
            }
            else
            {
                Console.WriteLine("With which method do you want search blog: ");
                Console.WriteLine("a) title");
                Console.WriteLine("b) firstname");
                string command = Console.ReadLine();

                if (command == "title")
                {
                    ShowBlogWithCommentsByTitle();
                }
                else if (command == "firstname")
                {
                    ShowFilteredBlogWithCommentsByFirstname();
                }
                else
                {
                    Console.WriteLine("Command not found");
                }
            }
        }

        private void ShowBlogWithCommentsByTitle()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (title == blog.Title)
                {
                    BlogDetails(blog);
                }
            }
        }

        private void ShowFilteredBlogWithCommentsByFirstname()
        {
            Console.Write("Enter firstname:  ");
            string firstName = Console.ReadLine();

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (firstName == blog.Owner.FirstName)
                {
                    BlogDetails(blog);
                }
            }
        }

        public void FindBlogWithCode()
        {
            if (BlogRepository.Blogs.Count == 0)
            {
                Console.WriteLine("There are not any blogs yet.");
            }
            else
            {
                Console.Write("Enter blogcode, which you want to search: ");
                string blogCode = Console.ReadLine();
                foreach (Blog blog in BlogRepository.Blogs)
                {
                    if (blogCode == blog.BlogCode)
                    {
                        BlogDetails(blog);
                    }
                }
            }
        }

        private void BlogDetails(Blog blog)
        {
            Console.WriteLine($"[{blog.BlogDateTime}] [{blog.BlogCode}] [{blog.Owner.FirstName} {blog.Owner.LastName}]");
            Console.WriteLine($"======{blog.Title}======");
            Console.WriteLine($"{blog.Content}");
            Console.WriteLine();

            foreach (Comment comment in blog.Comments)
            {
                Console.WriteLine($"{comment.RowNumber}. [{comment.CommentDateTime}] [{comment.Owner.FirstName} {comment.Owner.LastName}] - {comment.Content}.");
            }
        }

    }
}
