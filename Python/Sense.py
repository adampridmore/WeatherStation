from sense_hat import SenseHat
import datetime 
import urllib2,json
import time

sense = SenseHat()
# sense.show_message("Hello world!")



while True:
	temp = sense.get_temperature() 

	postdata = { 
		"DataPoints" : [{
			"SensorValueText" : str(temp)
		}]
	}

	# url = 'http://www.google.com/someservice'
	# url = 'http://calf/WeatherStationServer/api/serverDateTimeUtc'
	# url = 'http://calf/WeatherStationServer/api/test'
	url = 'http://calf/WeatherStationServer/api/dataPoints'

	req = urllib2.Request(url, postdata)
	req.add_header('Content-Type','application/json')

	response = urllib2.urlopen(req, json.dumps(postdata))

	
	print(str(datetime.datetime.now().time()) + ", " + str(temp) + ", " + response.read())
	time.sleep(5)
