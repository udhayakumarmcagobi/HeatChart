(function (app) {
    'use strict';

    app.controller('materialRegisterHeadersCtrl', materialRegisterHeadersCtrl);

    materialRegisterHeadersCtrl.$inject = ['$scope', '$modal', '$location', '$routeParams',
                                           'apiService', 'notificationService', 'heatChartReportService'];

    function materialRegisterHeadersCtrl($scope, $modal, $location, $routeParams,
                                        apiService, notificationService, heatChartReportService) {
        $scope.pageClass = 'page-materialRegisterHeaders';
        $scope.loadingMaterialRegisterHeaders = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.MaterialRegisterHeaders = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.editMaterialRegisterHeader = editMaterialRegisterHeader;
        $scope.openDeleteDialog = openDeleteDialog;
        $scope.DownloadAllReports = DownloadAllReports;
        

        $scope.filterMaterialRegisterHeaders = $routeParams.CTNumber != undefined ? $routeParams.CTNumber : '';

        function search(page) {
            page = page || 0;
            $scope.loadingMaterialRegisterHeaders = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterMaterialRegisterHeaders } };
            apiService.get('/api/materialregisters/search/', config, materialRegisterHeadersLoadCompleted, materialRegisterHeadersLoadFailed);
        }

        function addNew() {
            window.location.href = "#/materialregisters/create";
        }

        function editMaterialRegisterHeader(materialRegister) {
            $location.url("/materialregisters/edit/" + materialRegister.ID);
        }

        function openDeleteDialog(materialRegister) {
            $scope.CurrentMaterialRegisterHeader = materialRegister;
            $scope.myModalInstance = $modal.open({
                templateUrl: 'angular/templates/materialregisters/views/deleteMaterialRegisterHeaderModal.html',
                controller: 'deleteMaterialRegisterHeaderCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

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

        
        function materialRegisterHeadersLoadCompleted(result) {
            $scope.MaterialRegisterHeaders = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingMaterialRegisterHeaders = false;

            if ($scope.filterMaterialRegisterHeaders && $scope.filterMaterialRegisterHeaders.length) {
                notificationService.displayInfo(result.data.Items.length + ' materialRegisters found');
            }
        }

        function materialRegisterHeadersLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterMaterialRegisterHeaders = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))