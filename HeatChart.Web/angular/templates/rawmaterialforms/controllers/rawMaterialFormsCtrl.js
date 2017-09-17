(function (app) {
    'use strict';

    app.controller('rawMaterialFormsCtrl', rawMaterialFormsCtrl);

    rawMaterialFormsCtrl.$inject = ['$scope', '$modal', '$routeParams', 'apiService', 'notificationService'];

    function rawMaterialFormsCtrl($scope, $modal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-rawMaterialForms';
        $scope.loadingRawMaterialForms = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.RawMaterialForms = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openDeleteDialog = openDeleteDialog;

        $scope.filterRawMaterialForms = $routeParams.name != undefined ? $routeParams.name : '';

        function search(page) {
            page = page || 0;
            $scope.loadingRawMaterialForms = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterRawMaterialForms } };
            apiService.get('/api/rawmaterialforms/search/', config, rawMaterialFormsLoadCompleted, rawMaterialFormsLoadFailed);
        }

        function addNew() {
            window.location.href = "#/rawmaterialforms/create";
        }

        function openEditDialog(rawMaterialForm) {
            $scope.EditedRawMaterialForm = rawMaterialForm;
            $modal.open({
                templateUrl: 'angular/templates/rawMaterialForms/views/editRawMaterialFormModal.html',
                controller: 'editRawMaterialFormCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function openDeleteDialog(rawMaterialForm) {
            $scope.EditedRawMaterialForm = rawMaterialForm;
            $modal.open({
                templateUrl: 'angular/templates/rawMaterialForms/views/deleteRawMaterialFormModal.html',
                controller: 'editRawMaterialFormCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function rawMaterialFormsLoadCompleted(result) {
            $scope.RawMaterialForms = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingRawMaterialForms = false;

            if ($scope.filterRawMaterialForms && $scope.filterRawMaterialForms.length) {
                notificationService.displayInfo(result.data.Items.length + ' rawMaterialForms found');
            }
        }

        function rawMaterialFormsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterRawMaterialForms = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))