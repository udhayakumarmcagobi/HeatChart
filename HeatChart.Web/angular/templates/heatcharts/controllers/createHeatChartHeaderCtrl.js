(function (app) {
    'use strict';

    app.controller('createHeatChartHeaderCtrl', createHeatChartHeaderCtrl);

    createHeatChartHeaderCtrl.$inject = ['$scope', '$location', '$modal', '$rootScope', '$routeParams', 'apiService', 'notificationService'];

    function createHeatChartHeaderCtrl($scope, $location, $modal, $rootScope, $routeParams, apiService, notificationService) {
        $scope.newHeatChartHeader = {};

        $scope.Create = Create;

        $scope.ngDropdownMultiselectSettings = {
            scrollableHeight: '200px',
            scrollable: true,
            enableSearch: true
        };

        $scope.openDatePicker = openDatePicker;
        $scope.AddHeatChartDetailsDialog = AddHeatChartDetailsDialog;
        $scope.EditHeatChartDetailsDialog = EditHeatChartDetailsDialog;
        $scope.DeleteHeatChartDetails = DeleteHeatChartDetails;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.datepicker = {};

        var config = {};

        //Heat Chart Header Create Load Starts

        apiService.get('/api/heatCharts/create', config,
                LoadHeatChartHeaderSucceded,
                LoadHeatChartHeaderFailed);

        function LoadHeatChartHeaderSucceded(result) {
            $scope.newHeatChartHeader = result.data;
            $scope.newHeatChartHeader.NoOfEquipment = 1;
        }

        function LoadHeatChartHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        //Heat Chart Header Create Load Ends

        // Create Heat Chart Header Starts
        function Create() {
            $scope.newHeatChartHeader.CreatedBy = $rootScope.repository.loggedUser.username;
            $scope.newHeatChartHeader.ModifiedBy = $rootScope.repository.loggedUser.username;

            $scope.newHeatChartHeader.CreatedOn = new Date();
            $scope.newHeatChartHeader.ModifiedOn = new Date();

            apiService.post('/api/heatCharts/create', $scope.newHeatChartHeader,
                registerHeatChartHeaderSucceded,
                registerHeatChartHeaderFailed);
        }

        function registerHeatChartHeaderSucceded(response) {
            var heatChartHeaderCreated = response.data;
            notificationService.displaySuccess($scope.newHeatChartHeader.HeatChartNumber + ' has been successfully created')

            $location.url("/heatcharts/edit/" + heatChartHeaderCreated.ID);
        }
        function registerHeatChartHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        // Create Heat Chart Headers Ends

        // Heat Chart Details Starts

        function AddHeatChartDetailsDialog() {
            $modal.open({
                templateUrl: 'angular/templates/heatcharts/views/createHeatChartDetails.html',
                controller: 'createHeatChartDetailsCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function EditHeatChartDetailsDialog(heatChartDetail) {
            $scope.EditedHeatChartDetails = heatChartDetail;
            $modal.open({
                templateUrl: 'angular/templates/heatcharts/views/editHeatChartDetails.html',
                controller: 'editHeatChartDetailsCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function DeleteHeatChartDetails(heatChartDetail) {
            $scope.newHeatChartHeader.HeatChartDetails
                .splice($scope.newHeatChartHeader.HeatChartDetails.indexOf(heatChartDetail), 1);
        }

        // Heat Chart Details Ends

        $scope.OpenMaterialRegister = function (heatChartDetail) {
            $location.url("/materialregisters/edit/" + heatChartDetail.MaterialRegisterHeaderSelected.ID);
        }


        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datepicker.opened = true;
        };

    }
})(angular.module('heatChart'));