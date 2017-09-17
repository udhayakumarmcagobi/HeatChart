(function (app) {
    'use strict';

    app.controller('editRawMaterialFormCtrl', editRawMaterialFormCtrl);

    editRawMaterialFormCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function editRawMaterialFormCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateRawMaterialForm = updateRawMaterialForm;
        $scope.deleteRawMaterialForm = deleteRawMaterialForm;
        
        function updateRawMaterialForm() {
            $scope.EditedRawMaterialForm.ModifiedBy = $rootScope.repository.loggedUser.username;
            $scope.EditedRawMaterialForm.ModifiedOn = new Date();

            apiService.post('/api/rawMaterialForms/update/',
                $scope.EditedRawMaterialForm,
                updateRawMaterialFormCompleted,
                updateRawMaterialFormLoadFailed);
        }
        function updateRawMaterialFormCompleted(response) {
            notificationService.displaySuccess($scope.EditedRawMaterialForm.Name + ' has been updated');
            $scope.EditedRawMaterialForm = {};
            $modalInstance.dismiss();
        }
        function updateRawMaterialFormLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function deleteRawMaterialForm() {
            apiService.post('/api/rawMaterialForms/delete/', $scope.EditedRawMaterialForm,
            deleteRawMaterialFormCompleted,
            deleteRawMaterialFormLoadFailed);
        }
        function deleteRawMaterialFormCompleted(response) {
            notificationService.displaySuccess($scope.EditedRawMaterialForm.Name + ' has been deleted');
            $scope.EditedRawMaterialForm = {};
            $modalInstance.dismiss();
        }
        function deleteRawMaterialFormLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));