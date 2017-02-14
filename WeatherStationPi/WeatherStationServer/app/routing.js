(function() {
    var configFunction = function($locationProvider, $routeProvider) {
        $locationProvider.html5Mode(true);

        var createController = function(name) {
            return {
                templateUrl: "app/Views/" + name + "/" + name + "-template.html",
                controller: name,
                controllerAs: "vw"
            }
        };

        $routeProvider
            .when("/V2/MyWeatherStation/:stationId", createController("myWeatherStationView"))
            .when("/V2/MyWeatherStation", createController("myWeatherStationView"))
            .when("/V2/AllWeatherSensors", createController("allWeatherSensorsView"))
            .otherwise({ template: "<h1>Otherwise</h1>" });
    }

    angular
        .module("myApp")
        .config(["$locationProvider", "$routeProvider", configFunction]);
})();