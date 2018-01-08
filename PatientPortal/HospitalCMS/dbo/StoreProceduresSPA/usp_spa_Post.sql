﻿CREATE PROCEDURE [dbo].[usp_spa_Post]
	@languageCode CHAR(3) = 'vi',
	@categoryId TINYINT = 0,
	@numTop TINYINT = 1,
	@priority TINYINT = 1,
	@type TINYINT = 1
AS BEGIN
	BEGIN TRY
	SET NOCOUNT ON
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		IF @languageCode != '' AND @categoryId > 0
			IF @priority = 3
			BEGIN	--- get posts order by priority
				
				SELECT TOP (@numTop)
					p.Id, 
					p.CategoryId,
					p.PublishDate,
					P.[Image],
					P.Author,
					t.Title as TitleTran,
					t.Detail,
					t.[Description] as DescriptionTrans,
					c.Name AS CategoryName, 
					ws.Id AS WorkflowStateId,
					ws.Name AS WorkflowStateName,
					p.[Priority]
				FROM 
					[dbo].Post p, 
					[dbo].PostTrans t, 
					[dbo].Category c, 
					WorkflowState ws
				WHERE 
					p.Id = t.PostId
					AND p.CategoryId = c.Id
					AND p.WorkflowStateId = ws.Id
					AND WorkflowStateId = 3
					AND p.CategoryId = @categoryId
				ORDER BY p.PublishDate DESC
			END
			ELSE	
			BEGIN
				SELECT TOP (@numTop)
					p.Id, 
					p.CategoryId,
					p.PublishDate,
					P.[Image],
					P.Author,
					t.Title as TitleTran,
					t.Detail,
					t.[Description] as DescriptionTrans,
					c.Name AS CategoryName, 
					ws.Id AS WorkflowStateId,
					ws.Name AS WorkflowStateName,
					p.[Priority]
				FROM 
					[dbo].Post p, 
					[dbo].PostTrans t, 
					[dbo].Category c, 
					WorkflowState ws
				WHERE 
					p.Id = t.PostId
					AND p.CategoryId = c.Id
					AND p.WorkflowStateId = ws.Id
					AND WorkflowStateId = 3
					AND p.CategoryId = @categoryId
				ORDER BY p.PublishDate DESC
			END
		ELSE
			SELECT TOP (@numTop)
					p.Id, 
					p.CategoryId,
					p.PublishDate,
					P.[Image],
					P.Author,
					t.Title as TitleTran,
					t.Detail,
					t.[Description] as DescriptionTrans,
					c.Name AS CategoryName, 
					ws.Id AS WorkflowStateId,
					ws.Name AS WorkflowStateName,
					p.[Priority]
				FROM 
					[dbo].Post p, 
					[dbo].PostTrans t, 
					[dbo].Category c, 
					WorkflowState ws
				WHERE 
					p.Id = t.PostId
					AND p.CategoryId = c.Id
					AND p.WorkflowStateId = ws.Id
					AND WorkflowStateId = 3
					AND p.[Type] != 4
				ORDER BY p.PublishDate DESC

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			BEGIN
				RETURN NULL
			END
	END CATCH
END