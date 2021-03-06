﻿CREATE TABLE [dbo].[Component] (
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [DomainID]          INT           NOT NULL,
    [Interface]         VARCHAR (50)  NOT NULL,
    [Title]             VARCHAR (100) NOT NULL,
    [Description]       VARCHAR (MAX) NULL,
    [IsPrimaryApp]      BIT           CONSTRAINT [DF_Component_IsLaunchable] DEFAULT ((0)) NOT NULL,
    [IsSecondaryApp]    BIT           CONSTRAINT [DF_Component_IsSecondary] DEFAULT ((0)) NOT NULL,
    [PrimaryAppRoute]   VARCHAR (100) NOT NULL,
    [SecondaryAppRoute] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Component] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Component_Domain] FOREIGN KEY ([DomainID]) REFERENCES [dbo].[Domain] ([ID])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Component]
    ON [dbo].[Component]([Interface] ASC);

