if object_id('LeadBlackListInsert') is not null
	drop procedure LeadBlackListInsert

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE LeadBlackListInsert
	
	@lastName varchar(50),
	@phone	varchar(12),
	@city	varchar(50),
	@state	varchar(50),
	@zip	varchar(10)
	
AS
BEGIN
	
SET NOCOUNT ON;

	INSERT INTO LeadBlackList
		(lastName, phone, city, state, zip)
	VALUES
		(@lastName, @phone, @city, @state, @zip)
END
GO
