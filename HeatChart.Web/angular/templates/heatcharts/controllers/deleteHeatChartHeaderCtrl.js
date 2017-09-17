(function (app) {
    'use strict';

    app.controller('deleteHeatChartHeaderCtrl', deleteHeatChartHeaderCtrl);

    deleteHeatChartHeaderCtrl.$inject = ['$scope', '$location', '$modalInstance', 'apiService', 'notificationService'];

    function deleteHeatChartHeaderCtrl($scope, $location, $modalInstance, apiService, notificationService) {

        $scope.deleteHeatChartHeader = deleteHeatChartHeader;
        $scope.cancelEdit = cancelEdit;

        // Delete Heat Chart Starts
        function deleteHeatChartHeader() {
            apiService.post('/api/heatCharts/delete', $scope.CurrentHeatChartHeader,
               DeleteHeatChartHeaderSucceded,
               DeleteHeatChartHeaderFailed);
        }
        function DeleteHeatChartHeaderSucceded(response) {
            notificationService.displaySuccess($scope.CurrentHeatChartHeader.HeatChartNumber + ' has been deleted');

            $modalInstance.dismiss();
            $location.url("/heatcharts/");
        }
        function DeleteHeatChartHeaderFailed(response) {
            notificationService.displayError(response.data);
            $modalInstance.dismiss();
        }

        // Delete Heat Chart Ends

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));