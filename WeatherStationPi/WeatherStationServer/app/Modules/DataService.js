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

        var getLastStationData = function(stationId) {
            // TODO - should encode station id onto URL
            var url = "api/Station/lastValues/" + stationId;
            return $http
                .get(url)
                .then(function(response) {
                        var stationData = response.data;
                        return stationData;
                    },
                    function(error) {
                        console.log(error);
                    });
        };

        var getStationSensorsDataPoints = function(stationId, lastHours) {
            var url = "api/station/dataPoints/" + stationId;
            if (lastHours !== undefined) {
                url += "?lastHours=" + lastHours;
            }
            return $http
                .get(url)
                .then(function(response) {
                        return response.data;
                    },
                    function(error) {
                        console.log(error);
                    });
        };

        var getSensorsSummary = function() {
            var url = "api/sensor/list";
            return $http.get(url)
                .then(function(response) {
                        return response.data;
                    },
                    function(error) {
                        console.log(error);
                    });
        };

        return {
            getStationIds: getStationIds,
            getLastStationData: getLastStationData,
            getStationSensorsDataPoints: getStationSensorsDataPoints,
            getSensorsSummary: getSensorsSummary
        };
    };

    angular.module("myApp")
        .factory("dataService", ["$http", "$q", dataService]);
})();