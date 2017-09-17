(function (app) {
    'use strict';

    app.controller('editHeatChartDetailsCtrl', editHeatChartDetailsCtrl);

    editHeatChartDetailsCtrl.$inject = ['$scope', '$location', '$rootScope', '$modalInstance', '$timeout', '$routeParams',
                                                    'apiService', 'notificationService'];

    function editHeatChartDetailsCtrl($scope, $location, $rootScope, $modalInstance, $timeout, $routeParams, apiService, notificationService) {
        $scope.openDatePicker = openDatePicker;
        $scope.cancelEdit = cancelEdit;
        $scope.UpdateHeatChartDetails = UpdateHeatChartDetails;
        $scope.LoadMaterialSubSeries = LoadMaterialSubSeries;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

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
            $scope.EditedHeatChartDetails.MaterialRegisterHeaders = result.data.MaterialRegisterHeaders;
            LoadMaterialSubSeries($scope.EditedHeatChartDetails.MaterialRegisterHeaderSelected.ID);
            $scope.EditedHeatChartDetails.Specifications = result.data.Specifications;
        }

        function LoadHeatChartHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        //Heat Chart Details Load Ends

        //Load sub series (cascading) Load Starts
        $scope.$watch('EditedHeatChartDetails.MaterialRegisterHeaderSelected', function (materialHeader) {
            if (materialHeader) {
                LoadMaterialSubSeries(materialHeader.ID)
            }
        });

        function LoadMaterialSubSeries(materialRegisterHeaderID) {
            var subconfig = { params: { materialHeaderID: materialRegisterHeaderID } };
            apiService.get('/api/materialregisters/filtersubseries', subconfig,
                LoadMaterialSubSeriesSuccess,
                LoadMaterialSubSeriesFailed);
        }

        function LoadMaterialSubSeriesSuccess(result) {
            $scope.EditedHeatChartDetails.MaterialRegisterSubSeries = result.data;
        }

        function LoadMaterialSubSeriesFailed(response) {
            notificationService.displayError(response.data);
        }

        //Load sub series (cascading) Load Ends

        // Add Heat Chart Details Starts
        function UpdateHeatChartDetails() {
            $scope.isEnabled = false;
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