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
           ,'GOLD.CustomerDomain.Interfaces.ILuPreviewCustomerIncSearchAndSelect'
           ,'Preview Customer'
           ,'Shows the personal details of a specific customer'
           ,1
           ,1
           ,'Customer/LuPreviewCustomerIncSearchAndSelect'
           ,'Customer/LuPreviewCustomerIncSearchAndSelect')

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
           ,'GOLD.CustomerDomain.Interfaces.IUxCustomerSearchCriteria'
           ,'Search Customer'
           ,'UxCustomerSearchCriteria for Initial GOLD framework'
           ,0
           ,0
           ,'Customer/UxCustomerSearchCriteria'
           ,'Customer/UxCustomerSearchCriteria')

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
           ,'GOLD.CustomerDomain.Interfaces.IUxPreviewCustomer'
           ,'Preview Customer'
           ,'UxPreviewCustomer for Initial GOLD framework'
           ,0
           ,0
           ,'Customer/UxPreviewCustomer'
           ,'Customer/UxPreviewCustomer')

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
           ,'GOLD.CustomerDomain.Interfaces.IUxSelectCustomer'
           ,'Select Customer'
           ,'UxSelectCustomer for Initial GOLD framework'
           ,0
           ,0
           ,'Customer/UxSelectCustomer'
           ,'Customer/UxSelectCustomer')
GO
