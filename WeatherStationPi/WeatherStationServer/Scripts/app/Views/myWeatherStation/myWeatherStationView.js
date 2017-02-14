
(function() {
    angular
        .module("myApp")
        .component("myWeatherStationView",
        {
            bindings: {
                name: "@"
            },
            controller: [function() {}],
            templateUrl: "Scripts/app/Views/myWeatherStation/myWeatherStationView-template.html"
            //template: "Fish: {{$ctrl.name}}"
        });
})();