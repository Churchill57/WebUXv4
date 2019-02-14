CREATE TABLE [dbo].[Domain] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED ([ID] ASC)
);

