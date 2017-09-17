using HeatChart.ViewModels.Validators;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeatHeatChart.ViewModels.Domain
{
    /// <summary>
    /// HeatChart Supplier Information
    /// </summary>
    public class SupplierVM : IValidatableObject
    {
        public int ID { get; set; } // Auto generated ID
        public string Name { get; set; } // Name 
        public string Address { get; set; } // Address 
        public string Email { get; set; } // Email ID 
        public string Landline { get; set; } // Landline phone
        public string Mobile { get; set; } // Mobile phone
        public string AdditionalDetails { get; set; } // To store any additional details of the customer. 
        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	
        public Boolean IsDeleted { get; set; } // It is used for soft delete

        public IList<HeatChartHeaderVM> HeatChartHeaders { get; set; }
        public IList<MaterialRegisterHeaderVM> MaterialRegisterHeaders { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new SupplierVMValidator();
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
