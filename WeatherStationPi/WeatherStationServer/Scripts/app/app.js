"use strict";

// Declare app level module which depends on views, and components
//angular.module('myApp', [
//  'ngRoute',
//  'myApp.view1',
//  'myApp.view2',
//  'myApp.version'
//]).config(['$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
//    //$locationProvider.hashPrefix('!');
//    $loationnProvider.html5Mode(true);

//    $routeProvider.otherwise({ redirectTo: '/view1' });
//}]);

angular
    .module("myApp", [])
    .controller("myController",
    [
        "$scope", function($scope) {
            $scope.message = "Hello World from angular!";
            $scope.stationIds = ["testStation", "weatherStation1_raspberrypi"];
            $scope.selectedStation = {
                stationId: $scope.stationIds[0],
                latestSensorData: {
                    temperature: {
                        value: 25.0,
                        timestamp : new Date("2001-01-01T12:45:30Z")
                    }
                }
            }
        }
    ]);

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
        template:
            "<div>" +
                "  Weather Station:" +
                "  <select>" +
                "    <option ng-repeat='id in $ctrl.ids' value='{{id}}'>{{id}}</option>" +
                "  </select>" +
                "</div>"
    });