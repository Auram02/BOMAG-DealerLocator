IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'DL.MainCategory'))
BEGIN

	CREATE TABLE [dbo].[DL.MainCategory](
		[pk_mainCatID] [int] NOT NULL,
		[categoryName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[disable] [bit] NOT NULL,
		[position] [int] NULL,
		[dockMenuImageUrlLarge] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		[dockMenuImageUrlSmall] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		[dockMenuTitle] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		[AllowTerritoryOverlap] [bit] NOT NULL DEFAULT 0
	) ON [PRIMARY]

	PRINT 'Created table DL.MainCategory'
END

IF NOT Exists(SELECT * FROM sys.columns WHERE Name = N'AllowTerritoryOverlap'  
            and Object_ID = Object_ID(N'[DL.MainCategory]'))
BEGIN

	ALTER TABLE [dbo].[DL.MainCategory]
	ADD [AllowTerritoryOverlap] [bit] NOT NULL DEFAULT 0

	PRINT 'Added column AllowTerritoryOverlap'
END