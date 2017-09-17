using AutoMapper;
using AutoMapper.Configuration;
using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using HeatHeatChart.ViewModels;
using HeatHeatChart.ViewModels.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ModelMapper.DomainToViewModel
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerVM>();
            CreateMap<Users, UserVM>()
                .ForMember(vm => vm.UserRoles, map => map.Ignore());

            CreateMap<Supplier, SupplierVM>()
                .ForMember(vm => vm.MaterialRegisterHeaders, map => map.Ignore());
            CreateMap<ThirdPartyInspection, ThirdPartyInspectionVM>()
                .ForMember(vm => vm.MaterialRegisterHeaders, map => map.Ignore());
            CreateMap<RawMaterialForm, RawMaterialFormVM>()
                .ForMember(vm => vm.MaterialRegisterHeaders, map => map.Ignore());
            CreateMap<Specifications, SpecificationsVM>()
                .ForMember(vm => vm.MaterialRegisterHeaders, map => map.Ignore())
                .ForMember(vm => vm.HeatChartDetails, map => map.Ignore());
            CreateMap<Dimension, DimensionVM>()
                .ForMember(vm => vm.MaterialRegisterHeaders, map => map.Ignore())
                .ForMember(vm => vm.HeatChartDetails, map => map.Ignore());
            CreateMap<Test, TestVM>();

            CreateMap<MaterialRegisterHeader, MaterialRegisterHeaderVM>()
                .ForMember(vm => vm.Suppliers, map => map.Ignore())
                .ForMember(vm => vm.ThirdPartyInspections, map => map.Ignore())
                .ForMember(vm => vm.Specifications, map => map.Ignore())
                .ForMember(vm => vm.SpecificationSelected, map => map.MapFrom(m => m.Specification))
                .ForMember(vm => vm.Dimensions, map => map.Ignore())
                .ForMember(vm => vm.DimensionSelected, map => map.MapFrom(m => m.Dimension))
                .ForMember(vm => vm.RawMaterialForms, map => map.Ignore())
                .ForMember(vm => vm.MaterialRegisterSubSeriess, map => map.Ignore())
                .ForMember(vm => vm.Status, map => map.MapFrom(m => m.Status.Name))
                ;

            CreateMap<MaterialRegisterSubSeries, MaterialRegisterSubSeriesVM>()
                .ForMember(vm => vm.LabReport, map => map.MapFrom(m => m.LabReport))
                .ForMember(vm => vm.MillDetail, map => map.MapFrom(m => m.MillDetail))
                .ForMember(vm => vm.Tests, map => map.Ignore())
                .ForMember(vm => vm.Status, map => map.MapFrom(m => m.Status.Name))
                .ForMember(vm => vm.ReportSelected, map => map.MapFrom(m => SelectedReport(m.MillDetail, m.LabReport)))
                .ForMember(vm => vm.SelectedTests, map => map.MapFrom(m => GetTestList(m.MaterialRegisterSubSeriesTestRelationships)))
                .ForMember(vm => vm.MaterialRegisterFileDetails, map => map.MapFrom(m => m.MaterialRegisterFileDetails)) 
                ;

            CreateMap<LabReport, LabReportVM>()
                .ForMember(vm => vm.MaterialRegisterSubSeries, map => map.Ignore())
                .ForMember(vm => vm.LabName, map => map.MapFrom(m => m.LabName))
                .ForMember(vm => vm.TCDate, map => map.MapFrom(m => m.TCDate))
                .ForMember(vm => vm.TCNumber, map => map.MapFrom(m => m.TCNumber));

            CreateMap<MillDetail, MillDetailVM>()
                .ForMember(vm => vm.MaterialRegisterSubSeries, map => map.Ignore())
                .ForMember(vm => vm.MillName, map => map.MapFrom(m => m.MillName))
                .ForMember(vm => vm.TCDate, map => map.MapFrom(m => m.TCDate))
                .ForMember(vm => vm.TCNumber, map => map.MapFrom(m => m.TCNumber));

            CreateMap<Test, TestVM>();

            CreateMap<MaterialRegisterFileDetail, MaterialRegisterFileDetailVM>()
                 .ForMember(vm => vm.FileName, map => map.MapFrom(m => m.FileName))
                 .ForMember(vm => vm.MaterialRegisterSubSeries, map => map.Ignore())
                 .ForMember(vm => vm.Path, map => map.MapFrom(m => m.Path))
                 .ForMember(vm => vm.ID, map => map.MapFrom(m => m.ID))
                ;

            CreateMap<HeatChartHeader, HeatChartHeaderVM>()
                .ForMember(vm => vm.Customers, map => map.Ignore())
                .ForMember(vm => vm.ThirdPartyInspections, map => map.Ignore())
                .ForMember(vm => vm.HeatChartDetails, map => map.Ignore())
                ;

            CreateMap<HeatChartDetails, HeatChartDetailsVM>()
                .ForMember(vm => vm.MaterialRegisterHeaderSelected, map => map.MapFrom(m => m.HeathChartMaterialHeaderRelationships.MaterialRegisterHeaders))
                .ForMember(vm => vm.MaterialRegisterSubSeriesSelected, map => map.MapFrom(m => m.HeathChartMaterialHeaderRelationships.MaterialRegisterSubSeries))
                .ForMember(vm => vm.MaterialRegisterHeaders, map => map.Ignore())
                .ForMember(vm => vm.MaterialRegisterSubSeries, map => map.Ignore())
                ;
        }

        private List<Test> GetTestList(ICollection<MaterialRegisterSubseriesTestRelationship> testRelationship)
        {
            if (testRelationship == null || !testRelationship.Any()) return new List<Test>();

            List<Test> selectedTest = new List<Test>();
            testRelationship.ToList().ForEach(x =>
            {
                selectedTest.Add(x.Test);
            });

            return selectedTest;
        }

        private string SelectedReport(MillDetail millDetail, LabReport labReport)
        {
            if (millDetail != null && !string.IsNullOrWhiteSpace(millDetail.MillName) && !string.IsNullOrWhiteSpace(millDetail.TCNumber))
            {
                return "MillDetails";
            }
            else
            {
                return "LabReports";
            }
        }

    }
}
