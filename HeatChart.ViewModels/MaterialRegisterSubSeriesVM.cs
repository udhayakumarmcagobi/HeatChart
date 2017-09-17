using HeatHeatChart.ViewModels.Domain;
using System.Collections.Generic;

namespace HeatHeatChart.ViewModels
{
    public class MaterialRegisterSubSeriesVM 
    {

        public int ID { get; set; } // It is unique ID for Material register sub series. Each Material Header can have multiple sub series
        public string SubSeriesNumber { get; set; } // Sub series number
        public int MaterialRegisterHeaderID { get; set; } // Material Header ID
        public string Status { get; set; } // It is for Accepted, rejected, Retest
        public bool IsDeleted { get; set; } // The status of the records.whether it is deleted or not.

        public string ReportSelected { get; set; }
        public MaterialRegisterHeaderVM MaterialRegisterHeader { get; set; }
        public LabReportVM LabReport { get; set; }
        public MillDetailVM MillDetail { get; set; }
        public List<TestVM> Tests { get; set; }
        public List<TestVM> SelectedTests { get; set; }
        public IList<MaterialRegisterFileDetailVM> MaterialRegisterFileDetails { get; set; }

    }
}
