(function (app) {
    'use strict';

    app.factory('heatChartReportService', heatChartReportService);

    heatChartReportService.$inject = ['$rootScope', '$http', '$timeout', '$upload', 'notificationService', '$window', '$filter'];

    function heatChartReportService($rootScope, $http, $timeout, $upload, notificationService, $window, $filter) {
        $rootScope.upload = [];

        var service = {
            generateHeatChart: generateHeatChart,
            uploadMaterialRegisterFile: uploadMaterialRegisterFile,
            downloadMaterialRegisterFile: downloadMaterialRegisterFile
        }

        function generateHeatChart(heatChartID, success, failure) {
            $http({
                method: 'GET',
                url: 'api/reports/generateheatchart/',
                params: { heatChartID: heatChartID },
                responseType: 'arraybuffer'
            }).success(function (data, status, headers) {
                headers = headers();

                var filename = headers['x-filename'];
                var contentType = headers['content-type'];

                var linkElement = document.createElement('a');
                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = window.URL.createObjectURL(blob);

                    linkElement.setAttribute('href', url);
                    linkElement.setAttribute("download", filename);

                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });
                    linkElement.dispatchEvent(clickEvent);
                } catch (ex) {
                    console.log(ex);
                }
                success(data);
            }).error(function (data) {
                failure(data);
            });
        }

        function uploadMaterialRegisterFile($files, CTNumber, subSeriesNumber, success, failure) {
            //$files: an array of files selected
            var date = ($filter('date')(new Date(), "dd-MM-yyyy")).toString()
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $rootScope.upload[index] = $upload.upload({
                        url: "/api/reports/file/materialregisterupload",
                        params: { CTNumber: CTNumber, subSeriesNumber: subSeriesNumber, date : date },
                        async:false,
                        method: "POST",
                        file: $file
                    }).progress(function (evt) {
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        notificationService.displaySuccess(data.FileName + ' uploaded successfully');
                        success(data);
                        
                    }).error(function (data, status, headers, config) {
                        notificationService.displayError(data.Message);
                        failure(data);
                    });
                })(i);
            }
        }

        function downloadMaterialRegisterFile(fileName, success, failure) {
            $http({
                method: 'GET',
                url: 'api/reports/file/download',
                params: { fileName: fileName },
                responseType: 'arraybuffer'
            }).success(function (data, status, headers) {
                headers = headers();

                var filename = headers['x-filename'];
                var contentType = headers['content-type'];

                var linkElement = document.createElement('a');
                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = window.URL.createObjectURL(blob);

                    linkElement.setAttribute('href', url);
                    linkElement.setAttribute("download", filename);

                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });
                    linkElement.dispatchEvent(clickEvent);
                    success(fileName);
                } catch (ex) {
                    console.log(ex);
                }
            }).error(function (data) {
                failure(fileName);
            });
        }

        return service;
    }
})(angular.module('common.core'));