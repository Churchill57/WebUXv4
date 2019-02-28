USE [GOLD.AppRegister]
GO

DECLARE @CustomerDomainID uniqueidentifier = '2476C153-D94C-40B6-B0C3-221002AA5379';

INSERT INTO [dbo].[Domain]
           ([ID]
           ,[Name]
           ,[Description])
     VALUES
           (@CustomerDomainID
           ,'http://localhost:1975'
           ,'GOLD.CustomerDomain.MVC')

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
           (@CustomerDomainID
           ,'GOLD.CustomerDomain.Interfaces.ILuPreviewCustomer'
           ,'Preview Customer'
           ,'Shows the personal details of a specific customer'
           ,1
           ,1
           ,'Customer/PreviewCustomer'
           ,'Customer/PreviewCustomer')

GO

