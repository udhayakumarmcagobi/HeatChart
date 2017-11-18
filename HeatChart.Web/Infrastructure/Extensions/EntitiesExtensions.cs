using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using HeatHeatChart.ViewModels;
using HeatHeatChart.ViewModels.Domain;
using ModelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeatChart.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        #region Master screens
        public static void UpdateCustomer(this Customer customer, CustomerVM customerVm)
        {
            customer.Name = customerVm.Name;
            customer.Address = customerVm.Address;
            customer.Landline = customerVm.Landline;
            customer.Mobile = customerVm.Mobile;
            customer.Email = customerVm.Email;
            customer.ModifiedBy = customerVm.ModifiedBy;
            customer.ModifiedOn = customerVm.ModifiedOn;
            customer.AdditionalDetails = customerVm.AdditionalDetails;
            customer.IsDeleted = customerVm.IsDeleted;
        }

        public static void UpdateSupplier(this Supplier supplier, SupplierVM supplierVm)
        {
            supplier.Name = supplierVm.Name;
            supplier.Address = supplierVm.Address;
            supplier.Landline = supplierVm.Landline;
            supplier.Mobile = supplierVm.Mobile;
            supplier.Email = supplierVm.Email;
            supplier.ModifiedBy = supplierVm.ModifiedBy;
            supplier.ModifiedOn = supplierVm.ModifiedOn;
            supplier.AdditionalDetails = supplierVm.AdditionalDetails;
            supplier.IsDeleted = supplierVm.IsDeleted;
        }

        public static void UpdateThirdPartyInspection(this ThirdPartyInspection thirdPartyInspection, ThirdPartyInspectionVM thirdPartyInspectionVm)
        {
            thirdPartyInspection.Name = thirdPartyInspectionVm.Name;
            thirdPartyInspection.Address = thirdPartyInspectionVm.Address;
            thirdPartyInspection.Landline = thirdPartyInspectionVm.Landline;
            thirdPartyInspection.Mobile = thirdPartyInspectionVm.Mobile;
            thirdPartyInspection.Email = thirdPartyInspectionVm.Email;
            thirdPartyInspection.ModifiedBy = thirdPartyInspectionVm.ModifiedBy;
            thirdPartyInspection.ModifiedOn = thirdPartyInspectionVm.ModifiedOn;
            thirdPartyInspection.AdditionalDetails = thirdPartyInspectionVm.AdditionalDetails;
            thirdPartyInspection.IsDeleted = thirdPartyInspectionVm.IsDeleted;
        }

        public static void UpdateRawMaterialForm(this RawMaterialForm rawMaterialForm, RawMaterialFormVM rawMaterialFormVm)
        {
            rawMaterialForm.Name = rawMaterialFormVm.Name;
            rawMaterialForm.Description = rawMaterialFormVm.Description;
            rawMaterialForm.ModifiedBy = rawMaterialFormVm.ModifiedBy;
            rawMaterialForm.ModifiedOn = rawMaterialFormVm.ModifiedOn;
            rawMaterialForm.IsDeleted = rawMaterialFormVm.IsDeleted;
        }

        public static void UpdateSpecifications(this Specifications specifications, SpecificationsVM specificationsVm)
        {
            specifications.Name = specificationsVm.Name;
            specifications.Description = specificationsVm.Description;
            specifications.ModifiedBy = specificationsVm.ModifiedBy;
            specifications.ModifiedOn = specificationsVm.ModifiedOn;
            specifications.IsDeleted = specificationsVm.IsDeleted;
        }

        public static void UpdateDimension(this Dimension dimension, DimensionVM dimensionVM)
        {
            dimension.Name = dimensionVM.Name;
            dimension.Description = dimensionVM.Description;
            dimension.ModifiedBy = dimensionVM.ModifiedBy;
            dimension.ModifiedOn = dimensionVM.ModifiedOn;
            dimension.IsDeleted = dimensionVM.IsDeleted;
        }

        public static void UpdateTest(this Test test, TestVM testVM)
        {
            test.Name = testVM.Name;
            test.Description = testVM.Description;
            test.ModifiedBy = testVM.ModifiedBy;
            test.ModifiedOn = testVM.ModifiedOn;
            test.IsDeleted = testVM.IsDeleted;
        }

        #endregion

        #region Material Register

        public static void UpdateMaterialRegisterHeader(this MaterialRegisterHeader materialRegisterHeader, MaterialRegisterHeaderVM materialRegisterHeaderVM)
        {
            var materialRegisterHeaderNew = AutoMapper.Map<MaterialRegisterHeaderVM, MaterialRegisterHeader>(materialRegisterHeaderVM);

            materialRegisterHeader.SupplierID = materialRegisterHeaderNew.SupplierID;
            materialRegisterHeader.ThirdPartyInspectionID = materialRegisterHeaderNew.ThirdPartyInspectionID;
            materialRegisterHeader.SpecificationsID = materialRegisterHeaderNew.SpecificationsID;
            materialRegisterHeader.Dimension = materialRegisterHeaderNew.Dimension;
            materialRegisterHeader.RawMaterialFormID = materialRegisterHeaderNew.RawMaterialFormID;

            //materialRegisterHeader.MaterialRegisterSubSeriess = materialRegisterHeaderNew.MaterialRegisterSubSeriess;

            materialRegisterHeader.ModifiedBy = materialRegisterHeaderNew.ModifiedBy;
            materialRegisterHeader.ModifiedOn = materialRegisterHeaderNew.ModifiedOn;

            materialRegisterHeader.SupplierPONumber = materialRegisterHeaderNew.SupplierPONumber;
            materialRegisterHeader.SupplierPODate = materialRegisterHeaderNew.SupplierPODate;

            materialRegisterHeader.StatusID = materialRegisterHeaderNew.StatusID;

            materialRegisterHeader.PartiallyAcceptedRemarks = materialRegisterHeaderNew.PartiallyAcceptedRemarks;
            materialRegisterHeader.OtherInfo = materialRegisterHeaderVM.OtherInfo;
            materialRegisterHeader.Quantity = materialRegisterHeaderVM.Quantity;

            materialRegisterHeader.IsDeleted = materialRegisterHeaderNew.IsDeleted;            

        }

        public static void UpdateMaterialRegisterSubSeries(this MaterialRegisterSubSeries materialRegisterSubSeries, MaterialRegisterSubSeriesVM materialRegisterSubSeriesVM)
        {
            var materialRegisterSubSeriesNew = AutoMapper.Map<MaterialRegisterSubSeriesVM, MaterialRegisterSubSeries>(materialRegisterSubSeriesVM);

            materialRegisterSubSeries.MaterialRegisterHeaderID = materialRegisterSubSeriesNew.MaterialRegisterHeaderID;
            materialRegisterSubSeries.StatusID = materialRegisterSubSeriesNew.StatusID;
            materialRegisterSubSeries.SubSeriesNumber = materialRegisterSubSeriesNew.SubSeriesNumber;
        }

        public static void UpdateMaterialSubSeriesTestRelationship(this MaterialRegisterSubseriesTestRelationship materialRegisterSubseriesTestRelationship, 
                                                                    TestVM testVM, int subSeriesID)
        {
            materialRegisterSubseriesTestRelationship.MaterialRegisterSubSeriesID = subSeriesID;
            materialRegisterSubseriesTestRelationship.TestID = testVM.ID;
        }

        public static void UpdateMillDetail(this MillDetail millDetail, MillDetailVM millDetailVM)
        {
            millDetail.Description = millDetailVM.Description;
            millDetail.HeatOrLotNumber = millDetailVM.HeatOrLotNumber;
            millDetail.MillName = millDetailVM.MillName;
            millDetail.ProductNumber = millDetailVM.ProductNumber;
            millDetail.TCDate = millDetailVM.TCDate;
            millDetail.TCNumber = millDetailVM.TCNumber;
        }

        public static void UpdateLabReport(this LabReport labReport, LabReportVM labReportVM)
        {
            labReport.Description = labReportVM.Description;
            labReport.LabName = labReportVM.LabName;
            labReport.SeqNum = labReportVM.SeqNum;
            labReport.TCDate = labReportVM.TCDate;
            labReport.TCNumber = labReportVM.TCNumber;
        }

        public static void UpdateMaterialRegisterFileDetails(this MaterialRegisterFileDetail materialRegisterFileDetail,
                                                            MaterialRegisterFileDetailVM materialRegisterFileDetailVM, int subSeriesID)
        {
            materialRegisterFileDetail.MaterialRegisterSubSeriesID = subSeriesID;
            materialRegisterFileDetail.FileName = materialRegisterFileDetailVM.FileName;
            materialRegisterFileDetail.Path = materialRegisterFileDetailVM.Path;
        }

        #endregion

        #region Heat Chart

        public static void UpdateHeatChartHeader(this HeatChartHeader heatChartHeader, HeatChartHeaderVM heatChartHeaderVM)
        {
            heatChartHeader.CreatedBy = heatChartHeaderVM.CreatedBy;
            heatChartHeader.CreatedOn = heatChartHeaderVM.CreatedOn;
            heatChartHeader.ModifiedBy = heatChartHeaderVM.ModifiedBy;
            heatChartHeader.ModifiedOn = heatChartHeaderVM.ModifiedOn;

            heatChartHeader.CustomerID = heatChartHeaderVM.CustomerSelected.ID;
            heatChartHeader.CustomerPOEquipment = heatChartHeaderVM.CustomerPOEquipment;
            heatChartHeader.CustomerPONumber = heatChartHeaderVM.CustomerPONumber;

            heatChartHeader.DrawingNumber = heatChartHeaderVM.DrawingNumber;
            heatChartHeader.DrawingRevision = heatChartHeaderVM.DrawingRevision;

            heatChartHeader.JobNumber = heatChartHeaderVM.JobNumber;
            heatChartHeader.TagNumber = heatChartHeaderVM.TagNumber;
            heatChartHeader.OtherInfo = heatChartHeaderVM.OtherInfo;
            heatChartHeader.Plant = heatChartHeaderVM.Plant;
            heatChartHeader.NoOfEquipment = heatChartHeaderVM.NoOfEquipment;

            heatChartHeader.ThirdPartyInspectionID = heatChartHeaderVM.ThirdPartyInspectionSelected.ID;
        }

        public static void UpdateHeatChartDetails(this HeatChartDetails heatChartDetails, HeatChartDetailsVM heatChartDetailsVM)
        {
            heatChartDetails.HeatChartHeaderID = heatChartDetailsVM.HeatChartHeaderID;
            heatChartDetails.PartNumber = heatChartDetailsVM.PartNumber;
            heatChartDetails.SheetNo = heatChartDetailsVM.SheetNo;
            heatChartDetails.SpecificationsID = heatChartDetailsVM.SpecificationSelected.ID;
            heatChartDetails.Dimension = heatChartDetailsVM.Dimension;
        }

        #endregion
    }
}