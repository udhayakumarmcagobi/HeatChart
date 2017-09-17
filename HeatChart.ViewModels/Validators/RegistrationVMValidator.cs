using FluentValidation;

namespace HeatChart.ViewModels.Validators
{
    public class RegistrationVMValidator : AbstractValidator<RegistrationVM>
    {
        public RegistrationVMValidator()
        {
            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid Username");

            RuleFor(r => r.Password).NotEmpty()
               .WithMessage("Invalid Password");

            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .WithMessage("Invalid Email address");
        }
    }
}
