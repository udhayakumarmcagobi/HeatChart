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
    public static class SupplierExtensions
    {
        public static Supplier GetSingleBySupplierName(this IEFRepository<Supplier> SupplierRepository, string name)
        {
            return SupplierRepository.GetFirstOrDefault(x => x.Name.Equals(name));
        }

        public static Supplier GetSingleBySupplierID(this IEFRepository<Supplier> SupplierRepository, int id)
        {
            return SupplierRepository.GetFirstOrDefault(x => x.ID == id);
        }

        public static bool SupplierExists(this IEFRepository<Supplier> SupplierRepository, string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return SupplierRepository.GetAll()
                .Any(c => c.Name.ToLower().Equals(name.ToLower()));
            }

            return SupplierRepository.GetAll()
                .Any(c => c.Email.ToLower().Equals(email.ToLower()) || c.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
