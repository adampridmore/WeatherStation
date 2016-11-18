#r @"..\Repository\bin\Debug\Repository.dll"
//#r @"..\packages\FSharp.Data.2.3.2\lib\net40\FSharp.Data.dll"
#r @"..\packages\FSharp.Charting.0.90.14\lib\net40\FSharp.Charting.dll"
#r @"System.Windows.Forms.DataVisualization.dll"

open Repository
open Repository.RepositoryDto
open FSharp.Data
open FSharp.Charting

// Define your library scripting code here
//let stationId = "weatherStation1_raspberrypi"
let stationId = "weatherStation1_MNAB-DEV14L"

let repository = new Repository.DataPointRepository()
let dataPointToString (dp : DataPoint) = sprintf "%s - %s" dp.SensorValueText (dp.SensorTimestampUtc.ToString("O"))

let chartForStationSensor sensorType = 
    let dataPoints = 
        repository.GetDataPoints(stationId, sensorType)
        |> Seq.map (fun (dp : DataPoint) -> dp.SensorTimestampUtc, dp.SensorValueNumber)
        |> Seq.filter (fun (_, v) -> v <> 0.0)            
    
    let minValue = dataPoints |> Seq.map snd |> Seq.min 
    let maxValue = dataPoints |> Seq.map snd |> Seq.max
    let minMaxBorder = (maxValue - minValue) * 0.1

    dataPoints    
    |> Chart.Line
//    |> Chart.Point
    |> Chart.WithYAxis (Min = minValue - minMaxBorder, Max = maxValue + minMaxBorder)
    |> Chart.WithTitle (Text = (sprintf "%s (%s)" sensorType stationId), InsideArea = false)

//let chart = "Temperature" |> chartForStationSensor


let chart = 
    ["Temperature" |> chartForStationSensor ;
    "Humidity" |> chartForStationSensor ;
    "Pressure" |> chartForStationSensor]
    |> Chart.Rows

chart.ShowChart()


//chart.CopyChartToClipboard()

//let bitmap = chart.CopyAsBitmap()
//
//let filename = @"C:\Temp\Temp.bmp"
//bitmap.Save(filename)
//System.Diagnostics.Process.Start(filename)
