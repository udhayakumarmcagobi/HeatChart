using HeatChart.Entities.Sql.Domain;
using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Entities.Sql
{
    /// <summary>
    /// Heat Chart header - header details of the heat Chart
    /// </summary>
    public class HeatChartHeader : IEntityBase
    {
        public int ID { get; set; } // Auto generated ID
        public string HeatChartNumber { get; set; } // Auto calculate this number
        public int CustomerID { get; set; } // Heat Chart Customer ID
        public string JobNumber { get; set; } // Job number
        public string DrawingNumber { get; set; } // Drawing number
        public string DrawingRevision { get; set; } // Drawing revision
        public string CustomerPONumber { get; set; } // Customer PO Number
        public DateTime? CustomerPODate { get; set; } // Customer PO Date
        public string CustomerPOEquipment { get; set; } // Customer PO Equipment
        public string TagNumber { get; set; } // Tag Number
        public int ThirdPartyInspectionID { get; set; } // Third Party Inspection ID
        public string Plant { get; set; } // heatchart will be generated for this plant
        public string OtherInfo { get; set; } // Other Info Details

        //No of Equipment: This field is needed in Heat Chart screen, scenario behind this that, 
        //sometimes, they generate the same heatchart for multiple equipment, 
        //hence they want to generate one heatchart mentioning "No of Equipment". type is Number(+ve Integer)
        public int NoOfEquipment { get; set; } // No of Equipment
        public string CreatedBy { get; set; } // Created ByDimID
        public string ModifiedBy { get; set; } // Modified By
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date		
        public bool IsDeleted { get; set; } // For soft delete indication

        public virtual ICollection<HeatChartDetails> HeatChartDetails { get; set; } // Material register details

        public virtual Customer Customers { get; set; } // Customer Details

        public virtual ThirdPartyInspection ThirdPartyInspections { get; set; } // Customer Details
    }
}
