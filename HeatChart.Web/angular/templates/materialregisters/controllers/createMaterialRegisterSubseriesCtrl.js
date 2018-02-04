(function (app) {
    'use strict';

    app.controller('createMaterialRegisterSubseriesCtrl', createMaterialRegisterSubseriesCtrl);

    createMaterialRegisterSubseriesCtrl.$inject = ['$scope', '$location', '$rootScope', '$modalInstance', '$timeout', '$routeParams',
                                                    'apiService', 'notificationService', 'heatChartReportService'];

    function createMaterialRegisterSubseriesCtrl($scope, $location, $rootScope, $modalInstance,
        $timeout, $routeParams, apiService, notificationService, heatChartReportService) {
        $scope.openDatePicker = openDatePicker;
        $scope.cancelEdit = cancelEdit;
        $scope.AddSubSeries = AddSubSeries;
        $scope.prepareFiles = prepareFiles;

        var materialRegisterSubSeries = [];
        
        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        $scope.newMaterialSubSeries = {};
        $scope.newMaterialSubSeries.SelectedTests = [];
        $scope.newMaterialSubSeries.MaterialRegisterFileDetails = [];

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
            $scope.newMaterialSubSeries = result.data;
            $scope.newMaterialSubSeries.SelectedTests = [];
            $scope.newMaterialSubSeries.MaterialRegisterFileDetails = [];
        }

        function LoadMaterialRegisterHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        //Material Register Sub series Load Ends

        // Add Sub Series Starts
        function AddSubSeries() {
            $scope.materialRegisterSubSeries = $scope.newMaterialRegisterHeader != undefined ? $scope.newMaterialRegisterHeader.MaterialRegisterSubSeriess
                                        : $scope.EditedMaterialRegisterHeader.MaterialRegisterSubSeriess;

            if ($scope.materialRegisterSubSeries == null || $scope.materialRegisterSubSeries.length == 0) {

                $scope.materialRegisterSubSeries = [];
                $scope.materialRegisterSubSeries.splice(0, 0, $scope.newMaterialSubSeries);

                $scope.newMaterialSubSeries = [];
            }
            else
            {
                $scope.materialRegisterSubSeries.push($scope.newMaterialSubSeries);
                $scope.newMaterialSubSeries = [];
            }

            if ($scope.newMaterialRegisterHeader != undefined) {
                $scope.newMaterialRegisterHeader.MaterialRegisterSubSeriess = $scope.materialRegisterSubSeries;
            }

            if ($scope.EditedMaterialRegisterHeader != undefined) {
                $scope.EditedMaterialRegisterHeader.MaterialRegisterSubSeriess = $scope.materialRegisterSubSeries;
            }

            cancelEdit();
        }
         
        function prepareFiles($files) {
            $scope.newMaterialSubSeries.MaterialRegisterFileDetailsCurrent = $files;

            var materialRegisterFile = {};
            materialRegisterFile.FileName = $files[0].name;
            $scope.newMaterialSubSeries.MaterialRegisterFileDetails = [];
            $scope.newMaterialSubSeries.MaterialRegisterFileDetails.push(materialRegisterFile);
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