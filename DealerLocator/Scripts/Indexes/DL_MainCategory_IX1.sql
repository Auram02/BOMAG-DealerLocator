/****** Object:  Index [DL_MainCategory_IX1]    Script Date: 02/13/2011 11:31:17 ******/
CREATE UNIQUE NONCLUSTERED INDEX [DL_MainCategory_IX1] ON [dbo].[DL.MainCategory] 
(
	[pk_mainCatID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]