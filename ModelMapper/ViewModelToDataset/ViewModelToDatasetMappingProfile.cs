using AutoMapper;
using HeatChart.ViewModels.DatasetVM;
using HeatHeatChart.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.ViewModelToDataset
{
    public class ViewModelToDatasetMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDatasetMappingProfile"; }
        }

        public ViewModelToDatasetMappingProfile()
        {
            CreateMap<HeatChartHeaderVM, HeatChartHeaderDatasetVM>()
                .ForMember(vm => vm.Customer, map => map.MapFrom(m => m.CustomerSelected.Name))
                .ForMember(vm => vm.DrawingNumber, map => map.MapFrom(m => m.DrawingNumber))
                .ForMember(vm => vm.DrawingRevision, map => map.MapFrom(m => m.DrawingRevision))
                .ForMember(vm => vm.Equipment, map => map.MapFrom(m => m.CustomerPOEquipment))
                .ForMember(vm => vm.PurchaseOrder, map => map.MapFrom(m => m.CustomerPONumber))
                .ForMember(vm => vm.TagNumber, map => map.MapFrom(m => m.TagNumber))
                .ForMember(vm => vm.ThirdPartyInspectionName, map => map.MapFrom(m => m.ThirdPartyInspectionSelected.Name))
                ;

            CreateMap<HeatChartDetailsVM, HeatChartDetailsDatasetVM>()
                .ForMember(vm => vm.CTNumber, map => map.MapFrom(m => m.MaterialRegisterHeaderSelected.CTNumber))
                .ForMember(vm => vm.HeatChartDimension, map => map.MapFrom(m => m.DimensionSelected.Name))
                .ForMember(vm => vm.HeatChartSpecification, map => map.MapFrom(m => m.SpecificationSelected.Name))
                .ForMember(vm => vm.LabOrMillName, map => map.MapFrom(m => GetLabOrMillName(m.MaterialRegisterSubSeriesSelected.LabReport,    
                                                                        m.MaterialRegisterSubSeriesSelected.MillDetail)))
                .ForMember(vm => vm.LabOrMillTCNumber, map => map.MapFrom(m => GetLabOrMillTCNumber(m.MaterialRegisterSubSeriesSelected.LabReport,
                                                                        m.MaterialRegisterSubSeriesSelected.MillDetail)))
                .ForMember(vm => vm.LabOrMillTCDate, map => map.MapFrom(m => GetLabOrMillTCDate(m.MaterialRegisterSubSeriesSelected.LabReport,
                                                                        m.MaterialRegisterSubSeriesSelected.MillDetail)))
                .ForMember(vm => vm.MaterialRegisterDimension, map => map.MapFrom(m => m.MaterialRegisterHeaderSelected.DimensionSelected.Name))
                .ForMember(vm => vm.MaterialRegisterSpecification, map => map.MapFrom(m => m.MaterialRegisterHeaderSelected.SpecificationSelected.Name))
                .ForMember(vm => vm.PartNumber, map => map.MapFrom(m => m.PartNumber))
                .ForMember(vm => vm.SheetNo, map => map.MapFrom(m => m.SheetNo))
                ;
        }

        private string GetLabOrMillName(LabReportVM labReport, MillDetailVM millDetail)
        {
            if (millDetail != null && !string.IsNullOrWhiteSpace(millDetail.MillName) && !string.IsNullOrWhiteSpace(millDetail.TCNumber))
            {
                return millDetail.MillName;
            }

            if (labReport != null && !string.IsNullOrWhiteSpace(labReport.LabName) && !string.IsNullOrWhiteSpace(labReport.TCNumber))
            {
                return labReport.LabName;
            }

            return string.Empty;
        }

        private string GetLabOrMillTCNumber(LabReportVM labReport, MillDetailVM millDetail)
        {
            if (millDetail != null && !string.IsNullOrWhiteSpace(millDetail.MillName) && !string.IsNullOrWhiteSpace(millDetail.TCNumber))
            {
                return millDetail.TCNumber;
            }

            if (labReport != null && !string.IsNullOrWhiteSpace(labReport.LabName) && !string.IsNullOrWhiteSpace(labReport.TCNumber))
            {
                return labReport.TCNumber;
            }

            return string.Empty;
        }

        private string GetLabOrMillTCDate(LabReportVM labReport, MillDetailVM millDetail)
        {
            if (millDetail != null && !string.IsNullOrWhiteSpace(millDetail.MillName) && !string.IsNullOrWhiteSpace(millDetail.TCNumber))
            {
                return millDetail.TCDate.ToString("dd/MM/yyyy");
            }

            if (labReport != null && !string.IsNullOrWhiteSpace(labReport.LabName) && !string.IsNullOrWhiteSpace(labReport.TCNumber))
            {
                return labReport.TCDate.ToString("dd/MM/yyyy");
            }

            return string.Empty;
        }
    }
}
