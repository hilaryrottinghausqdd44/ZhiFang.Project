--������ 2019-7-8  ����������ӡȫ������   ���¹��財����ͼ
 
INSERT INTO [dbo].[B_Parameter]  VALUES (N'73', N'��ѯ��ӡҳ������', N'������ӡ', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO


ALTER VIEW [dbo].[ReportMarrowQueryDataSource]
AS
SELECT  ReportPublicationID AS ReportFormID, ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, OrderNo, 
                   ParItemNo, ItemNo, ParitemName, ItemCname, ItemEname, BloodNum, BloodPercent, BloodDesc, MarrowNum, 
                   MarrowPercent, MarrowDesc, RefRange, EquipNo, EquipName, ResultStatus, DiagMethod
FROM      dbo.ReportMarrowFull
GO


INSERT INTO [dbo].[B_Parameter]  VALUES (N'30', N'ҳ�湫������', N'allPageType', N'config', N'IsLabSignature', N'true', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'31', N'ҳ�湫������', N'allPageType', N'config', N'IsbTempReport', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[B_Parameter]  VALUES (N'32', N'ҳ�湫������', N'allPageType', N'config', N'IsQueryRequest', N'false', NULL, N'bool', NULL, NULL, NULL, NULL)
GO


