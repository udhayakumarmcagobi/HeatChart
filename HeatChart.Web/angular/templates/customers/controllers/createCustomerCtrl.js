(function (app) {
    'use strict';

    app.controller('createCustomerCtrl', createCustomerCtrl);

    createCustomerCtrl.$inject = ['$scope', '$location', '$rootScope', '$routeParams', 'apiService', 'notificationService'];

    function createCustomerCtrl($scope, $location, $rootScope, $routeParams, apiService, notificationService) {
        $scope.newCustomer = {};

        $scope.Create = Create;

        $scope.submission = {           
            errorMessages: ['Submission details will appear here.']
        };

        function Create() {
            $scope.newCustomer.CreatedBy = $rootScope.repository.loggedUser.username;
            $scope.newCustomer.ModifiedBy = $rootScope.repository.loggedUser.username;

            $scope.newCustomer.CreatedOn = new Date();
            $scope.newCustomer.ModifiedOn = new Date();

            apiService.post('/api/customers/create', $scope.newCustomer,
                registerCustomerSucceded,
                registerCustomerFailed);
        }
        function registerCustomerSucceded(response) {
            var customerRegistered = response.data;
            notificationService.displaySuccess($scope.newCustomer.Name + ' has been successfully registered')
            $scope.filterCustomers = $scope.newCustomer.Name;
            $location.url("/customers/search/" + $scope.newCustomer.Name);
        }
        function registerCustomerFailed(response) {
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }
    }
})(angular.module('heatChart'));