ALTER TABLE [dbo].[Lis_TestFormMsg]  WITH CHECK ADD  CONSTRAINT [FK_LIS_TestFormMsgTestFormID] FOREIGN KEY([TestFormID])
REFERENCES [dbo].[Lis_TestForm] ([TestFormID])
GO

ALTER TABLE [dbo].[Lis_TestFormMsg] CHECK CONSTRAINT [FK_LIS_TestFormMsgTestFormID]
GO