using AuthenticationWithClie.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuthenticationWithClie.ApplicationLogic.Validations
{
    public class UserValidation
    {
        public bool IsValidFirstName(string firstName)
        {
            Regex regex = new Regex(@"^(?=[A-Z]{1})([A-Za-z]{3,30})$");

            if (regex.IsMatch(firstName))
            {
                return true;
            }

            Console.WriteLine("Entered name is not correct! ");


            return false;
        }

        public bool IsValidLastName(string lastName)
        {
            Regex regex = new Regex(@"^(?=[A-Z]{1})([A-Za-z]{4,29})$");

            if (regex.IsMatch(lastName))
            {
                return true;
            }

            Console.WriteLine("Entered lastname is not correct! ");

            return false;
        }

        public  bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9]{10,20}@code\.edu\.az$");

            if (regex.IsMatch(email))
            {
                return true;
            }

            Console.WriteLine("Entered email is not correct! ");

            return false;
        }

        public  bool IsPasswordsMatch(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                return true;
            }

            Console.WriteLine("Passwords are not match");

            return false;

        }

        public  bool IsValidPassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=[a-zA-Z0-9]{8,}).*$");

            if (regex.IsMatch(password))
            {
                return true;
            }

            Console.WriteLine("Entered password is not correct.");

            return false;
        }

        public  bool IsUserExist(string email)
        {
            UserRepository userRepository = new UserRepository();
            if (userRepository.IsUserExistsByEmail(email))
            {
                Console.WriteLine("User already exists");

                return true;
            }

            return false;
        }
    }
}
