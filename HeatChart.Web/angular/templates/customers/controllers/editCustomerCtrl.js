(function (app) {
    'use strict';

    app.controller('editCustomerCtrl', editCustomerCtrl);

    editCustomerCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function editCustomerCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateCustomer = updateCustomer;
        $scope.deleteCustomer = deleteCustomer;
        
        function updateCustomer() {
            $scope.EditedCustomer.ModifiedBy = $rootScope.repository.loggedUser.username;
            $scope.EditedCustomer.ModifiedOn = new Date();

            apiService.post('/api/customers/update/',
                $scope.EditedCustomer,
                updateCustomerCompleted,
                updateCustomerLoadFailed);
        }
        function updateCustomerCompleted(response) {
            notificationService.displaySuccess($scope.EditedCustomer.Name + ' has been updated');
            $scope.EditedCustomer = {};
            $modalInstance.dismiss();
        }
        function updateCustomerLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function deleteCustomer() {
            apiService.post('/api/customers/delete/', $scope.EditedCustomer,
            deleteCustomerCompleted,
            deleteCustomerLoadFailed);
        }
        function deleteCustomerCompleted(response) {
            notificationService.displaySuccess($scope.EditedCustomer.Name + ' has been deleted');
            $scope.EditedCustomer = {};
            $modalInstance.dismiss();
        }
        function deleteCustomerLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));