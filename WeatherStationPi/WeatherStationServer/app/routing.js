(function() {
    var configFunction = function($locationProvider, $routeProvider) {
        $locationProvider.html5Mode(true);

        var createController = function(name) {
            return {
                templateUrl: "app/Views/" + name + "/" + name + "-template.html",
                controller: name,
                controllerAs: "vw"
            };
        };

        var myWeatherStationView = createController("myWeatherStationView");
        $routeProvider
            .when("/V2/MyWeatherStation/:stationId", myWeatherStationView)
            .when("/V2/MyWeatherStation", myWeatherStationView)
            .when("/V2/AllWeatherSensors", createController("allWeatherSensorsView"))
            .otherwise(myWeatherStationView);
    };
    angular
        .module("myApp")
        .config(["$locationProvider", "$routeProvider", configFunction]);
})();