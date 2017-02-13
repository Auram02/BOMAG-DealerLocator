IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Distributor'))
BEGIN

	CREATE TABLE [dbo].[Distributor](
		[pk_DistributorID] [int] NULL,
		[DistName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[BillingAddress] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[ShippingAddress] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[CityName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[fk_StateID] [int] NULL,
		[fk_ZipID] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[fk_CountryID] [int] NULL,
		[Phone] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[Fax] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[Contacts] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[BillingCityName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[fk_BillingStateID] [int] NULL,
		[fk_BillingZipID] [int] NULL,
		[fk_BillingCountryID] [int] NULL,
		[SAP] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[Node] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[MainDistributor] [int] NULL,
		[PartsOnly] [int] NULL,
		[ManufacturerRep] [int] NOT NULL DEFAULT 0
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	PRINT 'Created table Distributor'
END

IF NOT Exists(SELECT * FROM sys.columns WHERE Name = N'ManufacturerRep'  
            and Object_ID = Object_ID(N'Distributor'))
BEGIN

	ALTER TABLE [dbo].[Distributor]
	ADD [ManufacturerRep] [int]

	PRINT 'Added column ManufacturerRep'
END