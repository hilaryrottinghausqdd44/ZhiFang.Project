



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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录明细主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'RecordDtlId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录明细单号(如按自定义规则的条码号)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'RecordDtlNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务明细ID，
可能是院感样本登记主单表等表的主键Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'BObjectID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录项类型所属类别
冗余存记录项类型表的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'ContentTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'监测类型Id,
关联公共记录类型字典ID, SC_RecordType' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'RecordTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'监测样品Id,
关联公共记录字典主键ID, SC_RecordTypeItem' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'RecordTypeItemID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检验对照码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'TestItemCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结果值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'ItemResult'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数字型结果值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'NumberItemResult'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'Visible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'Memo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录项记录明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordDtl'
GO


