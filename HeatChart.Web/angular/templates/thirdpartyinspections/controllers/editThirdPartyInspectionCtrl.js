(function (app) {
    'use strict';

    app.controller('editThirdPartyInspectionCtrl', editThirdPartyInspectionCtrl);

    editThirdPartyInspectionCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function editThirdPartyInspectionCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateThirdPartyInspection = updateThirdPartyInspection;
        $scope.deleteThirdPartyInspection = deleteThirdPartyInspection;
        
        function updateThirdPartyInspection() {
            $scope.EditedThirdPartyInspection.ModifiedBy = $rootScope.repository.loggedUser.username;
            $scope.EditedThirdPartyInspection.ModifiedOn = new Date();

            apiService.post('/api/thirdPartyInspections/update/',
                $scope.EditedThirdPartyInspection,
                updateThirdPartyInspectionCompleted,
                updateThirdPartyInspectionLoadFailed);
        }
        function updateThirdPartyInspectionCompleted(response) {
            notificationService.displaySuccess($scope.EditedThirdPartyInspection.Name + ' has been updated');
            $scope.EditedThirdPartyInspection = {};
            $modalInstance.dismiss();
        }
        function updateThirdPartyInspectionLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function deleteThirdPartyInspection() {
            apiService.post('/api/thirdPartyInspections/delete/', $scope.EditedThirdPartyInspection,
            deleteThirdPartyInspectionCompleted,
            deleteThirdPartyInspectionLoadFailed);
        }
        function deleteThirdPartyInspectionCompleted(response) {
            notificationService.displaySuccess($scope.EditedThirdPartyInspection.Name + ' has been deleted');
            $scope.EditedThirdPartyInspection = {};
            $modalInstance.dismiss();
        }
        function deleteThirdPartyInspectionLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
})(angular.module('heatChart'));