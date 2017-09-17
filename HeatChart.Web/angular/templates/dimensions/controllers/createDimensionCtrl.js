(function (app) {
    'use strict';

    app.controller('createDimensionCtrl', createDimensionCtrl);

    createDimensionCtrl.$inject = ['$scope', '$location', '$rootScope', '$routeParams', 'apiService', 'notificationService'];

    function createDimensionCtrl($scope, $location, $rootScope, $routeParams, apiService, notificationService) {
        $scope.newDimension = {};

        $scope.Create = Create;

        $scope.submission = {           
            errorMessages: ['Submission details will appear here.']
        };

        function Create() {
            $scope.newDimension.CreatedBy = $rootScope.repository.loggedUser.username;
            $scope.newDimension.ModifiedBy = $rootScope.repository.loggedUser.username;

            $scope.newDimension.CreatedOn = new Date();
            $scope.newDimension.ModifiedOn = new Date();

            apiService.post('/api/dimensions/create', $scope.newDimension,
                registerDimensionSucceded,
                registerDimensionFailed);
        }
        function registerDimensionSucceded(response) {
            var dimensionRegistered = response.data;
            notificationService.displaySuccess($scope.newDimension.Name + ' has been successfully registered')
            $scope.filterDimensions = $scope.newDimension.Name;
            $location.url("/dimensions/search/" + $scope.newDimension.Name);
        }
        function registerDimensionFailed(response) {
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }
    }
})(angular.module('heatChart'));