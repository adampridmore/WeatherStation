(function() {
    var controllerFunction = function() {
        this.$onInit = () => {
            // console.log(this.ids);
        }
    };

    angular.module("myApp")
        .component("stationPicker",
        {
            bindings: {
                ids: "<"
            },
            controller: [controllerFunction],
            templateUrl: "Scripts/app/Component/stationPicker/stationPicker-template.html"
        });
})();