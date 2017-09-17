(function (app) {
    'use strict';

    app.controller('createTestCtrl', createTestCtrl);

    createTestCtrl.$inject = ['$scope', '$location', '$rootScope', '$routeParams', 'apiService', 'notificationService'];

    function createTestCtrl($scope, $location, $rootScope, $routeParams, apiService, notificationService) {
        $scope.newTest = {};

        $scope.Create = Create;

        $scope.submission = {           
            errorMessages: ['Submission details will appear here.']
        };

        function Create() {
            $scope.newTest.CreatedBy = $rootScope.repository.loggedUser.username;
            $scope.newTest.ModifiedBy = $rootScope.repository.loggedUser.username;

            $scope.newTest.CreatedOn = new Date();
            $scope.newTest.ModifiedOn = new Date();

            apiService.post('/api/tests/create', $scope.newTest,
                registerTestSucceded,
                registerTestFailed);
        }
        function registerTestSucceded(response) {
            var testRegistered = response.data;
            notificationService.displaySuccess($scope.newTest.Name + ' has been successfully registered')
            $scope.filterTests = $scope.newTest.Name;
            $location.url("/tests/search/" + $scope.newTest.Name);
        }
        function registerTestFailed(response) {
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }
    }
})(angular.module('heatChart'));