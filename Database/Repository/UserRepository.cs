using AuthenticationWithClie.ApplicationLogic;
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

        public static List<Report> Reports { get; set; } = new List<Report>();
        public static List<Blog> BlogList { get; set; } = new List<Blog>();


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
            DbContext.Add(new Admin("Mahmood", "Garibov", "qaribovmahmud@gmail.com", "123321"));
            DbContext.Add(new Admin("Eshqin", "Mahmudov", "eshqin@gmail.com", "123321"));
            DbContext.Add(new User("Yehya", "Mahmudov", "yehya@gmail.com", "123321"));
            DbContext.Add(new User("Said", "Mikayilli", "said@gmail.com", "123321"));
        }

        public User AddUser(string firstName, string lastName, string email, string password)
        {
            User user = new User(firstName, lastName, email, password, IdCounter);
            DbContext.Add(user);
            return user;
        }

        public User AddUser(string firstName, string lastName, string email, string password, int id)
        {
            User user = new User(firstName, lastName, email, password, id);
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
            return false; ;
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
            Console.WriteLine("bele bir istifadeci yoxdur.");
            return null;
        }



        public static void UpdateAdmin()
        {
            Console.WriteLine("adminin emailini yazin.");
            string emailUpdateAdmin = Console.ReadLine();
            User user1 = UserRepository.GetUserByEmail(emailUpdateAdmin);
            if (user1 is Admin)
            {
                Console.Write("Write new name: ");
                string newFirstName = Console.ReadLine();
                Console.Write("Write new name: ");
                string newLastName = Console.ReadLine();
                user1.FirstName = newFirstName;
                user1.LastName = newLastName;
                Console.WriteLine($"{user1.FirstName} {user1.LastName} changed to {newFirstName} {newLastName}");
            }
            else
            {
                Console.WriteLine("email tapilmadi");
            }

        }

        public static void ShowAdmins()
        {
            foreach (User user in DbContext)
            {
                if (user is Admin)
                {
                    Console.WriteLine(user.FirstName + " " + user.LastName);
                }          
            }
        }

        public static void ShowUsers()
        {
            foreach (User user in DbContext)
            {
                if (user is not Admin)
                {
                    Console.WriteLine(user.FirstName + " " + user.LastName);
                }
            }
        }


        public static void AddReport(User sender, string reason, User target)
        {
            Report report = new Report(sender, reason, target);
            Reports.Add(report);
            target.Reportinbox.Add(report);
        }

        public  Blog AddBlog(User owner,string content)
        {
            Blog blog = new Blog(owner,content);
            BlogList.Add(blog);
            return blog;
        }
       
        public static void ShowBlogs()
        {
            foreach (Blog blog in BlogList)
            {
                
                Console.WriteLine($"{blog.Id} {Authentication.Account.FirstName} {blog.Content} {blog.BlogDateTime} {blog.blogStatus}");
            }
        }

        public static Blog GetBlogbyId(int id)
        {
            foreach (Blog blog in BlogList)
            {
                if (blog.Id == 1)
                {
                    return blog;
                }
 
            }
            return null;
        }
    }
}
