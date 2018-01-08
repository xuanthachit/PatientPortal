﻿CREATE PROC [dbo].[usp_SurveyAnswers_Transaction]
(
	@Action CHAR(1) = 'I',
	@Id TINYINT = 0,
	@QuestionId VARCHAR(128) = '',
	@Answer NVARCHAR(150) = '',
	@LowScore NVARCHAR(128) = '',
	@HightScore NVARCHAR(128) = ''
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @return INT = 0

	BEGIN TRY
		BEGIN TRAN;
			IF @Action = 'I' --INSERT
			BEGIN
				INSERT [dbo].[SurveyAnswers] VALUES(@QuestionId, @Answer, @LowScore, @HightScore)

				SET @return = 1
			END

			IF @Action = 'U' --UPDATE
			BEGIN
				UPDATE [dbo].[SurveyAnswers]
				SET [QuestionId] = @QuestionId, [Answer] = @Answer, [LowScore] = @LowScore, [HightScore] = @HightScore
				WHERE [Id] = @Id  

				SET @return = 1
			END

			IF @Action = 'D' --DELETE
			BEGIN
				BEGIN
					DELETE FROM [dbo].[SurveyAnswers]
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