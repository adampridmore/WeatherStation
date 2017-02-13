(function() {
    angular.module("myApp")
        .component("weatherPage",
        {
            bindings: {
                name: "@"
            },
            controller: [function() {}],
            templateUrl: "Scripts/app/Component/weatherPage/weatherPage-template.html"
            //template: "Fish: {{$ctrl.name}}"
        });
})();