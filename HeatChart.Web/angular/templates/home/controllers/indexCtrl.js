(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', '$location', 'apiService', 'notificationService'];

    function indexCtrl($scope, $location, apiService, notificationService) {
        $scope.pageClass = "page-home";
        $scope.loadingMaterialRegisters = true;
        $scope.loadingHeatCharts = true;
        $scope.isReadOnly = true;

        $scope.recentMaterialRegisters = [];
        $scope.recentHeatCharts = [];

        apiService.get("/api/materialregisters/recent", null,
            materialRegisterLoadCompleted,
            materialRegisterLoadFailed);

        function materialRegisterLoadCompleted(result) {
            $scope.recentMaterialRegisters = result.data;
            $scope.loadingMaterialRegisters = false;
        }

        function materialRegisterLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        apiService.get("/api/heatcharts/recent", null,
            heatChartLoadCompleted,
            heatChartLoadFailed);

        function heatChartLoadCompleted(result) {
            $scope.recentHeatCharts = result.data;
            $scope.loadingHeatCharts = false;
        }

        function heatChartLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.editMaterialRegister = function (materialRegister) {
            $location.url("/materialregisters/edit/" + materialRegister.ID);
        }

        $scope.editHeatChart = function (heatChart) {
            $location.url("/heatcharts/edit/" + heatChart.ID);
        }
    }

})(angular.module('heatChart'));