# from sense_hat import SenseHat
import datetime 
import urllib.request
import json
import time

#sense = SenseHat()
# sense.show_message("Hello world!")

while True:
        #temp = sense.get_temperature()
        temp = 100
        postdata = {
                "DataPoints" : [{
                        "SensorValueText" : str(temp)
                }]
        }
        # url = 'http://www.google.com/someservice'
        # url = 'http://calf/WeatherStationServer/api/serverDateTimeUtc'
        # url = 'http://calf/WeatherStationServer/api/test'
        # url = 'http://calf/WeatherStationServer/api/dataPoints'
        #url = 'http://localhost/WeatherStationServer/api/dataPoints'
        url = 'http://localhost:59653/api/dataPoints'
       
        req = urllib.request.Request(url)
        req.add_header('Content-Type','application/json')
               
        jsondata = json.dumps(postdata)
        jsondatabytes = jsondata.encode('utf-8')
        response = urllib.request.urlopen(req, jsondatabytes)
	
        print(str(datetime.datetime.now().time()) + ", " + str(temp) + ", " + str(response.read()))
        time.sleep(5)
