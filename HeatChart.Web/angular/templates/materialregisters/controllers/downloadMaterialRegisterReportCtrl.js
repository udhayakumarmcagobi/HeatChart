(function (app) {
    'use strict';

    app.controller('downloadMaterialRegisterReportCtrl', downloadMaterialRegisterReportCtrl);

    downloadMaterialRegisterReportCtrl.$inject = ['$scope', '$location', '$modalInstance', 'apiService', 'notificationService', 'heatChartReportService'];

    function downloadMaterialRegisterReportCtrl($scope, $location, $modalInstance, apiService, notificationService, heatChartReportService) {

        $scope.downloadMaterialRegisterFile = downloadMaterialRegisterFile;
        $scope.cancelEdit = cancelEdit;

        // Download Material Register Report Starts
        $scope.downloadMaterialRegisterReports = function () {

            if ($scope.CurrentMaterialRegisterHeader != undefined && $scope.CurrentMaterialRegisterHeader.MaterialRegisterSubSeriess.length > 0) {

                var isReportFound = false;
                angular.forEach($scope.CurrentMaterialRegisterHeader.MaterialRegisterSubSeriess, function (subSeries) {

                    if (subSeries != undefined &&
                        subSeries.MaterialRegisterFileDetails.length > 0) {

                        angular.forEach(subSeries.MaterialRegisterFileDetails, function (fileDetail) {
                            isReportFound = true;
                            downloadMaterialRegisterFile(fileDetail.FileName);
                        });

                    }
                });

                if (isReportFound == false) {
                    notificationService.displayError("There is no reports to download.");
                }
            }
            else {
                notificationService.displayError("There is no subseries associated. Hence, there is no reports to download");
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
        // Download Material Register Report Ends

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));