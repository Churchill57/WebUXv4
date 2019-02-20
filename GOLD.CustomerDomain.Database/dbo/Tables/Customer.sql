CREATE TABLE [dbo].[Customer] (
    [ID]        INT          IDENTITY (1, 1) NOT NULL,
    [Title]     VARCHAR (50) NULL,
    [FirstName] VARCHAR (50) NOT NULL,
    [LastName]  VARCHAR (50) NOT NULL,
    [Gender]    INT          NULL,
    [DOB]       DATE         NULL,
    [NINO]      CHAR (8)     NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([ID] ASC)
);



