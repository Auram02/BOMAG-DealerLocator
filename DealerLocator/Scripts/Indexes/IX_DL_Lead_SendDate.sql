/****** Object:  Index [IX_DL_Lead_SendDate]    Script Date: 02/13/2011 11:15:33 ******/
CREATE NONCLUSTERED INDEX [IX_DL_Lead_SendDate] ON [dbo].[DL.Lead] 
(
	[pk_leadID] ASC,
	[sendDate] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]