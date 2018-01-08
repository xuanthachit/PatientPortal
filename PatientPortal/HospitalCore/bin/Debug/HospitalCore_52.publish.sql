﻿/*
Deployment script for HospitalCore

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "HospitalCore"
:setvar DefaultFilePrefix "HospitalCore"
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
PRINT N'Creating [dbo].[Permission]...';


GO
CREATE TABLE [dbo].[Permission] (
    [RoleId]    SMALLINT NOT NULL,
    [ModuleId]  SMALLINT NOT NULL,
    [IsRead]    BIT      NOT NULL,
    [IsWrite]   BIT      NOT NULL,
    [IsCreate]  BIT      NOT NULL,
    [IsModify]  BIT      NOT NULL,
    [IsDestroy] BIT      NOT NULL,
    [IsPrint]   BIT      NOT NULL,
    CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED ([RoleId] ASC, [ModuleId] ASC)
);


GO
PRINT N'Creating [dbo].[Role]...';


GO
CREATE TABLE [dbo].[Role] (
    [Id]   SMALLINT       IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[UserGroup]...';


GO
CREATE TABLE [dbo].[UserGroup] (
    [UserId]  NVARCHAR (128) NOT NULL,
    [GroupId] TINYINT        NOT NULL,
    CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED ([UserId] ASC, [GroupId] ASC)
);


GO
PRINT N'Creating [dbo].[UserRole]...';


GO
CREATE TABLE [dbo].[UserRole] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] SMALLINT       NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);


GO
PRINT N'Creating [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    [OrganizationId]       SMALLINT       NOT NULL,
    [Tags]                 NVARCHAR (250) NOT NULL,
    [HomePhone]            VARCHAR (20)   NOT NULL,
    [IsAdmin]              BIT            NOT NULL,
    [IsDoctor]             BIT            NOT NULL,
    [Gender]               TINYINT        NOT NULL,
    [Code]                 CHAR (20)      NOT NULL,
    [Name]                 NVARCHAR (100) NOT NULL,
    [PatientId]            NVARCHAR (128) NOT NULL,
    [DateOfBirth]          DATE           NOT NULL,
    [Image]                VARCHAR (256)  NOT NULL,
    [Status]               BIT            NOT NULL,
    CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    CONSTRAINT [UQ__USERS__A9D1053429B4C0C0] UNIQUE NONCLUSTERED ([Email] ASC)
);


GO
PRINT N'Creating [dbo].[Users].[idxUserAccess]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [idxUserAccess]
    ON [dbo].[Users]([Code] ASC, [Email] ASC);


GO
PRINT N'Creating [dbo].[FK_Permission_Module]...';


GO
ALTER TABLE [dbo].[Permission] WITH NOCHECK
    ADD CONSTRAINT [FK_Permission_Module] FOREIGN KEY ([ModuleId]) REFERENCES [dbo].[Module] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserGroup_Group]...';


GO
ALTER TABLE [dbo].[UserGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_UserGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserGroup_Users]...';


GO
ALTER TABLE [dbo].[UserGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_UserGroup_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserRole_Role]...';


GO
ALTER TABLE [dbo].[UserRole] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserRole_Users]...';


GO
ALTER TABLE [dbo].[UserRole] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRole_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Users_Organization]...';


GO
ALTER TABLE [dbo].[Users] WITH NOCHECK
    ADD CONSTRAINT [FK_Users_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organization] ([Id]);


GO
PRINT N'Creating [dbo].[ufnGenerationNumber]...';


GO
CREATE FUNCTION [dbo].[ufnGenerationNumber](
	@Time INT,
	@IntervalTime INT,
	@StartTime INT
)
RETURNS INT
AS
BEGIN
	DECLARE @STT INT = 1
	WHILE (@StartTime+@IntervalTime <= @Time)
	BEGIN
		SET @STT = @STT + 1
		SET @StartTime = @StartTime + @IntervalTime
	END
	RETURN @STT
END
GO
PRINT N'Altering [dbo].[usp_AppointmentLog]...';


GO
ALTER PROC [dbo].[usp_AppointmentLog](
	@Id UniqueIdentifier
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--cast(cast(0 as binary) as uniqueidentifier) convert zero to uniqueidentifier
	BEGIN TRY
		IF (@Id IS NOT NULL)
		BEGIN
			SELECT * FROM [dbo].[AppointmentLog] l
			WHERE l.Id = @Id
		END
		ELSE
		BEGIN
			SELECT * FROM [dbo].[AppointmentLog] l
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
PRINT N'Altering [dbo].[usp_Setting_Transaction]...';


GO
ALTER PROC [dbo].[usp_Setting_Transaction]
(
	@Action CHAR(1) = 'I',
	@Id TINYINT = 1,
	@Title NVARCHAR(70) = '',
	@Description NVARCHAR(150) = '',
	@Keyword NVARCHAR(150) = '',
	@Membership BIT = 1,
	@DefaultRole SMALLINT = 1,
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
GO
PRINT N'Creating [dbo].[usp_AppointmentLog_Confirm]...';


GO
CREATE PROC [dbo].[usp_AppointmentLog_Confirm]
(
	@Action CHAR(1) = 'I',
	@Id UNIQUEIDENTIFIER,
	@Date DATE = '',
	@Time int = 480,
	@PhysicianId NVARCHAR(128) = '',
	@PatientId NVARCHAR(128) = '',
	@Symptom NVARCHAR(300) = '',
	@PatientName NVARCHAR(50) = '',
	@PatientAddress NVARCHAR(150) = '',
	@PatientEmail VARCHAR(256) = '',
	@PatientPhone VARCHAR(20) = '',
	@PatientGender TINYINT = 1,
	@PatientDoB DATE = NULL,
	@Status TINYINT = 1
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @return BIT = 1
	DECLARE @CreatedDate VARCHAR(20)
	DECLARE @ModifiedDate VARCHAR(20)

	BEGIN TRY
		BEGIN TRAN;

			IF @Status = 2
			BEGIN
				SET @ModifiedDate = [dbo].[ufnGetDate]()
				SET @CreatedDate = @ModifiedDate

				SELECT * INTO #AppointmentLogById
				FROM [dbo].[AppointmentLog] l
				WHERE l.Id = @Id

				--Post data to Appointment
				DECLARE @ColectionId INT
				INSERT [dbo].[Appointment]([PhysicianId], [PatientId], [Symptom], [CreatedDate], [ModifiedDate], [ModifedUser])
				SELECT item.PhysicianId, item.PatientId, item.Symptom, @CreatedDate, @ModifiedDate, N'Patient Test' FROM #AppointmentLogById item
				SET @ColectionId = (SELECT SCOPE_IDENTITY())

				--Generation Number
				SELECT TOP 1 s.AppointmentIntervalTime, s.AppointmentStartTime INTO #GenerationTime
				FROM [dbo].[Setting] s
				--
				DECLARE @AppointmentNo INT
				DECLARE @IntervalTime INT
				DECLARE @StartTime INT
				SET @IntervalTime = (SELECT t.AppointmentIntervalTime FROM #GenerationTime t)
				SET @StartTime = (SELECT t.AppointmentStartTime FROM #GenerationTime t)
				SET @AppointmentNo = [dbo].[ufnGenerationNumber](@Time, @IntervalTime, @StartTime)

				--Post data to AppointmentCollection
				INSERT [dbo].[AppointmentCollection]([Id], [Date], [Time], [AppointmentNo], [PatientName], [PatientAddress], [PatientEmail], [PatientPhone], [PatientGender], [PatientDoB], [Status])
				SELECT @ColectionId, item.[Date], item.[Time], @AppointmentNo, item.PatientName, item.PatientAddress, item.PatientEmail, item.PatientPhone, item.PatientGender, item.PatientDoB, 2 FROM #AppointmentLogById item

				UPDATE [dbo].[AppointmentLog] SET [Status] = 2 WHERE [Id] = @Id

				-- DROP Temp Table
				DROP TABLE #AppointmentLogById
				DROP TABLE #GenerationTime

				SET @return = 1
			END

			IF @Status = 3
			BEGIN
				DECLARE @isCheck BIT
				SET @isCheck = (SELECT TOP 1 s.IsSaveCanceledAppointment FROM [dbo].[Setting] s)
				IF @isCheck = 1
				BEGIN
					UPDATE [dbo].[AppointmentLog]
					SET [Status] = 3
					WHERE [Id] = @Id

					SET @return = 1
				END
				ELSE
				BEGIN
					DELETE FROM [dbo].[AppointmentLog] WHERE [Id] = @Id

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
GO
PRINT N'Creating [dbo].[usp_AppointmentLog_Search]...';


GO
CREATE PROC [dbo].[usp_AppointmentLog_Search](
	@Status INT = 0,
	@FromDate DATE = '',
	@ToDate DATE = ''
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	BEGIN TRY
		SELECT * FROM [dbo].[AppointmentLog] l
		WHERE 
			(l.[Status] = @Status OR @Status IS NULL)
			AND
			(
				(l.[Date] BETWEEN @FromDate AND @ToDate)
				OR
				(@FromDate IS NULL AND @ToDate IS NULL)
				OR
				(@FromDate IS NULL AND l.[Date] <= @ToDate)
				OR
				(@ToDate IS NULL AND l.[Date] >= @FromDate)
			)
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0 
		BEGIN
			RETURN NULL
		END
	END CATCH
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
DELETE FROM [dbo].[Group]
GO
INSERT INTO [dbo].[Group] VALUES(N'Content Management System (CMS)', 1)
GO
INSERT INTO [dbo].[Group] VALUES(N'Patient Services', 1)
GO
INSERT INTO [dbo].[Group] VALUES(N'Internal Services', 1)
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Permission] WITH CHECK CHECK CONSTRAINT [FK_Permission_Module];

ALTER TABLE [dbo].[UserGroup] WITH CHECK CHECK CONSTRAINT [FK_UserGroup_Group];

ALTER TABLE [dbo].[UserGroup] WITH CHECK CHECK CONSTRAINT [FK_UserGroup_Users];

ALTER TABLE [dbo].[UserRole] WITH CHECK CHECK CONSTRAINT [FK_UserRole_Role];

ALTER TABLE [dbo].[UserRole] WITH CHECK CHECK CONSTRAINT [FK_UserRole_Users];

ALTER TABLE [dbo].[Users] WITH CHECK CHECK CONSTRAINT [FK_Users_Organization];


GO
PRINT N'Update complete.';


GO
