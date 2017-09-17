(function (app) {
    'use strict';

    app.controller('deleteMaterialRegisterHeaderCtrl', deleteMaterialRegisterHeaderCtrl);

    deleteMaterialRegisterHeaderCtrl.$inject = ['$scope', '$location', '$modalInstance', 'apiService', 'notificationService'];

    function deleteMaterialRegisterHeaderCtrl($scope, $location, $modalInstance, apiService, notificationService) {

        $scope.deleteMaterialRegisterHeader = deleteMaterialRegisterHeader;
        $scope.cancelEdit = cancelEdit;
      
        // Delete Material Registers Ends

        function deleteMaterialRegisterHeader() {
            apiService.post('/api/materialRegisters/delete', $scope.CurrentMaterialRegisterHeader,
               DeleteMaterialRegisterHeaderSucceded,
               DeleteMaterialRegisterHeaderFailed);
        }
        function DeleteMaterialRegisterHeaderSucceded(response) {
            notificationService.displaySuccess($scope.CurrentMaterialRegisterHeader.CTNumber + ' has been deleted');
            $modalInstance.dismiss();
            $location.url("/materialregisters/");          
        }
        function DeleteMaterialRegisterHeaderFailed(response) {
            notificationService.displayError(response.data);
            $modalInstance.dismiss();
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }


    }
})(angular.module('heatChart'));