﻿CREATE PROC [dbo].[usp_ArticleComment_Transaction]
(
	@Action CHAR(1) = 'I',
	@Id INT = 1,
	@ArticleId SMALLINT = 1,
	@Date datetime,
	@Detail NVARCHAR(1000) ='',
	@CreatedUser NVARCHAR(128) = 0,
	@Status TINYINT = 1
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @return INT = 0
	DECLARE @CurrentDate SMALLDATETIME

	BEGIN TRY
		BEGIN TRAN;
			SET @CurrentDate = GETDATE()

			IF @Action = 'I' --INSERT
			BEGIN				
				SELECT @Id = ISNULL(MAX([Id]),0) + 1 FROM [dbo].[ArticleComment] WHERE [ArticleId] = @ArticleId
				INSERT [dbo].[ArticleComment]
				VALUES(@Id, @ArticleId, @CurrentDate, @Detail, @CreatedUser, @Status)

				--SET @return = SCOPE_IDENTITY()
				SET @return = @Id
			END

			IF @Action = 'U' --UPDATE
			BEGIN
				IF @ArticleId > 0 AND @Id > 0
				BEGIN

					UPDATE [dbo].[ArticleComment]
					SET [Detail] = @Detail, [Status] = @Status, [Date] = @CurrentDate
					WHERE [Id] = @Id  AND [ArticleId] = @ArticleId
					
					SET @return = @Id
					print @return
				END
				ELSE
					SET @return = 0
			END

			IF @Action = 'D' --DELETE
			BEGIN
				IF @ArticleId > 0 AND @Id > 0
				BEGIN
					DELETE FROM [dbo].[ArticleComment]
					WHERE [Id] = @Id  AND [ArticleId] = @ArticleId

					SET @return = @Id
				END
				ELSE
					SET @return = 0
			END

			COMMIT TRAN;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0 
		BEGIN
			ROLLBACK TRAN;
			SET  @return = 0
		END
	END CATCH
	SELECT @return;
END