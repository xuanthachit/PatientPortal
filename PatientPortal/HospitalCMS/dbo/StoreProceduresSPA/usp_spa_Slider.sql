﻿CREATE PROC [dbo].[usp_spa_Slider]
	@Id TINYINT
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRY
		IF @Id > 0
			SELECT * FROM [dbo].[Slider]
			WHERE [Id] = @Id
		ELSE
			SELECT * FROM [dbo].[Slider] WHERE ([IsUsed] = 1 AND [ExpiredDate]  >= convert(varchar(10), GETDATE(), 120)) AND [Image] <> '' 
			ORDER BY [Id]
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0 
		BEGIN
			RETURN NULL
		END
	END CATCH
END