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
    public static class SpecificationsExtensions
    {
        public static Specifications GetSingleBySpecificationsName(this IEFRepository<Specifications> SpecificationsRepository, string name)
        {
            return SpecificationsRepository.GetFirstOrDefault(x => x.Name.Equals(name));
        }

        public static Specifications GetSingleBySpecificationsID(this IEFRepository<Specifications> SpecificationsRepository, int id)
        {
            return SpecificationsRepository.GetFirstOrDefault(x => x.ID == id);
        }

        public static bool SpecificationExists(this IEFRepository<Specifications> specificationsRepository, string name)
        {
            return specificationsRepository.GetAll()
                .Any(c => c.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
