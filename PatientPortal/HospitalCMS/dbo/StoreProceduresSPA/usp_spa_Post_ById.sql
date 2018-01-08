﻿CREATE PROCEDURE [dbo].[usp_spa_Post_ById]
	@Id int
AS
BEGIN
	BEGIN TRY
	SET NOCOUNT ON
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED

		SELECT p.Id, 
			p.[Image],
			p.PublishDate,
			p.Author, 
			p.WorkflowStateId,
			p.CategoryId,
			p.[Status],
			p.CreatedBy,
			p.ModifiedBy,
			p.[Priority],
			p.ExpiredDate,
			p.[Type],

			ps.Title as TitleSEO, 
			ps.[Description] as DescriptionSEO, 
			ps.Canonical,
			ps.MetaRobotIndex,
			ps.MetaRobotFollow,
			ps.MetaRobotAdvanced,
			ps.BreadcrumbsTitle,

			t.Title as TitleTrans, 
			c.Name as CategoryName, 
			t.[Description] as DescriptionTrans, 
			t.Detail,
			t.Tag

			FROM [dbo].Post p, [dbo].PostTrans t, [dbo].Category c, [dbo].PostSEO ps
			WHERE p.Id = t.PostId
			AND p.CategoryId = c.Id
			AND p.Id = ps.PostId
			--AND p.[Status] = 1
			AND p.Id = @Id

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			BEGIN
				RETURN NULL
			END
	END CATCH
END
