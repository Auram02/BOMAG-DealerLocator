IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Category'))
BEGIN

	CREATE TABLE [dbo].[Category](
		[CategoryID] [int] NULL,
		[CategoryName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[AllowTerritoryOverlap] [bit] NOT NULL DEFAULT 0
	) ON [PRIMARY]

	PRINT 'Created table Category'
END

IF NOT Exists(SELECT * FROM sys.columns WHERE Name = N'AllowTerritoryOverlap'  
            and Object_ID = Object_ID(N'Category'))
BEGIN

	ALTER TABLE [dbo].[Category]
	ADD [AllowTerritoryOverlap] [bit] NOT NULL DEFAULT 0

	PRINT 'Added column AllowTerritoryOverlap'
END