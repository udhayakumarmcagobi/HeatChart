using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.ViewModels.DatasetVM
{
    public class HeatChartHeaderDatasetVM
    {
        public string HeatChartNumber { get; set; }
        public string Customer { get; set; }
        public string Equipment { get; set; }
        public string PurchaseOrder { get; set; }
        public string DrawingNumber { get; set; }
        public string DrawingRevision { get; set; }
        public string TagNumber { get; set; }
        public string ThirdPartyInspectionName { get; set; }        
    }
}
