--2019-8-6  郭海祥 增加全局配置

INSERT INTO [dbo].[B_Parameter]  VALUES (N'73', N'查询打印页面配置', N'自助打印', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'30', N'页面公共配置', N'allPageType', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'31', N'页面公共配置', N'allPageType', N'config', N'IsbTempReport', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'32', N'页面公共配置', N'allPageType', N'config', N'IsQueryRequest', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO