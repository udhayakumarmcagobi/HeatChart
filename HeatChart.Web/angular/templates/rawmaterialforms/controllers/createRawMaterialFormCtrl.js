(function (app) {
    'use strict';

    app.controller('createRawMaterialFormCtrl', createRawMaterialFormCtrl);

    createRawMaterialFormCtrl.$inject = ['$scope', '$location', '$rootScope', '$routeParams', 'apiService', 'notificationService'];

    function createRawMaterialFormCtrl($scope, $location, $rootScope, $routeParams, apiService, notificationService) {
        $scope.newRawMaterialForm = {};

        $scope.Create = Create;

        $scope.submission = {           
            errorMessages: ['Submission details will appear here.']
        };

        function Create() {
            $scope.newRawMaterialForm.CreatedBy = $rootScope.repository.loggedUser.username;
            $scope.newRawMaterialForm.ModifiedBy = $rootScope.repository.loggedUser.username;

            $scope.newRawMaterialForm.CreatedOn = new Date();
            $scope.newRawMaterialForm.ModifiedOn = new Date();

            apiService.post('/api/rawMaterialForms/create', $scope.newRawMaterialForm,
                registerRawMaterialFormSucceded,
                registerRawMaterialFormFailed);
        }
        function registerRawMaterialFormSucceded(response) {
            var rawMaterialFormRegistered = response.data;
            notificationService.displaySuccess($scope.newRawMaterialForm.Name + ' has been successfully registered')
            $scope.filterRawMaterialForms = $scope.newRawMaterialForm.Name;
            $location.url("/rawmaterialforms/search/" + $scope.newRawMaterialForm.Name);
        }
        function registerRawMaterialFormFailed(response) {
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }
    }
})(angular.module('heatChart'));