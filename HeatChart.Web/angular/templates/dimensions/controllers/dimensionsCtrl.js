(function (app) {
    'use strict';

    app.controller('dimensionsCtrl', dimensionsCtrl);

    dimensionsCtrl.$inject = ['$scope', '$modal', '$routeParams', 'apiService', 'notificationService'];

    function dimensionsCtrl($scope, $modal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-dimensions';
        $scope.loadingDimensions = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Dimensions = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openDeleteDialog = openDeleteDialog;

        $scope.filterDimensions = $routeParams.name != undefined ? $routeParams.name : '';

        function search(page) {
            page = page || 0;
            $scope.loadingDimensions = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterDimensions } };
            apiService.get('/api/dimensions/search/', config, dimensionsLoadCompleted, dimensionsLoadFailed);
        }

        function addNew() {
            window.location.href = "#/dimensions/create";
        }

        function openEditDialog(dimension) {
            $scope.EditedDimension = dimension;
            $modal.open({
                templateUrl: 'angular/templates/dimensions/views/editDimensionModal.html',
                controller: 'editDimensionCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function openDeleteDialog(dimension) {
            $scope.EditedDimension = dimension;
            $modal.open({
                templateUrl: 'angular/templates/dimensions/views/deleteDimensionModal.html',
                controller: 'editDimensionCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function dimensionsLoadCompleted(result) {
            $scope.Dimensions = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingDimensions = false;

            if ($scope.filterDimensions && $scope.filterDimensions.length) {
                notificationService.displayInfo(result.data.Items.length + ' dimensions found');
            }
        }

        function dimensionsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterDimensions = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))