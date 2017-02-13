/// <reference path="stationPicker-template.html" />
angular.module("myApp")
    .component("stationPicker",
    {
        bindings: {
            ids: "<"
        },
        controller: [
            function() {
                this.$onInit = () => {
                    // console.log(this.ids);
                }
            }
        ],
        templateUrl: "Scripts/app/Component/stationPicker-template.html"
    });