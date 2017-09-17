(function (app) {
    'use strict';

    app.controller('userChangePasswordCtrl', userChangePasswordCtrl);

    userChangePasswordCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', 'apiService'];

    function userChangePasswordCtrl($scope, membershipService, notificationService, $rootScope, $location, apiService) {

        $scope.changePassword = changePassword;

        $scope.EditedUser = {};
        $scope.EditedUser.Username = $scope.username;
        $scope.EditedUser.Email = $scope.email;

        function changePassword() {
            apiService.post('/api/Account/changePassword/',
                $scope.EditedUser,
                updateUserCompleted,
                updateUserLoadFailed);
        }
        function updateUserCompleted(response) {
            notificationService.displaySuccess($scope.EditedUser.Username + ' password has been changed');
        }
        function updateUserLoadFailed(response) {
            notificationService.displayError(response.data);
        }       
    }
})(angular.module('heatChart'));