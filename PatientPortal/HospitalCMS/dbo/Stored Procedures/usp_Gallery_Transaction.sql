﻿CREATE PROCEDURE [dbo].[usp_Gallery_Transaction]
	@Action CHAR(1) = 'I',
	@Id VARCHAR(128) = '',
	@Title NVARCHAR(128) = '',
	@Description NVARCHAR(300) = '',
	@Highlight NVARCHAR(100) = '',
	@Img VARCHAR(256) = '',
	@YoutubeURL VARCHAR(500) = '',
	@Date VARCHAR(20) = '',
	@DepartmentId SMALLINT = 1,
	@IsMultiple BIT = 0	
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	DECLARE @return bit = 1
	BEGIN TRY
		BEGIN TRAN;
		
		IF @Action = 'I' --INSERT
		BEGIN
			SET @Date = [dbo].[ufnGetDate]();
			INSERT [dbo].[Gallery]([Id], [Title], [Description], [Highlight], [Img], [YoutubeURL], [Date], [DepartmentId], [IsMultiple])
			VALUES(@Id, @Title, @Description, @Highlight, @Img, @YoutubeURL, @Date, @DepartmentId, @IsMultiple)
		END

		IF @Action = 'U' --UPDATE
		BEGIN
			UPDATE [dbo].[Gallery]
			SET [Title] = @Title, [Description] = @Description, [Highlight] = @Highlight, [Img] = @Img, [YoutubeURL] = @YoutubeURL,
			[Date] = @Date, [DepartmentId] = @DepartmentId, [IsMultiple] = @IsMultiple
			WHERE [Id] = @Id  
		END

		IF @Action = 'D' --DELETE
		BEGIN
			DELETE FROM [dbo].[GalleryStore]
			WHERE [ParentId] = @Id

			DELETE FROM [dbo].[Gallery]
			WHERE [Id] = @Id
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
	SELECT @return
	
END