/****** Object:  Index [DL_LeadValues_ElementName_IX]    Script Date: 02/13/2011 11:24:33 ******/
CREATE NONCLUSTERED INDEX [DL_LeadValues_ElementName_IX] ON [dbo].[DL.LeadValues] 
(
	[elementName] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]