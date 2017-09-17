(function (app) {
    'use strict';

    app.controller('usersCtrl', usersCtrl);

    usersCtrl.$inject = ['$scope', '$modal', '$routeParams', 'apiService', 'notificationService'];

    function usersCtrl($scope, $modal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-users';
        $scope.loadingUsers = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Users = [];
        $scope.search = search;
        $scope.addNew = addNew;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openDeleteDialog = openDeleteDialog;

        $scope.filterUsers = $routeParams.name != undefined ? $routeParams.name : '';

        function search(page) {
            page = page || 0;
            $scope.loadingUsers = true;
            var config = { params: { page: page, pageSize: 4, filter: $scope.filterUsers } };
            apiService.get('/api/Account/search/', config, usersLoadCompleted, usersLoadFailed);
        }

        function addNew() {
            window.location.href = "#/register";
        }

        function openEditDialog(user) {
            $scope.EditedUser = user;
            $modal.open({
                templateUrl: 'angular/templates/account/views/editUserModal.html',
                controller: 'editUserCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () { });
        }

        function openDeleteDialog(user) {
            $scope.EditedUser = user;
            $modal.open({
                templateUrl: 'angular/templates/account/views/deleteUserModal.html',
                controller: 'editUserCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function usersLoadCompleted(result) {
            $scope.Users = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingUsers = false;

            if ($scope.filterUsers && $scope.filterUsers.length) {
                notificationService.displayInfo(result.data.Items.length + ' users found');
            }
        }

        function usersLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterUsers = '';
            search();
        }

        $scope.search();
    }

})(angular.module('heatChart'))