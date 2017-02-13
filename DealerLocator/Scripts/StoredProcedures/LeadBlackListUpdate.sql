if object_id('LeadBlackListUpdate') is not null
	drop procedure LeadBlackListUpdate

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE LeadBlackListUpdate
	
	@leadBlackListId		int,
	@emailAddress			varchar(100),
	@numberOfTriesToAdd		int
	
AS
BEGIN
	
SET NOCOUNT ON;

	UPDATE LeadBlackList
	SET submissionCount = (submissionCount + @numberOfTriesToAdd),
		emailAddress = COALESCE(@emailAddress, emailAddress, '')
	WHERE leadBlackListId = @leadBlackListId;
END
GO
