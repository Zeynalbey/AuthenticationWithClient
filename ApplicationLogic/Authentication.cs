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
    class Authentication : Repository<User, int>
    {

        public static User Account { get; set; }
        public static bool IsAuthorized { get; set; } = false;

        public static void Register()
        {
            UserRepository userRepository = new UserRepository();
            Console.WriteLine("Name's first letter must be uppercase, length is 3-30 and only letters.");
            Console.WriteLine("Lastname's first letter must be uppercase, length is 3-30 and only letters.");
            Console.WriteLine("Email username's length must be between 10-30, and ends with @code.edu.az");
            Console.WriteLine("Write gender: male or female");
            Console.WriteLine("Password contains minimum 1 uppercase, lowercase, digit and minimum length 8.");
            Console.WriteLine();

            Console.Write("Enter user's first name : ");
            string firstName = Console.ReadLine();

            Console.Write("Enter user's last name : ");
            string lastName = Console.ReadLine();

            Console.Write("Choose gender (Male or Female) : ");
            string gender = Console.ReadLine();

            Console.Write("Enter user's email : ");
            string email = Console.ReadLine();

            Console.Write("Enter user's password : ");
            string password = Console.ReadLine();

            Console.Write("Enter user's confirm password : ");
            string confirmPassword = Console.ReadLine();
            Console.WriteLine("_________________________________________________________");


            if (
                UserValidation.IsValidFirstName(firstName) &
                UserValidation.IsValidLastName(lastName) &
                UserValidation.IsValidEmail(email) &
                UserValidation.IsValidPassword(password) &
                Validation.IsValidGender(gender))
            {
                if (!UserValidation.IsUserExist(email))
                {
                    User user = new User(firstName, lastName, gender, email, password);
                    UserRepository.Add(user);
                    Console.WriteLine($"User added to system, details are : {user.GetInfo()}");
                }

            }

        }

        public static void Login()
        {
            UserRepository userRepository = new UserRepository();


            Console.WriteLine();
            Console.Write("Please enter user's email : ");
            string email = Console.ReadLine();

            Console.Write("Please enter user's password : ");
            string password = Console.ReadLine();

            if (UserRepository.IsUserExistByEmailAndPassword(email, password) && !IsAuthorized)
            {
                User user = UserRepository.GetUserByEmail(email);
                if (user is Admin)
                {
                    Console.WriteLine("_________________________________________________________");
                    Console.WriteLine($"Welcome to your account, dear admin, {user.GetInfo()}");
                    Account = user;
                    IsAuthorized = true;
                    Dashboard.AdminPanel();
                }
                else if (user is User)
                {
                    Console.WriteLine("_________________________________________________________");
                    Console.WriteLine($"Wellcome to your account, {user.GetInfo()}");
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
