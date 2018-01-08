﻿/*
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

--IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].Language WHERE Code = 'vi')
--	BEGIN
--		INSERT INTO [dbo].Language
--		VALUES('vi', 'VietNam')	
--	END
--GO