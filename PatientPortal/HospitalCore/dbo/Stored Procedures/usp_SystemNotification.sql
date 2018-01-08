﻿CREATE PROCEDURE [dbo].[usp_SystemNotification]
	@Id INT = 0,
	@UserId NVARCHAR(128) = '',
	@SendFrom NVARCHAR(128) = '',
	@NumTop TINYINT = 0,
	@PageIndex TINYINT = 1, 
	@NumberInPage TINYINT = 10,
	@TotalItem int out
AS
BEGIN
	IF(@Id <> 0)
	BEGIN
		SELECT sysNot.Id, sysNot.Detail, sysNot.[Date], sysNot.[Time], sysNot.SendFrom, sysNot.Link, sysNotUser.IsRead 
		FROM	[dbo].[SystemNotification] sysNot, [SystemNotificationUsers] sysNotUser
		WHERE	sysNot.Id = @Id
	END
	ELSE
	BEGIN
		IF(@NumTop <> 0)--SELECT TOP
		BEGIN
			SELECT TOP (@NumTop) 
					sysNot.Id, sysNot.Detail, sysNot.[Date], sysNot.[Time], sysNot.SendFrom, sysNot.Link, sysNotUser.IsRead
			FROM	[dbo].[SystemNotification] sysNot, [SystemNotificationUsers] sysNotUser
			WHERE	sysNot.Id = sysNotUser.Id 
					AND (sysNotUser.UserId = @UserId OR @UserId = '')
					AND (sysNot.SendFrom = @SendFrom OR @SendFrom = ''  OR @SendFrom = null)
			ORDER BY [Date]
		END
		ELSE
		BEGIN--PAGING
			SELECT  *, ROW_NUMBER() OVER (ORDER BY [Date]) AS RowNum INTO #tmpNotify FROM (
				SELECT  sysNot.Id, sysNot.Detail, sysNot.[Date], sysNot.[Time], sysNot.SendFrom, sysNot.Link, sysNotUser.IsRead
				FROM	[dbo].[SystemNotification] sysNot, [SystemNotificationUsers] sysNotUser
				WHERE	sysNot.Id = sysNotUser.Id 
						AND (sysNotUser.UserId = @UserId OR @UserId = '')
						AND (sysNot.SendFrom = @SendFrom OR @SendFrom = '' OR @SendFrom = null)
			)AS #tmp

			set @totalItem = (SELECT COUNT(Id) FROM #tmpNotify)
			SELECT  *
			FROM	#tmpNotify
			WHERE   RowNum BETWEEN (@PageIndex - 1) * @NumberInPage + 1
					AND @PageIndex * @NumberInPage ;
		END
	END
END
