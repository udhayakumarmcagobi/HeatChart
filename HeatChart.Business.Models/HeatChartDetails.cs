using HeatChart.Business.Models.Domain;
using System;

namespace HeatChart.Business.Models
{
    // Heat chart Details - Each Heat Chart header can have multiple heat Chart Details
    public class HeatChartDetails 
    {
        public int ID { get; set; } // Auto generated ID
        public int HeatChartHeaderID { get; set; } // Heat Chart Header ID
        public string PartNumber { get; set; } // Part numbers
        public string SheetNo { get; set; } // Sheet No
        public string Dimension { get; set; }
        public Nullable<int> HeatChartMaterialHeaderRelationshipID { get; set; }
        public int SpecificationsID { get; set; }
        public bool IsDeleted { get; set; }

        public HeatChartHeader HeatChartHeader { get; set; } // Material Header relationship
        public Specifications Specification { get; set; } // Specification
        public HeatChartMaterialHeaderRelationship HeathChartMaterialHeaderRelationships { get; set; }
    }
}
