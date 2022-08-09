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

                else if (command == "/show-admins" )
                {
                    dashboardService.ShowAdmins();
                }

                else if (command == "/show-auditing-blogs")
                {
                    dashboardService.ShowAuditingBlogs();
                }

                else if (command == "/approve-blog")
                {

                }
                else if (command == "/reject-blog")
                {

                }

                else if (command == "/logout")
                {

                    Authentication.IsAuthorized = false;
                  
                    break;
                }
                else
                {
                    Console.WriteLine("Command not found");
                }
                Console.WriteLine();
                Console.WriteLine("If you want return main menu, enter:  /log-out");
                Console.WriteLine();
            }
        }
    }
    public partial class Dashboard
    {
        public static void UserPanel()
        {
            DashboardService dashboardService = new DashboardService();

            while (true)
            {
                Console.WriteLine($"/inbox  /add-comment  /blogs  /add-blog  /delete-blog  /logout");
                Console.Write("Enter command : ");
                string command = Console.ReadLine();

                if (command == "/inbox")
                {

                }
                else if (command == "/add-comment")
                {

                }
                else if (command == "/blogs")
                {
                    dashboardService.ShowOwnBlogs();
                }
                else if (command == "/add-blog")
                {
                    dashboardService.AddBlog();
                }

                else if (command == "/delete-blog")
                {

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