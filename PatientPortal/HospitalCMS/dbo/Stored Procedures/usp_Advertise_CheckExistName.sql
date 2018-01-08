﻿CREATE PROCEDURE [dbo].[usp_Advertise_CheckExistName]
	@Name  NVARCHAR(50),
	@Id TINYINT
AS
BEGIN
	DECLARE @return BIT = 0

	print @Id
	IF(@Id = 0)
		IF(EXISTS(SELECT TOP 1 1 FROM [dbo].[Advertise] WHERE [Name] = @Name))
			SET @return = 1
	IF(EXISTS(SELECT TOP 1 1 FROM [dbo].[Advertise] WHERE [Name] = @Name and Id <> @Id))
			SET @return = 1
	SELECT @return
END
GO