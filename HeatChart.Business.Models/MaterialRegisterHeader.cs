using HeatChart.Business.Models.Domain;
using HeatChart.Business.Models.Enum;
using System;
using System.Collections.Generic;

namespace HeatChart.Business.Models
{
    /// <summary>
    /// Material register header is used to store header details of the material
    /// </summary>
    public class MaterialRegisterHeader 
    {
        public int ID { get; set; } // Material Header ID
        public string CTNumber { get; set; } // Check Test Number
        public int SupplierID { get; set; } // Supplier ID
        public string SupplierPONumber { get; set; } // Supplier Purchase Order Number
        public DateTime SupplierPODate { get; set; } // Supplier Purchase Order Date 
        public int CustomerID { get; set; } // Customer ID
        public string CustomerPONumber { get; set; } // Customer Purchase Order Number
        public string CustomerPoEquipment { get; set; } // Customer Purchase order equipment
        public int ThirdPartyInspectionID { get; set; } // Third Party Inspection ID
        public int RawMaterialFormID { get; set; } // Raw material ID
        public string Dimension { get; set; } // Dimension (Thk/Size/Dia or Other)
        public int SpecificationsID { get; set; } // Specification ID
        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified By
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date
        public Status status { get; set; } // It is for Accepted, Rejected
        public string PartiallyAcceptedRemarks { get; set; } // If it s accepted partially, Remarks should be provided
        public bool IsDeleted { get; set; } // The status of the records.whether it is deleted or not.

        public  Customer Customers { get; set; } // Customers
        public  Supplier Suppliers { get; set; } // Suppliers
        public  ThirdPartyInspection ThirdPartyInspections { get; set; } // ThirdPartyInspections
        public  Specifications Specification { get; set; } // Specification
        public  RawMaterialForm RawMaterialForms { get; set; } // Specification
        public  IList<MaterialRegisterSubSeries> MaterialRegisterSubSeriess { get; set; } // Materials Registers for sub series
    }
}
