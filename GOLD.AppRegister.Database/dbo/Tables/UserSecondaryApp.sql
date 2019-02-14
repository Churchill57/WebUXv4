CREATE TABLE [dbo].[UserSecondaryApp] (
    [ID]                   INT              IDENTITY (1, 1) NOT NULL,
    [UserID]               UNIQUEIDENTIFIER NOT NULL,
    [PrimaryComponentID]   INT              NOT NULL,
    [SecondaryComponentID] INT              NOT NULL,
    [Title]                VARCHAR (100)    NULL,
    [DisplayOrder]         INT              NOT NULL,
    CONSTRAINT [PK_UserSecondaryApp] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserSecondaryApp_PrimaryComponent] FOREIGN KEY ([PrimaryComponentID]) REFERENCES [dbo].[Component] ([ID]),
    CONSTRAINT [FK_UserSecondaryApp_SecondaryComponent] FOREIGN KEY ([SecondaryComponentID]) REFERENCES [dbo].[Component] ([ID])
);

