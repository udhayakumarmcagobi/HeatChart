using FluentValidation;
using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.ViewModels.Validators
{
    public class ThirdPartyInspectionVMValidator : AbstractValidator<ThirdPartyInspectionVM>
    {
        public ThirdPartyInspectionVMValidator()
        {
            RuleFor(t => t.Name).NotEmpty()
                .WithMessage("Invalid ThirdPartyInspection Name");

            RuleFor(t => t.Email).EmailAddress().When(t => !string.IsNullOrWhiteSpace(t.Email))
                .WithMessage("Invalid email Address");

            RuleFor(t => t.Mobile)
                .Matches(@"^(\t*\d\t*){10}$").When(t => !string.IsNullOrWhiteSpace(t.Mobile))
                .WithMessage("Mobile phone must have 10 digits");

            RuleFor(t => t.CreatedBy).NotEmpty()
                .WithMessage("Invalid CreatedBy");

            RuleFor(t => t.ModifiedBy).NotEmpty()
                .WithMessage("Invalid ModifiedBy");

            RuleFor(t => t.CreatedOn).NotNull()
                .WithMessage("Invalid CreatedOn");

            RuleFor(t => t.ModifiedOn).NotNull()
                .WithMessage("Invalid ModifiedOn");
        }
    }
}
