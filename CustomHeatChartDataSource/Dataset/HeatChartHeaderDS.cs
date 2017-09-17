using HeatChart.DataRepository.Sql.EFRepository;
using HeatChart.Entities.Sql;
using HeatHeatChart.ViewModels;
using System.Collections.Generic;

namespace HeatChart.CustomHeatChartDataSource.Dataset
{
    public class HeatChartHeaderDS
    {
        private static List<HeatChartHeaderVM> _heatChartHeaderVMList = null;

        public static List<HeatChartHeaderVM> GetHeatChartHeaderVMList()
        {
            if (_heatChartHeaderVMList == null)
            {
                var heatChartHeaderRepository = new EFRepository<HeatChartHeader>(null);

               
            }

           
            return _heatChartHeaderVMList;
        }
    }
}
