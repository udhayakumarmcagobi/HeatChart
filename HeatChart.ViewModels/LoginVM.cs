using HeatChart.ViewModels.Validators;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace HeatChart.ViewModels
{
    public class LoginVM : IValidatableObject
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new LoginVMValidator();
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
