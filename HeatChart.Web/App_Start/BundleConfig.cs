using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HeatChart.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/angular/vendors/modernizr.js"));

            // Vendor angular
            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/angular/vendors/jquery.js",
                "~/angular/vendors/bootstrap.js",
                "~/angular/vendors/toastr.js",
                "~/angular/vendors/jquery.raty.js",
                "~/angular/vendors/respond.src.js",
                "~/angular/vendors/angular.js",
                "~/angular/vendors/angular-route.js",
                "~/angular/vendors/angular-cookies.js",
                "~/angular/vendors/angular-validator.js",
                "~/angular/vendors/angular-base64.js",
                "~/angular/vendors/angular-file-upload.js",
                "~/angular/vendors/angucomplete-alt.min.js",
                "~/angular/vendors/ui-bootstrap-tpls-0.13.1.js",
                "~/angular/vendors/underscore.js",
                "~/angular/vendors/raphael.js",
                "~/angular/vendors/morris.js",
                "~/angular/vendors/jquery.fancybox.js",
                "~/angular/vendors/jquery.fancybox-media.js",
                "~/angular/vendors/lodash.min.js",
                "~/angular/vendors/loading-bar.js"));

            // Angular scripts
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                //Core
                "~/angular/app.js",
                "~/angular/modules/common.core.js",
                "~/angular/modules/common.ui.js",
                //Service Javascripts
                "~/angular/services/apiService.js",
                "~/angular/services/beforeUnload.js",
                "~/angular/services/membershipService.js",
                "~/angular/services/notificationService.js",
                "~/angular/services/heatChartReportService.js",
                "~/angular/services/dateHandlingService.js",
                //Directives
                "~/angular/directives/sideBar.directive.js",
                "~/angular/directives/topBar.directive.js",
                "~/angular/directives/ngCustomPager.directive.js",
                "~/angular/directives/ngCustomSelect.directive.js",
                "~/angular/directives/ngDropdownMultiselect.directive.js",
                //Root and Index               
                "~/angular/rootCtrl.js",
                "~/angular/templates/home/controllers/indexCtrl.js",
                // Account Javascripts
                "~/angular/templates/account/controllers/loginCtrl.js",
                "~/angular/templates/account/controllers/registerCtrl.js",
                "~/angular/templates/account/controllers/usersCtrl.js",
                "~/angular/templates/account/controllers/editUserCtrl.js",
                "~/angular/templates/account/controllers/userChangePasswordCtrl.js",                
                //Customer Javascripts
                "~/angular/templates/customers/controllers/customersCtrl.js",
                "~/angular/templates/customers/controllers/createCustomerCtrl.js",
                "~/angular/templates/customers/controllers/editCustomerCtrl.js",
                //Supplier Javascripts
                "~/angular/templates/suppliers/controllers/suppliersCtrl.js",
                "~/angular/templates/suppliers/controllers/createSupplierCtrl.js",
                "~/angular/templates/suppliers/controllers/editSupplierCtrl.js",
                //Third Party Inspections Javascripts
                "~/angular/templates/thirdpartyinspections/controllers/thirdPartyInspectionsCtrl.js",
                "~/angular/templates/thirdpartyinspections/controllers/createThirdPartyInspectionCtrl.js",
                "~/angular/templates/thirdpartyinspections/controllers/editThirdPartyInspectionCtrl.js",
                //Raw Material Form Javascripts
                "~/angular/templates/rawmaterialforms/controllers/rawMaterialFormsCtrl.js",
                "~/angular/templates/rawmaterialforms/controllers/createRawMaterialFormCtrl.js",
                "~/angular/templates/rawmaterialforms/controllers/editRawMaterialFormCtrl.js",
                //Specifications Javascripts
                "~/angular/templates/specifications/controllers/specificationsCtrl.js",
                "~/angular/templates/specifications/controllers/createSpecificationCtrl.js",
                "~/angular/templates/specifications/controllers/editSpecificationCtrl.js",
                //Dimensions Javascripts
                "~/angular/templates/dimensions/controllers/dimensionsCtrl.js",
                "~/angular/templates/dimensions/controllers/createDimensionCtrl.js",
                "~/angular/templates/dimensions/controllers/editDimensionCtrl.js",
                //Test Javascripts
                "~/angular/templates/tests/controllers/testsCtrl.js",
                "~/angular/templates/tests/controllers/createTestCtrl.js",
                "~/angular/templates/tests/controllers/editTestCtrl.js",
                //Material Register Javascripts
                "~/angular/templates/materialregisters/controllers/materialRegisterHeadersCtrl.js",
                "~/angular/templates/materialregisters/controllers/createMaterialRegisterHeaderCtrl.js",
                "~/angular/templates/materialregisters/controllers/editMaterialRegisterHeaderCtrl.js",
                "~/angular/templates/materialregisters/controllers/deleteMaterialRegisterHeaderCtrl.js",                
                "~/angular/templates/materialregisters/controllers/createMaterialRegisterSubseriesCtrl.js",
                "~/angular/templates/materialregisters/controllers/editMaterialRegisterSubseriesCtrl.js",
                "~/angular/templates/materialregisters/controllers/downloadMaterialRegisterReportCtrl.js",
                "~/angular/templates/materialregisters/controllers/downloadMaterialSubSeriesReportCtrl.js",                
                //Heat Chart Javascripts
                "~/angular/templates/heatcharts/controllers/heatChartHeadersCtrl.js",
                "~/angular/templates/heatcharts/controllers/createHeatChartHeaderCtrl.js",
                "~/angular/templates/heatcharts/controllers/editHeatChartHeaderCtrl.js",
                "~/angular/templates/heatcharts/controllers/deleteHeatChartHeaderCtrl.js",
                "~/angular/templates/heatcharts/controllers/generateHeatChartHeaderCtrl.js",
                "~/angular/templates/heatcharts/controllers/downloadHeatChartReportCtrl.js",
                "~/angular/templates/heatcharts/controllers/createHeatChartDetailsCtrl.js",
                "~/angular/templates/heatcharts/controllers/editHeatChartDetailsCtrl.js"
                ));

          // Css 
          bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/content/css/site.css",
                "~/content/css/custom-select.css",
                "~/content/css/bootstrap.css",
                "~/content/css/bootstrap-theme.css",
                "~/content/css/font-awesome.css",
                "~/content/css/morris.css",
                "~/content/css/toastr.css",
                "~/content/css/jquery.fancybox.css",
                "~/content/css/loading-bar.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}