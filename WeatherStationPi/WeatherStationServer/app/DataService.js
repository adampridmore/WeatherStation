(function() {
    var dataService = function($http, $q) {
        var stationIds = null;

        var getStationIdsFromServer = function() {
            var url = "api/Station/getIds";
            return $http
                .get(url)
                .then(function(response) {
                        stationIds = response.data.ids;
                        return stationIds;
                    },
                    function(error) {
                        console.log(error);
                    });
        };

        var getStationIds = function() {
            if (stationIds === null) {
                return getStationIdsFromServer();
            } else {
                var def = $q.defer();
                def.resolve(stationIds);
                return def.promise;
            }
        };

        return {
            getStationIds: getStationIds
        };
    };

    angular.module("myApp")
        .factory("dataService", ["$http", "$q", dataService]);
})();