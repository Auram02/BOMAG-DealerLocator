IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'DL.LeadProduct'))
BEGIN

	CREATE TABLE [dbo].[DL.LeadProduct](
		[pk_leadProductID] [int] NOT NULL,
		[fk_leadID] [int] NOT NULL,
		[fk_MainCatID] [int] NOT NULL,
		[fk_SubCatID] [int] NULL,
		[fk_ModelID] [int] NOT NULL,
		[IsMail] [bit] NOT NULL,
		[IsElectronic] [bit] NOT NULL,
		[distributorID] [int] NULL,
		[territoryManagerID] [int] NULL,
		[manufacturerDistributorID] [int] NOT NULL DEFAULT -1
	) ON [PRIMARY]


	PRINT 'Created table DL.LeadProduct'
END

IF NOT Exists(SELECT * FROM sys.columns WHERE Name = N'manufacturerDistributorID'  
            and Object_ID = Object_ID(N'[DL.LeadProduct]'))
BEGIN

	ALTER TABLE [dbo].[DL.LeadProduct]
	ADD [manufacturerDistributorID] [int] NOT NULL DEFAULT -1

	PRINT 'Added column manufacturerDistributorId'
END