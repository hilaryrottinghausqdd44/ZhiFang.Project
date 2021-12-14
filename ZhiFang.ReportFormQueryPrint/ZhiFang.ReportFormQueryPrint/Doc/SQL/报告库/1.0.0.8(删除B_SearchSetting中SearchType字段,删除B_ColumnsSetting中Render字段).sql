--- 郭海祥 2018-12-6
---删除B_SearchSetting中SearchType字段，删除B_ColumnsSetting中Render字段
---修改B_SearchSetting和B_ColumnsSetting中APPType字段值
delete from B_SearchSetting where SearchType = 2
GO

ALTER TABLE B_SearchSetting DROP COLUMN SearchType
GO

update B_SearchSetting set AppType='doctor' where AppType='医生'
GO
update B_SearchSetting set AppType='lis' where AppType='检验之星'
GO
update B_SearchSetting set AppType='nurse' where AppType='护士'
GO
update B_SearchSetting set AppType='odp' where AppType='查询台'
GO
update B_SearchSetting set AppType='selfhelp' where AppType='自助打印'
GO


update B_ColumnsSetting set AppType='doctor' where AppType='医生'
GO
update B_ColumnsSetting set AppType='lis' where AppType='检验之星'
GO
update B_ColumnsSetting set AppType='nurse' where AppType='护士'
GO
update B_ColumnsSetting set AppType='odp' where AppType='查询台'
GO
update B_ColumnsSetting set AppType='selfhelp' where AppType='自助打印'
GO

ALTER TABLE B_ColumnsSetting DROP COLUMN Render
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'26', N'页面公共配置', N'allPageType', N'config', N'printCountSetting', N'100', NULL, N'int', N'2019-01-02 10:13:00.000', NULL, NULL, NULL)
GO