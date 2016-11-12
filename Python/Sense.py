from sense_hat import SenseHat
import datetime 
import urllib2,json


sense = SenseHat()

# sense.show_message("Hello world!")

current_time = datetime.datetime.now().time() 

temp = sense.get_temperature() 

print(temp)

# url = 'http://www.google.com/someservice'
# url = 'http://calf/WeatherStationServer/api/serverDateTimeUtc'
# url = 'http://calf/WeatherStationServer/api/test'
url = 'http://calf/WeatherStationServer/api/dataPoints'

postdata = { 
	"DataPoints" : [{
	    "SensorValueText" : str(temp)
    }]
}

# POST: http://localhost:59653/api/dataPoints
# {
#	"DataPoints" : [{
#		"SensorValueText" : "ValueText"
#		}]
#	}/

req = urllib2.Request(url, postdata)
req.add_header('Content-Type','application/json')
data = json.dumps(postdata)

#response = urllib2.urlopen(req,data)
response = urllib2.urlopen(req, data)
print(response.read())





