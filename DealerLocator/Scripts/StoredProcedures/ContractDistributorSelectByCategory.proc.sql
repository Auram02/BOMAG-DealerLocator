USE DealerLocator

if object_id('ContractDistributorSelectByCategory') is not null
	drop procedure [dbo].[ContractDistributorSelectByCategory]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ContractDistributorSelectByCategory]
	@CategoryID INT,
	@States nvarchar(max)
AS
BEGIN
	
SET NOCOUNT ON;

	CREATE TABLE #States (FullName nvarchar(max) )
	
	DECLARE @StatesSQL nvarchar(max);
	SELECT @StatesSQL = 'INSERT INTO #States (FullName) 
		SELECT [FullName] FROM [State] WHERE (FullName in (' + @States + '))'

	exec sp_executesql @StatesSQL

	SELECT ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID, 
		IsManufacturerRepContract, CategoryID, CategoryName, AllowTerritoryOverlap,
		pk_DistributorID, DistName, CountyName, CountyID, State.FullName, State.Abbreviation, 
		ShippingAddress, CityName, fk_ZipID, s2.FullName AS GroupStateName, s2.Abbreviation AS GroupStateAbbreviation
	FROM Contract
	INNER JOIN ContractCategory cc
		ON Contract.ContractID = cc.fk_ContractID
	INNER JOIN Category cat
		ON cat.CategoryID = cc.fk_CategoryID
	INNER JOIN Distributor dist
		ON dist.pk_DistributorID = MainDistributorID
	INNER JOIN ContractCounty ccty
		ON ccty.fk_ContractID = Contract.ContractID
	INNER JOIN County
		ON County.CountyID = ccty.fk_CountyID
	INNER JOIN State
		ON State.StateID = County.fk_StateID
	INNER JOIN State s2
		ON s2.StateID = dist.fk_StateID
	WHERE cat.CategoryID = @CategoryID
		AND State.FullName IN (SELECT FullName FROM #States)
	ORDER BY FullName, CountyName
	
	DROP TABLE #States
	
END
GO
