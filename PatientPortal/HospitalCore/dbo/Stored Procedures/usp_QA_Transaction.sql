﻿
CREATE PROC [dbo].[usp_QA_Transaction]
(
	@Action CHAR(1) = 'I',
	@Id INT = 1,
	@Title NVARCHAR(150) = '',
	@Question NVARCHAR(1000) = '',
	@Name NVARCHAR(32) = '',
	@Email VARCHAR(256),
	@Phone VARCHAR(20),
	@DoctorId NVARCHAR(128) = '',
	@Answer NVARCHAR(MAX) = '',
	@Department NVARCHAR(100) = ''
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @return INT = 0

	BEGIN TRY
		BEGIN TRAN;
			DECLARE @Date SMALLDATETIME

			IF @Action = 'I' --INSERT
			BEGIN
				SET @Date = GETDATE()

				INSERT [dbo].[QA] VALUES(@Date, @Title, @Question, @Name, @Email, @Phone, @DoctorId, @Answer, @Department)

				SET @return = 1
			END

			IF @Action = 'U' --UPDATE
			BEGIN
				UPDATE [dbo].[QA]
				SET [Title] = @Title, [Question] = @Question, [Name] = @Name, [Email] = @Email, [Phone] = @Phone, [DoctorId] = @DoctorId, [Answer] =  @Answer, [Department] = @Department
				WHERE [Id] = @Id  

				SET @return = 1
			END

			IF @Action = 'D' --DELETE
			BEGIN
				BEGIN
					DELETE FROM [dbo].[QA]
					WHERE [Id] = @Id

					SET @return = 1
				END
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