--郭海祥 2019-8-13 
--更新试图RequestFormQueryDataSource增加字段SuperGroupNo，SuperGroupName
--增加全局设置
--更新表B_ColumnsUnit中Sname字段值为自助打印的更新成selfhelp

ALTER VIEW [dbo].[RequestFormQueryDataSource]
AS
SELECT  (SELECT  ParaValue
                   FROM       dbo.SysPara
                   WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, rf.SectionNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(char, rf.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, rf.SampleNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), rf.ReceiveDate, 20))) AS ReportFormID, CONVERT(varchar(10), rf.ReceiveDate, 
                   21) + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), rf.TestTypeNo) + ';' + CONVERT(varchar(30), 
                   rf.SampleNo) AS FormNo, gt.CName AS Gender, gt.ShortCode AS GenderEName, st.CName AS SickType, 
                   st.ShortCode AS SickEname, spt.CName AS sampletype, spt.ShortCode AS sampletypeename, ft.CName AS folk, 
                   ft.ShortCode AS folkename, dt.CName AS Dept, dt.ShortCode AS Deptename, dct.CName AS district, 
                   dct.ShortCode AS districtename, wt.CName AS ward, au.CName AS AgeUnit, au.ShortCode AS AgeUnitename, 
                   tt.CName AS TestType, tt.ShortCode AS TestTypeename, dgs.CName, clt.CNAME AS client, clt.ADDRESS, 
                   pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate, 
                   rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName AS Expr1, rf.GenderNo, 
                   rf.Birthday, rf.Age, rf.AgeUnitNo, rf.FolkNo, rf.DistrictNo, rf.WardNo, rf.Bed, rf.DeptNo, rf.Doctor, rf.ChargeNo, rf.Charge, 
                   rf.Collecter, rf.CollectDate, rf.CollectTime, rf.FormMemo, rf.TestDate, rf.TestTime, rf.Technician, rf.Operator, rf.OperDate, 
                   rf.OperTime, rf.CheckDate, rf.CheckTime, rf.Checker, rf.SerialNo, rf.RequestSource, rf.DiagNo, rf.PrintTimes, 
                   rf.FormComment, rf.Artificerorder, rf.Sickorder, rf.SickTypeNo, rf.Chargeflag, rf.TestDest, rf.SLable, rf.zdy1, rf.zdy2, rf.zdy3, 
                   rf.zdy4, rf.zdy5, rf.inceptdate, rf.incepttime, rf.onlinedate, rf.incepter, rf.onlinetime, rf.bmanno, rf.isReceive, rf.ReceiveMan, 
                   rf.ReceiveTime, rf.concessionNum, rf.resultstatus, rf.testaim, rf.resultprinttimes, rf.Sender2, rf.SenderTime2, 
                   rf.paritemname, rf.clientprint, rf.resultsend, rf.reportsend, rf.CountNodesFormSource, rf.commsendflag, rf.PrintDateTime, 
                   rf.PrintOper, rf.abnormityflag, rf.HISDateTime, rf.allowprint, rf.UrgentState, rf.RemoveFeesReason, rf.ZDY6, rf.ZDY7, 
                   rf.ZDY8, rf.ZDY9, rf.phoneCode, rf.IsNode, rf.PhoneNodeCount, rf.AutoNodeCount, rf.FormDesc, rf.EquipCommMemo, 
                   rf.ESampleNo, rf.EPosition, rf.ISUsePG, rf.OperMemo, rf.FromQCL, rf.ESend, rf.IsDel, rf.EModule, rf.IsRedo, 
                   rf.EAchivPosition, rf.FenzhuDatetime, rf.EQCLPosition, rf.EquipResult, rf.EResultToHIS, rf.NurseSender, rf.NurseSendTime, 
                   rf.NurseSendCarrier, rf.bGetAllResults, rf.bSendWjz, rf.CollecterID, rf.PrintDateTime1, rf.PrintOper1, rf.HisDoctorId, 
                   rf.HisDoctorPhoneCode, rf.LisDoctorNo, rf.LischeckerNo, rf.LastOperator, rf.LastOperDatetime, rf.I_Reportstatus, 
                   rf.AutoSended, rf.PatState, rf.EquipCommSend, rf.EquipSampleNo, pg.SectionType, CASE WHEN CONVERT(varchar(50), 
                   rf.clientno) IS NULL THEN '425' WHEN CONVERT(varchar(50), rf.clientno) = '' THEN '425' ELSE CONVERT(varchar(50), 
                   rf.clientno) END AS Expr2, dbo.GetBarCodeItemList(rf.SerialNo, '0') AS ItemName, rf.PageCount, rf.PageName, 
                   CASE WHEN ((rf.SectionNo >= 2 AND rf.SectionNo <= 14) OR
                   rf.SectionNo = 41 OR
                   rf.SectionNo = 43) THEN rf.CheckDate ELSE rf.SenderTime2 END AS Expr3, st.CName AS SICKTYPENAME, rf.clientno, 
                   pg.SuperGroupNo, pg.CName AS SuperGroupName, spt.CName AS SampletypeName
FROM      dbo.RequestForm AS rf LEFT OUTER JOIN
                   dbo.GenderType AS gt ON rf.GenderNo = gt.GenderNo LEFT OUTER JOIN
                   dbo.SickType AS st ON st.SickTypeNo = rf.SickTypeNo LEFT OUTER JOIN
                   dbo.SampleType AS spt ON spt.SampleTypeNo = rf.SampleTypeNo LEFT OUTER JOIN
                   dbo.FolkType AS ft ON ft.FolkNo = rf.FolkNo LEFT OUTER JOIN
                   dbo.Department AS dt ON dt.DeptNo = rf.DeptNo LEFT OUTER JOIN
                   dbo.District AS dct ON dct.DistrictNo = rf.DistrictNo LEFT OUTER JOIN
                   dbo.WardType AS wt ON wt.WardNo = rf.WardNo LEFT OUTER JOIN
                   dbo.AgeUnit AS au ON au.AgeUnitNo = rf.AgeUnitNo LEFT OUTER JOIN
                   dbo.TestType AS tt ON tt.TestTypeNo = rf.TestTypeNo LEFT OUTER JOIN
                   dbo.Diagnosis AS dgs ON dgs.DiagNo = rf.DiagNo LEFT OUTER JOIN
                   dbo.CLIENTELE AS clt ON clt.ClIENTNO = rf.clientno LEFT OUTER JOIN
                   dbo.PGroup AS pg ON pg.SectionNo = rf.SectionNo LEFT OUTER JOIN
                   dbo.SuperGroup AS sg ON sg.SuperGroupNo = pg.SuperGroupNo

GO


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





