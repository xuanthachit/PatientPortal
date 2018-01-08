﻿CREATE TABLE [dbo].[Post] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [PublishDate]            DATETIME          NOT NULL,
    [Image]           VARCHAR (256) NOT NULL,
    [Author]          NVARCHAR (50) NOT NULL,
    [WorkflowStateId] TINYINT       NOT NULL,
    [CategoryId]      TINYINT       NOT NULL,
    [Status]          TINYINT       NOT NULL,
    [CreatedDate]     VARCHAR (20)  NOT NULL,
    [CreatedBy]       NVARCHAR(128)           NOT NULL,
    [ModifiedDate]    VARCHAR (20)  NOT NULL,
    [ModifiedBy]       NVARCHAR(128)           NOT NULL,
    [Priority] TINYINT NOT NULL, 
    [ExpiredDate] DATE NULL, 
    [Type] TINYINT NOT NULL, 
    CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Post_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [idxPostCategory]
    ON [dbo].[Post]([CategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [idxPostWorkflow]
    ON [dbo].[Post]([WorkflowStateId] ASC);

