(function () {
    'use strict';

    var controllerId = 'homeController';

    function getStatsByChampion(summoner, id) {
        var stats = null;
        summoner.championStats.forEach(function (champStats) {
            if (champStats.championStats.id === id)
                stats = champStats;
        });
        return stats;
    }

    function getChampionEnabled(champions) {
        return champions.filter(
            function (data) { return data.disabled === false }
        );
    }

    function getArrayOfId(champions) {
        return champions.map(function (champ) {
            return champ.id;
        });
    }

    //Busca los championStats por ID
    function getChampionById(id) {
        return $scope.champions.filter(
            function (data) { return data.code === id }
        );
    }

    function indexFindByKey(array, key, value) {
        for (var i = 0; i < array.length; i++) {
            if (array[i][key] === value) {
                return i;
            }
        }
        return null;
    }

    // ANGULAR


    angular.module('appControllers', [])
        .controller(controllerId, ['$scope', 'Summoner', 'Champion', function ($scope, Summoner, Champion) {

            $scope.init = function () {
                $scope.order('totalMinionKills_byGame');
                $scope.reverse = true;
                $scope.friends = [];
            };

            $scope.buscar = function () {
                $scope.loading = true;
                Summoner.get({ summonerId: $scope.summonerName }, function (summoners) {
                    $scope.summoners = summoners;
                    $scope.champions = Champion.query();
                    filterByChampion();
                    $scope.loading = false;
                });
            };

            $scope.order = function (predicate) {
                $scope.reverse = ($scope.orderProp === predicate) ? !$scope.reverse : true;
                $scope.orderProp = predicate;
            };

            $scope.selectChampion = function (champ) {
                toogleChampion(champ);
                filterByChampion();
            };

            function updateStatsByChampions(championsArray) {
                var arrayID = getArrayOfId(championsArray);
                if (arrayID.length === 0)
                    arrayID = [0];
                $scope.summoners.forEach(function (s) {
                    var index = indexFindByKey($scope.friends, 'name', s.name);
                    var summonerStat = { totalMinionKills_byGame: 0, winrate: 0, totalPentakills: 0, kda: 0 };
                    var games = 0;  //Juegos totales con todos los campeones
                    arrayID.forEach(function (id) {
                        var stats = getStatsByChampion(s, id);
                        if (stats != null) {
                            var gamesByChamp = stats.calculatedChampionStat.games;
                            summonerStat = {
                                show: false,
                                name: s.name,
                                urlImage: s.urlImage,
                                totalMinionKills_byGame: stats.calculatedChampionStat.totalMinionKills_byGame * gamesByChamp + summonerStat.totalMinionKills_byGame,
                                totalPentakills: stats.championStats.stats.totalPentaKills + summonerStat.totalPentakills,
                                kda: stats.calculatedChampionStat.kda * gamesByChamp + summonerStat.kda,
                                winrate: stats.calculatedChampionStat.winrate * gamesByChamp + summonerStat.winrate
                            };
                            games = games + gamesByChamp;
                        }
                    });
                    if (summonerStat.name !== undefined)    //Si está undefined es que no agregó ningún stat
                    {
                        summonerStat.totalMinionKills_byGame = summonerStat.totalMinionKills_byGame / games;
                        summonerStat.kda = summonerStat.kda / games;
                        summonerStat.winrate = summonerStat.winrate / games;
                        summonerStat.show = true;
                        //$scope.friends.push(summonerStat);
                    }
                    actualizarEnListaAmigos(summonerStat, s.name);
                });
            }



            function actualizarEnListaAmigos(summonerStat, name) {
                var index = indexFindByKey($scope.friends, 'name', name);
                if (index === undefined || index === null)    //Antes no existía
                {
                    $scope.friends.push(summonerStat);
                }
                else {      // Update 
                    $scope.friends[index] = summonerStat;
                }
                
            }

            function filterByChampion() {
                var championsArray = getChampionEnabled($scope.champions);
                updateStatsByChampions(championsArray);
            };

            function toogleChampion(champ) {
                var index = $scope.champions.indexOf(champ);
                $scope.champions[index].disabled = $scope.champions[index].disabled === false ? true : false;
            }

            $scope.filterNameEmpty = function (item) {
                // must have array, and array must be empty
                return item !== undefined && item.name !== undefined && item.name.length > 0;
            };
        }]);



})();
