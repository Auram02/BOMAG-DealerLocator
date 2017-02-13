IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Contract'))
BEGIN

	CREATE TABLE [dbo].[Contract](
		[ContractID] [int] NULL,
		[ContractNumber] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[ContractDate] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[ModifyDate] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[MainDistributorID] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[IsManufacturerRepContract] [int] NOT NULL DEFAULT 0
	) ON [PRIMARY]

	PRINT 'Created table Contract'
END

IF NOT Exists(SELECT * FROM sys.columns WHERE Name = N'IsManufacturerRepContract'  
            and Object_ID = Object_ID(N'Contract'))
BEGIN

	ALTER TABLE [dbo].[Contract]
	ADD [IsManufacturerRepContract] [int]

	PRINT 'Added column IsManufacturerRepContract'
END