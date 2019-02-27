CREATE TABLE [dbo].[ExecutionThread] (
    [ID]                      INT             IDENTITY (1, 1) NOT NULL,
    [LaunchCommandLineJson]   NVARCHAR (128)  NULL,
    [LaunchInputsJson]        NVARCHAR (256)  NULL,
    [ExecutionStatus]         INT             NOT NULL,
    [LockUserName]            VARCHAR (256)   NULL,
    [LockUserID]              VARCHAR (128)   NULL,
    [LockDateTime]            DATE            NULL,
    [RootComponentTitle]      VARCHAR (128)   NOT NULL,
    [ExecutingComponentTitle] VARCHAR (128)   NULL,
    [ComponentExecutingID]    INT             NOT NULL,
    [ExecutingComponentsJson] NVARCHAR (MAX)  NOT NULL,
    [PendingOutcomeJson]      NVARCHAR (1024) NULL,
    CONSTRAINT [PK_ExecutionThread] PRIMARY KEY CLUSTERED ([ID] ASC)
);



