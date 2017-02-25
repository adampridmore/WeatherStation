(function() {
    var controller = function($routeParams, $scope, dataService) {
        var currentStationId = $routeParams.stationId;

        var loadStationData = function() {
            if (currentStationId === undefined) {
                return;
            }
            dataService.getStationData(currentStationId)
                .then(function(data) {
                    // {"sensorTimestampUtc":"2016-11-17T17:42:29.477","value":27.623125076293945,"sensorType":"Temperature"}
                    $scope.lastStationData = data.lastValues;
                });
        };
        
        $scope.currentStationId = currentStationId;
        $scope.stationIds = [];
        $scope.lastStationData = [];

        dataService.getStationIds()
            .then(function(stationIds) {
                $scope.stationIds = stationIds;

                if (currentStationId === undefined && stationIds.length > 0) {
                    currentStationId = stationIds[0];
                    loadStationData();
                }
            });

        loadStationData();
    };

    angular
        .module("myApp")
        .controller("myWeatherStationView", ["$routeParams", "$scope", "dataService", controller]);
})();