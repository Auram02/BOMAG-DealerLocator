if object_id('LeadValuesSelectByLastName') is not null
	drop procedure LeadValuesSelectByLastName

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE LeadValuesSelectByLastName
	
	@lastName varchar(50)	
AS
BEGIN
	
SET NOCOUNT ON;

	SELECT pk_valueId, fk_leadId, elementName, elementValue
	FROM [DL.LeadValues]
	WHERE elementName IN ('FirstName', 'LastName', 'City', 'State', 'Zip', 'Phone')
		AND fk_leadId IN (
					SELECT fk_leadId 
					FROM [DL.LeadValues] 
					WHERE elementName = 'LastName' 
					AND elementValue LIKE '%' + @lastName + '%')
	order by fk_leadId;

END
GO
