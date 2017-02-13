if object_id('LeadBlackListDelete') is not null
	drop procedure LeadBlackListDelete

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE LeadBlackListDelete
	
	@leadBlackListId		int
	
AS
BEGIN
	
SET NOCOUNT ON;

	DELETE FROM LeadBlackList
	WHERE leadBlackListId = @leadBlackListId;
END
GO
