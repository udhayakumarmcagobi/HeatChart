(function (app) {
    'use strict';

    app.controller('customersCtrl', customersCtrl);

    customersCtrl.$inject = ['$scope', '$modal', '$routeParams', 'apiService', 'notificationService'];

    function customersCtrl($scope, $modal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-customers';
        $scope.loadingCustomers = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Customers = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openDeleteDialog = openDeleteDialog;

        $scope.filterCustomers = $routeParams.name != undefined ? $routeParams.name : '';

        function search(page) {
            page = page || 0;
            $scope.loadingCustomers = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterCustomers } };
            apiService.get('/api/customers/search/', config, customersLoadCompleted, customersLoadFailed);
        }

        function addNew() {
            window.location.href = "#/customers/create";
        }

        function openEditDialog(customer) {
            $scope.EditedCustomer = customer;
            $modal.open({
                templateUrl: 'angular/templates/customers/views/editCustomerModal.html',
                controller: 'editCustomerCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function openDeleteDialog(customer) {
            $scope.EditedCustomer = customer;
            $modal.open({
                templateUrl: 'angular/templates/customers/views/deleteCustomerModal.html',
                controller: 'editCustomerCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function customersLoadCompleted(result) {
            $scope.Customers = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingCustomers = false;

            if ($scope.filterCustomers && $scope.filterCustomers.length) {
                notificationService.displayInfo(result.data.Items.length + ' customers found');
            }
        }

        function customersLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterCustomers = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))