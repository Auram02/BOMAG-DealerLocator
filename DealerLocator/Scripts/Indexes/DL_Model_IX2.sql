/****** Object:  Index [DL_Model_IX2]    Script Date: 02/13/2011 11:27:34 ******/
CREATE NONCLUSTERED INDEX [DL_Model_IX2] ON [dbo].[DL.Model] 
(
	[pk_modelID] ASC,
	[fk_subCatID] ASC,
	[fk_mainCatID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]