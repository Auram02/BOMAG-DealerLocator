
if object_id('RepresentativeGetTerritoryRep') is not null
	drop procedure [dbo].[RepresentativeGetTerritoryRep]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RepresentativeGetTerritoryRep]

AS
BEGIN
	
SET NOCOUNT ON;

    SELECT *
    FROM Representative Rep
    INNER JOIN RepresentativeType RepType
        ON Rep.fk_RepTypeID = RepType.RepTypeID
    INNER JOIN [DL.RepMapping] repMap
        ON repMap.fk_RepID = Rep.RepID
    INNER JOIN [DL.User]
        ON pk_userID = repMap.fk_userID
    WHERE RepType.Description = 'Territory'
    ORDER BY Repname 	
	
END
GO

