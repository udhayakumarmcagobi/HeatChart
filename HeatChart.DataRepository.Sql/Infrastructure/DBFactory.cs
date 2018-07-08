using HeatChart.DataRepository.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeatChart.Data.Sql;

namespace HeatChart.DataRepository.Sql.Infrastructure
{
    public sealed class DBFactory : Disposable, IDBFactory
    {
        HeatChartContext dbContext ;
        public HeatChartContext GetContext()
        {
            return dbContext ?? (dbContext = new HeatChartContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        public async Task<HeatChartContext> GetContextAsync()
        {
            if (dbContext != null) return dbContext;

            await Task.Run(() =>
            {
                dbContext = new HeatChartContext();                
            });

            return dbContext;
        }

        internal class Instance
        {
            static Instance() { }

            internal static readonly HeatChartContext dbContext = new HeatChartContext();
        }
    }
}
