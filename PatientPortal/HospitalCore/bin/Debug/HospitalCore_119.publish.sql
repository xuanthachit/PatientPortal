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
/*
The column [dbo].[SystemNotification].[Link] on table [dbo].[SystemNotification] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The column [dbo].[SystemNotification].[SendFrom] on table [dbo].[SystemNotification] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[SystemNotification])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[UserNotification].[SendTo] is being dropped, data loss could occur.

The column [dbo].[UserNotification].[UserId] on table [dbo].[UserNotification] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[UserNotification])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Rename refactoring operation with key a41b7d98-d5b0-436f-8351-059d6d2d10a3 is skipped, element [dbo].[QA].[Doctor] (SqlSimpleColumn) will not be renamed to DoctorId';


GO
PRINT N'Altering [dbo].[SystemNotification]...';


GO
ALTER TABLE [dbo].[SystemNotification]
    ADD [SendFrom] NVARCHAR (128) NOT NULL,
        [Link]     NVARCHAR (256) NOT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[UserNotification]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_UserNotification] (
    [Id]     VARCHAR (128)  NOT NULL,
    [Detail] NVARCHAR (300) NOT NULL,
    [Date]   VARCHAR (10)   NOT NULL,
    [Time]   VARCHAR (8)    NOT NULL,
    [UserId] NVARCHAR (128) NOT NULL,
    [IsRead] BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[UserNotification])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_UserNotification] ([Id], [Detail], [Date], [Time], [IsRead])
        SELECT   [Id],
                 [Detail],
                 [Date],
                 [Time],
                 [IsRead]
        FROM     [dbo].[UserNotification]
        ORDER BY [Id] ASC;
    END

DROP TABLE [dbo].[UserNotification];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_UserNotification]', N'UserNotification';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Altering [dbo].[usp_OfferAdvise_Transaction]...';


GO
ALTER PROC [dbo].[usp_OfferAdvise_Transaction]
(
	@Action CHAR(1) = 'I',
	@Date SMALLDATETIME,
	@Id INT = 1,
	@Detail NVARCHAR(MAX) = '',
	@PatientId NVARCHAR(128),
	@Tag NVARCHAR(150),
	@Status TINYINT = 0,
	@Message NVARCHAR(MAX) = '',
	@IdNotificate NVARCHAR(128)
)
AS BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @return INT = 0

	BEGIN TRY
		BEGIN TRAN;
			--DECLARE @Date SMALLDATETIME

			IF @Action = 'I' --INSERT
			BEGIN
				SET @Date = GETDATE()

				INSERT [dbo].[OfferAdvise] ([Date], Detail, PatientId, Tag,[Status], [Message])
				VALUES(@Date, @Detail, @PatientId, @Tag, @Status, @Message)

				SET @return = SCOPE_IDENTITY()
			END

			IF @Action = 'U' --UPDATE
			BEGIN
				UPDATE [dbo].[OfferAdvise]
				SET [Detail] = @Detail, Tag = @Tag,
				[Status] = @Status,
				[Message] = @Message
				WHERE [Id] = @Id  

				SET @return = @Id

				---insert Notification if OfferAdvise is not approved
				IF(@Status = 3)
				BEGIN
					INSERT [dbo].[UserNotification](Id, Detail, [Date],[Time], [UserId], [IsRead])
					VALUES(@IdNotificate, @Message, CONVERT(VARCHAR(10), GETDATE(), 103), convert(VARCHAR(8), GETDATE(), 108), @PatientId, 0)
				END
			END

			IF @Action = 'D' --DELETE
			BEGIN
				BEGIN
					DELETE FROM [dbo].[OfferAdvise]
					WHERE [Id] = @Id

					SET @return = @Id
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
PRINT N'Altering [dbo].[usp_UserNotification]...';


GO
ALTER PROCEDURE [dbo].[usp_UserNotification]
	@Id VARCHAR(128) = '',
	@UserId NVARCHAR(128) = '',
	@NumTop TINYINT = 0,
	@PageIndex TINYINT = 1, 
	@NumberInPage TINYINT = 10,
	@TotalItem int out
AS
BEGIN
	IF(@Id <> '')
	BEGIN
		SELECT * FROM [dbo].[UserNotification] WHERE Id = @Id
	END
	ELSE
	BEGIN
		IF(@NumTop <> 0)--SELECT TOP
		BEGIN
			SELECT TOP (@NumTop) *
			FROM [dbo].[UserNotification] WHERE [UserId] = @UserId
			ORDER BY [Date]
		END
		ELSE
		BEGIN--PAGING
			SELECT  *, ROW_NUMBER() OVER (ORDER BY [Date]) AS RowNum INTO #tmpNotify FROM (
				SELECT * FROM [dbo].[UserNotification] 
				WHERE [UserId] = @UserId
			)AS #tmp

			set @totalItem = (SELECT COUNT(Id) FROM #tmpNotify)
			SELECT  *
			FROM	#tmpNotify
			WHERE   RowNum BETWEEN (@PageIndex - 1) * @NumberInPage + 1
					AND @PageIndex * @NumberInPage ;
		END
	END
END
GO
PRINT N'Altering [dbo].[usp_UserNotification_Transaction]...';


GO
ALTER PROCEDURE [dbo].[usp_UserNotification_Transaction]
	@Action CHAR(1) = 'I',
	@Id varchar(128),
	@Detail nvarchar(300),
	@Date varchar(10),
	@Time varchar(8),
	@UserId nvarchar(128),
	@IsRead bit
AS
BEGIN
DECLARE @return bit = 0
BEGIN TRY
	BEGIN TRAN;
	IF(@Action = 'I')
	BEGIN
		INSERT INTO [dbo].[UserNotification](Id, Detail, [Date],[Time], [UserId], [IsRead]) 
		VALUES(@Id, @Detail, @Date, @Time, @UserId, @IsRead)
		SET @return = 1
	END
	ELSE
	BEGIN
		UPDATE [dbo].[UserNotification]
		SET		Detail = @Detail, [Date] = @Date, [Time] = @Time, [UserId] = @UserId, [IsRead] = @IsRead
		WHERE Id = @Id
		SET @return = 1
	END
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
PRINT N'Refreshing [dbo].[usp_UserNotification_UpdateStatus]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[usp_UserNotification_UpdateStatus]';


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'a41b7d98-d5b0-436f-8351-059d6d2d10a3')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('a41b7d98-d5b0-436f-8351-059d6d2d10a3')

GO

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
-- DELETE MODULE
GO
DELETE FROM [dbo].[Module]
GO
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (1, N'Main', N'', 1, 0, N'3', NULL)
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (2, N'Dashboard', N'Home', 1, 1, N'3', N'overview')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (3, N'Lịch làm việc', N'Schedule', 1, 1, N'3', N'schedule')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (4, N'Chuyên khoa', N'Department', 1, 1, N'3', N'department')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (5, N'Chức năng', N'Module', 1, 1, N'3', N'category')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (6, N'Communication', N'', 1, 0, N'3', NULL)
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (7, N'Hộp thư', N'', 1, 6, N'3', N'message-box')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (8, N'System', N'', 1, 0, N'3', NULL)
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (9, N'Tài khoản', N'Account', 1, 8, N'3', N'users')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (10, N'Cài đặt', N'Setting', 1, 8, N'3', N'configuration')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (11, N'Email Marketing', N'EmailMarketing', 1, 6, N'3', N'email-marketing')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (12, N'Quản lý đặt kịch hẹn', N'Appointment', 1, 1, N'3', N'prescription')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (13, N'Hỏi đáp y tế', N'Article', 1, 6, N'3', N'question')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (14, N'Góp ý xây dựng', N'OfferAdvise', 0, 6, N'3', N'offder-advise')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (15, N'Khảo sát', N'Survey', 1, 6, N'3', N'survey')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (16, N'Hỏi đáp y tế cộng đồng', N'QA', 1, 6, N'3', N'question')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (17, N'Chi tiết quyền', N'Permission', 1, 8, N'3', N'permission')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (18, N'Nhóm tài khoản', N'Role', 1, 8, N'3', N'role')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (19, N'Main', N'#', 1, 0, N'1', N'')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (20, N'Dashboard', N'Home', 1, 19, N'1', N'dashboard')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (21, N'Post Management', N'#', 1, 0, N'1', N'')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (22, N'Ngôn ngữ', N'Language', 1, 21, N'1', N'language')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (23, N'Chuyên mục', N'Category', 1, 21, N'1', N'category')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (24, N'Bản tin', N'Post', 1, 21, N'1', N'posts')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (25, N'Duyệt tin', N'ApprovePost', 1, 21, N'1', N'approved-posts')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (26, N'Xuất bản', N'PublishPost', 1, 21, N'1', N'publish-post')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (27, N'Services', N'#', 1, 0, N'1', N'')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (28, N'Liên kết Website', N'LinkBuilding', 1, 27, N'1', N'link-building')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (29, N'Dịch vụ nổi bật', N'Feature', 1, 27, N'1', N'feature')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (30, N'Trình chiếu', N'Slider', 1, 27, N'1', N'slider')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (31, N'Quảng cáo', N'Advertise', 1, 27, N'1', N'advertise')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (32, N'Library', N'#', 1, 0, N'1', N'')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (33, N'Thư viện', N'Gallery', 1, 32, N'1', N'gallery')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (34, N'Resources', N'#', 1, 0, N'1', N'')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (35, N'Hình ảnh', N'ImageLibrary', 1, 34, N'1', N'image-lib')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (36, N'Video', N'VideoLibrary', 0, 34, N'1', N'video-lib')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (37, N'Workflow', N'#', 1, 0, N'1', N'')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (38, N'Quy trình', N'Workflow', 1, 37, N'1', N'workflow')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (39, N'Bước xử lý quy trình', N'WorkflowState', 1, 37, N'1', N'workflow')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (40, N'System', N'#', 1, 0, N'1', N'')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (41, N'Cài đặt', N'Configuration', 1, 40, N'1', N'configuration')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (42, N'Main', N'#', 1, 0, N'2', N'')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (43, N'Dashboard', N'Home', 1, 42, N'2', N'overview')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (44, N'Bảo hiểm', N'#', 1, 42, N'2', N'insurance')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (45, N'Hồ sơ y tế', N'PHR', 1, 42, N'2', N'health-result')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (46, N'Thanh toán', N'#', 1, 42, N'2', N'payment')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (47, N'Communication', N'#', 1, 0, N'2', N'')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (48, N'Hỏi đáp y tế', N'Article', 1, 47, N'2', N'question')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (49, N'Góp ý xây dựng', N'OfferAdvise', 1, 47, N'2', N'offder-advise')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (50, N'Hộp thư', N'#', 1, 47, N'2', N'message-box')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (51, N'Truy cập bản tin', N'AccessPost', 1, 21, N'1', N'posts')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (52, N'Redis Manager', N'Redis', 1, 19, N'1', N'databases')
INSERT [dbo].[Module] ([Id], [Title], [Handler], [Sort], [ParentId], [Group], [ClassName]) VALUES (53, N'Tin nhắn nội bộ', N'Notification', 1, 6, N'3', N'notification')
GO

GO
PRINT N'Update complete.';


GO
