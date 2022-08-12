using AuthenticationWithClie.Database.Models;
using AuthenticationWithClie.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationWithClie.ApplicationLogic.Validations;
using AuthenticationWithClie.Database.Models.Enums;
using AuthenticationWithClie.ApplicationLogic.Validations.Services;
using AuthenticationWithClie.UI;

namespace AuthenticationWithClie.ApplicationLogic
{
    public partial class Dashboard : Repository<User, int>
    {
        public static void AdminPanel()
        {
            BlogService blogService = new BlogService();
            DashboardService dashboardService = new DashboardService();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"COMMANDS : /show-users  /show-admins  /show-auditing-blogs  /approve-blog  /reject-blog  /logout");

                Console.WriteLine();
                Console.Write("Enter command: ");
                string command = Console.ReadLine();
                Console.WriteLine();

                if (command == "/show-users")
                {
                    dashboardService.ShowUsers();
                }

                else if (command == "/show-admins")
                {
                    dashboardService.ShowAdmins();
                }

                else if (command == "/show-auditing-blogs")
                {
                    blogService.ShowAuditingBlogs();
                }

                else if (command == "/approve-blog")
                {
                    blogService.Approve();
                }

                else if (command == "/reject-blog")
                {
                    blogService.Reject();
                }

                else if (command == "/logout")
                {
                    Authentication.Account = null;
                    Authentication.IsAuthorized = false;
                    break;
                }

                else
                {
                    Console.WriteLine("Command not found");
                }
                Console.WriteLine();
            }
        }
    }
    public partial class Dashboard
    {
        public static void UserPanel()
        {
            BlogRepository blogRepository = new BlogRepository();
            BlogService blogService = new BlogService();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"/inbox  /add-comment  /blogs  /add-blog  /delete-blog  /logout");
                Console.Write("Enter command : ");
                string command = Console.ReadLine();

                if (command == "/inbox")
                {
                    blogService.ShowInbox();
                }
                else if (command == "/add-comment")
                {
                    blogService.AddComment();
                }
                else if (command == "/blogs")
                {
                    blogService.ShowOwnBlogs();
                }
                else if (command == "/add-blog")
                {
                    blogRepository.AddBlog();
                }
                else if (command == "/delete-blog")
                {
                    blogService.DeleteOwnBlog();
                }
                else if (command == "/logout")
                {
                    Authentication.Account = null;
                    Authentication.IsAuthorized = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Command not found");
                }
                Console.WriteLine();
            }
        }
    }
}