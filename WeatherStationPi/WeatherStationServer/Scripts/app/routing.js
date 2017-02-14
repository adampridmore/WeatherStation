(function() {
    var configFunction = function($locationProvider, $routeProvider) {
        $locationProvider.html5Mode(true);
        $routeProvider.when("/V2/MyWeatherStation",
            {
                template: '<weather-page name="{{pageName}}"></weather-page>',
                controller: "myWeatherStationView",
                controllerAs : "vw"
            })
            .otherwise({
                template: '<weather-page name="default"></state-page>'
            });
    }

    angular
        .module("myApp")
        .config(['$locationProvider', '$routeProvider', configFunction]);
})();