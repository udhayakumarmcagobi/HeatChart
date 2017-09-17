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
    public static class DimensionsExtensions
    {
        public static Dimension GetSingleByDimensionName(this IEFRepository<Dimension> DimensionsRepository, string name)
        {
            return DimensionsRepository.GetFirstOrDefault(x => x.Name.Equals(name));
        }

        public static Dimension GetSingleByDimensionID(this IEFRepository<Dimension> DimensionsRepository, int id)
        {
            return DimensionsRepository.GetFirstOrDefault(x => x.ID == id);
        }

        public static bool DimensionExists(this IEFRepository<Dimension> DimensionsRepository, string name)
        {
            return DimensionsRepository.GetAll()
                .Any(c => c.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
