(function () {
    'use strict';

    angular.module('appServices', ['ngResource'])
        .factory('Summoner', ['$resource',
              function ($resource) {
                  return $resource('/api/:summonerId/friendsStats', {}, {
                      get: { method: 'GET', params: {}, isArray: true }
                  });
              }])
        .factory('Champion', ['$resource',
              function ($resource) {
                  return $resource('/api/champion', null,
                      {
                          'query': { method: 'GET', isArray: true }
                      }
                      );
              }]);

})();