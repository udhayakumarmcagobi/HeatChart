(function (app) {
    'use strict';

    app.controller('thirdPartyInspectionsCtrl', thirdPartyInspectionsCtrl);

    thirdPartyInspectionsCtrl.$inject = ['$scope', '$modal', '$routeParams', 'apiService', 'notificationService'];

    function thirdPartyInspectionsCtrl($scope, $modal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-thirdPartyInspections';
        $scope.loadingThirdPartyInspections = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.ThirdPartyInspections = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openDeleteDialog = openDeleteDialog;

        $scope.filterThirdPartyInspections = $routeParams.name != undefined ? $routeParams.name : '';

        function search(page) {
            page = page || 0;
            $scope.loadingThirdPartyInspections = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterThirdPartyInspections } };
            apiService.get('/api/thirdpartyinspections/search/', config, thirdPartyInspectionsLoadCompleted, thirdPartyInspectionsLoadFailed);
        }

        function addNew() {
            window.location.href = "#/thirdpartyinspections/create";
        }

        function openEditDialog(thirdPartyInspection) {
            $scope.EditedThirdPartyInspection = thirdPartyInspection;
            $modal.open({
                templateUrl: 'angular/templates/thirdPartyInspections/views/editThirdPartyInspectionModal.html',
                controller: 'editThirdPartyInspectionCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function openDeleteDialog(thirdPartyInspection) {
            $scope.EditedThirdPartyInspection = thirdPartyInspection;
            $modal.open({
                templateUrl: 'angular/templates/thirdPartyInspections/views/deleteThirdPartyInspectionModal.html',
                controller: 'editThirdPartyInspectionCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function thirdPartyInspectionsLoadCompleted(result) {
            $scope.ThirdPartyInspections = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingThirdPartyInspections = false;

            if ($scope.filterThirdPartyInspections && $scope.filterThirdPartyInspections.length) {
                notificationService.displayInfo(result.data.Items.length + ' thirdPartyInspections found');
            }
        }

        function thirdPartyInspectionsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterThirdPartyInspections = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))