
if object_id('DistributorsSelectByTerritoryRep') is not null
	drop procedure [dbo].[DistributorsSelectByTerritoryRep]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DistributorsSelectByTerritoryRep]
	@TerritoryRepID int
AS
BEGIN
	
SET NOCOUNT ON;



SELECT *
FROM Distributor
WHERE pk_DistributorID IN (
	SELECT fk_MainDistID FROM
	DistributorBranch db
	INNER JOIN DistributorRepresentative dr
		ON dr.fk_DistributorID = db.fk_BranchDistID
	WHERE fk_TerritoryRepID = @TerritoryRepID
)
ORDER BY DistName ASC

END
GO
