module WeatherCharts

open XPlot.GoogleCharts
open Repository
open Repository.RepositoryDto

let getChartHtml() = 
    let chart = 
        [for x in -System.Math.PI..0.1..System.Math.PI -> x, sin x ]
        |> Chart.Line

    chart.GetInlineHtml()
    
  
let getChartForSensor stationId sensorType = 
    let repository = new Repository.DataPointSqlRepository(new DefaultConnectionStringFactory())

    let data = 
        repository.GetDataPoints(stationId, sensorType,DateTimeRange.Unbounded) 
        |> Seq.filter (fun dp -> dp.SensorValueNumber <> 0.0)
        |> Seq.toList
    
    if (data.IsEmpty) then 
        ""
    else
        data
        |> Seq.map (fun (dp : DataPoint) -> dp.SensorTimestampUtc, dp.SensorValueNumber)
        |> Chart.Line
        |> Chart.WithOptions(Options(title=sensorType))
        |> (fun c -> c.GetInlineHtml()) 

let getChartsHtml(stationId) =
    [
        "Temperature" |> getChartForSensor stationId
        "Humidity" |> getChartForSensor stationId
        "Pressure" |> getChartForSensor stationId
    ] |> List.toSeq
        
let getChartHtmlForTemperature() = 
    "Temperature" |> getChartForSensor
