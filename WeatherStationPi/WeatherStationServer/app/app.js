(function() {
    var app = angular.module("myApp", ["ngRoute", "ui.bootstrap"]);

    app.controller("myController",
    [
        "$scope", function($scope) {
            $scope.message = "Hello World from angular!";
            $scope.stationIds = ["testStation", "weatherStation1_raspberrypi"];
            $scope.selectedStation = {
                stationId: $scope.stationIds[0],
                latestSensorData: {
                    temperature: {
                        value: 25.0,
                        timestamp: new Date("2001-01-01T12:45:30Z")
                    }
                }
            }
        }
    ]);
})();