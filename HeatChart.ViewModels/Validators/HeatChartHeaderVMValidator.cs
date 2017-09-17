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
    public class HeatChartHeaderVMValidator : AbstractValidator<HeatChartHeaderVM>
    {
        public HeatChartHeaderVMValidator()
        {
            RuleFor(hc => hc.HeatChartNumber).NotEmpty()
                .WithMessage("Invalid Heat Chart Number");

            RuleFor(hc => hc.CustomerSelected).NotEmpty().When(hc => string.IsNullOrWhiteSpace(hc.CustomerSelected.Name))
                .WithMessage("Invalid Customer");

            RuleFor(hc => hc.ThirdPartyInspectionSelected).NotEmpty().When(hc => string.IsNullOrWhiteSpace(hc.ThirdPartyInspectionSelected.Name))
                .WithMessage("Invalid Third Party Inspection");

            RuleFor(hc => hc.NoOfEquipment).LessThan(1).WithMessage("No Of Equipment should not be less than 1");

            RuleFor(hc => hc.CreatedBy).NotEmpty()
                .WithMessage("Invalid CreatedBy");

            RuleFor(hc => hc.ModifiedBy).NotEmpty()
                .WithMessage("Invalid ModifiedBy");

            RuleFor(hc => hc.CreatedOn).NotNull()
                .WithMessage("Invalid CreatedOn");

            RuleFor(hc => hc.ModifiedOn).NotNull()
                .WithMessage("Invalid ModifiedOn");
        }
    }
}
