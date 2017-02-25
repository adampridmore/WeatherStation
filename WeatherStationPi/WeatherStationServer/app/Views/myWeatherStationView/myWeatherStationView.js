(function() {
    var controller = function($routeParams, $scope, dataService) {
        var currentStationId = $routeParams.stationId;

        $scope.currentStationId = currentStationId;
        $scope.stationIds = [];
        $scope.lastStationData = [];

        dataService.getStationIds()
            .then(function(stationIds) {
                //console.log(stationIds);
                $scope.stationIds = stationIds;
            });

        if (currentStationId !== undefined) {
            dataService.getStationData(currentStationId)
                .then(function(data) {
                    // {"sensorTimestampUtc":"2016-11-17T17:42:29.477","value":27.623125076293945,"sensorType":"Temperature"}
                    $scope.lastStationData = data.lastValues;
                });
        }
    };

    angular
        .module("myApp")
        .controller("myWeatherStationView", ["$routeParams", "$scope", "dataService", controller]);
})();