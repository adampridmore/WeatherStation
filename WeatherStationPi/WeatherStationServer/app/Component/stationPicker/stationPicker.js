(function() {
    var controllerFunction = function() {
        var vm = this;

        var setDefaultSelectedStation = () => {
            if (vm.currentStationId === undefined) {
                if (vm.ids.length > 0) {
                    vm.currentStationId = vm.ids[0];
                }
            }
        }

        this.$onInit = () => {
            if (vm.ids === undefined) {
                vm.ids = [];
            }
            setDefaultSelectedStation();
        };

        this.$onChanges = () => {
            setDefaultSelectedStation();
        }
    };

    angular.module("myApp")
        .component("stationPicker",
        {
            bindings: {
                ids: "<",
                currentStationId: "<"
            },
            controller: [controllerFunction],
            templateUrl: "app/Component/stationPicker/stationPicker-template.html"
        });
})();