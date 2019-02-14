CREATE TABLE [dbo].[UserAppInput] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [UserAppID]     INT           NOT NULL,
    [LaunchInputID] INT           NOT NULL,
    [Value]         VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_UserAppInput] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserAppInput_LaunchInput] FOREIGN KEY ([LaunchInputID]) REFERENCES [dbo].[LaunchInput] ([ID]),
    CONSTRAINT [FK_UserAppInput_UserApp] FOREIGN KEY ([UserAppID]) REFERENCES [dbo].[UserApp] ([ID])
);

