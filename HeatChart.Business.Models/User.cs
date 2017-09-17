using System;
using System.Collections.Generic;

namespace HeatChart.Business.Models
{
    public class User 
    {
        public User()
        {
            UserRoles = new List<UserRole>();
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }

        public IList<UserRole> UserRoles { get; set; }
    }
}
