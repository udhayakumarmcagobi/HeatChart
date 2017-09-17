(function () {
    'use strict';

    angular.module('heatChart', ['common.ui', 'common.core'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider'];

    function config($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "angular/templates/home/views/index.html",
                controller: "indexCtrl"
            })
            .when("/login", {
                templateUrl: "angular/templates/account/views/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "angular/templates/account/views/register.html",
                controller: "registerCtrl"
            })
            .when("/users", {
                templateUrl: "angular/templates/account/views/users.html",
                controller: "usersCtrl"
            })
            .when("/changepassword", {
                templateUrl: "angular/templates/account/views/userChangePassword.html",
                controller: "userChangePasswordCtrl"
            })        
            // Customer
            .when("/customers", {
                templateUrl: "angular/templates/customers/views/customers.html",
                controller: "customersCtrl"
            })
            .when("/customers/search/:name", {
                templateUrl: "angular/templates/customers/views/customers.html",
                controller: "customersCtrl"
            })
            .when("/customers/create", {
                templateUrl: "angular/templates/customers/views/createCustomer.html",
                controller: "createCustomerCtrl"
            })
            //Supplier
            .when("/suppliers", {
                templateUrl: "angular/templates/suppliers/views/suppliers.html",
                controller: "suppliersCtrl"
            })
            .when("/suppliers/search/:name", {
                templateUrl: "angular/templates/suppliers/views/suppliers.html",
                controller: "suppliersCtrl"
            })
            .when("/suppliers/create", {
                templateUrl: "angular/templates/suppliers/views/createSupplier.html",
                controller: "createSupplierCtrl"
            })
            //Third Party Inspections
            .when("/thirdpartyinspections", {
                templateUrl: "angular/templates/thirdpartyinspections/views/thirdPartyInspections.html",
                controller: "thirdPartyInspectionsCtrl"
            })
            .when("/thirdpartyinspections/search/:name", {
                templateUrl: "angular/templates/thirdpartyinspections/views/thirdPartyInspections.html",
                controller: "thirdPartyInspectionsCtrl"
            })
            .when("/thirdpartyinspections/create", {
                templateUrl: "angular/templates/thirdpartyinspections/views/createThirdPartyInspection.html",
                controller: "createThirdPartyInspectionCtrl"
            })
            //Raw Material Forms
            .when("/rawmaterialforms", {
                templateUrl: "angular/templates/rawmaterialforms/views/rawMaterialForms.html",
                controller: "rawMaterialFormsCtrl"
            })
            .when("/rawmaterialforms/search/:name", {
                templateUrl: "angular/templates/rawmaterialforms/views/rawMaterialForms.html",
                controller: "rawMaterialFormsCtrl"
            })
            .when("/rawmaterialforms/create", {
                templateUrl: "angular/templates/rawmaterialforms/views/createRawMaterialForm.html",
                controller: "createRawMaterialFormCtrl"
            })
            //Specifications
            .when("/specifications", {
                templateUrl: "angular/templates/specifications/views/specifications.html",
                controller: "specificationsCtrl"
            })
            .when("/specifications/search/:name", {
                templateUrl: "angular/templates/specifications/views/specifications.html",
                controller: "specificationsCtrl"
            })
            .when("/specifications/create", {
                templateUrl: "angular/templates/specifications/views/createSpecification.html",
                controller: "createSpecificationCtrl"
            })
            //Dimensions
            .when("/dimensions", {
                templateUrl: "angular/templates/dimensions/views/dimensions.html",
                controller: "dimensionsCtrl"
            })
            .when("/dimensions/search/:name", {
                templateUrl: "angular/templates/dimensions/views/dimensions.html",
                controller: "dimensionsCtrl"
            })
            .when("/dimensions/create", {
                templateUrl: "angular/templates/dimensions/views/createDimension.html",
                controller: "createDimensionCtrl"
            })
            //Test
            .when("/tests", {
                templateUrl: "angular/templates/tests/views/tests.html",
                controller: "testsCtrl"
            })
            .when("/tests/search/:name", {
                templateUrl: "angular/templates/tests/views/tests.html",
                controller: "testsCtrl"
            })
            .when("/tests/create", {
                templateUrl: "angular/templates/tests/views/createTest.html",
                controller: "createTestCtrl"
            })
            //Material Registers
            .when("/materialregisters", {
                templateUrl: "angular/templates/materialregisters/views/materialRegisterHeaders.html",
                controller: "materialRegisterHeadersCtrl"
            })
            .when("/materialregisters/search/:CTNumber", {
                templateUrl: "angular/templates/materialregisters/views/materialRegisterHeaders.html",
                controller: "materialRegisterHeadersCtrl"
            })
            .when("/materialregisters/create", {
                templateUrl: "angular/templates/materialregisters/views/createMaterialRegisterHeader.html",
                controller: "createMaterialRegisterHeaderCtrl"
            })
            .when("/materialregisters/edit", {
                templateUrl: "angular/templates/materialregisters/views/editMaterialRegisterHeader.html",
                controller: "editMaterialRegisterHeaderCtrl"
            })
            .when("/materialregisters/edit/:ID", {
                templateUrl: "angular/templates/materialregisters/views/editMaterialRegisterHeader.html",
                controller: "editMaterialRegisterHeaderCtrl"
            })
            //Heat Charts
            .when("/heatcharts", {
                templateUrl: "angular/templates/heatcharts/views/heatChartHeaders.html",
                controller: "heatChartHeadersCtrl"
            })
            .when("/heatcharts/search/:HCNumber", {
                templateUrl: "angular/templates/heatcharts/views/heatChartHeaders.html",
                controller: "heatChartHeadersCtrl"
            })
            .when("/heatcharts/create", {
                templateUrl: "angular/templates/heatcharts/views/createHeatChartHeader.html",
                controller: "createHeatChartHeaderCtrl"
            })
            .when("/heatcharts/edit", {
                templateUrl: "angular/templates/heatcharts/views/editHeatChartHeader.html",
                controller: "editHeatChartHeaderCtrl"
            })
            .when("/heatcharts/edit/:ID", {
                templateUrl: "angular/templates/heatcharts/views/editHeatChartHeader.html",
                controller: "editHeatChartHeaderCtrl"
            })
           .otherwise({ redirectTo: "/" });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        // Handle Page refreshes

        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] =
                $rootScope.repository.loggedUser.authdata;
        }

        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];

    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path("/login");
        }
    }

})();