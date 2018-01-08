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
PRINT N'Altering [dbo].[Post]...';


GO
ALTER TABLE [dbo].[Post] ALTER COLUMN [CreatedBy] NVARCHAR (128) NOT NULL;

ALTER TABLE [dbo].[Post] ALTER COLUMN [ModifiedBy] NVARCHAR (128) NOT NULL;


GO
PRINT N'Refreshing [dbo].[usp_ApprovePost_Transaction]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_ApprovePost_Transaction]';


GO
PRINT N'Refreshing [dbo].[usp_Category_CheckIsUsed]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_Category_CheckIsUsed]';


GO
PRINT N'Refreshing [dbo].[usp_Category_Transaction]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_Category_Transaction]';


GO
PRINT N'Refreshing [dbo].[usp_Language_Transaction]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_Language_Transaction]';


GO
PRINT N'Refreshing [dbo].[usp_Post]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_Post]';


GO
PRINT N'Refreshing [dbo].[usp_Post_Transaction]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_Post_Transaction]';


GO
PRINT N'Refreshing [dbo].[usp_Post_WorkflowState_Transaction]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_Post_WorkflowState_Transaction]';


GO
PRINT N'Refreshing [dbo].[usp_PublishPost_Transaction]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_PublishPost_Transaction]';


GO
PRINT N'Refreshing [dbo].[usp_spa_Post]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_spa_Post]';


GO
PRINT N'Refreshing [dbo].[usp_spa_Post_ById]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_spa_Post_ById]';


GO
PRINT N'Refreshing [dbo].[usp_spa_Post_Paging]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_spa_Post_Paging]';


GO
PRINT N'Refreshing [dbo].[usp_WorkflowNavigation_Transaction]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_WorkflowNavigation_Transaction]';


GO
PRINT N'Refreshing [dbo].[usp_WorkflowState_Transaction]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_WorkflowState_Transaction]';


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

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[Workflow] WHERE Name = N'Workflow post')
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
			INSERT INTO [dbo].WorkflowState
			VALUES(N'Đã duyệt', @WorkflowId, 1, 0)

			SET @WorkflowStateId = SCOPE_IDENTITY()
		END
	--------------
	IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].WorkflowState WHERE Name = N'Đã đăng bài')
		BEGIN
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

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Category WHERE Name = N'Trang chủ')
	BEGIN
		INSERT INTO [dbo].Category
		VALUES(N'Trang chủ', '', '/', 1, 0)	
	END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Category WHERE Name = N'Dịch vụ')
	BEGIN
		INSERT INTO [dbo].Category
		VALUES(N'Dịch vụ', '', '#services', 2, 0)	
	END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Category WHERE Name = N'Chuyên khoa')
	BEGIN
		INSERT INTO [dbo].Category
		VALUES(N'Chuyên khoa', '', '#portfolio', 3, 0)	
	END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Category WHERE Name = N'Giới thiệu')
	BEGIN
		INSERT INTO [dbo].Category
		VALUES(N'Giới thiệu', '', '#about', 4, 0)	
	END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Category WHERE Name = N'Tin tức')
	BEGIN
		INSERT INTO [dbo].Category
		VALUES(N'Tin tức', '', '#blog', 5, 0)	
	END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Category WHERE Name = N'Liên hệ')
	BEGIN
		INSERT INTO [dbo].Category
		VALUES(N'Liên hệ', '', '#get-in-touch', 6, 0)	
	END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Category WHERE Name = N'Tuyển dụng')
	BEGIN
		INSERT INTO [dbo].Category
		VALUES(N'Tuyển dụng', '', '#career', 7, 0)	
	END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Language WHERE Code = 'vi')
	BEGIN
		INSERT INTO [dbo].Language
		VALUES('vi', 'VietNam')	
	END
GO

GO
PRINT N'Update complete.';


GO
