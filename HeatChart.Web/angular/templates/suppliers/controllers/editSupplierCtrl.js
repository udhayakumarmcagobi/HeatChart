(function (app) {
    'use strict';

    app.controller('editSupplierCtrl', editSupplierCtrl);

    editSupplierCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function editSupplierCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateSupplier = updateSupplier;
        $scope.deleteSupplier = deleteSupplier;
        
        function updateSupplier() {
            $scope.EditedSupplier.ModifiedBy = $rootScope.repository.loggedUser.username;
            $scope.EditedSupplier.ModifiedOn = new Date();

            apiService.post('/api/suppliers/update/',
                $scope.EditedSupplier,
                updateSupplierCompleted,
                updateSupplierLoadFailed);
        }
        function updateSupplierCompleted(response) {
            notificationService.displaySuccess($scope.EditedSupplier.Name + ' has been updated');
            $scope.EditedSupplier = {};
            $modalInstance.dismiss();
        }
        function updateSupplierLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function deleteSupplier() {
            apiService.post('/api/suppliers/delete/', $scope.EditedSupplier,
            deleteSupplierCompleted,
            deleteSupplierLoadFailed);
        }
        function deleteSupplierCompleted(response) {
            notificationService.displaySuccess($scope.EditedSupplier.Name + ' has been deleted');
            $scope.EditedSupplier = {};
            $modalInstance.dismiss();
        }
        function deleteSupplierLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));