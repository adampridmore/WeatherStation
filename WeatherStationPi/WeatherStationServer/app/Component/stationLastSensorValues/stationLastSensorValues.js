(function() {
    var controllerFunction = function() {
        var vm = this;

        this.$onInit = function() {
        };

        this.$onChanges = function() {
        };
    };

    angular.module("myApp")
        .component("stationLastSensorValues",
        {
            bindings: {
                sensorValues: "<"
            },
            controller: [controllerFunction],
            templateUrl: "app/Component/stationLastSensorValues/stationLastSensorValues-template.html"
        });
})();