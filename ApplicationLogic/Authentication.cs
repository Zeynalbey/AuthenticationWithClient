using AuthenticationWithClie.ApplicationLogic.Validations;
using AuthenticationWithClie.Database.Repository;
using AuthenticationWithClie.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.ApplicationLogic
{
    class Authentication  : Repository<User, int>
    {
       
        public static User Account { get; set; }
        public static bool IsAuthorized { get; set; } = false;
        
        public static void Register()
        {
            UserRepository userRepository = new UserRepository();

            Console.Write("Please enter user's first name : ");
            string firstName = Console.ReadLine();

            Console.Write("Please enter user's last name : ");
            string lastName = Console.ReadLine();

            Console.Write("Please enter user's email : ");
            string email = Console.ReadLine();

            Console.Write("Please enter user's password : ");
            string password = Console.ReadLine();

            Console.Write("Please enter user's confirm password : ");
            string confirmPassword = Console.ReadLine();
            
            Console.WriteLine();

            if (
                UserValidation.IsValidFirstName(firstName)&
                UserValidation.IsValidLastName(lastName) &
                UserValidation.IsValidEmail(email)&
                UserValidation.IsValidPassword(password))
            {
                if (!UserValidation.IsUserExist(email))
                {
                    User user = new User(firstName, lastName, email, password);
                    UserRepository.Add(user);
                    Console.WriteLine($"User added to system, his/her details are : {user.GetInfo()}");
                }
                
            }

        }

        public static void Login()
        {
            UserRepository userRepository = new UserRepository();

            while (true)
            {
                Console.Write("Please enter user's email : ");
                string email = Console.ReadLine();

                Console.Write("Please enter user's password : ");
                string password = Console.ReadLine();

                if (UserRepository.IsUserExistByEmailAndPassword(email, password) && !IsAuthorized)
                {
                    User user = UserRepository.GetUserByEmail(email);
                    if (user is Admin)
                    {
                        Console.WriteLine($"Admin successfully authenticated : {user.GetInfo()}");
                        Account = user;
                        IsAuthorized = true;
                        Dashboard.AdminPanel();
                    }
                    else if (user is User)
                    {
                        Console.WriteLine($"User successfully authenticated : {user.GetInfo()}");
                        Account = user;
                        IsAuthorized = true;
                        Dashboard.UserPanel();
                    }
                    else
                    {
                        Console.WriteLine("Email or password is not correct! ");
                    }
                }
            }
        }
        


    }
}
