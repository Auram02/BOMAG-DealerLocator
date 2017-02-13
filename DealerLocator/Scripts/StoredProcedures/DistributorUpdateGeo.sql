

if object_id('DistributorUpdateGeo') is not null
	drop procedure [dbo].[DistributorUpdateGeo]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DistributorUpdateGeo]
	@DistributorID int,
	@Lat float,
	@Lng float
AS
BEGIN
	
SET NOCOUNT ON;

UPDATE Distributor 
SET Lat = @Lat, 
	Lng = @Lng, 
	GeoLastUpdated = getdate()
WHERE pk_distributorID = @DistributorID


END
GO



