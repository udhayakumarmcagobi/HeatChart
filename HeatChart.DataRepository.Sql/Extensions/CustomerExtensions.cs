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
    public static class CustomerExtensions
    {
        public static Customer GetSingleByCustomerName(this IEFRepository<Customer> customerRepository, string name)
        {
            return customerRepository.GetFirstOrDefault(x => x.Name.Equals(name));
        }

        public static Customer GetSingleByCustomerID(this IEFRepository<Customer> customerRepository, int id)
        {
            return customerRepository.GetFirstOrDefault(x => x.ID == id);
        }

        public static bool CustomerExists(this IEFRepository<Customer> customersRepository, string email, string name)
        {     
            if (string.IsNullOrWhiteSpace(email))
            {
                return customersRepository.GetAll()
                .Any(c => c.Name.ToLower().Equals(name.ToLower()));
            } 
                 
            return  customersRepository.GetAll()
                .Any(c => c.Email.ToLower().Equals(email.ToLower()) || c.Name.ToLower().Equals(name.ToLower()));            
        }
    }
}
