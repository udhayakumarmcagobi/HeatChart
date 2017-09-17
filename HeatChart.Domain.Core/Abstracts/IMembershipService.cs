using HeatChart.Domain.Core.Utilities;
using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using System;
using System.Collections.Generic;

namespace HeatChart.Domain.Core.Abstracts
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        Tuple<Users, string> CreateUser(string username, string email, string password, int[] roles);
        Tuple<Users, string> UpdateUser(string username, string email, string password);
        Tuple<Users, string> DeleteUser(string username, string email);
        Users GetUser(int userId);
        Tuple<List<Users>, int> GetUsers(int? page, int? pageSize, string filter);
        List<Role> GetUserRoles(string username);
    }
}
