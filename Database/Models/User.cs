using AuthenticationWithClie.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationWithClie.Database.Models;
using AuthenticationWithClie.Database.Common;
using AuthenticationWithClie.Database.Models.Enums;

namespace AuthenticationWithClie.Database.Models
{
    public class User: Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserGender { get; set; }
        public List<Message> Inbox { get; set; } = new List<Message>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public User(string firstName, string lastName, string userGender, string email,  string password, int? id=null)
        {
            FirstName = firstName;
            LastName = lastName;
            UserGender = userGender;
            Email = email;
            Password = password;

            if (id.HasValue)
            {
                Id = id.Value;
            }
            else
            {
                Id = UserRepository.IdCounter;
            }
        }

        public virtual string GetInfo()
        {
            return $"{FirstName} {LastName} {Email}";
        }
    }
}
