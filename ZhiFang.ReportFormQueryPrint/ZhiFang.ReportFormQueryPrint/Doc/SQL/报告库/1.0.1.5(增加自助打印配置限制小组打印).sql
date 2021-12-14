--郭海祥 2019-6-25 增加自助打印页面配置  限制小组打印 样本类型显示列

INSERT INTO [dbo].[B_Parameter]  VALUES (N'72', N'查询打印页面配置', N'自助打印', N'config', N'notPrintSectionNo', N'', NULL, N'string', NULL, NULL, NULL, NULL)
GO

update B_ColumnsUnit set ColumnName = 'SAMPLENO' where ColID = 10