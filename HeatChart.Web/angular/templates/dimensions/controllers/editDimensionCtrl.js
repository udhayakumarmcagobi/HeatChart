(function (app) {
    'use strict';

    app.controller('editDimensionCtrl', editDimensionCtrl);

    editDimensionCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function editDimensionCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateDimension = updateDimension;
        $scope.deleteDimension = deleteDimension;
        
        function updateDimension() {
            $scope.EditedDimension.ModifiedBy = $rootScope.repository.loggedUser.username;
            $scope.EditedDimension.ModifiedOn = new Date();

            apiService.post('/api/dimensions/update/',
                $scope.EditedDimension,
                updateDimensionCompleted,
                updateDimensionLoadFailed);
        }
        function updateDimensionCompleted(response) {
            notificationService.displaySuccess($scope.EditedDimension.Name + ' has been updated');
            $scope.EditedDimension = {};
            $modalInstance.dismiss();
        }
        function updateDimensionLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function deleteDimension() {
            apiService.post('/api/dimensions/delete/', $scope.EditedDimension,
            deleteDimensionCompleted,
            deleteDimensionLoadFailed);
        }
        function deleteDimensionCompleted(response) {
            notificationService.displaySuccess($scope.EditedDimension.Name + ' has been deleted');
            $scope.EditedDimension = {};
            $modalInstance.dismiss();
        }
        function deleteDimensionLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));