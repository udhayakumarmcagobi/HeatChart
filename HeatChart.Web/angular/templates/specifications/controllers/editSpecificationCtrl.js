(function (app) {
    'use strict';

    app.controller('editSpecificationCtrl', editSpecificationCtrl);

    editSpecificationCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function editSpecificationCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateSpecification = updateSpecification;
        $scope.deleteSpecification = deleteSpecification;
        
        function updateSpecification() {
            $scope.EditedSpecification.ModifiedBy = $rootScope.repository.loggedUser.username;
            $scope.EditedSpecification.ModifiedOn = new Date();

            apiService.post('/api/specifications/update/',
                $scope.EditedSpecification,
                updateSpecificationCompleted,
                updateSpecificationLoadFailed);
        }
        function updateSpecificationCompleted(response) {
            notificationService.displaySuccess($scope.EditedSpecification.Name + ' has been updated');
            $scope.EditedSpecification = {};
            $modalInstance.dismiss();
        }
        function updateSpecificationLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function deleteSpecification() {
            apiService.post('/api/specifications/delete/', $scope.EditedSpecification,
            deleteSpecificationCompleted,
            deleteSpecificationLoadFailed);
        }
        function deleteSpecificationCompleted(response) {
            notificationService.displaySuccess($scope.EditedSpecification.Name + ' has been deleted');
            $scope.EditedSpecification = {};
            $modalInstance.dismiss();
        }
        function deleteSpecificationLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));