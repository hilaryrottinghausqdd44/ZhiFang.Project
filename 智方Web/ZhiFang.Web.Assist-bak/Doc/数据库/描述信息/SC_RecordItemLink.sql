


GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SC_RecordItemLink]  WITH CHECK ADD  CONSTRAINT [FK_SC_RecordItemLink_SC_RecordType] FOREIGN KEY([RecordTypeID])
REFERENCES [dbo].[SC_RecordType] ([RecordTypeID])
GO

ALTER TABLE [dbo].[SC_RecordItemLink] CHECK CONSTRAINT [FK_SC_RecordItemLink_SC_RecordType]
GO

ALTER TABLE [dbo].[SC_RecordItemLink]  WITH CHECK ADD  CONSTRAINT [FK_SC_RecordItemLink_SC_RecordTypeItem] FOREIGN KEY([RecordTypeItemID])
REFERENCES [dbo].[SC_RecordTypeItem] ([RecordTypeItemID])
GO

ALTER TABLE [dbo].[SC_RecordItemLink] CHECK CONSTRAINT [FK_SC_RecordItemLink_SC_RecordTypeItem]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关系Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordItemLink', @level2type=N'COLUMN',@level2name=N'RecordLinkId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录项类型Id，关联SC_RecordType的主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordItemLink', @level2type=N'COLUMN',@level2name=N'RecordTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录项字典Id,关联SC_RecordTypeItem的Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordItemLink', @level2type=N'COLUMN',@level2name=N'RecordTypeItemID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检验项目对照码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordItemLink', @level2type=N'COLUMN',@level2name=N'TestItemCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否开单可见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordItemLink', @level2type=N'COLUMN',@level2name=N'IsBillVisible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录项类型Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordItemLink', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录项类型与记录项字典关系' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_RecordItemLink'
GO


