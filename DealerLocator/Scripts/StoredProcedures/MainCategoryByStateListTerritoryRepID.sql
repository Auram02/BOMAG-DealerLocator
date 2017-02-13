
if object_id('MainCategoryByStateListTerritoryRepID') is not null
	drop procedure [dbo].[MainCategoryByStateListTerritoryRepID]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[MainCategoryByStateListTerritoryRepID]
	@TerritoryRepID int,
	@StateIDList varchar(200)
AS
BEGIN
	
SET NOCOUNT ON;

	 SELECT DISTINCT pk_mainCatID, categoryName, [disable], position, dockMenuImageUrlLarge, 
		dockMenuImageUrlSmall, dockMenuTitle, AllowTerritoryOverlap, DisplayName
	 FROM [DL.MainCategory]
	 INNER JOIN ContractCategory cc
		ON cc.fk_CategoryID = pk_MainCatID
	 INNER JOIN ContractCounty cCounty
		ON cCounty.fk_ContractID = cc.fk_ContractID
	 INNER JOIN contractdistributor cd
		ON cd.fk_ContractID = cc.fk_ContractID
	INNER JOIN County
		ON County.CountyID = cCounty.fk_CountyID
	 WHERE fk_DistributorID IN (
		SELECT db.fk_BranchDistID as fk_DistributorID FROM Distributor
		INNER JOIN DistributorBranch db on
			pk_DistributorID = db.fk_MainDistID
		INNER JOIN DistributorRepresentative dr
			ON dr.fk_DistributorID = db.fk_MainDistID
		WHERE fk_TerritoryRepID = @TerritoryRepID
	 )
	 AND County.fk_StateID IN (SELECT * FROM dbo.SplitInts(@StateIDList, ','))
	 
	 GROUP BY pk_mainCatID, categoryName, [disable], position, dockMenuImageUrlLarge, 
		dockMenuImageUrlSmall, dockMenuTitle, AllowTerritoryOverlap, DisplayName 
	ORDER BY position ASC
	
	
END
GO
