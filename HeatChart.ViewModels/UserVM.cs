using HeatChart.Entities.Sql.Domain;
using System;
using System.Collections.Generic;

namespace HeatHeatChart.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {
            UserRoles = new List<UserRoleVM>();
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }

        public IList<UserRoleVM> UserRoles { get; set; }
    }
}
