/****** Object:  Index [DL_SubCategory_IX1]    Script Date: 02/13/2011 11:30:04 ******/
CREATE NONCLUSTERED INDEX [DL_SubCategory_IX1] ON [dbo].[DL.SubCategory] 
(
	[pk_subCatID] ASC,
	[fk_mainCatID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]