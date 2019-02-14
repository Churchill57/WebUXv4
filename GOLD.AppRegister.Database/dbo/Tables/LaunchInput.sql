CREATE TABLE [dbo].[LaunchInput] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [ComponentID] INT          NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [DataType]    VARCHAR (50) CONSTRAINT [DF_LaunchInput_DataType] DEFAULT ('string') NOT NULL,
    CONSTRAINT [PK_LaunchInput] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_LaunchInput_Component] FOREIGN KEY ([ComponentID]) REFERENCES [dbo].[Component] ([ID])
);

