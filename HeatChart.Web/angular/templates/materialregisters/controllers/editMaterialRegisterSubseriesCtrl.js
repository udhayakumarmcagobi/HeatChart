(function (app) {
    'use strict';

    app.controller('editMaterialRegisterSubseriesCtrl', editMaterialRegisterSubseriesCtrl);

    editMaterialRegisterSubseriesCtrl.$inject = ['$scope', '$location', '$rootScope', '$modalInstance', '$timeout', '$routeParams',
                                                    'apiService', 'notificationService', 'heatChartReportService'];

    function editMaterialRegisterSubseriesCtrl($scope, $location, $rootScope, $modalInstance, $timeout,
        $routeParams, apiService, notificationService, heatChartReportService) {
        $scope.openDatePicker = openDatePicker;
        $scope.cancelEdit = cancelEdit;
        $scope.UpdateSubSeries = UpdateSubSeries;
        $scope.prepareFiles = prepareFiles;
        
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

        //Material Register Sub series Load Starts

        apiService.get('/api/materialRegisters/subseriescreate', config,
                LoadMaterialRegisterSubseriesSucceded,
                LoadMaterialRegisterHeaderFailed);

        function LoadMaterialRegisterSubseriesSucceded(result) {
            $scope.EditedMaterialSubSeries.Tests = result.data.Tests;

            if ($scope.EditedMaterialSubSeries.MillDetail != null) {
                $scope.EditedMaterialSubSeries.MillDetail.TCDate = new Date($scope.EditedMaterialSubSeries.MillDetail.TCDate);
            }
            if ($scope.EditedMaterialSubSeries.LabReport != null) {
                $scope.EditedMaterialSubSeries.LabReport.TCDate = new Date($scope.EditedMaterialSubSeries.LabReport.TCDate);
            }
        }

        function LoadMaterialRegisterHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        //Material Register Sub series Load Ends

        // Update Sub Series Starts
        function UpdateSubSeries() {
            $scope.materialRegisterSubSeries = $scope.newMaterialRegisterHeader != undefined ? $scope.newMaterialRegisterHeader.MaterialRegisterSubSeriess
                                       : $scope.EditedMaterialRegisterHeader.MaterialRegisterSubSeriess;
         
            $scope.isEnabled = false;
            cancelEdit();
        }

        function prepareFiles($files) {
            $scope.EditedMaterialSubSeries.MaterialRegisterFileDetailsCurrent = $files;

            var materialRegisterFile = {};
            materialRegisterFile.FileName = $files[0].name;
            $scope.EditedMaterialSubSeries.MaterialRegisterFileDetails = [];
            $scope.EditedMaterialSubSeries.MaterialRegisterFileDetails.push(materialRegisterFile);
        }

        // Add Sub Series Ends

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