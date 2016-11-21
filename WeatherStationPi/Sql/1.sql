-------------------------------
-- Check and remove Duplicates
-------------------------------
BEGIN TRANSACTION

DECLARE @CountVal int;
SELECT @CountVal = COUNT(*) FROM DataPoints
PRINT 'Before Count: ' + STR(@CountVal)


SELECT MIN(Id) As Id
INTO #Temp_DataPointsToRemove
FROM DataPoints
GROUP BY SensorType, StationId, SensorTimestampUtc
HAVING COUNT(*) > 1

DELETE dp 
FROM DataPoints dp
INNER JOIN #Temp_DataPointsToRemove temp ON (dp.Id = temp.Id)

SELECT @CountVal = COUNT(*) FROM DataPoints
PRINT 'After Count: ' + STR(@CountVal)

DROP TABLE #Temp_DataPointsToRemove


COMMIT


---------------------------------------------------
-- Change primary key to compound key, and drop Id
---------------------------------------------------

BEGIN TRANSACTION

ALTER TABLE dbo.DataPoints
DROP CONSTRAINT [PK_dbo.DataPoints]

ALTER TABLE DataPoints ALTER COLUMN StationId nvarchar(128) NOT NULL
ALTER TABLE DataPoints ALTER COLUMN SensorType nvarchar(128) NOT NULL
ALTER TABLE DataPoints ALTER COLUMN SensorTimestampUtc  datetime NOT NULL
ALTER TABLE DataPoints DROP COLUMN SensorValueText
ALTER TABLE DataPoints DROP COLUMN Id


ALTER TABLE dbo.DataPoints
ADD CONSTRAINT  PK_DataPoints_StationId_SensorType_SensorTimestampUtc PRIMARY KEY(StationId, SensorType, SensorTimestampUtc)

PRINT 'Primary key changed to a compound key'

COMMIT


