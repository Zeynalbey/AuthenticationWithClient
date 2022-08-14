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
    public partial class BlogService
    {
        public void ShowOwnBlogs()
        {
            bool isThereBlog = true;
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
                        isThereBlog = false;
                    }
                }
                if (isThereBlog)
                {
                    Console.WriteLine("You have not any blogs. ");
                }
            }
        }

        public void ShowAuditingBlogs()
        {
            bool isBlog = true;
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
                        isBlog = false;
                    }
                }
                if (isBlog)
                {
                    Console.WriteLine("There are not any auditing blogs");
                }
            }
        }

        public void DeleteOwnBlog()
        {
            bool delete = true;
            Console.Write("Enter blogcode:");
            string code = Console.ReadLine();

            if (BlogRepository.Blogs.Count == 0)
            {
                Console.WriteLine("There are not any blogs.");
            }
            else
            {
                for (int i = 0; i < BlogRepository.Blogs.Count; i++)
                {
                    if (code == BlogRepository.Blogs[i].BlogCode && Authentication.Account == BlogRepository.Blogs[i].Owner)
                    {
                        BlogRepository.Blogs.Remove(BlogRepository.Blogs[i]);
                        Console.WriteLine("Blog deleted.");
                        delete = false;
                    }
                }
                if (delete)
                {
                    Console.WriteLine("The code is wrong or you have not any blogs.");
                }
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
                PartOFShowBlogWithComments();
            }
        }

        public void Approve()
        {
            Console.Write("Please enter blogcode : ");
            string blogCode = Console.ReadLine();
            bool approve = true;

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (blogCode == blog.BlogCode)
                {
                    blog.blogStatus = BlogStatus.Approved;
                    Message message = new Message(new BlogMessage
                    {
                        BlogCode = blog.BlogCode,
                        Status = Inbox.Approve
                    });
                    blog.Owner.Inbox.Add(message);
                    Console.WriteLine("Status approved. ");
                    approve = false;
                    break;
                }
            }
            if (approve)
            {
                Console.WriteLine("The code is wrong or not any pending blogs.");
            }
        }

        public void Reject()
        {
            Console.Write("Please enter blogcode : ");
            string blogCode = Console.ReadLine();
            bool reject = true;

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (blogCode == blog.BlogCode)
                {
                    blog.blogStatus = BlogStatus.Rejected;
                    Message message = new Message(new BlogMessage
                    {
                        BlogCode = blog.BlogCode,
                        Status = Inbox.Reject
                    });

                    blog.Owner.Inbox.Add(message);
                    Console.WriteLine("Status rejected. ");
                    reject = false;
                    break;
                }
            }
            if (reject)
            {
                Console.WriteLine("The code is wrong or not any pending blogs.");
            }
        }

        public void ShowInbox()
        {
            Console.WriteLine();
            if (Authentication.Account.Inbox.Count == 0)
            {
                Console.WriteLine("There are not any message yet.");
            }
            else
            {
                foreach (Message inbox in Authentication.Account.Inbox)
                {
                    Console.WriteLine(inbox.Result);
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
                bool IsApprove = true;

                foreach (Blog blog in BlogRepository.Blogs)
                {

                    BlogValidations blogValidations = new BlogValidations();

                    if (blogCode == blog.BlogCode & blog.blogStatus == BlogStatus.Approved & blogValidations.IsValidContent(content))
                    {
                        Comment comment = new Comment(Authentication.Account, content);
                        Console.WriteLine("Comment added.");

                        Message message = new Message(new CommentMessage
                        {
                            BlogCode = blog.BlogCode,
                            FirstName = Authentication.Account.FirstName,
                            LastName = Authentication.Account.LastName,
                        });

                        blog.Owner.Inbox.Add(message);

                        blog.Comments.Add(comment);
                        IsApprove = false;

                    }
                    else if (blog.blogStatus == BlogStatus.Pending)
                    {
                        Console.WriteLine("Blog not approved yet.");
                        IsApprove = false;
                        break;
                    }

                }
                if (IsApprove)
                {
                    Console.WriteLine("This blog is not in system. ");
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
                Console.WriteLine();
                string command = Console.ReadLine();
                Console.WriteLine();

                if (command == "title")
                {
                    BlogFilteredByTitle();
                }
                else if (command == "firstname")
                {
                    BlogFilteredByFirstname();
                }
                else
                {
                    Console.WriteLine("Command not found");
                }
            }
        }

        public void FindBlogWithCode()
        {
            bool code = true;
            if (BlogRepository.Blogs.Count == 0)
            {
                Console.WriteLine("There are not any blogs yet.");
            }
            else
            {
                Console.Write("Enter blogcode, which you want to search: ");
                Console.WriteLine();
                string blogCode = Console.ReadLine();
                foreach (Blog blog in BlogRepository.Blogs)
                {
                    if (blogCode == blog.BlogCode & blog.blogStatus == BlogStatus.Approved)
                    {
                        Console.WriteLine();
                        BlogDetails(blog);
                        code = false;
                    }
                }
                if (code)
                {
                    Console.WriteLine("The code is wrong or dont approved.");
                }
            }
        }
    }
    public partial class BlogService
    {
        private void BlogFilteredByTitle()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            bool isTitle = true;

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (title == blog.Title & blog.blogStatus == BlogStatus.Approved)
                {
                    BlogDetails(blog);
                    isTitle = false;
                }
            }
            if (isTitle)
            {
                Console.WriteLine("Title is not correct, this blog is not approved");
            }
        }

        private void BlogFilteredByFirstname()
        {
            Console.Write("Enter firstname:  ");
            string firstName = Console.ReadLine();
            bool isComment = true;

            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (firstName == blog.Owner.FirstName & blog.blogStatus == BlogStatus.Approved)
                {
                    BlogDetails(blog);
                    isComment = false;
                }
            }
            if (isComment)
            {
                Console.WriteLine("This blog is not approved.");
            }
        }

        private void PartOFShowBlogWithComments()
        {
            bool IsApproved = true;
            foreach (Blog blog in BlogRepository.Blogs)
            {
                if (blog.blogStatus == BlogStatus.Approved)
                {
                    Console.WriteLine();
                    BlogDetails(blog);
                    IsApproved = false;
                }
            }
            if (IsApproved)
            {
                Console.WriteLine("There are not any approved blogs.");
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


