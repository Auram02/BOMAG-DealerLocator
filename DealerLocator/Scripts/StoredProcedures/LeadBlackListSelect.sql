if object_id('LeadBlackListSelect') is not null
	drop procedure LeadBlackListSelect

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE LeadBlackListSelect
	
AS
BEGIN
	
SET NOCOUNT ON;

    SELECT leadBlackListId, phone, lastName, city, state, zip, emailAddress, submissionCount
	FROM LeadBlackList

END
GO
