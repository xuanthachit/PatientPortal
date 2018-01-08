﻿CREATE TABLE [dbo].[UserNotification]
(
	[Id] VARCHAR(128) NOT NULL PRIMARY KEY, 
    [Detail] NVARCHAR(300) NOT NULL, --Message
    [Date] VARCHAR(10) NOT NULL, 
    [Time] VARCHAR(8) NOT NULL, 
    [UserId] NVARCHAR(128) NOT NULL, -- UserId
    [IsRead] BIT NOT NULL
)
