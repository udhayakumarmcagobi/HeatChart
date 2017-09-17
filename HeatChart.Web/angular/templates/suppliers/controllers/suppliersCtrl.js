(function (app) {
    'use strict';

    app.controller('suppliersCtrl', suppliersCtrl);

    suppliersCtrl.$inject = ['$scope', '$modal', '$routeParams', 'apiService', 'notificationService'];

    function suppliersCtrl($scope, $modal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-suppliers';
        $scope.loadingSuppliers = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Suppliers = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openDeleteDialog = openDeleteDialog;

        $scope.filterSuppliers = $routeParams.name != undefined ? $routeParams.name : '';

        function search(page) {
            page = page || 0;
            $scope.loadingSuppliers = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterSuppliers } };
            apiService.get('/api/suppliers/search/', config, suppliersLoadCompleted, suppliersLoadFailed);
        }

        function addNew() {
            window.location.href = "#/suppliers/create";
        }

        function openEditDialog(supplier) {
            $scope.EditedSupplier = supplier;
            $modal.open({
                templateUrl: 'angular/templates/suppliers/views/editSupplierModal.html',
                controller: 'editSupplierCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function openDeleteDialog(supplier) {
            $scope.EditedSupplier = supplier;
            $modal.open({
                templateUrl: 'angular/templates/suppliers/views/deleteSupplierModal.html',
                controller: 'editSupplierCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function suppliersLoadCompleted(result) {
            $scope.Suppliers = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingSuppliers = false;

            if ($scope.filterSuppliers && $scope.filterSuppliers.length) {
                notificationService.displayInfo(result.data.Items.length + ' suppliers found');
            }
        }

        function suppliersLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterSuppliers = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))