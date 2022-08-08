using AuthenticationWithClie.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Models
{
    public sealed class Admin : User
    {
        public Admin(string firstName, string lastName, string userGender, string email, string password, int id)
            :base(firstName, lastName, userGender, email, password, id){}

        public Admin(string firstName, string lastName, string userGender, string email, string password)
            : base(firstName, lastName, userGender, email, password){}

        public override string GetInfo()
        {
            return $"Admin name: {FirstName}, Lastname: {LastName}, Gender: {UserGender}, Email: {Email},  Password: {Password}, Date: {CreatedAt}";
        }
    }
}
