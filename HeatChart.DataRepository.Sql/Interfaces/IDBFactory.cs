using HeatChart.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.DataRepository.Sql.Interfaces
{
    public interface IDBFactory
    {
        HeatChartContext GetContext();
        Task<HeatChartContext> GetContextAsync();
    }
}
