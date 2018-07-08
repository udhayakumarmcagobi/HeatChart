(function (app) {
    'use strict';

    app.factory('beforeUnload', beforeUnload);

    beforeUnload.$inject = ['$http', '$location', '$window', '$rootScope'];

    function beforeUnload($rootScope, $window) {
        // Events are broadcast outside the Scope Lifecycle

        $window.onbeforeunload = function (e) {
            var confirmation = {};
            var event = $rootScope.$broadcast('onBeforeUnload', confirmation);
            if (event.defaultPrevented) {
                return confirmation.message;
            }
        };

        $window.onunload = function () {
            $rootScope.$broadcast('onUnload');
        };
        return {};
    }

    app.run(function (beforeUnload) {
        // Must invoke the service at least once
    });
})(angular.module('common.core'));