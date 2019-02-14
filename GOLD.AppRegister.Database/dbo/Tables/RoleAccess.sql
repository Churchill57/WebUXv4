CREATE TABLE [dbo].[RoleAccess] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [ComponentID] INT          NOT NULL,
    [Role]        VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_RoleAccess] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RoleAccess_Component] FOREIGN KEY ([ComponentID]) REFERENCES [dbo].[Component] ([ID])
);

