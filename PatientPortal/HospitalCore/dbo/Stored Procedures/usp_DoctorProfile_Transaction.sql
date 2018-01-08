﻿CREATE PROCEDURE [dbo].[usp_DoctorProfile_Transaction]
(
	@Action CHAR(1) = 'I',
	@UserId NVARCHAR(128),
	@Speciality NVARCHAR(150) = '',
	@Degrees NVARCHAR(150) = '',
	@Training NVARCHAR(500) = '',
	@Office NVARCHAR(150) = '',
	@Workdays NVARCHAR(50) = '',
	@DepartmentId SMALLINT = 0
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @return INT = 0

	BEGIN TRY
		BEGIN TRAN;

		IF @Action = 'I' --INSERT
		BEGIN
			IF NOT EXISTS( SELECT TOP 1 1 FROM [dbo].[DoctorProfile] WHERE [UserId] = @UserId) --insert
			BEGIN
				INSERT [dbo].[DoctorProfile] VALUES(@UserId, @Speciality, @Degrees, @Training, @Office, @Workdays, @DepartmentId)
			END
			ELSE
			BEGIN
				UPDATE [dbo].[DoctorProfile]
				SET [Speciality] = @Speciality, [Degrees] = @Degrees, [Training] = @Training, [Office] = @Office, [Workdays] = @Workdays,[DepartmentId] =  @DepartmentId
				WHERE [UserId] = @UserId
			END

			SET @return = 1
		END

		IF @Action = 'U' --UPDATE
		BEGIN
			UPDATE [dbo].[DoctorProfile]
			SET [Speciality] = @Speciality, [Degrees] = @Degrees, [Training] = @Training, [Office] = @Office, [Workdays] = @Workdays,[DepartmentId] =  @DepartmentId
			WHERE [UserId] = @UserId

			SET @return = 1
		END

		IF @Action = 'D' --DELETE
		BEGIN
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
GO


