module WeatherCharts

open XPlot.GoogleCharts
open Repository
open Repository.RepositoryDto

let getChartHtml() = 
    let chart = 
        [for x in -System.Math.PI..0.1..System.Math.PI -> x, sin x ]
        |> Chart.Line

    chart.GetInlineHtml()
    
let getChartHtmlForTemperature() = 
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
    chart.GetInlineHtml()

//    "Humidity" |> chartForStationSensor;
//    "Pressure" |> chartForStationSensor
