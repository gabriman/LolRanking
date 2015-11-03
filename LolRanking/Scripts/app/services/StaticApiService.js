(function () {
    'use strict';

    angular.module('appServices', ['ngResource'])
        .factory('Champion', ['$resource',
              function ($resource) {
                  return $resource('api/champions', {});
              }]);

})();