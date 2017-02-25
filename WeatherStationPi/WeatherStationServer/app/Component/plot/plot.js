(function() {
    var controllerFunction = function(uniqueIdService, $timeout) {
        var vm = this;
        vm.elementId = uniqueIdService.getId();

        var createChartData = function(data)
        {
            var rows = data.map(function (sensorValue) {
                return [new Date(sensorValue.timestampUtc), sensorValue.value];
            });

            var dataTable = new google.visualization.DataTable();
            dataTable.addColumn("date", "Time");
            dataTable.addColumn("number", vm.title);
            dataTable.addRows(rows);

            return dataTable;
        }

        var renderChart = function() {
            if (vm.data === undefined) {
                return;
            }

            var div = document.getElementById(vm.elementId);

            var chart = new google.visualization.LineChart(div);
            var options = {
                title: vm.title,
                legend: "none"
            };

            var dataTable = createChartData(vm.data);
            chart.draw(dataTable, options);
        };

        this.$postLink = () => {
            // This waits until the page is rendred before attaching the google chart.
            $timeout(renderChart, 0);
        };

        this.$onChanges = () => {
            console.log("data: " + vm.data);
        }
    };

    angular.module("myApp")
        .component("plot",
        {
            bindings: {
                title: "@",
                data: "<"
            },
            controller: ["uniqueIdService", "$timeout", controllerFunction],
            templateUrl: "app/Component/plot/plot-template.html"
        });
})();