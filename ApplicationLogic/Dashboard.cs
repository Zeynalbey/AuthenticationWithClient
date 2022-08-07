using AuthenticationWithClie.Database.Models;
using AuthenticationWithClie.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationWithClie.ApplicationLogic.Validations;
using AuthenticationWithClie.Database.Models.Enums;

namespace AuthenticationWithClie.ApplicationLogic
{
    public partial class Dashboard : Repository<User, int>
    {
        public static void AdminPanel()
        {
            UserRepository userRepository = new UserRepository();
            while (true)
            {
                Console.WriteLine($"/add-user   /update-user   /reports   /all-reports   /add-admin   " +
                    $"/show-admins   /update-admin   /remove-admin   /blog-status   /show-users  /show-blogs   /logout");

                Console.WriteLine();
                Console.Write("enter command: ");
                string command = Console.ReadLine();
                Console.WriteLine();
                if (command == "/add-user")
                {
                    Authentication.Register();
                }
                else if (command == "/update-user")
                {
                    Console.WriteLine("Write user's email which you want change.");
                    string email = Console.ReadLine();
                    User user1 = UserRepository.GetUserByEmail(email);
                    if (!(user1 == null && user1 is Admin))
                    {
                        Console.Write("Write new name:");
                        string user1Name = Console.ReadLine();
                        Console.Write("Write new lastname:");
                        string user1LastName = Console.ReadLine();
                        user1.FirstName = user1Name;
                        user1.LastName = user1LastName;
                        Console.WriteLine($"{user1.FirstName} {user1.LastName} changed to {user1Name} {user1LastName}");
                    }
                    else
                    {
                        Console.WriteLine("You can't change this email.");
                    }
                }
                else if (command == "/remove-user")
                {
                    Console.Write("Enter user's email which yo want delete:");
                    string removeEmail = Console.ReadLine();
                    User user1 = UserRepository.GetUserByEmail(removeEmail);
                    if (!(user1 is null && user1 is Admin))
                    {

                        userRepository.Delete(user1);
                        Console.WriteLine($"{user1.GetInfo()} deleted");
                    }
                    else
                    {
                        Console.WriteLine("You can't delete this user, because this email is not in system or it is admin's email.");
                    }

                }
                else if (command == "/reports")
                {
                    for (int i = 0; i < Authentication.Account.Reportinbox.Count; i++)
                    {
                        Report report = Authentication.Account.Reportinbox[i];
                        Console.WriteLine($"{i + 1}. (report ID : {report.Id}) User ({report.Sender.Email}) report {report.Target.Email} Date : {report.Sent}\n{report.Text}");
                    }
                }
                else if (command == "/all-reports")
                {
                    for (int i = 0; i < UserRepository.Reports.Count; i++)
                    {
                        Report report = UserRepository.Reports[i];
                        Console.WriteLine($"{i + 1}. (report ID : {report.Id}) User ({report.Sender.Email}) report {report.Target.Email} Date : {report.Sent}\n{report.Text}");
                    }
                }
                else if (command == "/add-admin")
                {
                    Console.Write("Enter user's email which you want create new admin:");
                    string email = Console.ReadLine();
                    User user = UserRepository.GetUserByEmail(email);
                    if (!(user is null && user is Admin))
                    {
                        userRepository.Delete(user);
                        Admin admin = new Admin(user.FirstName, user.LastName, user.Email, user.Password, user.Id);
                        UserRepository.Add(admin);
                    }
                    else
                    {
                        Console.WriteLine("Enter email correctly.");
                    }

                }
                else if (command == "/show-admins")
                {
                    UserRepository.ShowAdmins();
                }
                else if (command == "/update-admin")
                {
                    UserRepository.UpdateAdmin();
                }
                else if (command == "/remove-admin")
                {
                    Console.Write("Enter admin's email which you want delete:");
                    string emailDeleteAdmin = Console.ReadLine();
                    User user = UserRepository.GetUserByEmail(emailDeleteAdmin);
                    if (user is Admin)
                    {
                        userRepository.Delete(user);
                        Console.WriteLine($"{user.FirstName} {user.LastName} deleted.");
                    }
                }
                else if (command == "/blog-status")
                {

                }
                else if (command == "/show-users")
                {
                    UserRepository.ShowUsers();
                }
                else if (command == "/show-blogs")
                {
                    UserRepository.ShowBlogs();
                    Console.WriteLine("tesdiq ya inkar etmek istediyin blogun id-ni yaz");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Choose Approve ol Reject");
                    Blog blog1 = UserRepository.GetBlogbyId(id);
                    string command1 = Console.ReadLine();

                    if (command1 == "Approved")
                    {
                        blog1.blogStatus = BlogStatus.Approved;
                        Console.WriteLine("status approved");
                    }
                    else if (command1 == "Rejected")
                    {
                        blog1.blogStatus = BlogStatus.Rejected;
                    }
                    else
                    {
                        Console.WriteLine("not command.");
                    }
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
                Console.WriteLine("if you want return main menu, enter:  /log-out");
            }
        }
    }
    public partial class Dashboard
    {
        public static void UserPanel()
        {
            UserRepository userRepository1 = new UserRepository();
            while (true)
            {
                Console.WriteLine("/update-info" + "  " + "/show-users" + "  " + "/report-user" + "  " + "/report" + "  " + "/write-blog" + "  " + "/show-blogs" + "  " + "logout");
                Console.WriteLine("Enter command");
                string command = Console.ReadLine();
                if (command == "/update-info")
                {
                    Console.Write("Please enter your password : ");
                    string password = Console.ReadLine();
                    if (Authentication.Account.Password == password)
                    {
                        Console.Write("Write new name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Write new lastname: ");
                        string newLastName = Console.ReadLine();
                        Authentication.Account.FirstName = newName;
                        Authentication.Account.LastName = newLastName;
                        Console.WriteLine($"Name and lastname changed to: {newName} {newLastName}.");
                    }
                    else
                    {
                        Console.WriteLine("Enter email correctly.");
                    }
                }
                else if (command == "/show-users")
                {
                    UserRepository.ShowUsers();
                }
                else if (command == "/report-user")
                {
                    Console.Write("Please enter target's email : ");
                    string email = Console.ReadLine();
                    Console.Write("Please enter reason of report : ");
                    string reason = Console.ReadLine();
                    UserRepository userRepository = new UserRepository();

                    if (email != Authentication.Account.Email && Validation.IsLengthBetween(reason, 10, 50) && userRepository.IsUserExistsByEmail(email))
                    {
                        User target = UserRepository.GetUserByEmail(email);
                        UserRepository.AddReport(Authentication.Account, reason, target);
                        Console.WriteLine("User Reported");
                    }
                    else
                    {
                        Console.WriteLine("Rules : \n1. A User cannot report their own account \n2. The email entered must be valid \n3. The reason's length entered must be higher than 10 and less than 30 ");
                    }
                }
                else if (command == "/reports")
                {
                    for (int i = 0; i < Authentication.Account.Reportinbox.Count; i++)
                    {
                        Report report = Authentication.Account.Reportinbox[i];
                        Console.WriteLine($"{i + 1}. (report ID : {report.Id}) User ({report.Sender.Email}) report {report.Target.Email} Date : {report.Sent}\n{report.Text}");
                    }
                }
                else if (command == "/logout")
                {
                    Authentication.IsAuthorized = false;
                    break;
                }
                else if(command == "/write-blog")
                {
                    Console.Write("Please enter your blog : ");
                    string content = Console.ReadLine();

                    userRepository1.AddBlog(Authentication.Account,content);
                    Console.WriteLine("blog addded");
                }
                else if (command == "/show-blogs")
                {
                    UserRepository.ShowBlogs();
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