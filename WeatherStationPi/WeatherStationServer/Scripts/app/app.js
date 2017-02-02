"use strict";

var app = angular.module("myApp", ['ngRoute', 'ui.bootstrap']);

app.config([
    '$locationProvider', '$routeProvider',
    function config($locationProvider, $routeProvider) {
        $locationProvider.html5Mode(true);
        $routeProvider.when("/index.html/page/:pageName",
            {
                template: '<weather-page name="{{pageName}}"></weather-page>',
                controller: [
                    '$scope', '$routeParams', function ($scope, $routeParams) {
                        console.log($routeParams);
                        $scope.pageName = $routeParams.pageName;
                    }
                ]
            })
            .otherwise({
                template: '<weather-page name="default"></state-page>'
            });
    }
]);

app.controller("myController",
[
    "$scope", function($scope) {
        $scope.message = "Hello World from angular!";
        $scope.stationIds = ["testStation", "weatherStation1_raspberrypi"];
        $scope.selectedStation = {
            stationId: $scope.stationIds[0],
            latestSensorData: {
                temperature: {
                    value: 25.0,
                    timestamp: new Date("2001-01-01T12:45:30Z")
                }
            }
        }
    }
]);

