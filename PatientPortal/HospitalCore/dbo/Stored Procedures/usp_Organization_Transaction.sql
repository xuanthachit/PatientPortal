﻿CREATE PROC [dbo].[usp_Organization_Transaction]
(
	@Action CHAR(1) = 'I',
	@Id SMALLINT = 1,
	@ParentId SMALLINT,
	@Name NVARCHAR(150) = '',
	@Phone VARCHAR(50) = '',
	@Fax VARCHAR(50) = '',
	@Email VARCHAR(256) ='',
	@Address NVARCHAR(150) = ''
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @return INT = 0

	BEGIN TRY
		BEGIN TRAN;
		
			IF @Action = 'I' --INSERT
			BEGIN
				INSERT [dbo].[Organization] VALUES(@ParentId, @Name, @Phone, @Fax, @Email, @Address)

				SET @return = SCOPE_IDENTITY()
			END

			IF @Action = 'U' --UPDATE
			BEGIN
				UPDATE [dbo].[Organization]
				SET [Name] = @Name, [ParentId] = @ParentId, [Phone] = @Phone, [Fax] = @Fax, [Address] = @Address
				WHERE [Id] = @Id  

				SET @return = @Id
			END

			IF @Action = 'D' --DELETE
			BEGIN
				IF NOT EXISTS( SELECT TOP 1 1 FROM [dbo].[Users] WHERE [OrganizationId] = @Id)
				BEGIN
					DELETE FROM [dbo].[Organization]
					WHERE [Id] = @Id

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