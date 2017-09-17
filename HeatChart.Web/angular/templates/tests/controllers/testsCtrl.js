(function (app) {
    'use strict';

    app.controller('testsCtrl', testsCtrl);

    testsCtrl.$inject = ['$scope', '$modal', '$routeParams', 'apiService', 'notificationService'];

    function testsCtrl($scope, $modal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-tests';
        $scope.loadingTests = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Tests = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openDeleteDialog = openDeleteDialog;

        $scope.filterTests = $routeParams.name != undefined ? $routeParams.name : '';

        function search(page) {
            page = page || 0;
            $scope.loadingTests = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterTests } };
            apiService.get('/api/tests/search/', config, testsLoadCompleted, testsLoadFailed);
        }

        function addNew() {
            window.location.href = "#/tests/create";
        }

        function openEditDialog(test) {
            $scope.EditedTest = test;
            $modal.open({
                templateUrl: 'angular/templates/tests/views/editTestModal.html',
                controller: 'editTestCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function openDeleteDialog(test) {
            $scope.EditedTest = test;
            $modal.open({
                templateUrl: 'angular/templates/tests/views/deleteTestModal.html',
                controller: 'editTestCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function testsLoadCompleted(result) {
            $scope.Tests = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingTests = false;

            if ($scope.filterTests && $scope.filterTests.length) {
                notificationService.displayInfo(result.data.Items.length + ' tests found');
            }
        }

        function testsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterTests = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))