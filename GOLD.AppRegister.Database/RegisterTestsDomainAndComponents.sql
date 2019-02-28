USE [GOLD.AppRegister]
GO

DECLARE @TestsDomain uniqueidentifier = '84F53AE2-F10F-4C9C-8FEC-D4FA1E4EA891';

INSERT INTO [dbo].[Domain]
           ([ID]
           ,[Name]
           ,[Description])
     VALUES
           (@TestsDomain
           ,'http://localhost:16466'
           ,'GOLD.TestsDomain.MVC')

INSERT INTO [dbo].[Component]
           ([DomainID]
           ,[InterfaceFullname]
           ,[Title]
           ,[Description]
           ,[IsPrimaryApp]
           ,[IsSecondaryApp]
           ,[PrimaryAppRoute]
           ,[SecondaryAppRoute])
     VALUES
           (@TestsDomain
           ,'GOLD.TestsDomain.Interfaces.ILuTest1'
           ,'Test1'
           ,'Initial GOLD framework Test1'
           ,1
           ,1
           ,'Tests/LuTest1Primary'
           ,'Tests/LuTest1Secondary')

INSERT INTO [dbo].[Component]
           ([DomainID]
           ,[InterfaceFullname]
           ,[Title]
           ,[Description]
           ,[IsPrimaryApp]
           ,[IsSecondaryApp]
           ,[PrimaryAppRoute]
           ,[SecondaryAppRoute])
     VALUES
           (@TestsDomain
           ,'GOLD.TestsDomain.Interfaces.IUxA'
           ,'UxA'
           ,'UxA for Initial GOLD framework Test1'
           ,0
           ,0
           ,'Tests/UxA'
           ,'Tests/UxA')

INSERT INTO [dbo].[Component]
           ([DomainID]
           ,[InterfaceFullname]
           ,[Title]
           ,[Description]
           ,[IsPrimaryApp]
           ,[IsSecondaryApp]
           ,[PrimaryAppRoute]
           ,[SecondaryAppRoute])
     VALUES
           (@TestsDomain
           ,'GOLD.TestsDomain.Interfaces.IUxB'
           ,'UxB'
           ,'UxB for Initial GOLD framework Test1'
           ,0
           ,0
           ,'Tests/UxB'
           ,'Tests/UxB')

GO

