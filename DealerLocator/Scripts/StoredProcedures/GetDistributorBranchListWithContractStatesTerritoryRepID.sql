

if object_id('GetDistributorBranchListWithContractStatesTerritoryRepID') is not null
	drop procedure [dbo].[GetDistributorBranchListWithContractStatesTerritoryRepID]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetDistributorBranchListWithContractStatesTerritoryRepID]
	@CategoryID int,
	@MainDistributorID int,
	@StateList varchar(1000) = null,
	@TerritoryRepID int = 0
AS
BEGIN
	
SET NOCOUNT ON;

CREATE TABLE #County (CountyID INT)

if (len(@StateList) > 0)
BEGIN
	DECLARE @sql varchar(2000)
	
	SET @sql = 'INSERT INTO #County SELECT DISTINCT CountyID 
							FROM County WHERE fk_StateID IN (' + @StateList + ' )'
			
			exec(@sql)
END
ELSE
	INSERT INTO #County SELECT DISTINCT CountyID FROM County 

	SELECT pk_DistributorID, Distname, s2.FullName, s2.Abbreviation, ShippingAddress, CityName, fk_ZipID, s1.FullName as ContractState,
			Lat, Lng, GeoLastUpdated
    FROM ContractCounty cc, ContractCategory cCat, ContractDistributor cDist, Distributor dist, State s1, County, State s2, DistributorBranch distBranch
    WHERE cc.fk_countyID IN (   SELECT DISTINCT CountyID FROM #County )
		AND cCat.fk_CategoryID = @CategoryID 
		AND cCat.fk_contractID = cc.fk_contractID
		AND cCat.fk_contractID = cDist.fk_contractID
		AND dist.pk_DistributorID LIKE cDist.fk_DistributorID
		AND County.CountyID = cc.fk_countyID
		AND s1.StateID = County.fk_StateID
		AND s2.StateID = dist.fk_StateID
		AND Dist.pk_DistributorID LIKE distBranch.fk_BranchDistID
		AND distBranch.fk_MainDistID = @MainDistributorID
		AND distBranch.fk_BranchDistID IN 
		(

		SELECT DISTINCT db.fk_BranchDistID as fk_DistributorID FROM Distributor
				INNER JOIN DistributorBranch db on
					pk_DistributorID = db.fk_BranchDistID
				INNER JOIN DistributorRepresentative dr
					ON dr.fk_DistributorID = db.fk_BranchDistID
				WHERE fk_TerritoryRepID = @TerritoryRepID
					AND db.fk_MainDistID = @MainDistributorID 

		)
    GROUP BY  pk_DistributorID, Distname, s2.FullName, s2.Abbreviation, ShippingAddress, CityName, fk_ZipID, s1.FullName, Lat, Lng, GeoLastUpdated

END
GO



