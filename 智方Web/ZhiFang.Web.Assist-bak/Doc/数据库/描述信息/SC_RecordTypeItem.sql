USE [digitlab_bs]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��¼��ϸ¼��������ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'RecordTypeItemID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¼�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ItemCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¼��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'SName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ShortCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ƴ����ͷ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'PinYinZiTou'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����¼����Ϣ
���¼����������ͣ�Ĭ��ֵ������Դ����Ϣ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ItemEditInfo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��¼��Ĭ��ֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DefaultValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ItemXType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��¼�λ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ItemUnit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��¼������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ɫ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'BGColor'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ע' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ʾ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�ʹ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'IsUse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DataUpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʱ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO


