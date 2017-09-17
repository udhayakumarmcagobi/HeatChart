(function (app) {
    'use strict';

    app.controller('createMaterialRegisterHeaderCtrl', createMaterialRegisterHeaderCtrl);

    createMaterialRegisterHeaderCtrl.$inject = ['$scope', '$location', '$modal', '$rootScope', '$routeParams',
                                            'apiService', 'notificationService', 'heatChartReportService', '$filter'];

    function createMaterialRegisterHeaderCtrl($scope, $location, $modal, $rootScope, $routeParams,
                                            apiService, notificationService, heatChartReportService, $filter) {
        $scope.newMaterialRegisterHeader = {};

        $scope.Create = Create;

        $scope.ngDropdownMultiselectSettings = {
            scrollableHeight: '200px',
            scrollable: true,
            enableSearch: true
        };

        $scope.openDatePicker = openDatePicker;
        $scope.AddSubSeriesDialog = AddSubSeriesDialog;
        $scope.EditSubSeriesDialog = EditSubSeriesDialog;
        $scope.DeleteSubSeries = DeleteSubSeries;
        $scope.UploadMaterialRegisterFiles = UploadMaterialRegisterFiles;
        $scope.GetProperFileNameWithFormat = GetProperFileNameWithFormat;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.datepicker = {};

        var config = {};

        //Material Register Subseries Create Load Starts

        apiService.get('/api/materialRegisters/create', config,
                LoadMaterialRegisterHeaderSucceded,
                LoadMaterialRegisterHeaderFailed);

        function LoadMaterialRegisterHeaderSucceded(result) {
            $scope.newMaterialRegisterHeader = result.data;
            $scope.newMaterialRegisterHeader.Quantity = "";
        }

        function LoadMaterialRegisterHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        //Material Register Create Load Ends

        // Create Material Registers Starts
        function Create() {
            $scope.newMaterialRegisterHeader.CreatedBy = $rootScope.repository.loggedUser.username;
            $scope.newMaterialRegisterHeader.ModifiedBy = $rootScope.repository.loggedUser.username;

            $scope.newMaterialRegisterHeader.CreatedOn = new Date();
            $scope.newMaterialRegisterHeader.ModifiedOn = new Date();
          
            UploadMaterialRegisterFiles();
            apiService.post('/api/materialRegisters/create', $scope.newMaterialRegisterHeader,
                registerMaterialRegisterHeaderSucceded,
                registerMaterialRegisterHeaderFailed);
           
        }

        function UploadMaterialRegisterFiles() {           
            if ($scope.newMaterialRegisterHeader.MaterialRegisterSubSeriess != null && $scope.newMaterialRegisterHeader.MaterialRegisterSubSeriess.length > 0) {
                var index = 0;
                $scope.newMaterialRegisterHeader.MaterialRegisterSubSeriess.forEach(function (materialSub) {

                    if (materialSub.MaterialRegisterFileDetailsCurrent != undefined && materialSub.MaterialRegisterFileDetailsCurrent.length > 0) {

                        var fileName = GetProperFileNameWithFormat(materialSub, materialSub.MaterialRegisterFileDetails[0].FileName);

                        materialSub.MaterialRegisterFileDetails[0].FileName = fileName;

                        heatChartReportService.uploadMaterialRegisterFile(materialSub.MaterialRegisterFileDetailsCurrent,
                            $scope.newMaterialRegisterHeader.CTNumber,
                            materialSub.SubSeriesNumber,
                            successFileUpload,
                            FailureFileUpload);
                    }
                    index = index + 1;
                });
            }
        }

        function successFileUpload(response) {
        }

        function FailureFileUpload(response) {
            notificationService.displayError('failed');
        }

        function registerMaterialRegisterHeaderSucceded(response) {
            var materialRegisterHeaderRegistered = response.data;
            notificationService.displaySuccess($scope.newMaterialRegisterHeader.CTNumber + ' has been successfully created')

            $location.url("/materialregisters/edit/" + materialRegisterHeaderRegistered.ID);
        }
        function registerMaterialRegisterHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        // Create Material Registers Ends

        // Material Register SubSeries Starts

        function AddSubSeriesDialog() {
            $modal.open({
                templateUrl: 'angular/templates/materialregisters/views/createMaterialRegisterSubseries.html',
                controller: 'createMaterialRegisterSubseriesCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function EditSubSeriesDialog(materialSubSeries) {
            $scope.EditedMaterialSubSeries = materialSubSeries;
            $modal.open({
                templateUrl: 'angular/templates/materialregisters/views/editMaterialRegisterSubseries.html',
                controller: 'editMaterialRegisterSubseriesCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function DeleteSubSeries(materialSubSeries) {
            $scope.newMaterialRegisterHeader.MaterialRegisterSubSeriess
                .splice($scope.newMaterialRegisterHeader.MaterialRegisterSubSeriess.indexOf(materialSubSeries), 1);
        }

        // Material Register SubSeries Ends

        // Construct selected Tests Starts

        $scope.ConstructSelectedTests = function (materialSub) {
            var testSelectedString = "";

            if (materialSub.SelectedTests.length > 0) {
                materialSub.SelectedTests.forEach(function (item) {                   
                    testSelectedString += item.Name + ", ";
                });

                testSelectedString = testSelectedString.trim().slice(0, -1);
            }
            return testSelectedString;
        };

        // Costruct selected Tests Ends

        // Construct selected File Name Starts

        $scope.ConstructSelectedFileNames = function (materialSub) {
            var selectedFileNames = "";

            if (materialSub.MaterialRegisterFileDetails.length > 0) {
                materialSub.MaterialRegisterFileDetails.forEach(function (item) {
                    selectedFileNames += item.FileName + ", ";
                });

                selectedFileNames = selectedFileNames.trim().slice(0, -1);
            }
            return selectedFileNames;
        };

        // Costruct selected File Name Ends

        //Construct File Name with proper format Starts

        function GetProperFileNameWithFormat(materialSub, selectedFileName) {
            var currentDate = $filter('date')(new Date(), "dd-MM-yyyy");
            return $scope.newMaterialRegisterHeader.CTNumber.replace("/", "") + "_" +
                                       materialSub.SubSeriesNumber.replace("/", "") + "_" +
                                       currentDate + "_" + selectedFileName;
        }
        //Construct File Name with proper format Ends

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datepicker.opened = true;
        };

    }
})(angular.module('heatChart'));