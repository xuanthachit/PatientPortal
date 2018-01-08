﻿CREATE PROC [dbo].[usp_Setting_Transaction]
(
	@Action CHAR(1) = 'I',
	@Id TINYINT = 1,
	@Title NVARCHAR(70) = '',
	@Description NVARCHAR(150) = '',
	@Keyword NVARCHAR(150) = '',
	@Membership BIT = 1,
	@DefaultRole NVARCHAR(128) = '',
	@LoginURL VARCHAR(256) = '',
	@LockedIPNoteDefault NVARCHAR(150) = '',
	@IsSaveCanceledAppointment BIT = 1,
	@AppointmentIntervalTime INT = 15,
	@AppointmentStartTime INT = 8,
	@AppointmentEndTime INT = 5
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	DECLARE @return bit = 1
	BEGIN TRY
		BEGIN TRAN;
		
		IF EXISTS(SELECT TOP 1 1 FROM [dbo].[Setting])
		BEGIN
			UPDATE 
				[dbo].[Setting]
			SET 
				[Title] = @Title, 
				[Keyword] = @Keyword, 
				[Description] =  @Description, 
				[Membership] = @Membership,
				[DefaultRole] = @DefaultRole,
				[LoginURL] = @LoginURL,
				[LockedIPNoteDefault] = @LockedIPNoteDefault,
				[IsSaveCanceledAppointment] = @IsSaveCanceledAppointment,
				[AppointmentIntervalTime] = @AppointmentIntervalTime,
				[AppointmentStartTime] = @AppointmentStartTime,
				[AppointmentEndTime] = @AppointmentEndTime
			--WHERE [Id] = 1
		END
		ELSE
		BEGIN
			INSERT INTO 
				[dbo].[Setting] 
			VALUES(
				@Title, 
				@Description, 
				@Keyword, 
				@Membership, 
				@DefaultRole, 
				@LoginURL, 
				@LockedIPNoteDefault,
				@IsSaveCanceledAppointment,
				@AppointmentIntervalTime,
				@AppointmentStartTime,
				@AppointmentEndTime)
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