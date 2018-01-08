﻿CREATE PROC [dbo].[usp_Category]
	@Id TINYINT,
	@ParentId TINYINT
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRY
		IF @Id > 0
			SELECT * FROM [dbo].[Category]
			WHERE [Id] = @Id
			ORDER BY [Id]
		ELSE
		BEGIN
			IF @ParentId > 0
				SELECT * FROM [dbo].[Category]
				WHERE [ParentId] = @ParentId 
				ORDER BY [Id]
			ELSE
				SELECT * FROM [dbo].[Category]
		END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0 
		BEGIN
			RETURN NULL
		END
	END CATCH
END