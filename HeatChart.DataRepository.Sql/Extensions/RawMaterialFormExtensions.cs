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
    public static class RawMaterialFormExtensions
    {
        public static RawMaterialForm GetSingleByRawMaterialFormName(this IEFRepository<RawMaterialForm> RawMaterialFormRepository, string name)
        {
            return RawMaterialFormRepository.GetFirstOrDefault(x => x.Name.Equals(name));
        }

        public static RawMaterialForm GetSingleByRawMaterialFormID(this IEFRepository<RawMaterialForm> RawMaterialFormRepository, int id)
        {
            return RawMaterialFormRepository.GetFirstOrDefault(x => x.ID == id);
        }

        public static bool RawMaterialFormExists(this IEFRepository<RawMaterialForm> rawMaterialFormsRepository, string name)
        {
            return rawMaterialFormsRepository.GetAll()
                .Any(c => c.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
