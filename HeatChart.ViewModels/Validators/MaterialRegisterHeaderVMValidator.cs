using FluentValidation;
using HeatHeatChart.ViewModels;
using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.ViewModels.Validators
{
    public class MaterialRegisterHeaderVMValidator : AbstractValidator<MaterialRegisterHeaderVM>
    {
        public MaterialRegisterHeaderVMValidator()
        {
            RuleFor(mr => mr.SupplierSelected).NotEmpty().When(mr => string.IsNullOrWhiteSpace(mr.SupplierSelected.Name))
                .WithMessage("Invalid Supplier");

            RuleFor(mr => mr.ThirdPartyInspectionSelected).NotEmpty().When(mr => string.IsNullOrWhiteSpace(mr.ThirdPartyInspectionSelected.Name))
                .WithMessage("Invalid Third Party Inspection");

            RuleFor(mr => mr.SpecificationSelected).NotEmpty().When(mr => string.IsNullOrWhiteSpace(mr.SpecificationSelected.Name))
                .WithMessage("Invalid Specification");

            RuleFor(mr => mr.RawMaterialFormSelected).NotEmpty().When(mr => string.IsNullOrWhiteSpace(mr.RawMaterialFormSelected.Name))
                .WithMessage("Invalid RawMaterialForm");

            RuleFor(mr => mr.CreatedBy).NotEmpty()
                .WithMessage("Invalid CreatedBy");

            RuleFor(mr => mr.ModifiedBy).NotEmpty()
                .WithMessage("Invalid ModifiedBy");

            RuleFor(mr => mr.CreatedOn).NotNull()
                .WithMessage("Invalid CreatedOn");

            RuleFor(mr => mr.ModifiedOn).NotNull()
                .WithMessage("Invalid ModifiedOn");
        }
    }
}
