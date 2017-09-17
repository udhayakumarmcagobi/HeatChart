using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.DataRepository.Sql.Extensions
{
    public static class UserExtensions
    {
        public static Users GetSingleByUsernameOrEmail(this IEFRepository<Users> userRepository, string username, string email)
        {
            return userRepository.GetFirstOrDefault(x => x.Username.Equals(username) || x.Email.Equals(email));
        }

        public static Users GetSingleByUserID(this IEFRepository<Users> userRepository, int userID)
        {
            return userRepository.GetFirstOrDefault(x => x.ID == userID);
        }

    }
}
