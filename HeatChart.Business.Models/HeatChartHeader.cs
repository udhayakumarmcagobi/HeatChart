using HeatChart.Business.Models.Domain;
using System;
using System.Collections.Generic;

namespace HeatChart.Business.Models
{
    /// <summary>
    /// Heat Chart header - header details of the heat Chart
    /// </summary>
    public class HeatChartHeader 
    {
        public int ID { get; set; } // Auto generated ID
        public string HeatChartNumber { get; set; } // Auto calculate this number
        public int CustomerID { get; set; } // Heat Chart Customer ID
        public string DrawingNumber { get; set; } // Drawing number
        public string DrawingRevision { get; set; } // Drawing revision
        public string CustomerPONumber { get; set; } // Customer PO Number
        public string CustomerPoEquipment { get; set; } // Customer PO Equipment
        public string TagNumber { get; set; } // Tag Number
        public int ThirdPartyInspectionID { get; set; } // Third Party Inspection ID
        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date		
        public bool IsDeleted { get; set; } // For soft delete indication

        public IList<HeatChartDetails> HeatChartDetails { get; set; } // Material register details
        public Customer Customers { get; set; } // Customer Details
        public ThirdPartyInspection ThirdPartyInspections { get; set; } // Customer Details
    }
}
