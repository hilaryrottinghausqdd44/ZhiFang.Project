




EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实验室ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开瓶管理记录主单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'OBottleOperDocId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货品ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'GoodsID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'QtyDtlID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出库明细单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'OutDtlID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出库总单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'OutDocID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开瓶使用日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'BOpenDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存货品开瓶后最后有效期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'InvalidBOpenDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是使用完成标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'IsUseCompleteFlag'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用完成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'UseCompleteDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'IsObsolete'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废人Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'ObsoleteID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废备注Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'ObsoleteMemoId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'ObsoleteName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'ObsoleteTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作废备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'ObsoleteMemo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'Visible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'CreaterID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'CreaterName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'Memo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开瓶管理记录主单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rea_OpenBottleOperDoc'
GO


