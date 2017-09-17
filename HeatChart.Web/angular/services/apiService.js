(function (app) {
    'use strict';

    app.factory('apiService', apiService);

    apiService.$inject = ['$http', '$location', 'notificationService', '$rootScope'];

    function apiService($http, $location, notificationService, $rootScope) {

        var service = {
            get: get,
            post: post
        };

        function get(url, config, success, failure) {
            return $http.get(url, config)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == "401") {
                        notificationService.displayError("Authentication Required");
                        $rootScope.previousState = $location.path();
                        if ($rootScope.repository.loggedUser == null) {
                            $location.path('/login');
                        }
                    }
                    else if (failure != null) {
                        failure(error);
                    }
                });
        }

        function post(url, config, success, failure) {            
            return $http.post(url, config)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == "401") {
                        notificationService.displayError("Authentication Required");
                        $rootScope.previousState = $location.path();
                        if ($rootScope.repository.loggedUser == null) {
                            $location.path('/login');
                        }
                    }
                    else if (failure != null) {
                        failure(error);
                    }
                });
        }

        return service;

    }

})(angular.module('common.core'));