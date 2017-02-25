(function() {
    var controllerFunction = function(uniqueIdService, $timeout) {
        var vm = this;
        vm.elementId = uniqueIdService.getId();

        var renderChart = function() {
            var data = google.visualization.arrayToDataTable([
                ['Year', 'Sales', 'Expenses'],
                ['2004', 1000, 400],
                ['2005', 1170, 460],
                ['2006', 660, 1120],
                ['2007', 1030, 540]
            ]);
            var options = {
                title: vm.title
            };

            var div = document.getElementById(vm.elementId);
            console.log(div);

            var chart = new google.visualization.LineChart(div);

            chart.draw(data, options);
        };

        this.$postLink = () => {
            // This waits until the page is rendred before attaching the google chart.
            $timeout(renderChart, 0);
        };

        this.$onChanges = () => {
        }
    };

    angular.module("myApp")
        .component("plot",
        {
            bindings: {
                title: "@",
                dataPoints: "<"
            },
            controller: ["uniqueIdService", "$timeout", controllerFunction],
            templateUrl: "app/Component/plot/plot-template.html"
        });
})();