(function() {
    var configFunction = function($locationProvider, $routeProvider) {
        $locationProvider.html5Mode(true);
        $routeProvider.when("/V2/MyWeatherStation",
            {
                templateUrl: "app/Views/myWeatherStation/myWeatherStationView-template.html",
                controller: "myWeatherStationView",
                controllerAs : "vw"
            })
            .otherwise({
                template: "<h1>Otherwise</h1>"
            });
    }

    angular
        .module("myApp")
        .config(["$locationProvider", "$routeProvider", configFunction]);
})();