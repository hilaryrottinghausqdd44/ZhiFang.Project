--郭海祥 2019-8-13 
--增加全局设置
--更新表B_ColumnsUnit中Sname字段值为自助打印的更新成selfhelp



INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'12', N'样本类型', N'SampletypeName', NULL, N'60', NULL, NULL, NULL, N'1')
GO

INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'13', N'科室', N'DeptName', NULL, N'60', NULL, NULL, NULL, N'1')
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'74', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameTop', N'13', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'75', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameLeft', N'26', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'76', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameFontSize', N'26', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'77', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameColor', N'#0e121a', NULL, N'string', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'78', N'查询打印页面配置', N'自助打印', N'config', N'printnumTop', N'14', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'79', N'查询打印页面配置', N'自助打印', N'config', N'printnumLeft', N'34', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'80', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameFontSize', N'26', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'81', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameColor', N'#0e121a', NULL, N'string', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'82', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextTop', N'16', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'83', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextLeft', N'16', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'84', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextFontSize', N'35', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'85', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextColor', N'black', NULL, N'string', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'86', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeTop', N'13', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'87', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeLeft', N'6', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'88', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeFontSize', N'26', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'89', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeColor', N'#0e121a', NULL, N'string', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'90', N'查询打印页面配置', N'自助打印', N'config', N'tabgridTop', N'66', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'91', N'查询打印页面配置', N'自助打印', N'config', N'tabgridLeft', N'8', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'92', N'查询打印页面配置', N'自助打印', N'config', N'closeprintnumTop', N'13', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'93', N'查询打印页面配置', N'自助打印', N'config', N'closeprintnumLeft', N'41', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'94', N'查询打印页面配置', N'自助打印', N'config', N'caridTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'95', N'查询打印页面配置', N'自助打印', N'config', N'caridLeft', N'15', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'96', N'查询打印页面配置', N'自助打印', N'config', N'caridWidth', N'260', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'97', N'查询打印页面配置', N'自助打印', N'config', N'openJianpanTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'98', N'查询打印页面配置', N'自助打印', N'config', N'openJianpanLeft', N'53', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'99', N'查询打印页面配置', N'自助打印', N'config', N'closeJianpanTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'100', N'查询打印页面配置', N'自助打印', N'config', N'closeJianpanLeft', N'53', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'101', N'查询打印页面配置', N'自助打印', N'config', N'closeTop', N'69', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'102', N'查询打印页面配置', N'自助打印', N'config', N'closeLeft', N'15', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'103', N'查询打印页面配置', N'自助打印', N'config', N'closeFontSize', N'16', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'104', N'查询打印页面配置', N'自助打印', N'config', N'closeColor', N'black', NULL, N'string', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'105', N'查询打印页面配置', N'自助打印', N'config', N'reportviewTop', N'70', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'106', N'查询打印页面配置', N'自助打印', N'config', N'reportviewLeft', N'28', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'107', N'查询打印页面配置', N'自助打印', N'config', N'reportviewFontSize', N'14', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'108', N'查询打印页面配置', N'自助打印', N'config', N'reportviewColor', N'black', NULL, N'string', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'109', N'查询打印页面配置', N'自助打印', N'config', N'panelTop', N'36', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'110', N'查询打印页面配置', N'自助打印', N'config', N'panelLeft', N'15', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'111', N'查询打印页面配置', N'自助打印', N'config', N'panelWidth', N'600', NULL, N'int', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'112', N'查询打印页面配置', N'自助打印', N'config', N'panelHeight', N'242', NULL, N'int', NULL, NULL, NULL, NULL)
GO





