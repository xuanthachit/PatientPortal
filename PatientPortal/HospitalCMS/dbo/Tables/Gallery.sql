﻿CREATE TABLE [dbo].[Gallery]
(
	[Id] VARCHAR(128) NOT NULL PRIMARY KEY, 
	[Title] NVARCHAR(128) NOT NULL, 
	[Description] NVARCHAR(300) NOT NULL, 
	[Highlight] NVARCHAR(100) NOT NULL, 
	[Img] VARCHAR(256) NOT NULL, 
	[YoutubeURL] VARCHAR(500) NULL,
	[Date] VARCHAR(20) NOT NULL, 
	[DepartmentId] TINYINT NOT NULL, 
	[IsMultiple] BIT NOT NULL 
)
