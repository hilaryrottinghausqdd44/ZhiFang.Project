--- 郭海祥 2020-02-10
--- 增加自助打印页面设置	
INSERT INTO [dbo].[B_Parameter]  VALUES (N'119', N'查询打印页面配置', N'自助打印', N'config', N'printnumnameIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'120', N'查询打印页面配置', N'自助打印', N'config', N'printnumIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'121', N'查询打印页面配置', N'自助打印', N'config', N'closeprintnumIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'122', N'查询打印页面配置', N'自助打印', N'config', N'tabgridHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'123', N'查询打印页面配置', N'自助打印', N'config', N'tabgridHangTouFontSize', N'20', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'124', N'查询打印页面配置', N'自助打印', N'config', N'tabgridNeiRongFontSize', N'16', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'125', N'查询打印页面配置', N'自助打印', N'config', N'selfhelpTextIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'126', N'查询打印页面配置', N'自助打印', N'config', N'DateTimeIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'127', N'查询打印页面配置', N'自助打印', N'config', N'caridHeight', N'30', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'128', N'查询打印页面配置', N'自助打印', N'config', N'caridFontSize', N'20', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'129', N'查询打印页面配置', N'自助打印', N'config', N'caridIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'130', N'查询打印页面配置', N'自助打印', N'config', N'openJianpanIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'131', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'132', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonLeft', N'65', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'133', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonWidth', N'100', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'134', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonHeight', N'36', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'135', N'查询打印页面配置', N'自助打印', N'config', N'readCardButtonIsHidden', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'136', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsFontSize', N'20', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'137', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'138', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonTop', N'68', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'139', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonLeft', N'80', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'140', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonWidth', N'100', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'141', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonHeight', N'36', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'142', N'查询打印页面配置', N'自助打印', N'config', N'printreportButtonIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'143', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewFontSize', N'30', NULL, N'int', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_Parameter]  VALUES (N'144', N'查询打印页面配置', N'自助打印', N'config', N'bigReportviewIsHidden', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'36', N'拼音码', N'PinYinMa', N'search', N'60', N'180', NULL, NULL, N'textfield', N'like ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''like'', name: ''PinYinMa'', fieldLabel: ''姓名拼音'', labelWidth: 60, width: 180 }')
GO

INSERT INTO [dbo].[B_SearchUnit]  VALUES (N'37', N'住院号', N'ZDY5', N'search', N'60', N'180', NULL, NULL, N'textfield', N'= ', NULL, N'{ type: ''search'', xtype: ''textfield'', mark: ''='', name: ''ZDY5'', fieldLabel: ''住院号'', labelWidth: 60, width: 180 }')
GO

ALTER TABLE [dbo].[SectionPrint] ADD IsRFGraphdataPDf bit


INSERT INTO [dbo].[B_ColumnsUnit]  VALUES (N'17', N'是否二审', N'ZDY3', NULL, N'100', NULL, NULL, '{renderer:function (v, meta, record) {
	                var result = '''';
	                if(v == null || v == ""){
                        meta.style="background-color:red";
	                    result = "否";
	                }else{
						result = "是";
                    }
	                return result;
	            }}', N'1')
GO

