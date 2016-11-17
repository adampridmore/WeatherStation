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

let repository = new Repository.DataPointRepository()

let dataPointToString (dp:DataPoint) = 
    sprintf "%s - %s" dp.SensorValueText (dp.TimeStamp.Value.ToString("O"))

let chartForStationSendor stationId sensorType =
    repository.GetDataPoints(stationId, sensorType)
    //|> Seq.map dataPointToString
    //|> Seq.iter (printfn "%A")
    |> Seq.map (fun (dp:DataPoint) -> dp.TimeStamp.Value, dp.SensorValueNumber)
    |> Seq.filter (fun (_ , v) -> v <> 0.0)
    |> Chart.Line |> Chart.WithTitle sensorType

[   chartForStationSendor "weatherStation1_raspberrypi" "Temperature";
    chartForStationSendor "weatherStation1_raspberrypi" "Humidity";
    chartForStationSendor "weatherStation1_raspberrypi" "Pressure"]
|> Chart.Rows
|> Chart.Show