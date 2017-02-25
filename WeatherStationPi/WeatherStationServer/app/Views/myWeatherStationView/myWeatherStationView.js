(function() {
    var controller = function($routeParams, $scope, dataService) {
        var currentStationId = $routeParams.stationId;

        var lastHours = $routeParams.lastHours;
        //console.log("LH:" + lastHours);

        var loadStationLastData = function() {
            if (currentStationId === undefined) {
                return;
            }
            dataService.getLastStationData(currentStationId)
                .then(function(data) {
                    // {"sensorTimestampUtc":"2016-11-17T17:42:29.477","value":27.623125076293945,"sensorType":"Temperature"}
                    $scope.lastStationData = data.lastValues;
                });
        };

        var loadStationSensorData = function() {
            if (currentStationId === undefined) {
                return;
            }
            dataService.getStationSensorsDataPoints(currentStationId, lastHours)
                .then(function(data) {
                    // {"sensorTimestampUtc":"2016-11-17T17:42:29.477","value":27.623125076293945,"sensorType":"Temperature"}
                    $scope.sensors = data.sensorValues;
                    //console.log(data);
                });
        };

        $scope.currentStationId = currentStationId;
        $scope.stationIds = [];
        $scope.lastStationData = [];
        $scope.sensors = [];

        dataService.getStationIds()
            .then(function(stationIds) {
                $scope.stationIds = stationIds;

                if (currentStationId === undefined && stationIds.length > 0) {
                    currentStationId = stationIds[0];
                    loadStationLastData();
                    loadStationSensorData();
                }
            });

        loadStationSensorData();
        loadStationLastData();
    };

    angular
        .module("myApp")
        .controller("myWeatherStationView", ["$routeParams", "$scope", "dataService", controller]);
})();