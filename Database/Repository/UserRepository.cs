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
    public class UserRepository : Repository<User, int>
    {
        UserValidation userValidation = new UserValidation();
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
            DbContext.Add(new Admin("Mahmood", "Garibov", "male", "qaribovmahmud@gmail.com", "123321"));
            DbContext.Add(new Admin("Eshqin", "Mahmudov", "male", "eshqin@gmail.com", "123321"));
            DbContext.Add(new User("Inci", "Mikayilli", "female", "inci@gmail.com", "123321"));
            DbContext.Add(new User("Said", "Mikayilli", "male", "said@gmail.com", "123321"));
            DbContext.Add(new User("Xumar", "Xumarli", "female", "xumar@gmail.com", "123321"));
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

        public bool IsUserExistByEmailAndPassword(string email, string password)
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

        public User GetUserByEmail(string email)
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

    }
}
