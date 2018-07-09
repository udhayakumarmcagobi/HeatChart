(function (app) {
    'use strict';

    app.controller('createHeatChartDetailsCtrl', createHeatChartDetailsCtrl);

    createHeatChartDetailsCtrl.$inject = ['$scope', '$location', '$rootScope', '$modalInstance', '$timeout', '$routeParams',
                                                    'apiService', 'notificationService'];

    function createHeatChartDetailsCtrl($scope, $location, $rootScope, $modalInstance, $timeout, $routeParams, apiService, notificationService) {
        $scope.openDatePicker = openDatePicker;
        $scope.cancelEdit = cancelEdit;
        $scope.AddHeatChartDetails = AddHeatChartDetails;

        var heatChartDetails = [];
        
        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        $scope.newHeatChartDetails = {};
        $scope.newHeatChartDetails.MaterialRegisterSubSeries = [];

        $scope.ngDropdownMultiselectSettings = {
            scrollableHeight: '300px',
            scrollable: true,
            enableSearch: true
        };

        var config = {};

        //Heat Chart Details Load Starts

        apiService.get('/api/heatCharts/heatchartdetailscreate', config,
                LoadHeatChartDetailsSucceded,
                LoadHeatChartHeaderFailed);

        function LoadHeatChartDetailsSucceded(result) {
            $scope.newHeatChartDetails = result.data;
            $scope.newHeatChartDetails.MaterialRegisterSubSeries = [];
        }

        function LoadHeatChartHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        //Heat Chart Details Load Ends

        //Load sub series (cascading) Load Starts
        $scope.$watch('newHeatChartDetails.MaterialRegisterHeaderSelected', function (materialHeader) {
            if (materialHeader) {
                var subconfig = { params: { materialHeaderID: materialHeader.ID } };
                apiService.get('/api/materialregisters/filtersubseries', subconfig,
                    LoadMaterialSubSeriesSuccess,
                    LoadMaterialSubSeriesFailed);
            }
        });
        
        function LoadMaterialSubSeriesSuccess(result) {
            $scope.newHeatChartDetails.MaterialRegisterSubSeries = result.data;
        }

        function LoadMaterialSubSeriesFailed(response) {
            notificationService.displayError(response.data);
        }

        //Load sub series (cascading) Load Ends

        // Add Heat Chart Details Starts
        function AddHeatChartDetails() {
           
            $scope.heatChartDetails = $scope.newHeatChartHeader != undefined ? $scope.newHeatChartHeader.HeatChartDetails
                                        : $scope.EditedHeatChartHeader.HeatChartDetails;

            if ($scope.heatChartDetails == null || $scope.heatChartDetails.length == 0) {

                $scope.heatChartDetails = [];
                $scope.heatChartDetails.splice(0, 0, $scope.newHeatChartDetails);

                $scope.newHeatChartDetails = [];
            }
            else
            {
                if ($scope.newHeatChartDetails.MaterialRegisterHeaderSelected.CTNumber != "") {
                    $scope.heatChartDetails.push($scope.newHeatChartDetails);
                    $scope.newHeatChartDetails = [];
                }
            }

            if ($scope.newHeatChartHeader != undefined) {
                $scope.newHeatChartHeader.HeatChartDetails = $scope.heatChartDetails;
            }

            if ($scope.EditedHeatChartHeader != undefined) {
                $scope.EditedHeatChartHeader.HeatChartDetails = $scope.heatChartDetails;
            }

            cancelEdit();
        }         

        // Add Heat Chart Details Ends

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();
          
            $timeout(function () {
                $scope.datepicker.opened = true;
            });
        };

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

    }
})(angular.module('heatChart'));