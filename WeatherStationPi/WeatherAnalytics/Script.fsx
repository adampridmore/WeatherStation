// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.
#r @"..\Repository\bin\Debug\Repository.dll"
//#r @"..\packages\FSharp.Data.2.3.2\lib\net40\FSharp.Data.dll"
#r @"..\packages\FSharp.Charting.0.90.14\lib\net40\FSharp.Charting.dll"

open Repository
open Repository.RepositoryDto
open FSharp.Data
open FSharp.Charting

// Define your library scripting code here
let stationId = "weatherStation1_raspberrypi"
//let stationId = "weatherStation1_MNAB-DEV14L"

let repository = new Repository.DataPointRepository()
let dataPointToString (dp : DataPoint) = sprintf "%s - %s" dp.SensorValueText (dp.TimeStamp.Value.ToString("O"))

let chartForStationSendor stationId sensorType = 
    repository.GetDataPoints(stationId, sensorType)
    //|> Seq.map dataPointToString
    //|> Seq.iter (printfn "%A")
    |> Seq.map (fun (dp : DataPoint) -> dp.TimeStamp.Value, dp.SensorValueNumber)
    |> Seq.filter (fun (_, v) -> v <> 0.0)
    |> Chart.Line
//    |> Chart.WithYAxis (Min = 950.0 , Max = 990.0)
    |> Chart.WithTitle (Text = (sprintf "%s (%s)" sensorType stationId), InsideArea = false)

[ chartForStationSendor stationId "Temperature"
  chartForStationSendor stationId "Humidity"
  chartForStationSendor stationId "Pressure" ]
|> Chart.Rows
|> Chart.Show
