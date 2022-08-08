using AuthenticationWithClie.ApplicationLogic;
using AuthenticationWithClie.ApplicationLogic.Validations;
using AuthenticationWithClie.Database.Models;
using AuthenticationWithClie.Database.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Repository
{
    public class UserRepository : Repository<User,int>
    {
        private static int _idCounter;

        public static int IdCounter
        {
            get
            {
                _idCounter++;
                return _idCounter;
            }
        }

        static UserRepository()
        {
            SeedUsers();
        }

        private static void SeedUsers()
        {
            DbContext.Add(new Admin("Mahmood", "Garibov","male","qaribovmahmud@gmail.com", "123321"));
            DbContext.Add(new Admin("Eshqin", "Mahmudov","male", "eshqin@gmail.com", "123321"));
            DbContext.Add(new User("Inci", "Mikayilli", "female", "inci@gmail.com", "123321"));
            DbContext.Add(new User("Said", "Mikayilli", "male", "said@gmail.com", "123321"));
        }

        public User AddUser(string firstName, string lastName, string email, string userGender, string password)
        {
            User user = new User(firstName, lastName, email, userGender, password, IdCounter);
            DbContext.Add(user);
            return user;
        }

        public User AddUser(string firstName, string lastName, string email, string userGender, string password, int id)
        {
            User user = new User(firstName, lastName, email, password, userGender, id);
            DbContext.Add(user);
            return user;
        }

        public bool IsUserExistsByEmail(string email)
        {
            foreach (User user in DbContext)
            {
                if (user.Email == email)
                {
                    return true;
                }
            }
            return false;
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            foreach (User user in DbContext)
            {
                if (user.Email == email && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public static bool IsUserExistByEmailAndPassword(string email, string password)
        {
            foreach (User user in DbContext)
            {
                if (user.Email == email && user.Password == password)
                {
                    return true;
                }
            }
            Console.WriteLine("Email or password is not correct.");
            return false; 
        }

        public static User GetUserByEmail(string email)
        {
            foreach (User user in DbContext)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            Console.WriteLine("This email is not registered.");
            return null;
        }

        public void UpdateInfo()
        {
            if (Authentication.IsAuthorized)
            {
                IsValidInfo();
            }
        }

        public void UpdateUserbyId(int id)
        {
            foreach (User user in DbContext)
            {
                if (user.Id == id)
                {
                    IsValidInfo();
                }
            }

        }

        private static void IsValidInfo()
        {
            Console.Write("Write new name: ");
            string newFirstName = Console.ReadLine();
            Console.Write("Write new lastname: ");
            string newLastName = Console.ReadLine();
            Console.Write("Write your gender: ");
            string newUserGender = Console.ReadLine();

            if (UserValidation.IsValidFirstName(newFirstName) &
            UserValidation.IsValidLastName(newLastName) &
            Validation.IsValidGender(newUserGender))
            {
                Authentication.Account.FirstName = newFirstName;
                Authentication.Account.LastName = newLastName;
                Authentication.Account.UserGender = newUserGender;
                Console.WriteLine($"Refreshed info: {newFirstName} {newLastName} {newUserGender}");
            }
            
        }

        public static void ShowAdmins()
        {
            foreach (User user in DbContext)
            {
                if (user is Admin)
                {
                    Console.WriteLine($"Name: {user.FirstName}, Lastname: {user.LastName}, Email: {user.Email}  Date: {user.CreatedAt}");
                }          
            }
        }

        public static void ShowUsers()
        {
            foreach (User user in DbContext)
            {
                if (user is not Admin)
                {
                    Console.WriteLine($"Name: {user.FirstName}, Lastname: {user.LastName}, Email: {user.Email}  Date: {user.CreatedAt}");
                }
            }
        }


       

        
    }
}
