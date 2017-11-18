using HeatChart.ViewModels.Validators;
using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HeatHeatChart.ViewModels
{
    /// <summary>
    /// Material register header is used to store header details of the material
    /// </summary>
    public class MaterialRegisterHeaderVM
    {
        public int ID { get; set; } // Material Header ID
        public string CTNumber { get; set; } // Check Test Number
        public SupplierVM SupplierSelected { get; set; } // Selected Supplier
        public List<SupplierVM> Suppliers { get; set; } // Suppliers
        public string SupplierPONumber { get; set; } // Supplier Purchase Order Number
        public DateTime SupplierPODate { get; set; } // Supplier Purchase Order Date 
        public string CustomerPONumber { get; set; } // Customer Purchase Order Number
        public string CustomerPOEquipment { get; set; } // Customer Purchase order equipment
        public string OtherInfo { get; set; } // Other Info Details
        public ThirdPartyInspectionVM ThirdPartyInspectionSelected { get; set; } // Selected ThirdPartyInspection
        public List<ThirdPartyInspectionVM> ThirdPartyInspections { get; set; } // ThirdPartyInspection
        public RawMaterialFormVM RawMaterialFormSelected { get; set; } // Selected Raw Material Form
        public List<RawMaterialFormVM> RawMaterialForms { get; set; } // Raw Material Forms
        public SpecificationsVM SpecificationSelected { get; set; } // Selected Specification
        public List<SpecificationsVM> Specifications { get; set; } // Specifications
        public DimensionVM DimensionSelected { get; set; } // Selected Dimension
        public List<DimensionVM> Dimensions { get; set; } // Dimensions

        public string Dimension { get; set; }

        //They just need one Quantity field in the Material Register. No quantity tracking needed. Need behind this 
        //requirement is that they just want to know what the quantity they received against that 
        //Check Test No: for eg., they order 10 pipes, they want this entered in the material register. 
        //type should be varchar(50), it could be 10 nos, 1 tonne, etc
        public string Quantity { get; set; }
        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified By
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date
        public string Status { get; set; } // It is for Accepted, rejected, Retest
        public string PartiallyAcceptedRemarks { get; set; } // If it s accepted partially, Remarks should be provided
        public bool IsDeleted { get; set; } // The status of the records.whether it is deleted or not.
        public bool IsCheckTestNumberAutoCalculate { get; set; } // It is used to identify the Check Test Number to be auto calculated or not

        public IList<MaterialRegisterSubSeriesVM> MaterialRegisterSubSeriess { get; set; } // Materials Registers for sub series

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new MaterialRegisterHeaderVMValidator();
            var result = validator.Validate(this);

            return result.Errors.Select(item =>
                new ValidationResult(
                        item.ErrorMessage,
                        new[] { item.PropertyName }
                    )
                );
        }
    }
}
