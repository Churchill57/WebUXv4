CREATE TABLE [dbo].[DependantComponent] (
    [SubjectComponentID]      INT NOT NULL,
    [DirectlyUsesComponentID] INT NOT NULL,
    CONSTRAINT [PK_DependantComponent] PRIMARY KEY CLUSTERED ([SubjectComponentID] ASC, [DirectlyUsesComponentID] ASC),
    CONSTRAINT [FK_DependantComponent_Component] FOREIGN KEY ([DirectlyUsesComponentID]) REFERENCES [dbo].[Component] ([ID]),
    CONSTRAINT [FK_SubjectComponent_Component] FOREIGN KEY ([SubjectComponentID]) REFERENCES [dbo].[Component] ([ID])
);

