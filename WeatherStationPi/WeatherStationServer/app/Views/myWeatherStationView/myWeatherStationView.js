(function() {
    var controller = function($routeParams, $scope, dataService) {
        var currentStationId = $routeParams.stationId;

        $scope.currentStationId = currentStationId;
        $scope.stationIds = [];

        dataService.getStationIds()
            .then(function (stationIds) {
                console.log(stationIds);
                $scope.stationIds = stationIds;
            });

    };

    angular
        .module("myApp")
        .controller("myWeatherStationView", ["$routeParams", "$scope", "dataService", controller]);
})();