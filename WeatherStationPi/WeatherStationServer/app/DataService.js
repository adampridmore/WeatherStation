(function() {
    var dataService = function($http, $q) {
        var service = {
            getStationIds: function() {
                var url = "api/Station";
                return $http
                    .get(url)
                    .then(function(responce) {
                        var stationIdsResponse = responce.data;
                        //console.table(people);
                        return stationIdsResponse.stationIds;
                    });
            }
        };
        return service;
    };

    angular.module("myApp")
        .factory("dataService", ["$http", "$q", dataService]);
})();