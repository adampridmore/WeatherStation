from sense_hat import SenseHat
import datetime 
import urllib2,json


sense = SenseHat()

# sense.show_message("Hello world!")

current_time = datetime.datetime.now().time() 

print(sense.get_temperature())

# url = 'http://www.google.com/someservice'
#url = 'http://calf/WeatherStationServer/api/serverDateTimeUtc'
url = 'http://calf/WeatherStationServer/api/test'
# postdata = {'key':'value'}

req = urllib2.Request(url)
req.add_header('Content-Type','application/json')
#data = json.dumps(postdata)

#response = urllib2.urlopen(req,data)
response = urllib2.urlopen(req)
print(response.read())





