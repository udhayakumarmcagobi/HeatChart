(function (app) {
    'use strict';

    app.controller('downloadMaterialSubSeriesReportCtrl', downloadMaterialSubSeriesReportCtrl);

    downloadMaterialSubSeriesReportCtrl.$inject = ['$scope', '$location', '$modalInstance', 'apiService', 'notificationService', 'heatChartReportService'];

    function downloadMaterialSubSeriesReportCtrl($scope, $location, $modalInstance, apiService, notificationService, heatChartReportService) {

        $scope.downloadMaterialRegisterFile = downloadMaterialRegisterFile;
        $scope.cancelEdit = cancelEdit;

        // Download Material SubSeries Report Starts
        $scope.downloadMaterialRegisterReports = function () {

            if ($scope.CurrentMaterialRegisterSubSeries != undefined &&
                $scope.CurrentMaterialRegisterSubSeries.MaterialRegisterFileDetails.length > 0) {

                angular.forEach($scope.CurrentMaterialRegisterSubSeries.MaterialRegisterFileDetails, function (fileDetail) {
                    downloadMaterialRegisterFile(fileDetail.FileName);
                });
            }

            else {
                notificationService.displayError("There is no reports to download");
            }

            $modalInstance.dismiss();

        }

        function downloadMaterialRegisterFile(fileName) {
            heatChartReportService.downloadMaterialRegisterFile(fileName,
                            DownloadMaterialRegisterHeaderSucceded,
                            DownloadMaterialRegisterHeaderFailed);
        }
        function DownloadMaterialRegisterHeaderSucceded(fileName) {
            notificationService.displaySuccess(fileName + " downloaded successfully");
        }
        function DownloadMaterialRegisterHeaderFailed(fileName) {
            notificationService.displayError(fileName + " download failed");
        }
        // Download Material SubSeries Report Ends

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));