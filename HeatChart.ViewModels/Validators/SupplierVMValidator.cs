using FluentValidation;
using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.ViewModels.Validators
{
    public class SupplierVMValidator : AbstractValidator<SupplierVM>
    {
        public SupplierVMValidator()
        {
            RuleFor(s => s.Name).NotEmpty()
                .WithMessage("Invalid Supplier Name");

            RuleFor(s => s.Email).EmailAddress().When(s => !string.IsNullOrWhiteSpace(s.Email))
                .WithMessage("Invalid email Address");

            RuleFor(s => s.Mobile)
                .Matches(@"^(\s*\d\s*){10}$").When(s => !string.IsNullOrWhiteSpace(s.Mobile))
                .WithMessage("Mobile phone must have 10 digits");

            RuleFor(s => s.CreatedBy).NotEmpty()
                .WithMessage("Invalid CreatedBy");

            RuleFor(s => s.ModifiedBy).NotEmpty()
                .WithMessage("Invalid ModifiedBy");

            RuleFor(s => s.CreatedOn).NotNull()
                .WithMessage("Invalid CreatedOn");

            RuleFor(s => s.ModifiedOn).NotNull()
                .WithMessage("Invalid ModifiedOn");
        }
    }
}
