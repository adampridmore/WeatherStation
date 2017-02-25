(function() {
    var dataService = function($http, $q) {
        var service = {
            getStationIds: function() {
                var url = "api/Station/getIds";
                return $http
                    .get(url)
                    .then(function (response) {
                        var stationIds = response.data.ids;
                        return stationIds;
                    },function(error) {
                        console.log(error);
                    });
            }
        };
        return service;
    };

    angular.module("myApp")
        .factory("dataService", ["$http", "$q", dataService]);
})();