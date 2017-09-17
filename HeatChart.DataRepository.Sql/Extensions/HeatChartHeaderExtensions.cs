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
    public static class HeatChartHeaderExtensions
    {
        public static HeatChartHeader GetSingleHeatChartHeaderHeatChartNumber(this IEFRepository<HeatChartHeader> heatChartHeaderRepository, string heatChartNumber)
        {
            return heatChartHeaderRepository.GetFirstOrDefault(x => x.HeatChartNumber.Equals(heatChartNumber));
        }

        public static HeatChartHeader GetSingleByHeatChartHeaderID(this IEFRepository<HeatChartHeader> heatChartHeaderRepository, int id)
        {
            return heatChartHeaderRepository.GetFirstOrDefault(x => x.ID == id);
        }

        public static bool HeatChartHeaderExists(this IEFRepository<HeatChartHeader> heatChartHeaderRepository, string heatChartNumber)
        {
            return heatChartHeaderRepository.GetAll()
                .Any(c => c.HeatChartNumber.ToLower().Equals(heatChartNumber.ToLower()));
        }
    }
}
