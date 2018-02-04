using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.ViewModels.DatasetVM
{
    public class HeatChartDetailsDatasetVM
    {
        public string PartNumber { get; set; }
        public string PartNumberDescription { get; set; }
        public string SheetNo { get; set; }
        public string CTNumber { get; set; }
        public string HeatChartDimension { get; set; }
        public string HeatChartSpecification { get; set; }
        public string MaterialRegisterDimension { get; set; }
        public string MaterialRegisterSpecification { get; set; }
        public string LabOrMillName { get; set; }
        public string LabOrMillTCNumber { get; set; }
        public string LabOrMillTCDate { get; set; }
    }
}
