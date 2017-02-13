/****** Object:  Index [DL_Model_IX1]    Script Date: 02/13/2011 11:26:50 ******/
CREATE NONCLUSTERED INDEX [DL_Model_IX1] ON [dbo].[DL.Model] 
(
	[pk_modelID] ASC,
	[modelName] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]