USE DealerLocator
GO

if object_id('StateListByTerritoryRep') is not null
	drop procedure [dbo].[StateListByTerritoryRep]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[StateListByTerritoryRep]
	@TerritoryRepID INT
AS
BEGIN
	
SET NOCOUNT ON;

	SELECT StateID, Abbreviation, FullName FROM [State] 
INNER JOIN County
	ON County.fk_StateID = [State].StateID
INNER JOIN ContractCounty cc
	ON cc.fk_CountyID = County.CountyID 
INNER JOIN contractdistributor cd
	ON cd.fk_ContractID = cc.fk_ContractID
WHERE fk_DistributorID IN (
	SELECT db.fk_BranchDistID as fk_DistributorID FROM Distributor
	INNER JOIN DistributorBranch db on
		pk_DistributorID = db.fk_MainDistID
	INNER JOIN DistributorRepresentative dr
		ON dr.fk_DistributorID = db.fk_BranchDistID  -- Was ON dr.fk_DistributorID = db.fk_BranchDistID 02/13/17
	WHERE fk_TerritoryRepID = @TerritoryRepID
)
GROUP BY StateID, Abbreviation, FullName
ORDER BY Abbreviation
	
END
GO



