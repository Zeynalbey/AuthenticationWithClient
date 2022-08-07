﻿using AuthenticationWithClie.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationWithClie.Database.Models;
using AuthenticationWithClie.Database.Common;

namespace AuthenticationWithClie.Database.Models
{
    public class User: Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Report> Reportinbox { get; set; } = new List<Report>();
        public User(string firstName, string lastName, string email, string password, int? id=null)
        {
            FirstName = firstName;
            LastName = lastName;
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
            return $"{FirstName} {LastName}";
        }
    }
}