(function (app) {
    'use strict';

    app.controller('specificationsCtrl', specificationsCtrl);

    specificationsCtrl.$inject = ['$scope', '$modal', '$routeParams', 'apiService', 'notificationService'];

    function specificationsCtrl($scope, $modal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-specifications';
        $scope.loadingSpecifications = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Specifications = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openDeleteDialog = openDeleteDialog;

        $scope.filterSpecifications = $routeParams.name != undefined ? $routeParams.name : '';

        function search(page) {
            page = page || 0;
            $scope.loadingSpecifications = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterSpecifications } };
            apiService.get('/api/specifications/search/', config, specificationsLoadCompleted, specificationsLoadFailed);
        }

        function addNew() {
            window.location.href = "#/specifications/create";
        }

        function openEditDialog(specification) {
            $scope.EditedSpecification = specification;
            $modal.open({
                templateUrl: 'angular/templates/specifications/views/editSpecificationModal.html',
                controller: 'editSpecificationCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function openDeleteDialog(specification) {
            $scope.EditedSpecification = specification;
            $modal.open({
                templateUrl: 'angular/templates/specifications/views/deleteSpecificationModal.html',
                controller: 'editSpecificationCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function specificationsLoadCompleted(result) {
            $scope.Specifications = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingSpecifications = false;

            if ($scope.filterSpecifications && $scope.filterSpecifications.length) {
                notificationService.displayInfo(result.data.Items.length + ' specifications found');
            }
        }

        function specificationsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterSpecifications = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))