using HeatChart.ViewModels.Validators;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace HeatChart.ViewModels
{
    public class RegistrationVM : IValidatableObject
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string UserRole { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new RegistrationVMValidator();
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
