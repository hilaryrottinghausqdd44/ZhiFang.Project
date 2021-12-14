USE [digitlab_bs]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录明细录入项主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'RecordTypeItemID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入项编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ItemCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入项名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'CName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'SName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快捷码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ShortCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'汉字拼音字头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'PinYinZiTou'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'辅助录入信息
如记录项的数据类型，默认值，数据源等信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ItemEditInfo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录项默认值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DefaultValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表单项类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ItemXType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录项单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'ItemUnit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录项描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'背景颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'BGColor'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'IsUse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DataUpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordTypeItem', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO


