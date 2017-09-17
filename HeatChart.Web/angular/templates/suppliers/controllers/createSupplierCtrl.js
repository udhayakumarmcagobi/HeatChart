(function (app) {
    'use strict';

    app.controller('createSupplierCtrl', createSupplierCtrl);

    createSupplierCtrl.$inject = ['$scope', '$location', '$rootScope', '$routeParams', 'apiService', 'notificationService'];

    function createSupplierCtrl($scope, $location, $rootScope, $routeParams, apiService, notificationService) {
        $scope.newSupplier = {};

        $scope.Create = Create;

        $scope.submission = {           
            errorMessages: ['Submission details will appear here.']
        };

        function Create() {
            $scope.newSupplier.CreatedBy = $rootScope.repository.loggedUser.username;
            $scope.newSupplier.ModifiedBy = $rootScope.repository.loggedUser.username;

            $scope.newSupplier.CreatedOn = new Date();
            $scope.newSupplier.ModifiedOn = new Date();

            apiService.post('/api/suppliers/create', $scope.newSupplier,
                registerSupplierSucceded,
                registerSupplierFailed);
        }
        function registerSupplierSucceded(response) {
            var supplierRegistered = response.data;
            notificationService.displaySuccess($scope.newSupplier.Name + ' has been successfully registered')
            $scope.filterSuppliers = $scope.newSupplier.Name;
            $location.url("/suppliers/search/" + $scope.newSupplier.Name);
        }
        function registerSupplierFailed(response) {
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }
    }
})(angular.module('heatChart'));