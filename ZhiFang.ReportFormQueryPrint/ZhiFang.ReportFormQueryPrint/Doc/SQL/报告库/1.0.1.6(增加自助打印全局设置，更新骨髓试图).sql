--郭海祥 2019-7-8  增加自助打印全局设置   更新骨髓病理试图
 
INSERT INTO [dbo].[B_Parameter]  VALUES (N'73', N'查询打印页面配置', N'自助打印', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO


ALTER VIEW [dbo].[ReportMarrowQueryDataSource]
AS
SELECT  ReportPublicationID AS ReportFormID, ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, OrderNo, 
                   ParItemNo, ItemNo, ParitemName, ItemCname, ItemEname, BloodNum, BloodPercent, BloodDesc, MarrowNum, 
                   MarrowPercent, MarrowDesc, RefRange, EquipNo, EquipName, ResultStatus, DiagMethod
FROM      dbo.ReportMarrowFull
GO


INSERT INTO [dbo].[B_Parameter]  VALUES (N'30', N'页面公共配置', N'allPageType', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'31', N'页面公共配置', N'allPageType', N'config', N'IsbTempReport', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'32', N'页面公共配置', N'allPageType', N'config', N'IsQueryRequest', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO


