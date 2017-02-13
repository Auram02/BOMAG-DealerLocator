USE DealerLocator

if object_id('MainCategoryByDistributorID') is not null
	drop procedure [dbo].[MainCategoryByDistributorID]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[MainCategoryByDistributorID]
	@DistributorID int
AS
BEGIN
	
SET NOCOUNT ON;

	 SELECT DISTINCT pk_mainCatID, categoryName, [disable], position, dockMenuImageUrlLarge, 
		dockMenuImageUrlSmall, dockMenuTitle, AllowTerritoryOverlap, DisplayName
	 FROM [DL.MainCategory]
	 INNER JOIN ContractCategory cc
		ON cc.fk_CategoryID = pk_MainCatID
	 INNER JOIN ContractDistributor cd
		ON cd.fk_ContractID = cc.fk_ContractID
	 WHERE cd.fk_Distributorid IN (SELECT fk_BranchDistID FROM DistributorBranch WHERE fk_MainDistID = @DistributorID)
	 GROUP BY pk_mainCatID, categoryName, [disable], position, dockMenuImageUrlLarge, 
		dockMenuImageUrlSmall, dockMenuTitle, AllowTerritoryOverlap, DisplayName 
	ORDER BY position ASC
	
END
GO
