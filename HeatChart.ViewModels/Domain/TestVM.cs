using System;
using System.Linq;
using System.Collections.Generic;
using HeatChart.ViewModels.Validators;
using System.ComponentModel.DataAnnotations;

namespace HeatHeatChart.ViewModels.Domain
{
    /// <summary>
    /// Used to store Test details
    /// </summary>
    public class TestVM
    {
        public int ID { get; set; } // Auto generated ID
        public string Name { get; set; } // Name of the Test
        public string Description { get; set; } // Description of the Test
        public Boolean IsDeleted { get; set; } // It is used for soft delete

        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new TestVMValidator();
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
