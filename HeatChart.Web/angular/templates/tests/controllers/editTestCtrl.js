(function (app) {
    'use strict';

    app.controller('editTestCtrl', editTestCtrl);

    editTestCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function editTestCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateTest = updateTest;
        $scope.deleteTest = deleteTest;
        
        function updateTest() {
            $scope.EditedTest.ModifiedBy = $rootScope.repository.loggedUser.username;
            $scope.EditedTest.ModifiedOn = new Date();

            apiService.post('/api/tests/update/',
                $scope.EditedTest,
                updateTestCompleted,
                updateTestLoadFailed);
        }
        function updateTestCompleted(response) {
            notificationService.displaySuccess($scope.EditedTest.Name + ' has been updated');
            $scope.EditedTest = {};
            $modalInstance.dismiss();
        }
        function updateTestLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function deleteTest() {
            apiService.post('/api/tests/delete/', $scope.EditedTest,
            deleteTestCompleted,
            deleteTestLoadFailed);
        }
        function deleteTestCompleted(response) {
            notificationService.displaySuccess($scope.EditedTest.Name + ' has been deleted');
            $scope.EditedTest = {};
            $modalInstance.dismiss();
        }
        function deleteTestLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));