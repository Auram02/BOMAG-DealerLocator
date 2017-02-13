
/****** Object:  Index [IX_DL_Lead_Submitted]    Script Date: 02/13/2011 11:15:20 ******/
CREATE NONCLUSTERED INDEX [IX_DL_Lead_Submitted] ON [dbo].[DL.Lead] 
(
	[submitDate] ASC,
	[submitted] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]