﻿CREATE PROCEDURE [dbo].[usp_Advertise]
	@Id TINYINT
AS
	IF(@Id > 0)
	BEGIN
		SELECT * FROM [dbo].[Advertise]
		WHERE [Id] = @Id 
	END
	ELSE
	BEGIN
		SELECT * FROM [dbo].[Advertise]
		ORDER BY [Id]
	END
RETURN 0