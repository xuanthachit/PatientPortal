﻿CREATE PROCEDURE [dbo].[usp_UserRole_Transaction]
	@Action CHAR(1) = 'I',
	@UserId NVARCHAR(128) = '',
	@RoleId int
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @return bit = 1

	BEGIN TRY
		BEGIN TRAN
			IF @Action = 'I'
				IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[UserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId)
					BEGIN
						INSERT INTO [dbo].[UserRole]
						VALUES(@UserId, @RoleId)
					END
				ELSE
					SET @return = 0;
			IF @Action = 'U'
				IF EXISTS(SELECT TOP 1 1 FROM [dbo].UserRole WHERE [UserId] = @UserId AND [RoleId] = @RoleId)
				BEGIN
					IF @UserId <> 0 AND @RoleId <> 0
						UPDATE [dbo].[UserRole]
						SET [UserId] = @UserId,
						[RoleId] = @RoleId
						WHERE [UserId] = @UserId
						AND [RoleId] = @RoleId
					ELSE
						SET @return = 0;
				END
				ELSE
					SET @return = 0;
				
			IF @Action = 'D'
				
				IF EXISTS(SELECT TOP 1 1 FROM [dbo].[UserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId)
				BEGIN
					IF @UserId <> 0 AND @RoleId <> 0
						DELETE [dbo].[UserRole]
						WHERE [UserId] = @UserId
						AND [RoleId] = @RoleId
					ELSE
						SET @return = 0;
				END
				ELSE
					SET @return = 0;
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			BEGIN	
				ROLLBACK TRAN
				SET @return = 0;
			END
	END CATCH
	SELECT @return
END
