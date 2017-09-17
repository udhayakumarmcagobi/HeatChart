using FluentValidation;
using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.ViewModels.Validators
{
    public class CustomerVMValidator : AbstractValidator<CustomerVM>
    {
        public CustomerVMValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("Invalid Customer Name");

            RuleFor(c => c.Email).EmailAddress().When(c => !string.IsNullOrWhiteSpace(c.Email))
                .WithMessage("Invalid email Address");

            RuleFor(c => c.Mobile)
                .Matches(@"^(\s*\d\s*){10}$").When(c => !string.IsNullOrWhiteSpace(c.Mobile))
                .WithMessage("Mobile phone must have 10 digits");

            RuleFor(c => c.CreatedBy).NotEmpty()
                .WithMessage("Invalid CreatedBy");

            RuleFor(c => c.ModifiedBy).NotEmpty()
                .WithMessage("Invalid ModifiedBy");

            RuleFor(c => c.CreatedOn).NotNull()
                .WithMessage("Invalid CreatedOn");

            RuleFor(c => c.ModifiedOn).NotNull()
                .WithMessage("Invalid ModifiedOn");
        }
    }
}
