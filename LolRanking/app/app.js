(function () {
    'use strict';
    
    angular.module('app', ['appControllers', 'appServices', 'percentage', 'ngAnimate']);


    angular.module('percentage', [])
    .filter('percentage', ['$window', function ($window) {
        return function (input, decimals, suffix) {
            decimals = angular.isNumber(decimals) ? decimals : 3;
            suffix = suffix || '%';
            if ($window.isNaN(input)) {
                return '';
            }
            return Math.round(input * Math.pow(10, decimals + 2)) / Math.pow(10, decimals) + suffix
        };
    }]);
})();

//app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
//        $routeProvider.when('/', {
//            templateUrl: '/Home/Home',
//            controller: 'homeController'
//        });
//        $routeProvider.otherwise({
//            redirectTo: '/'
//        });

//        // Specify HTML5 mode (using the History APIs) or HashBang syntax.
//        $locationProvider.html5Mode(true);
//        //$locationProvider.html5Mode(false).hashPrefix('!');

//    }]);