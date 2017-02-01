/// <reference path="stationPicker-template.html" />
angular.module("myApp")
    .component("stationPicker",
    {
        bindings: {
            ids: "<"
        },
        controller: [
            function() {
            }
        ],
        templateUrl: "Scripts/app/Component/stationPicker-template.html"
    });