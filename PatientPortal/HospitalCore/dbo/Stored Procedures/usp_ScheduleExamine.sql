﻿CREATE PROCEDURE [dbo].[usp_ScheduleExamine]
	@UserId NVARCHAR(128) = '',
	@Start DATETIME
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRY
		SELECT *
			FROM [dbo].Schedule
			WHERE UserId = @UserId
			AND DATEDIFF(DAY,@Start , [start])>= 0
			AND DATEDIFF(DAY, [start], DATEADD(day,2,getdate()))>= 0
			AND [IsExamine] = 1
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			RETURN NULL
	END CATCH
END