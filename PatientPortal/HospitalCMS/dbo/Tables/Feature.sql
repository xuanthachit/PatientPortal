﻿CREATE TABLE [dbo].[Feature]
(
	[Id] TINYINT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Image] VARCHAR(256) NOT NULL, 
    [Description] NVARCHAR(256) NOT NULL, 
    [Handler] NVARCHAR(256) NOT NULL, 
    [IsUsed] BIT NOT NULL
)
