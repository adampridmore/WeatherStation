#r @"..\Repository\bin\Debug\Repository.dll"

let connectionString = "server=.\SQLEXPRESS;database=WeatherStation;Integrated Security = True; Connect Timeout = 5";
let factory = Repository.ConnectionStringFactory.CreateFromConnectionString(connectionString)

let repository = new Repository.DataPointRepository(factory)

let data = repository.FindAllByStationId("weatherStation1_raspberrypi")

let startDate = new System.DateTime(2016,11,18,0,0,0, System.DateTimeKind.Utc)
let endDate = new System.DateTime(2016,11,19,0,0,0, System.DateTimeKind.Utc)


let dataToString (dataPoint: Repository.RepositoryDto.DataPoint) =
    sprintf "%s (%s) %f" (dataPoint.SensorTimestampUtc.ToString("o")) dataPoint.SensorType dataPoint.SensorValueNumber

data 
|> Seq.filter (fun dp -> dp.SensorTimestampUtc < endDate)
|> Seq.filter (fun dp -> dp.SensorTimestampUtc > startDate)
|> Seq.filter (fun dp -> dp.SensorType = "Temperature")
|> Seq.sortBy (fun dp -> dp.SensorTimestampUtc)
|> Seq.iter(fun dp-> printfn "%s" (dp |> dataToString ) )
