(function (app) {
    'use strict';

    app.controller('createSpecificationCtrl', createSpecificationCtrl);

    createSpecificationCtrl.$inject = ['$scope', '$location', '$rootScope', '$routeParams', 'apiService', 'notificationService'];

    function createSpecificationCtrl($scope, $location, $rootScope, $routeParams, apiService, notificationService) {
        $scope.newSpecification = {};

        $scope.Create = Create;

        $scope.submission = {           
            errorMessages: ['Submission details will appear here.']
        };

        function Create() {
            $scope.newSpecification.CreatedBy = $rootScope.repository.loggedUser.username;
            $scope.newSpecification.ModifiedBy = $rootScope.repository.loggedUser.username;

            $scope.newSpecification.CreatedOn = new Date();
            $scope.newSpecification.ModifiedOn = new Date();

            apiService.post('/api/specifications/create', $scope.newSpecification,
                registerSpecificationSucceded,
                registerSpecificationFailed);
        }
        function registerSpecificationSucceded(response) {
            var specificationRegistered = response.data;
            notificationService.displaySuccess($scope.newSpecification.Name + ' has been successfully registered')
            $scope.filterSpecifications = $scope.newSpecification.Name;
            $location.url("/specifications/search/" + $scope.newSpecification.Name);
        }
        function registerSpecificationFailed(response) {
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }
    }
})(angular.module('heatChart'));