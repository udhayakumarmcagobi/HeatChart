(function (app) {
    'use strict';

    app.controller('editHeatChartHeaderCtrl', editHeatChartHeaderCtrl);

    editHeatChartHeaderCtrl.$inject = ['$scope', '$location', '$modal', '$rootScope', '$routeParams', 'apiService', 'notificationService'];

    function editHeatChartHeaderCtrl($scope, $location, $modal, $rootScope, $routeParams, apiService, notificationService) {
        $scope.EditedHeatChartHeader = {};

        $scope.Update = Update;
        $scope.deleteHeatChartHeader = deleteHeatChartHeader;
        $scope.GenerateHeatChart = GenerateHeatChart;
        $scope.DownloadAllReports = DownloadAllReports;
        $scope.DownloadAll = DownloadAll;
        
        $scope.cancelEdit = cancelEdit;

        $scope.ngDropdownMultiselectSettings = {
            scrollableHeight: '200px',
            scrollable: true,
            enableSearch: true
        };

        $scope.openDatePicker = openDatePicker;
        $scope.AddHeatChartDetailsDialog = AddHeatChartDetailsDialog;
        $scope.EditHeatChartDetailsDialog = EditHeatChartDetailsDialog;
        $scope.DeleteHeatChartDetails = DeleteHeatChartDetails;

        $scope.ID = $routeParams.ID != undefined ? $routeParams.ID : '';

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.datepicker = {};

        var config = {
            params: {
                ID: $scope.ID
            }
        };

        //Material Register Subseries Create Load Starts
        if ($scope.ID != "") {
            apiService.get('/api/heatCharts/edit', config,
                    LoadHeatChartHeaderSucceded,
                    LoadHeatChartHeaderFailed);
        }
        else {
            $scope.EditedHeatChartHeader = $scope.CurrentHeatChartHeader;
        }

        function LoadHeatChartHeaderSucceded(result) {
            $scope.EditedHeatChartHeader = result.data;
        }

        function LoadHeatChartHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        //Material Register Create Load Ends

        // Update Material Registers Starts
        function Update() {
            $scope.EditedHeatChartHeader.ModifiedBy = $rootScope.repository.loggedUser.username;
            $scope.EditedHeatChartHeader.ModifiedOn = new Date();

            apiService.post('/api/heatCharts/update', $scope.EditedHeatChartHeader,
                UpdateHeatChartHeaderSucceded,
                UpdateHeatChartHeaderFailed);
        }

        function UpdateHeatChartHeaderSucceded(response) {
            var heatChartHeaderRegistered = response.data;
            notificationService.displaySuccess($scope.EditedHeatChartHeader.HeatChartNumber + ' has been successfully updated')
            $location.url("/heatcharts/edit/" + heatChartHeaderRegistered.ID);
        }
        function UpdateHeatChartHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        // Update Material Registers Ends

        // Delete Material Registers Ends

        function deleteHeatChartHeader() {
            apiService.post('/api/heatCharts/delete', $scope.EditedHeatChartHeader,
               DeleteHeatChartHeaderSucceded,
               DeleteHeatChartHeaderFailed);
        }
        function DeleteHeatChartHeaderSucceded(response) {
            notificationService.displaySuccess($scope.EditedHeatChartHeader.HeatChartNumber + ' has been deleted');
            $location.url("/heatcharts/");

            //$scope.myModalInstance.dismiss();

        }
        function DeleteHeatChartHeaderFailed(response) {
            notificationService.displayError(response.data);
            //$scope.myModalInstance.dismiss();
        }

        // Delete Register SubSeries Starts

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
            $scope.EditedHeatChartHeader.HeatChartDetails
                .splice($scope.EditedHeatChartHeader.HeatChartDetails.indexOf(heatChartDetail), 1);
        }

        // Heat Chart Details Ends

        // Generate Heat Chart Starts

        function GenerateHeatChart() {
            $scope.CurrentHeatChartHeader = $scope.EditedHeatChartHeader;
            $scope.myModalInstance = $modal.open({
                templateUrl: 'angular/templates/heatcharts/views/generateHeatChartHeaderModal.html',
                controller: 'generateHeatChartHeaderCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }
        // Generate Heat Chart Ends

        // Download Heat Chart Reports Starts

        function DownloadAllReports(heatChart) {
            $scope.CurrentHeatChartHeader = heatChart;
            $modal.open({
                templateUrl: 'angular/templates/heatcharts/views/downloadHeatChartReportModal.html',
                controller: 'downloadHeatChartReportCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }
        // Download Heat Chart Reports Ends

        // Download Heat Chart Details Sub Series Reports Starts

        function DownloadAll(materialSubSeries) {
            $scope.CurrentMaterialRegisterSubSeries = materialSubSeries;
            $modal.open({
                templateUrl: 'angular/templates/materialregisters/views/downloadMaterialSubSeriesReportModal.html',
                controller: 'downloadMaterialSubSeriesReportCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }
        // Download Heat Chart Details Sub Series Reports Ends

        $scope.OpenMaterialRegister = function (heatChartDetail) {
            $location.url("/materialregisters/edit/" + heatChartDetail.MaterialRegisterHeaderSelected.ID);
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datepicker.opened = true;
        };

        function cancelEdit() {
            $scope.isEnabled = false;
            //$scope.myModalInstance.close();
            //$modalInstance.dismiss();
        }


    }
})(angular.module('heatChart'));