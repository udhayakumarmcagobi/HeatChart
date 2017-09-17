using FluentValidation;
using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.ViewModels.Validators
{
    public class RawMaterialFormVMValidator : AbstractValidator<RawMaterialFormVM>
    {
        public RawMaterialFormVMValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("Invalid RawMaterialForm Name");
        
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
