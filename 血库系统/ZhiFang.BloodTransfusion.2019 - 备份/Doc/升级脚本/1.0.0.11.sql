IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 10) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,10,N'医嘱作废原因',100,N'医生站医嘱申请作废原因',1,N'2019/08/21 09:29:26',N'BReqFormObsolete'); 

 IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 4713710510004576645) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,4713710510004576645,10,N'患者出院',10,N'患者出院',1,N'2019/08/21 09:35:00'); 
                
IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5418855651502951451) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5418855651502951451,10,N'其他原因',30,N'其他原因',1,N'2019/08/21 09:35:37'); 
                
IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5753580128494855321) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5753580128494855321,10,N'患者转院',20,N'患者转院',1,N'2019/08/21 09:35:14'); 
                
IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_B_DIC_REFERENCE_B_DICCLA]') AND parent_object_id = OBJECT_ID(N'[dbo].[B_Dict]')) ALTER TABLE [dbo].[B_Dict] DROP CONSTRAINT [FK_B_DIC_REFERENCE_B_DICCLA]; ALTER TABLE [dbo].[B_Dict] WITH CHECK ADD CONSTRAINT [FK_B_DIC_REFERENCE_B_DICCLA] FOREIGN KEY([DCId]) REFERENCES [dbo].[B_DictType] ([DCId]); ALTER TABLE [dbo].[B_Dict] CHECK CONSTRAINT [FK_B_DIC_REFERENCE_B_DICCLA]; 

 IF COL_LENGTH('Blood_BReqForm', 'ObsoleteMemoId') IS NULL ALTER TABLE Blood_BReqForm ADD ObsoleteMemoId bigint; 