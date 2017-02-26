(function() {
    var uniqueIdService = function() {
        var id = 0;

        return {
            getId: function() {
                return ++id;
            }
        };
    };

    angular.module("myApp")
        .factory("uniqueIdService", [uniqueIdService]);
})();