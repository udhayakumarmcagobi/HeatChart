using HeatChart.Entities.Sql.Domain;
using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeatChart.Entities.Sql
{
    /// <summary>
    /// Material register header is used to store header details of the material
    /// </summary>
    public class MaterialRegisterHeader : IEntityBase
    {
        public int ID { get; set; } // Material Header ID
        public string CTNumber { get; set; } // Check Test Number
        public int SupplierID { get; set; } // Supplier ID
        public string SupplierPONumber { get; set; } // Supplier Purchase Order Number
        public DateTime SupplierPODate { get; set; } // Supplier Purchase Order Date 
        public int ThirdPartyInspectionID { get; set; } // Third Party Inspection ID
        public int RawMaterialFormID { get; set; } // Raw material ID
        public int SpecificationsID { get; set; } // Specification ID
        public string  Dimension { get; set; } // Specification ID
        public string OtherInfo { get; set; } // Other Info Details
        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified By
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date
        public int StatusID { get; set; } // It is for Accepted, Rejected
        public string PartiallyAcceptedRemarks { get; set; } // If it s accepted partially, Remarks should be provided

        //They just need one Quantity field in the Material Register. No quantity tracking needed. Need behind this 
        //requirement is that they just want to know what the quantity they received against that 
        //Check Test No: for eg., they order 10 pipes, they want this entered in the material register. 
        //type should be varchar(50), it could be 10 nos, 1 tonne, etc
        public string Quantity { get; set; } 
        public bool IsDeleted { get; set; } // The status of the records.whether it is deleted or not.

        public virtual Status Status { get; set; }

        public virtual Supplier Suppliers { get; set; } // Suppliers

        public virtual ThirdPartyInspection ThirdPartyInspections { get; set; } // ThirdPartyInspections

        public virtual Specifications Specification { get; set; } // Specification

        public virtual RawMaterialForm RawMaterialForms { get; set; } // Specification

        public virtual ICollection<MaterialRegisterSubSeries> MaterialRegisterSubSeriess { get; set; } // Materials Registers for sub series
    }
}
