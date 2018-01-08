﻿CREATE PROC [dbo].[usp_QA]
	@Id INT
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRY
		IF @Id > 0
			SELECT * FROM [dbo].[QA]
			WHERE [Id] = @Id
		ELSE
			SELECT * FROM [dbo].[QA]
			ORDER BY [Id]
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0 
		BEGIN
			RETURN NULL
		END
	END CATCH
END