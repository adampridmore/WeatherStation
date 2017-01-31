'use strict';

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
    .module('myApp', [])
    .controller('myController', ['$scope', function($scope) {
        $scope.message = 'Hello World from angular!';
    }]);