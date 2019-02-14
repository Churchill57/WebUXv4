CREATE TABLE [dbo].[UserApp] (
    [ID]                    INT              IDENTITY (1, 1) NOT NULL,
    [UserID]                UNIQUEIDENTIFIER NOT NULL,
    [LaunchableComponentID] INT              NOT NULL,
    [Title]                 VARCHAR (100)    NULL,
    [DisplayOrder]          INT              NOT NULL,
    CONSTRAINT [PK_UserApp] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserApp_Component] FOREIGN KEY ([LaunchableComponentID]) REFERENCES [dbo].[Component] ([ID])
);

