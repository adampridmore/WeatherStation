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

    let data = repository.GetDataPoints(stationId, sensorType)

    if (data.Count <> 0) then 
        data
        |> Seq.map (fun (dp : DataPoint) -> dp.SensorTimestampUtc, dp.SensorValueNumber)
        |> Seq.filter (fun (_, v) -> v <> 0.0)            
        |> Chart.Line
        |> Chart.WithOptions(Options(title=sensorType))
        |> (fun c -> c.GetInlineHtml()) 
    else
        ""

let getChartsHtml() =
    [
        "Temperature" |> getChartForSensor 
        "Humidity" |> getChartForSensor 
        "Pressure" |> getChartForSensor 
    ] |> List.toSeq
        
let getChartHtmlForTemperature() = 
    "Temperature" |> getChartForSensor
