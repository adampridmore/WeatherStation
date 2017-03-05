/****** Object:  Table [dbo].[DataPoints]    Script Date: 05/03/2017 16:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataPoints](
	[StationId] [nvarchar](128) NOT NULL,
	[SensorType] [nvarchar](128) NOT NULL,
	[SensorValueNumber] [float] NOT NULL,
	[SensorTimestampUtc] [datetime] NOT NULL,
	[ReceivedTimestampUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.DataPoints] PRIMARY KEY CLUSTERED 
(
	[StationId] ASC,
	[SensorType] ASC,
	[SensorTimestampUtc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
