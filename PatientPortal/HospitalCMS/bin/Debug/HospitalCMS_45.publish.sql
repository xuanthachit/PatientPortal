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
PRINT N'Altering [dbo].[usp_Post]...';


GO
ALTER PROCEDURE [dbo].[usp_Post]
	@languageCode CHAR(3) = 'vi',
	@PostId INT = 0
	--@PageIndex int, 
	--@NumberPerPage int, 
	--@TotalRecordCount int out
AS BEGIN
	BEGIN TRY
		SET NOCOUNT ON
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		DECLARE @return BIT = 1;

		--IF @languageId > 0
		--	BEGIN
		--		SET @TotalRecordCount = (SELECT COUNT(Id)
		--		FROM [dbo].Post
		--		WHERE [Status] = 1)

		--		;WITH tmpData AS
		--		(
		--			SELECT t.Title, c.Name, p.PublishDate,
		--			ROW_NUMBER() OVER (ORDER BY p.Id) AS RowNum
		--			FROM [dbo].Post p, [dbo].PostTrans t, [dbo].Category c
		--			WHERE p.Id = t.PostId
		--			AND p.CategoryId = c.Id
		--			AND p.[Status] = 1
		--		)
		--		SELECT *
		--		FROM tmpData
		--		WHERE RowNum BETWEEN (@PageIndex - 1) * @NumberPerPage + 1
		--		AND @PageIndex * @NumberPerPage ;

		--		SET NOCOUNT OFF
		--	END
		--ELSE
		--	SET @return = 0

		IF @languageCode != '' AND @PostId = 0
			SELECT p.Id, t.Title, c.Name as CategoryName, p.PublishDate, 
			CASE WHEN p.[Status] = 1 THEN N'Chưa duyệt' 
			WHEN p.[Status] = 2 THEN N'Đã duyệt' ELSE N'Đã đăng bài' END AS [Status]
			FROM [dbo].Post p, [dbo].PostTrans t, [dbo].Category c
			WHERE p.Id = t.PostId
			AND p.CategoryId = c.Id
			AND p.[Status] = 1
		ELSE IF @languageCode = '' AND @PostId > 0
			SELECT p.Id, 
			p.[Image],
			p.PublishDate,
			p.Author, 
			p.WorkflowStateId,
			p.CategoryId,
			p.[Status],
			p.CreatedBy,
			p.ModifiedBy,

			t.Title as TitleTrans, 
			c.Name as CategoryName, 
			t.[Description] as DescriptionTrans, 
			t.Detail,
			t.Tag,

			ps.Title as TitleSEO, 
			ps.[Description] as DescriptionSEO, 
			ps.Canonical,
			ps.MetaRobotIndex,
			ps.MetaRobotFollow,
			ps.MetaRobotAdvanced

			FROM [dbo].Post p, [dbo].PostTrans t, [dbo].Category c, [dbo].PostSEO ps
			WHERE p.Id = t.PostId
			AND p.CategoryId = c.Id
			AND p.Id = ps.PostId
			AND p.[Status] = 1
		ELSE
			SET @return = 0

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			BEGIN
				RETURN NULL
			END
	END CATCH
	SELECT @return
END
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
GO

GO
PRINT N'Update complete.';


GO
