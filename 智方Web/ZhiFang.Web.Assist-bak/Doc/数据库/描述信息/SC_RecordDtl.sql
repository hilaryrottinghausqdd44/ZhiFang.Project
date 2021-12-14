



GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SC_RecordDtl]  WITH CHECK ADD  CONSTRAINT [FK_SC_RecordDtl_SC_RecordTypeItem] FOREIGN KEY([RecordTypeItemID])
REFERENCES [dbo].[SC_RecordTypeItem] ([RecordTypeItemID])
GO

ALTER TABLE [dbo].[SC_RecordDtl] CHECK CONSTRAINT [FK_SC_RecordDtl_SC_RecordTypeItem]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LabID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��¼��ϸ����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'RecordDtlId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��¼��ϸ����(�簴�Զ������������)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'RecordDtlNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ҵ����ϸID��
������Ժ�������Ǽ�������ȱ������Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'BObjectID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��¼�������������
������¼�����ͱ��ֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'ContentTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������Id,
����������¼�����ֵ�ID, SC_RecordType' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'RecordTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ƷId,
����������¼�ֵ�����ID, SC_RecordTypeItem' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'RecordTypeItemID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'TestItemCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'ItemResult'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ͽ��ֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'NumberItemResult'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�ʹ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'Visible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ע' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'Memo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ʾ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʱ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��¼���¼��ϸ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl'
GO


