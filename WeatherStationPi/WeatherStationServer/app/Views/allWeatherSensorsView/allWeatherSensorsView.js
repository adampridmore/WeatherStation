﻿(function() {
    var controller = function (dataService, $scope) {
        $scope.sensorList = [];

        dataService.getSensorsSummary()
            .then(function (data) {
                console.log(data);
                $scope.sensorList = data;
            });
    };

    angular
        .module("myApp")
        .controller("allWeatherSensorsView", ["dataService", "$scope",controller]);
})();