(function (app) {
    'use strict';

    app.controller('createThirdPartyInspectionCtrl', createThirdPartyInspectionCtrl);

    createThirdPartyInspectionCtrl.$inject = ['$scope', '$location', '$rootScope', '$routeParams', 'apiService', 'notificationService'];

    function createThirdPartyInspectionCtrl($scope, $location, $rootScope, $routeParams, apiService, notificationService) {
        $scope.newThirdPartyInspection = {};

        $scope.Create = Create;

        $scope.submission = {           
            errorMessages: ['Submission details will appear here.']
        };

        function Create() {
            $scope.newThirdPartyInspection.CreatedBy = $rootScope.repository.loggedUser.username;
            $scope.newThirdPartyInspection.ModifiedBy = $rootScope.repository.loggedUser.username;

            $scope.newThirdPartyInspection.CreatedOn = new Date();
            $scope.newThirdPartyInspection.ModifiedOn = new Date();

            apiService.post('/api/thirdPartyInspections/create', $scope.newThirdPartyInspection,
                registerThirdPartyInspectionSucceded,
                registerThirdPartyInspectionFailed);
        }
        function registerThirdPartyInspectionSucceded(response) {
            var thirdPartyInspectionRegistered = response.data;
            notificationService.displaySuccess($scope.newThirdPartyInspection.Name + ' has been successfully registered')
            $scope.filterThirdPartyInspections = $scope.newThirdPartyInspection.Name;
            $location.url("/thirdpartyinspections/search/" + $scope.newThirdPartyInspection.Name);
        }
        function registerThirdPartyInspectionFailed(response) {
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }
    }
})(angular.module('heatChart'));