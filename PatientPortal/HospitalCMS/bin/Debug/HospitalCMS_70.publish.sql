﻿/*
Deployment script for HospitalCMS

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "HospitalCMS"
:setvar DefaultFilePrefix "HospitalCMS"
:setvar DefaultDataPath "D:\MSSQL\DATA\"
:setvar DefaultLogPath "D:\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
GO

GO
/*
The column [dbo].[WorkflowState].[IsFirst] on table [dbo].[WorkflowState] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[WorkflowState])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Altering [dbo].[WorkflowState]...';


GO
ALTER TABLE [dbo].[WorkflowState]
    ADD [IsFirst] BIT NOT NULL;


GO
PRINT N'Altering [dbo].[usp_WorkflowState]...';


GO
ALTER PROCEDURE [dbo].[usp_WorkflowState]
	@Id SMALLINT,
	@WorkflowId TINYINT
AS BEGIN
	BEGIN TRY
		IF(@Id > 0) ---get 'WorkflowState' by Id
		BEGIN
			SELECT	wfs.*, wf.Name as WorkflowName
			FROM	[dbo].[WorkflowState] wfs
					INNER JOIN [dbo].[Workflow] wf on wf.Id = wfs.WorkflowId
			WHERE	wfs.Id = @Id
		END
		BEGIN       ---get 'WorkflowState' by workflowId
			IF(@WorkflowId > 0)
			BEGIN
				SELECT	wfs.*, wf.Name as WorkflowName
				FROM	[dbo].[WorkflowState] wfs
						INNER JOIN [dbo].[Workflow] wf on wf.Id = wfs.WorkflowId
				WHERE   wfs.WorkflowId = @WorkflowId
			END
			ELSE
			BEGIN
				SELECT	wfs.*, wf.Name as WorkflowName
				FROM	[dbo].[WorkflowState] wfs
						INNER JOIN [dbo].[Workflow] wf on wf.Id = wfs.WorkflowId
			END
		END

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			BEGIN
				RETURN NULL
			END
	END CATCH
END
GO
PRINT N'Altering [dbo].[usp_WorkflowState_Transaction]...';


GO
ALTER PROCEDURE [dbo].[usp_WorkflowState_Transaction]
	@Action VARCHAR(1) = 'I',
	@Id SMALLINT,
	@Name NVARCHAR(50) = '',
	@WorkflowId TINYINT = 0,
	@IsActive BIT = 1,
	@IsFirst BIT = 0
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	DECLARE @return BIT = 1
	BEGIN TRY
		BEGIN TRAN
			IF @Action = 'I' -- INSERT
			BEGIN
				INSERT INTO [dbo].[WorkflowState]
				VALUES(@Name, @WorkflowId, @IsActive, @IsFirst)
			END

			IF @Action = 'U' -- UPDATE
			BEGIN
				IF NOT EXISTS( SELECT TOP 1 1 FROM [dbo].[WorkflowNavigation] WHERE [WorkflowStateId] = @Id)  AND NOT EXISTS( SELECT TOP 1 1 FROM [dbo].[Post] WHERE [WorkflowStateId] = @Id)
				BEGIN
					UPDATE [dbo].[WorkflowState]
					SET [Name] = @Name, [WorkflowId] = @WorkflowId, [IsActive] = @IsActive, [IsFirst] = @IsFirst
					WHERE [Id] = @Id
				END
				ELSE
					SET @return = 0
			END

			IF @Action = 'D' -- DELETE
			BEGIN
				IF NOT EXISTS( SELECT TOP 1 1 FROM [dbo].[Post] WHERE [WorkflowStateId] = @Id)
				BEGIN
					DELETE FROM [dbo].[WorkflowState]
					WHERE [Id] = @Id
				END
				ELSE
					SET @return = 0
			END

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			BEGIN
				ROLLBACK TRAN
				SET @return = 0
			END
	END CATCH
	SELECT @return
END
GO
PRINT N'Altering [dbo].[usp_Workflow]...';


GO
ALTER PROCEDURE [dbo].[usp_Workflow]
	@Id TINYINT
AS BEGIN
	BEGIN TRY
	IF(@Id > 0)
	BEGIN
		SELECT * FROM [dbo].[Workflow] WHERE [Id] = @ID
	END
	ELSE
	BEGIN
		SELECT * FROM [dbo].[Workflow]
	END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			BEGIN
				RETURN NULL
			END
	END CATCH
END
GO
PRINT N'Altering [dbo].[usp_WorkflowNavigation]...';


GO
ALTER PROCEDURE [dbo].[usp_WorkflowNavigation]
	@WorkflowId smallint = 0
AS BEGIN
	BEGIN TRY
	IF(@WorkflowId > 0)
	BEGIN
		SELECT DISTINCT wfs.*
		FROM dbo.WorkflowState wf
		INNER JOIN dbo.WorkflowNavigation wfs ON wf.WorkflowId = @WorkflowId
	END
	ELSE
	BEGIN
		SELECT * FROM [dbo].[WorkflowNavigation]
	END
		
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0 
			BEGIN
				RETURN NULL
			END
	END CATCH
END
GO
PRINT N'Creating [dbo].[usp_Workflow_CheckExistName]...';


GO
CREATE PROCEDURE [dbo].[usp_Workflow_CheckExistName]
	@Name  NVARCHAR(50),
	@Id TINYINT
AS
BEGIN
	DECLARE @return BIT = 0;

	IF(@Id > 0)
		IF(EXISTS(SELECT TOP 1 1 FROM [dbo].[Workflow] WHERE [Name] = @Name and Id != @Id))
		begin
			SET @return = 1;
		end
	ELSE
	BEGIN
	IF(EXISTS(SELECT TOP 1 1 FROM [dbo].[Workflow] WHERE [Name] = @Name))
		begin
			SET @return = 1;
		end
	END
	SELECT @return
END
GO
PRINT N'Creating [dbo].[usp_Workflow_CheckIsUsed]...';


GO
CREATE PROCEDURE [dbo].[usp_Workflow_CheckIsUsed]
	@Id TINYINT
AS
BEGIN
	DECLARE @return BIT = 0;
	IF(EXISTS(SELECT TOP 1 1 FROM [dbo].[WorkflowState] WHERE WorkflowId = @Id))
		SET @return = 1

	SELECT @return
END
GO
PRINT N'Creating [dbo].[usp_WorkflowState_CheckExistName]...';


GO
CREATE PROCEDURE [dbo].[usp_WorkflowState_CheckExistName]
	@Id SMALLINT,
	@Name NVARCHAR(50) = '',
	@WorkflowId TINYINT
AS
BEGIN
	DECLARE @return BIT = 0;

	IF(@Id > 0)
		IF(EXISTS(SELECT TOP 1 1 FROM [dbo].[WorkflowState] WHERE [Name] = @Name and [WorkflowId] = @WorkflowId and Id != @Id))
		begin
			SET @return = 1;
		end
	ELSE
	BEGIN
	IF(EXISTS(SELECT TOP 1 1 FROM [dbo].[WorkflowState] WHERE [Name] = @Name and [WorkflowId] = @WorkflowId))
		begin
			SET @return = 1;
		end
	END
	SELECT @return
END
GO
PRINT N'Creating [dbo].[usp_WorkflowState_CheckIsUsed]...';


GO
CREATE PROCEDURE [dbo].[usp_WorkflowState_CheckIsUsed]
	@Id SMALLINT
AS
BEGIN
	DECLARE @return BIT = 0;
	IF(EXISTS(SELECT TOP 1 1 FROM [dbo].[WorkflowNavigation] WHERE [WorkflowStateId] = @Id or [NextWorkflowStateId] = @Id))
		SET @return = 1

	SELECT @return
END
GO
PRINT N'Refreshing [dbo].[usp_DefaultData_Insert]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_DefaultData_Insert]';


GO
PRINT N'Refreshing [dbo].[usp_Post]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_Post]';


GO
PRINT N'Refreshing [dbo].[usp_Workflow_Transaction]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_Workflow_Transaction]';


GO
PRINT N'Refreshing [dbo].[usp_DefaultData_Exec]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_DefaultData_Exec]';


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Workflow WHERE Name = N'Workflow post')
BEGIN
	DECLARE @WorkflowId TINYINT = 0
	DECLARE @WorkflowStateId TINYINT = 0
	DECLARE @NextWorkflowStateId TINYINT = 0
	---------------
	INSERT INTO [dbo].Workflow
	VALUES(N'Workflow post')
	---------
	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].WorkflowState WHERE Name = N'Đang soạn thảo')
		BEGIN
			SET @WorkflowId = SCOPE_IDENTITY()
			INSERT INTO [dbo].WorkflowState
			VALUES(N'Đang soạn thảo', @WorkflowId, 1, 1)

			SET @WorkflowStateId = SCOPE_IDENTITY()
		END
	---------------
	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].WorkflowState WHERE Name = N'Đã duyệt')
		BEGIN
			SET @WorkflowId = SCOPE_IDENTITY()
			INSERT INTO [dbo].WorkflowState
			VALUES(N'Đã duyệt', @WorkflowId, 1, 0)

			SET @WorkflowStateId = SCOPE_IDENTITY()
		END
	--------------
	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].WorkflowState WHERE Name = N'Đã đăng bài')
		BEGIN
			SET @WorkflowId = SCOPE_IDENTITY()
			INSERT INTO [dbo].WorkflowState
			VALUES(N'Đã đăng bài', @WorkflowId, 1, 0)

			SET @NextWorkflowStateId = SCOPE_IDENTITY()
		END
	----------------------
	IF @WorkflowStateId > 0 AND @NextWorkflowStateId > 0
		BEGIN
			INSERT INTO [dbo].WorkflowNavigation
			VALUES(@WorkflowStateId, @NextWorkflowStateId)
		END
END
GO

GO
PRINT N'Update complete.';


GO
