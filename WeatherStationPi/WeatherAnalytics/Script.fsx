// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.
#r @"..\Repository\bin\Debug\Repository.dll"

open Repository
open Repository.RepositoryDto

// Define your library scripting code here

let repository = new Repository.DataPointRepository()

let dataPointToString (dp:DataPoint) = 
    sprintf "%s - %s" dp.SensorValueText (dp.TimeStamp.Value.ToString("O"))

repository.GetDataPoints("weatherStation1_raspberrypi", "Temperature")
|> Seq.map dataPointToString
|> Seq.iter (printfn "%A")

