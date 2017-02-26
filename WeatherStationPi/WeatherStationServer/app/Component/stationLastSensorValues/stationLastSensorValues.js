(function() {
    var controllerFunction = function() {
        var vm = this;

        this.$onInit = () => {
        };

        this.$onChanges = () => {
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