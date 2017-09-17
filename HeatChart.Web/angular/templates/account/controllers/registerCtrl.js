(function (app) {
    'use strict';

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location'];

    function registerCtrl($scope, membershipService, notificationService, $rootScope, $location) {

        $scope.pageClass = 'page-login';
        $scope.register = register;
        $scope.user = {};

        function register() {
            membershipService.register($scope.user, registerCompleted);
        }

        function registerCompleted(result) {
            if (result.data.success) {
                if (result.data.role == "Admin") {
                    membershipService.saveCredentials($scope.user, result.data.role, result.data.email);
                    notificationService.displaySuccess('Hello ' + $scope.user.username);

                    $scope.userData.displayUserInfo(); $location.path('/');
                }
                else {
                    notificationService.displaySuccess('New User ' + $scope.user.username + ' has been created.');
                }
            }
            else if (result.data.message != null && result.data.message != "") {
                notificationService.displayError(result.data.message);
            }
            else {
                notificationService.displayError('Registration failed. Try again.');
            }
        }
    }
})(angular.module('common.core'));