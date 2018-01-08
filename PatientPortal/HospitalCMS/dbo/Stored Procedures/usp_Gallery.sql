﻿CREATE PROC [dbo].[usp_Gallery]
	@Id VARCHAR(128)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRY
		IF @Id <> ''
			SELECT * FROM [dbo].[Gallery]
			WHERE [Id] = @Id
			ORDER BY [Id]
		ELSE
			SELECT * FROM [dbo].[Gallery]
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0 
		BEGIN
			RETURN NULL
		END
	END CATCH
END