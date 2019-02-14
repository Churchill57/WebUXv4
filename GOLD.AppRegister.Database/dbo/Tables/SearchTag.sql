CREATE TABLE [dbo].[SearchTag] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [ComponentID] INT          NOT NULL,
    [Tag]         VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SearchTag] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_SearchTag_Component] FOREIGN KEY ([ComponentID]) REFERENCES [dbo].[Component] ([ID])
);

