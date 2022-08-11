using AuthenticationWithClie.Database.Models;
using AuthenticationWithClie.Database.Models.Enums;
using AuthenticationWithClie.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuthenticationWithClie.ApplicationLogic.Validations.Services
{
    public class DashboardService : Repository<User, int>
    {
        BlogRepository blogRepository = new BlogRepository();


        public void ShowUsers()
        {
            foreach (User user in DbContext)
            {
                if (user is not Admin)
                {
                    Console.WriteLine($"Id: {user.Id}, Name: {user.FirstName}, Lastname: {user.LastName}, Email: {user.Email}, Date: {user.CreatedAt}");
                }
            }
        }

        public void ShowAdmins()
        {
            foreach (User user in DbContext)
            {
                if (user is Admin)
                {
                    Console.WriteLine($"Id: {user.Id}, Name: {user.FirstName}, Lastname: {user.LastName}, Email: {user.Email}, Date: {user.CreatedAt}");
                }
            }
        }

      

    




    }



}
