---郭海祥 2019-12-04


INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'14', N'重申标记', N'BRevised', NULL, N'60', NULL, NULL, NULL, N'1')
GO

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'15', N'报告类型', N'ReportType', NULL, N'60', NULL, NULL, NULL, N'1')
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'116', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewTop', N'50', NULL, N'int', NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[B_Parameter]  VALUES (N'117', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewLeft', N'6', NULL, N'int', NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[B_Parameter]  VALUES (N'118', N'查询打印页面配置', N'自助打印', N'config', N'defaultCondition', N'', NULL, N'string', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'35', N'身份证号', N'ZDY13', N'search', N'35', N'110', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY13'', fieldLabel: ''身份证号'', labelWidth: 35, width: 110 }')
GO


