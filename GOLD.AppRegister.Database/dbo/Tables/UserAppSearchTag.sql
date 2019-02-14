CREATE TABLE [dbo].[UserAppSearchTag] (
    [ID]        INT          IDENTITY (1, 1) NOT NULL,
    [UserAppId] INT          NOT NULL,
    [Tag]       VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserAppSearchTag] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserAppSearchTag_UserApp] FOREIGN KEY ([UserAppId]) REFERENCES [dbo].[UserApp] ([ID])
);

