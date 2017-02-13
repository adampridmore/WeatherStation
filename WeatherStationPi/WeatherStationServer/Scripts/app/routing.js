(function() {
    var configFunction = function($locationProvider, $routeProvider) {
        $locationProvider.html5Mode(true);
        $routeProvider.when("/index.html/page/:pageName",
            {
                template: '<weather-page name="{{pageName}}"></weather-page>',
                controller: [
                    '$scope', '$routeParams', function($scope, $routeParams) {
                        console.log($routeParams);
                        $scope.pageName = $routeParams.pageName;
                    }
                ]
            })
            .otherwise({
                template: '<weather-page name="default"></state-page>'
            });
    }

    angular
        .module("myApp")
        .config(['$locationProvider', '$routeProvider', configFunction]);
})();