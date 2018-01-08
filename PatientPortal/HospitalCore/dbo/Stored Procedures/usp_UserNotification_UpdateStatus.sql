﻿CREATE PROCEDURE [dbo].[usp_UserNotification_UpdateStatus]
	@Action CHAR(1) = 'U',
	@Id varchar(128),
	@Detail nvarchar(300),
	@Date varchar(10),
	@Time varchar(8),
	@UserId nvarchar(128),
	@IsRead bit
AS
BEGIN
DECLARE @return bit = 0
BEGIN TRY
		UPDATE [dbo].[UserNotification]
		SET		[IsRead] = @IsRead
		WHERE Id = @Id
		SET @return = 1
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
