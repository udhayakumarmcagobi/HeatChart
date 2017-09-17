using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.ViewModels.Validators
{
    public class LoginVMValidator : AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid Username");

            RuleFor(r => r.Password).NotEmpty()
               .WithMessage("Invalid Password");
        }
    }
}
