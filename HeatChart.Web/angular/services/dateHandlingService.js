(function (app) {
    'use strict';

    app.factory('dateHandlingService', dateHandlingService);

    dateHandlingService.$inject = ['$rootScope', '$http', '$timeout', '$upload', 'notificationService', '$window', '$filter'];

    function dateHandlingService($rootScope, $http, $timeout, $upload, notificationService, $window, $filter) {
        $rootScope.upload = [];

        var service = {
            getMaxDate: getMaxDate,
        }

        function getMaxDate() {
            var dtToday = new Date();

            var month = dtToday.getMonth() + 1;
            var day = dtToday.getDate();
            var year = dtToday.getFullYear();

            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();

            var maxDate = year + '-' + month + '-' + day;

            return maxDate;            
        }
        return service;
    }
})(angular.module('common.core'));