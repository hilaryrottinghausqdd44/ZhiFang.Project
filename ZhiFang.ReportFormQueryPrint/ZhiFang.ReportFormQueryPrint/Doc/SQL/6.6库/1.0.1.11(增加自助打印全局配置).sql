  --郭海祥 2019-11-6
  --更新自助打印全局配置，增加新的全局配置
  
  
  
  
  update [dbo].[B_Parameter] set ParaValue = '34' where ParaNo = 'tabgridTop' and SName = '自助打印'
  update [dbo].[B_Parameter] set ParaValue = '40' where ParaNo = 'tabgridLeft' and SName = '自助打印'
  update [dbo].[B_Parameter] set ParaValue = '34' where ParaNo = 'tabgridTop' and SName = 'selfhelp'
  update [dbo].[B_Parameter] set ParaValue = '40' where ParaNo = 'tabgridLeft' and SName = 'selfhelp'
  INSERT INTO [dbo].[B_Parameter]  VALUES (N'113', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsTop', N'66', NULL, N'int', NULL, NULL, NULL, NULL)
  INSERT INTO [dbo].[B_Parameter]  VALUES (N'114', N'查询打印页面配置', N'自助打印', N'config', N'reportformlistsLeft', N'8', NULL, N'int', NULL, NULL, NULL, NULL)
  INSERT INTO [dbo].[B_Parameter]  VALUES (N'115', N'查询打印页面配置', N'自助打印', N'config', N'noparitemname', N'', NULL, N'string', NULL, NULL, NULL, NULL)

