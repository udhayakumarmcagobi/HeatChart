using HeatChart.Business.Models.Enum;
using System.Collections.Generic;

namespace HeatChart.Business.Models
{
    public class MaterialRegisterSubSeries 
    {
        public int ID { get; set; } // It is unique ID for Material register sub series. Each Material Header can have multiple sub series
        public string SubSeriesNumber { get; set; } // Sub series number
        public int MaterialRegisterHeaderID { get; set; } // Material Header ID
        public Status status { get; set; } // It is for Accepted, rejected
        public bool IsDeleted { get; set; } // The status of the records.whether it is deleted or not.

        public  MaterialRegisterHeader MaterialRegisterHeader { get; set; }
        public  LabReport LabReport { get; set; }
        public  MillDetail MillDetail { get; set; }
        public IList<MaterialRegisterFileDetail> MaterialRegisterFileDetails { get; set; }
        public IList<MaterialRegisterSubseriesTestRelationship> MaterialRegisterSubSeriesTestRelationships { get; set; }

    }
}
