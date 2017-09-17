using HeatChart.ViewModels.Validators;
using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HeatHeatChart.ViewModels
{
    /// <summary>
    /// Heat Chart header - header details of the heat Chart
    /// </summary>
    public class HeatChartHeaderVM
    {
        public int ID { get; set; } // Auto generated ID
        public string HeatChartNumber { get; set; } // Auto calculate this number
        public CustomerVM CustomerSelected { get; set; } // Selected Customer
        public List<CustomerVM> Customers { get; set; } // Customers
        public string JobNumber { get; set; } // Job number
        public string DrawingNumber { get; set; } // Drawing number
        public string DrawingRevision { get; set; } // Drawing revision
        public string CustomerPONumber { get; set; } // Customer PO Number
        public string CustomerPOEquipment { get; set; } // Customer PO Equipment
        public string TagNumber { get; set; } // Tag Number
        public string OtherInfo { get; set; } // Other Info Details
        public ThirdPartyInspectionVM ThirdPartyInspectionSelected { get; set; } // Selected ThirdPartyInspection
        public List<ThirdPartyInspectionVM> ThirdPartyInspections { get; set; } // ThirdPartyInspection
        public string Plant { get; set; } // heatchart will be generated for this plant

        //No of Equipment: This field is needed in Heat Chart screen, scenario behind this that, 
        //sometimes, they generate the same heatchart for multiple equipment, 
        //hence they want to generate one heatchart mentioning "No of Equipment". type is Number(+ve Integer)
        public int NoOfEquipment { get; set; } // No of Equipment
        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date		
        public bool IsDeleted { get; set; } // For soft delete indication
        public bool IsHeatChartNumberAutoCalculate { get; set; } // It is used to identify the Heat Chart number to be auto calculated or not

        public IList<HeatChartDetailsVM> HeatChartDetails { get; set; } // Material register details

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new HeatChartHeaderVMValidator();
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
