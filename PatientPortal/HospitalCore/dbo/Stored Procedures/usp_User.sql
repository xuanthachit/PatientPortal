﻿CREATE PROC [dbo].[usp_User]
	@Id NVARCHAR(128) = '',
	@Type tinyint = 0,
	@Search nvarchar(250) = ''
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRY
	IF(@Id <> '')
		SELECT * FROM [dbo].[Users]
		WHERE Id = @Id
	ELSE
	BEGIN
		IF(@Type = 1)
			SELECT * FROM [dbo].[Users]
			WHERE IsAdmin = 1 AND (Name like '%' + @Search + '%' OR @Search is null)
		IF(@Type = 2)
			SELECT * FROM [dbo].[Users]
			WHERE IsDoctor = 1 AND (Name like '%' + @Search + '%' OR @Search is null)
							   AND (Tags like '%' + @Search + '%' OR @Search is null)
	END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0 
		BEGIN
			RETURN NULL
		END
	END CATCH
END