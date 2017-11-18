using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using HeatHeatChart.ViewModels;
using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.DomainToViewModel
{
    public class DomainToViewModelCustomMapper
    {
        #region Material Registers

        public static List<MaterialRegisterHeaderVM> MapMaterialRegisterHeaders(List<MaterialRegisterHeader> materialRegisterHeaders)
        {
            if (materialRegisterHeaders == null || !materialRegisterHeaders.Any()) return new List<MaterialRegisterHeaderVM>();

            var materialRegisterHeaderVMs = new List<MaterialRegisterHeaderVM>();

            materialRegisterHeaders.ForEach(x =>
            {
                materialRegisterHeaderVMs.Add(MapMaterialRegisterHeader(x));
            });

            return materialRegisterHeaderVMs;
        }

       public static MaterialRegisterHeaderVM MapMaterialRegisterHeader(MaterialRegisterHeader materialRegisterHeader)
        {
            var materialRegisterHeaderVM =  AutoMapper.Map<MaterialRegisterHeader, MaterialRegisterHeaderVM>(materialRegisterHeader);

            materialRegisterHeaderVM.MaterialRegisterSubSeriess = MapMaterialRegisterSubSeriesList(materialRegisterHeader.MaterialRegisterSubSeriess.ToList());

            materialRegisterHeaderVM.SupplierSelected = AutoMapper.Map<Supplier, SupplierVM>(materialRegisterHeader.Suppliers);
            materialRegisterHeaderVM.ThirdPartyInspectionSelected = AutoMapper.Map<ThirdPartyInspection, ThirdPartyInspectionVM>(materialRegisterHeader.ThirdPartyInspections);
            materialRegisterHeaderVM.SpecificationSelected = AutoMapper.Map<Specifications, SpecificationsVM>(materialRegisterHeader.Specification);
            materialRegisterHeaderVM.Dimension = materialRegisterHeader.Dimension;
            materialRegisterHeaderVM.RawMaterialFormSelected = AutoMapper.Map<RawMaterialForm, RawMaterialFormVM>(materialRegisterHeader.RawMaterialForms);

            return materialRegisterHeaderVM;
        }

        public static List<MaterialRegisterSubSeriesVM> MapMaterialRegisterSubSeriesList(List<MaterialRegisterSubSeries> materialRegisterSubseries)
        {
            if (materialRegisterSubseries == null || !materialRegisterSubseries.Any()) return new List<MaterialRegisterSubSeriesVM>();

            var materialRegisterSubseriesVMs = new List<MaterialRegisterSubSeriesVM>();

            materialRegisterSubseries.ForEach(x =>
            {
                materialRegisterSubseriesVMs.Add(MapMaterialRegisterSubSeries(x));
            });

            return materialRegisterSubseriesVMs;
        }

        public static MaterialRegisterSubSeriesVM MapMaterialRegisterSubSeries(MaterialRegisterSubSeries materialRegisterSubSeries)
        {
            var materialRegisterSubSeriesVM = AutoMapper.Map<MaterialRegisterSubSeries, MaterialRegisterSubSeriesVM>(materialRegisterSubSeries);

            return materialRegisterSubSeriesVM;
        }

        #endregion

        #region Heat Charts

        public static List<HeatChartHeaderVM> MapHeatChartHeaders(List<HeatChartHeader> heatChartHeaders)
        {
            if (heatChartHeaders == null || !heatChartHeaders.Any()) return new List<HeatChartHeaderVM>();

            var heatChartHeaderVMs = new List<HeatChartHeaderVM>();

            heatChartHeaders.ForEach(x =>
            {
                heatChartHeaderVMs.Add(MapHeatChartHeader(x));
            });

            return heatChartHeaderVMs;
        }

        public static HeatChartHeaderVM MapHeatChartHeader(HeatChartHeader heatChartHeader)
        {
            var heatChartHeaderVM = AutoMapper.Map<HeatChartHeader, HeatChartHeaderVM>(heatChartHeader);

            heatChartHeaderVM.HeatChartDetails = MapHeatChartDetailsList(heatChartHeader.HeatChartDetails.ToList());

            heatChartHeaderVM.CustomerSelected = AutoMapper.Map<Customer, CustomerVM>(heatChartHeader.Customers);            
            heatChartHeaderVM.ThirdPartyInspectionSelected = AutoMapper.Map<ThirdPartyInspection, ThirdPartyInspectionVM>(heatChartHeader.ThirdPartyInspections);            

            return heatChartHeaderVM;
        }

        public static List<HeatChartDetailsVM> MapHeatChartDetailsList(List<HeatChartDetails> heatChartDetails)
        {
            if (heatChartDetails == null || !heatChartDetails.Any()) return new List<HeatChartDetailsVM>();

            var heatChartDetailsVMs = new List<HeatChartDetailsVM>();

            heatChartDetails.ForEach(x =>
            {
                heatChartDetailsVMs.Add(MapHeatChartDetail(x));
            });

            return heatChartDetailsVMs;
        }

        public static HeatChartDetailsVM MapHeatChartDetail(HeatChartDetails heatChartDetail)
        {            
            var heatChartDetailVM = AutoMapper.Map<HeatChartDetails, HeatChartDetailsVM>(heatChartDetail);

            heatChartDetailVM.SpecificationSelected = AutoMapper.Map<Specifications, SpecificationsVM>(heatChartDetail.Specification);
            heatChartDetailVM.Dimension = heatChartDetail.Dimension;

            return heatChartDetailVM;
        }

        #endregion


    }
}
