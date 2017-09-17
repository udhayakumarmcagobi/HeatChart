using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.DataRepository.Sql.Extensions
{
    public static class ThirdPartyInspectionExtensions
    {
        public static ThirdPartyInspection GetSingleByThirdPartyInspectionName(this IEFRepository<ThirdPartyInspection> ThirdPartyInspectionRepository, string name)
        {
            return ThirdPartyInspectionRepository.GetFirstOrDefault(x => x.Name.Equals(name));
        }

        public static ThirdPartyInspection GetSingleByThirdPartyInspectionID(this IEFRepository<ThirdPartyInspection> ThirdPartyInspectionRepository, int id)
        {
            return ThirdPartyInspectionRepository.GetFirstOrDefault(x => x.ID == id);
        }

        public static bool ThirdPartyInspectionExists(this IEFRepository<ThirdPartyInspection> ThirdPartyInspectionRepository, string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return ThirdPartyInspectionRepository.GetAll()
                .Any(c => c.Name.ToLower().Equals(name.ToLower()));
            }

            return ThirdPartyInspectionRepository.GetAll()
                .Any(c => c.Email.ToLower().Equals(email.ToLower()) || c.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
