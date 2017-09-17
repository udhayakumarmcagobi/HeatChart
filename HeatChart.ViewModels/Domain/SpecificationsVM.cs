using System;
using System.Linq;
using System.Collections.Generic;
using HeatChart.ViewModels.Validators;
using System.ComponentModel.DataAnnotations;

namespace HeatHeatChart.ViewModels.Domain
{
    /// <summary>
    /// Used to store Specifications details
    /// </summary>
    public class SpecificationsVM
    {
        public int ID { get; set; } // Auto generated ID
        public string Name { get; set; } // Name of the Specifications
        public string Description { get; set; } // Description of the Specifications
        public Boolean IsDeleted { get; set; } // It is used for soft delete

        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	

        public IList<HeatChartDetailsVM> HeatChartDetails { get; set; }
        public IList<MaterialRegisterHeaderVM> MaterialRegisterHeaders { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new SpecificationsVMValidator();
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
