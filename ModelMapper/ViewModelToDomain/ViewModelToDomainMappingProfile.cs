using AutoMapper;
using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using HeatHeatChart.ViewModels;
using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.ViewModelToDomain
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerVM, Customer>();
            CreateMap<SupplierVM, Supplier>();
            CreateMap<ThirdPartyInspectionVM, ThirdPartyInspection>();
            CreateMap<RawMaterialFormVM, RawMaterialForm>();
            CreateMap<SpecificationsVM, Specifications>();
            CreateMap<DimensionVM, Dimension>();
            CreateMap<LabReportVM, LabReport>();
            CreateMap<MillDetailVM, MillDetail>();
            CreateMap<TestVM, Test>();

            CreateMap<MaterialRegisterHeaderVM, MaterialRegisterHeader>()
                .ForMember(mr => mr.MaterialRegisterSubSeriess, map => map.MapFrom(m => m.MaterialRegisterSubSeriess))
                .ForMember(mr => mr.SupplierID, map => map.MapFrom(m => m.SupplierSelected.ID))
                .ForMember(mr => mr.Suppliers, map => map.Ignore())
                .ForMember(mr => mr.ThirdPartyInspectionID, map => map.MapFrom(m => m.ThirdPartyInspectionSelected.ID))
                .ForMember(mr => mr.ThirdPartyInspections, map => map.Ignore())
                .ForMember(mr => mr.SpecificationsID, map => map.MapFrom(m => m.SpecificationSelected.ID))
                .ForMember(mr => mr.Specification, map => map.Ignore())
                .ForMember(mr => mr.Dimension, map => map.MapFrom(m => m.Dimension))
                .ForMember(mr => mr.RawMaterialFormID, map => map.MapFrom(m => m.RawMaterialFormSelected.ID))
                .ForMember(mr => mr.RawMaterialForms, map => map.Ignore())
                .ForMember(mr => mr.StatusID, map => map.MapFrom(m => GetStatusID(m.Status)))
                .ForMember(mr => mr.Status, map => map.Ignore())
                ;

            CreateMap<MaterialRegisterSubSeriesVM, MaterialRegisterSubSeries>()
                .ForMember(mr => mr.LabReport, map => map.MapFrom(m =>  m.LabReport != null 
                                                                       && !string.IsNullOrWhiteSpace(m.LabReport.TCNumber) 
                                                                       && !string.IsNullOrWhiteSpace(m.LabReport.LabName) ? m.LabReport : null))
                .ForMember(mr => mr.MillDetail, map => map.MapFrom(m => m.MillDetail != null
                                                                       && !string.IsNullOrWhiteSpace(m.MillDetail.TCNumber)
                                                                       && !string.IsNullOrWhiteSpace(m.MillDetail.MillName) ? m.MillDetail : null))                
                .ForMember(mr => mr.StatusID, map => map.MapFrom(m => GetStatusID(m.Status)))
                .ForMember(mr => mr.Status, map => map.Ignore())
                .ForMember(mr => mr.MaterialRegisterSubSeriesTestRelationships, map => map.MapFrom(m => m.SelectedTests))
                .ForMember(mr => mr.MaterialRegisterFileDetails, map => map.MapFrom(m => m.MaterialRegisterFileDetails))
                ;                

            CreateMap<TestVM, MaterialRegisterSubseriesTestRelationship>()
                .ForMember(mr => mr.TestID, map => map.MapFrom(m => m.ID))
                .ForMember(mr => mr.ID, map => map.Ignore());

            CreateMap<HeatChartHeaderVM, HeatChartHeader>()
                .ForMember(hc => hc.HeatChartDetails, map => map.MapFrom(m => m.HeatChartDetails))
                .ForMember(hc => hc.CustomerID, map => map.MapFrom(m => m.CustomerSelected.ID))
                .ForMember(hc => hc.Customers, map => map.Ignore())
                .ForMember(hc => hc.ThirdPartyInspectionID, map => map.MapFrom(m => m.ThirdPartyInspectionSelected.ID))
                .ForMember(hc => hc.ThirdPartyInspections, map => map.Ignore())
                ;

            CreateMap<HeatChartDetailsVM, HeatChartDetails>()
                .ForMember(hc => hc.HeathChartMaterialHeaderRelationships, map => map.MapFrom(m => GetHeatChartMaterialHeaderRelationship(m.MaterialRegisterHeaderSelected, m.MaterialRegisterSubSeriesSelected)))
                .ForMember(hc => hc.SpecificationsID, map => map.MapFrom(m => m.SpecificationSelected.ID))
                .ForMember(hc => hc.Dimension, map => map.MapFrom(m => m.Dimension));

            CreateMap<MaterialRegisterFileDetailVM, MaterialRegisterFileDetail>();

            CreateMap<HeatChartMaterialHeaderRelationshipVM, HeatChartMaterialHeaderRelationship>()
                .ForMember(hc => hc.MaterialRegisterHeaderID, map => map.MapFrom(m => m.MaterialRegisterHeaderID))
                .ForMember(hc => hc.MaterialRegisterHeaders, map => map.Ignore())
                .ForMember(hc => hc.MaterialRegisterSubSeriesID, map => map.MapFrom(m => m.MaterialRegisterSubSeriesID))
                .ForMember(hc => hc.MaterialRegisterSubSeries, map => map.Ignore())
                .ForMember(hc => hc.ID, map => map.Ignore());
        }

        private int GetStatusID(string status)
        {
            int result  = 5;
            switch(status)
            {
                case  "Accepted":
                    result = 1;
                    break;
                case "Rejected":
                    result = 2;
                    break;
                case "Retest":
                    result = 3;
                    break;
                case "PartiallyAccepted":
                    result = 4;
                    break;
                default:
                    result = 5;
                    break;
            }

            return result; 
        }

        private Status GetStatus(string status)
        {
            return new Status()
            {
                ID = GetStatusID(status)
            };
        }

        private HeatChartMaterialHeaderRelationshipVM GetHeatChartMaterialHeaderRelationship(MaterialRegisterHeaderVM materialRegisterHeaderVM, MaterialRegisterSubSeriesVM materialRegisterSubSeriesVM)
        {
            return new HeatChartMaterialHeaderRelationshipVM()
            {
                MaterialRegisterHeaderID = materialRegisterHeaderVM.ID,
                MaterialRegisterSubSeriesID = materialRegisterSubSeriesVM.ID
            };
        }
    }
}
