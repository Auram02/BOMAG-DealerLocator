USE DealerLocator
GO

if object_id('OverviewReport') is not null
	drop procedure [dbo].[OverviewReport]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[OverviewReport]
	@CategoryIDList varchar(200)
AS
BEGIN
	
SET NOCOUNT ON;

--SELECT * FROM #County

SELECT ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, 
                    CategoryName, AllowTerritoryOverlap, pk_DistributorID, Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, 
                    '' AS GroupStateName, '' AS GroupStateAbbreviation, Distributor.fk_StateID 
FROM Contract
INNER JOIN ContractCounty
	ON Contract.ContractID = ContractCounty.fk_ContractID 
INNER JOIN County
	ON County.CountyID = ContractCounty.fk_CountyID
INNER JOIN ContractCategory cc
	ON cc.fk_ContractID = ContractID 
INNER JOIN Category cat
	ON CategoryID = cc.fk_CategoryID
INNER JOIN Distributor
	ON Distributor.pk_DistributorID LIKE MainDistributorID 
INNER JOIN State s1
	ON s1.StateID = County.fk_StateID
WHERE CategoryID IN ( SELECT * FROM dbo.SplitInts(@CategoryIDList, ',') ) 
GROUP BY  ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, CategoryName, AllowTerritoryOverlap, pk_DistributorID, 
                    Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, Distributor.fk_StateID
	
END
GO



