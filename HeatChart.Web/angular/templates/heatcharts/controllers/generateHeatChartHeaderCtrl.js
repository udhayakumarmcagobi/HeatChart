(function (app) {
    'use strict';

    app.controller('generateHeatChartHeaderCtrl', generateHeatChartHeaderCtrl);

    generateHeatChartHeaderCtrl.$inject = ['$scope', '$location', '$modalInstance', 'apiService', 'notificationService', 'heatChartReportService'];

    function generateHeatChartHeaderCtrl($scope, $location, $modalInstance, apiService, notificationService, heatChartReportService) {

        $scope.generateHeatChartHeader = generateHeatChartHeader;
        $scope.cancelEdit = cancelEdit;

        // Generate Heat Chart Starts
        function generateHeatChartHeader() {

            heatChartReportService.generateHeatChart($scope.CurrentHeatChartHeader.ID,
               GenerateHeatChartHeaderSucceded,
               GenerateHeatChartHeaderFailed);
        }
        function GenerateHeatChartHeaderSucceded(response) {
            notificationService.displaySuccess("Heatchart generated and downloaded successfully");
            $modalInstance.dismiss();
        }
        function GenerateHeatChartHeaderFailed(response) {
            notificationService.displayError(response.data);
            $modalInstance.dismiss();
        }
        // Generate Heat Chart Ends

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));