(function (app) {
    'use strict';

    app.controller('editMaterialRegisterHeaderCtrl', editMaterialRegisterHeaderCtrl);

    editMaterialRegisterHeaderCtrl.$inject = ['$scope', '$location', '$modal', '$rootScope', '$routeParams',
                                            'apiService', 'notificationService', 'heatChartReportService', '$filter'];

    function editMaterialRegisterHeaderCtrl($scope, $location, $modal, $rootScope, $routeParams,
                                            apiService, notificationService, heatChartReportService, $filter) {
        $scope.EditedMaterialRegisterHeader = {};

        $scope.Update = Update;
        $scope.deleteMaterialRegisterHeader = deleteMaterialRegisterHeader;
        $scope.cancelEdit = cancelEdit;

        $scope.ngDropdownMultiselectSettings = {
            scrollableHeight: '200px',
            scrollable: true,
            enableSearch: true
        };

        $scope.openDatePicker = openDatePicker;
        $scope.AddSubSeriesDialog = AddSubSeriesDialog;
        $scope.EditSubSeriesDialog = EditSubSeriesDialog;
        $scope.DeleteSubSeries = DeleteSubSeries;
        $scope.DownloadAllReports = DownloadAllReports;
        $scope.UploadMaterialRegisterFiles = UploadMaterialRegisterFiles;
        $scope.GetProperFileNameWithFormat = GetProperFileNameWithFormat;

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
            apiService.get('/api/materialRegisters/edit', config,
                    LoadMaterialRegisterHeaderSucceded,
                    LoadMaterialRegisterHeaderFailed);
        }
        else {
            $scope.EditedMaterialRegisterHeader = $scope.CurrentMaterialRegisterHeader;
            $scope.EditedMaterialRegisterHeader.SupplierPODate = new Date($scope.EditedMaterialRegisterHeader.SupplierPODate);
        }

        function LoadMaterialRegisterHeaderSucceded(result) {
            $scope.EditedMaterialRegisterHeader = result.data;
            $scope.EditedMaterialRegisterHeader.SupplierPODate = new Date($scope.EditedMaterialRegisterHeader.SupplierPODate);
        }

        function LoadMaterialRegisterHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        //Material Register Create Load Ends

        // Update Material Registers Starts
        function Update() {
            $scope.EditedMaterialRegisterHeader.ModifiedBy = $rootScope.repository.loggedUser.username;
            $scope.EditedMaterialRegisterHeader.ModifiedOn = new Date();

            UploadMaterialRegisterFiles();

            apiService.post('/api/materialRegisters/update', $scope.EditedMaterialRegisterHeader,
                UpdateMaterialRegisterHeaderSucceded,
                UpdateMaterialRegisterHeaderFailed);
        }

        function UpdateMaterialRegisterHeaderSucceded(response) {
            var materialRegisterHeaderRegistered = response.data;
            notificationService.displaySuccess($scope.EditedMaterialRegisterHeader.CTNumber + ' has been successfully updated')
            $location.url("/materialregisters/edit/" + materialRegisterHeaderRegistered.ID);
        }
        function UpdateMaterialRegisterHeaderFailed(response) {
            notificationService.displayError(response.data);
        }

        // Update Material Registers Ends

        // File upload
        function UploadMaterialRegisterFiles() {
            if ($scope.EditedMaterialRegisterHeader.MaterialRegisterSubSeriess.length > 0) {
                var index = 0;
                $scope.EditedMaterialRegisterHeader.MaterialRegisterSubSeriess.forEach(function (materialSub) {

                    if (materialSub.MaterialRegisterFileDetailsCurrent != undefined && materialSub.MaterialRegisterFileDetailsCurrent.length > 0) {

                        var fileName = GetProperFileNameWithFormat(materialSub, materialSub.MaterialRegisterFileDetails[0].FileName);
                        materialSub.MaterialRegisterFileDetails[0].FileName = fileName;

                        heatChartReportService.uploadMaterialRegisterFile(materialSub.MaterialRegisterFileDetailsCurrent,
                            $scope.EditedMaterialRegisterHeader.CTNumber,
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

        // File upload

        // Delete Material Registers Ends

        function deleteMaterialRegisterHeader() {
            apiService.post('/api/materialRegisters/delete', $scope.EditedMaterialRegisterHeader,
               DeleteMaterialRegisterHeaderSucceded,
               DeleteMaterialRegisterHeaderFailed);
        }
        function DeleteMaterialRegisterHeaderSucceded(response) {
            notificationService.displaySuccess($scope.EditedMaterialRegisterHeader.CTNumber + ' has been deleted');
            $location.url("/materialregisters/");

            //$scope.myModalInstance.dismiss();

        }
        function DeleteMaterialRegisterHeaderFailed(response) {
            notificationService.displayError(response.data);
            //$scope.myModalInstance.dismiss();
        }

        // Delete Register SubSeries Starts

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
            $scope.EditedMaterialRegisterHeader.MaterialRegisterSubSeriess
                .splice($scope.EditedMaterialRegisterHeader.MaterialRegisterSubSeriess.indexOf(materialSubSeries), 1);
        }

        // Material Register SubSeries Ends

        // Download Material Register Reports Starts

        function DownloadAllReports(materialRegisterHeader) {
            $scope.CurrentMaterialRegisterHeader = materialRegisterHeader;
            $modal.open({
                templateUrl: 'angular/templates/materialregisters/views/downloadMaterialRegisterReportModal.html',
                controller: 'downloadMaterialRegisterReportCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }
        // Download Material Register Reports Ends

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
            return $scope.EditedMaterialRegisterHeader.CTNumber.replace("/", "") + "_" +
                                       materialSub.SubSeriesNumber.replace("/", "") + "_" +
                                       currentDate + "_" + selectedFileName;
        }
        //Construct File Name with proper format Ends

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