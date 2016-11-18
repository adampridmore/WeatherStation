#r @"..\Repository\bin\Debug\Repository.dll"
//#r @"..\packages\FSharp.Data.2.3.2\lib\net40\FSharp.Data.dll"
//#r @"..\packages\FSharp.Charting.0.90.14\lib\net40\FSharp.Charting.dll"
//#r @"System.Windows.Forms.DataVisualization.dll"
#r @"..\packages\XPlot.GoogleCharts.1.4.2\lib\net45\XPlot.GoogleCharts.dll"
#r @"..\packages\Google.DataTable.Net.Wrapper.3.1.2.0\lib\Google.DataTable.Net.Wrapper.dll"
#r @"..\packages\Newtonsoft.Json.9.0.1\lib\net45\Newtonsoft.Json.dll"

#load "WeatherCharts.fs"

WeatherCharts.getChartHtml();


open XPlot.GoogleCharts

//[   
//    [for x in -System.Math.PI..0.1..System.Math.PI -> x, sin x ] ;
//    [for x in -System.Math.PI..0.1..System.Math.PI -> x, cos x ]
//]
//|> Chart.Line 
////|> Chart.WithOptions(Options(curveType = "function"))
//|> Chart.Show



open Repository
open Repository.RepositoryDto

let stationId = "weatherStation1_MNAB-DEV14L"

let repository = new Repository.DataPointRepository()
let dataPointToString (dp : DataPoint) = sprintf "%s - %s" dp.SensorValueText (dp.SensorTimestampUtc.ToString("O"))

let chartForStationSensor sensorType = 
    let dataPoints = 
        repository.GetDataPoints(stationId, sensorType)
        |> Seq.map (fun (dp : DataPoint) -> dp.SensorTimestampUtc, dp.SensorValueNumber)
        |> Seq.filter (fun (_, v) -> v <> 0.0)            
    
    dataPoints
    |> Chart.Line
    |> Chart.WithOptions(Options(title=sensorType))

let chart = "Temperature" |> chartForStationSensor
//    "Humidity" |> chartForStationSensor;
//    "Pressure" |> chartForStationSensor

chart.GetInlineHtml()




