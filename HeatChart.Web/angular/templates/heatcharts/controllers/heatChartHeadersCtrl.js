(function (app) {
    'use strict';

    app.controller('heatChartHeadersCtrl', heatChartHeadersCtrl);

    heatChartHeadersCtrl.$inject = ['$scope', '$modal', '$location', '$routeParams', 'apiService', 'notificationService'];

    function heatChartHeadersCtrl($scope, $modal, $location, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-heatChartHeaders';
        $scope.loadingHeatChartHeaders = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.HeatChartHeaders = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.editHeatChartHeader = editHeatChartHeader;
        $scope.openDeleteDialog = openDeleteDialog;
        $scope.GenerateHeatChart = GenerateHeatChart;
        $scope.DownloadAllReports = DownloadAllReports;
               
        $scope.filterHeatChartHeaders = $routeParams.HCNumber != undefined ? $routeParams.HCNumber : '';

        function search(page) {
            page = page || 0;
            $scope.loadingHeatChartHeaders = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterHeatChartHeaders } };
            apiService.get('/api/heatcharts/search/', config, heatChartHeadersLoadCompleted, heatChartHeadersLoadFailed);
        }

        function addNew() {
            window.location.href = "#/heatcharts/create";
        }

        function editHeatChartHeader(heatChart) {
            $location.url("/heatcharts/edit/" + heatChart.ID);
        }

        function openDeleteDialog(heatChart) {
            $scope.CurrentHeatChartHeader = heatChart;
            $modal.open({
                templateUrl: 'angular/templates/heatcharts/views/deleteHeatChartHeaderModal.html',
                controller: 'deleteHeatChartHeaderCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        // Generate Heat Chart Starts

        function GenerateHeatChart(heatChart) {
            $scope.CurrentHeatChartHeader = heatChart;
             $modal.open({
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

        function heatChartHeadersLoadCompleted(result) {
            $scope.HeatChartHeaders = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingHeatChartHeaders = false;

            if ($scope.filterHeatChartHeaders && $scope.filterHeatChartHeaders.length) {
                notificationService.displayInfo(result.data.Items.length + ' heatCharts found');
            }
        }

        function heatChartHeadersLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterHeatChartHeaders = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))