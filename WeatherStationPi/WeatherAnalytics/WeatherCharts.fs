module WeatherCharts

open XPlot.GoogleCharts
open Repository
open Repository.RepositoryDto

let getChartHtml() = 
    let chart = 
        [for x in -System.Math.PI..0.1..System.Math.PI -> x, sin x ]
        |> Chart.Line

    chart.GetInlineHtml()
    
let stationId = "weatherStation1_MNAB-DEV14L"
  
let getChartForSensor sensorType = 
    let repository = new Repository.DataPointRepository()

    let chart =     
        repository.GetDataPoints(stationId, sensorType)
        |> Seq.map (fun (dp : DataPoint) -> dp.SensorTimestampUtc, dp.SensorValueNumber)
        |> Seq.filter (fun (_, v) -> v <> 0.0)            
        |> Chart.Line
        |> Chart.WithOptions(Options(title=sensorType))

    chart.GetInlineHtml()

let getChartsHtml() =
    [
        "Temperature" |> getChartForSensor 
        "Humidity" |> getChartForSensor 
        "Pressure" |> getChartForSensor 
    ] |> List.toSeq
        
let getChartHtmlForTemperature() = 
    "Temperature" |> getChartForSensor
