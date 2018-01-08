﻿CREATE TABLE [dbo].[Schedule]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Priority] TINYINT NOT NULL, 
    [Detail] NVARCHAR(300) NOT NULL, 
    [IsAlarm] BIT NOT NULL, 
    [Start] DATETIME NOT NULL, 
	[End] DATETIME NOT NULL, 
    [Color] VARCHAR(6) NOT NULL, 
    [IsExamine] BIT NOT NULL, 
    
    [UserId] NVARCHAR(128) NOT NULL
)