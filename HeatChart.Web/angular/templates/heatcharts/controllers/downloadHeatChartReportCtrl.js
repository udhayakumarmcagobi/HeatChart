(function (app) {
    'use strict';

    app.controller('downloadHeatChartReportCtrl', downloadHeatChartReportCtrl);

    downloadHeatChartReportCtrl.$inject = ['$scope', '$location', '$modalInstance', 'apiService', 'notificationService', 'heatChartReportService'];

    function downloadHeatChartReportCtrl($scope, $location, $modalInstance, apiService, notificationService, heatChartReportService) {

        $scope.downloadMaterialRegisterFile = downloadMaterialRegisterFile;
        $scope.cancelEdit = cancelEdit;

        $scope.HeatChart = [];      

        $scope.HeatChart = $scope.CurrentHeatChartHeader;

        // Download Heat Chart Report Starts
        $scope.downloadHeatChartReports = function() {

            if ($scope.CurrentHeatChartHeader != undefined && $scope.CurrentHeatChartHeader.HeatChartDetails.length > 0) {

                angular.forEach($scope.CurrentHeatChartHeader.HeatChartDetails, function (heatChartDetail) {

                    if (heatChartDetail.MaterialRegisterSubSeriesSelected != undefined &&
                        heatChartDetail.MaterialRegisterSubSeriesSelected.MaterialRegisterFileDetails.length > 0) {

                        angular.forEach(heatChartDetail.MaterialRegisterSubSeriesSelected.MaterialRegisterFileDetails, function (fileDetail) {
                            downloadMaterialRegisterFile(fileDetail.FileName);
                        });

                    }                   
                });
            }
            else {
                notificationService.displayError("There is no reports to download");
            }

            $modalInstance.dismiss();

        }

        function downloadMaterialRegisterFile(fileName) {
            heatChartReportService.downloadMaterialRegisterFile(fileName,
                            DownloadHeatChartHeaderSucceded,
                            DownloadHeatChartHeaderFailed);
        }
        function DownloadHeatChartHeaderSucceded(fileName) {
            notificationService.displaySuccess(fileName + " downloaded successfully");
        }
        function DownloadHeatChartHeaderFailed(fileName) {
            notificationService.displayError(fileName + " download failed");
        }
        // Download Heat Chart Report Ends

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));