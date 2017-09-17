(function (app) {
    'use strict';

    app.controller('editUserCtrl', editUserCtrl);

    editUserCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function editUserCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateUser = updateUser;
        $scope.deleteUser = deleteUser;
        
        function updateUser() {
            apiService.post('/api/Account/update/',
                $scope.EditedUser,
                updateUserCompleted,
                updateUserLoadFailed);
        }
        function updateUserCompleted(response) {
            notificationService.displaySuccess($scope.EditedUser.Username + ' has been updated');
            $scope.EditedUser = {};
            $modalInstance.dismiss();
        }
        function updateUserLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function deleteUser() {
            apiService.post('/api/Account/delete/', $scope.EditedUser,
            deleteUserCompleted,
            deleteUserLoadFailed);
        }
        function deleteUserCompleted(response) {
            notificationService.displaySuccess($scope.EditedUser.Username + ' has been deleted');
            $scope.EditedUser = {};
            $modalInstance.dismiss();
        }
        function deleteUserLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));