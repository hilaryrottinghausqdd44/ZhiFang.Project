---郭海祥 2019-7-18 
---更新试图ReportFormQueryDataSource和ReportItemQueryDataSource为分布报告试图

---ReportFormQueryDataSource
ALTER VIEW [dbo].[ReportFormQueryDataSource]
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
                   tt.CName AS TestType, tt.ShortCode AS TestTypeename, dgs.CName AS diag, clt.CNAME AS client, clt.ADDRESS, 
                   pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate, 
                   rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName, rf.GenderNo, rf.Birthday, 
                   rf.Age, rf.AgeUnitNo, rf.FolkNo, rf.DistrictNo, rf.WardNo, rf.Bed, rf.DeptNo, rf.Doctor, rf.ChargeNo, rf.Charge, rf.Collecter, 
                   rf.CollectDate, rf.CollectTime, rf.FormMemo, rf.TestDate, rf.TestTime, rf.Technician, rf.Operator, rf.OperDate, rf.OperTime, 
                   rf.CheckDate, rf.CheckTime, rf.Checker, rf.SerialNo, rf.RequestSource, rf.DiagNo, rf.PrintTimes, rf.FormComment, 
                   rf.Artificerorder, rf.Sickorder, rf.SickTypeNo, rf.Chargeflag, rf.TestDest, rf.SLable, rf.zdy1, rf.zdy2, rf.zdy3, rf.zdy4, rf.zdy5, 
                   rf.inceptdate, rf.incepttime, rf.onlinedate, rf.incepter, rf.onlinetime, rf.bmanno, rf.isReceive, rf.ReceiveMan, rf.ReceiveTime, 
                   rf.concessionNum, rf.resultstatus, rf.testaim, rf.resultprinttimes, rf.Sender2, rf.SenderTime2, rf.paritemname, rf.clientprint, 
                   rf.resultsend, rf.reportsend, rf.CountNodesFormSource, rf.commsendflag, rf.PrintDateTime, rf.PrintOper, rf.abnormityflag, 
                   rf.HISDateTime, rf.allowprint, rf.UrgentState, rf.RemoveFeesReason, rf.ZDY6, rf.ZDY7, rf.ZDY8, rf.ZDY9, rf.phoneCode, 
                   rf.IsNode, rf.PhoneNodeCount, rf.AutoNodeCount, rf.FormDesc, rf.EquipCommMemo, rf.ESampleNo, rf.EPosition, 
                   rf.ISUsePG, rf.OperMemo, rf.FromQCL, rf.ESend, rf.IsDel, rf.EModule, rf.IsRedo, rf.EAchivPosition, rf.FenzhuDatetime, 
                   rf.EQCLPosition, rf.EquipResult, rf.EResultToHIS, rf.NurseSender, rf.NurseSendTime, rf.NurseSendCarrier, 
                   rf.bGetAllResults, rf.bSendWjz, rf.CollecterID, rf.PrintDateTime1, rf.PrintOper1, rf.HisDoctorId, rf.HisDoctorPhoneCode, 
                   rf.LisDoctorNo, rf.LischeckerNo, rf.LastOperator, rf.LastOperDatetime, rf.I_Reportstatus, rf.AutoSended, rf.PatState, 
                   rf.EquipCommSend, rf.EquipSampleNo, pg.SectionType, CASE WHEN CONVERT(varchar(50), rf.clientno) IS NULL 
                   THEN '425' WHEN CONVERT(varchar(50), rf.clientno) = '' THEN '425' ELSE CONVERT(varchar(50), rf.clientno) 
                   END AS clientno, dbo.GetBarCodeItemList(rf.SerialNo, '0') AS ItemName, rf.PageCount, rf.PageName, 
                   CASE WHEN ((rf.SectionNo >= 2 AND rf.SectionNo <= 14) OR
                   rf.SectionNo = 41 OR
                   rf.SectionNo = 43) THEN rf.CheckDate ELSE rf.SenderTime2 END AS ZDY10, st.CName AS SICKTYPENAME, 
                   tt.CName AS TestTypeName, spt.CName AS SampletypeName, '' AS SecretType, '' AS InpatientNo, '' AS PatCardNo, 
                   au.CName AS AgeUnitName, dct.CName AS DistrictName, wt.CName AS WardName, dt.CName AS DeptName, 
                   dgs.CName AS DiagName, '' AS LabID, '' AS MainTesterId, '' AS ActiveFlag, '2' AS bTempReport
FROM      dbo.ReportForm AS rf LEFT OUTER JOIN
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
                   dbo.PGroup AS pg ON pg.SectionNo = rf.SectionNo
UNION ALL
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
                   tt.CName AS TestType, tt.ShortCode AS TestTypeename, dgs.CName AS diag, clt.CNAME AS client, clt.ADDRESS, 
                   pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate, 
                   rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName, rf.GenderNo, rf.Birthday, 
                   rf.Age, rf.AgeUnitNo, rf.FolkNo, rf.DistrictNo, rf.WardNo, rf.Bed, rf.DeptNo, rf.Doctor, rf.ChargeNo, rf.Charge, rf.Collecter, 
                   rf.CollectDate, rf.CollectTime, rf.FormMemo, rf.TestDate, rf.TestTime, rf.Technician, rf.Operator, rf.OperDate, rf.OperTime, 
                   rf.CheckDate, rf.CheckTime, rf.Checker, rf.SerialNo, rf.RequestSource, rf.DiagNo, rf.PrintTimes, rf.FormComment, 
                   rf.Artificerorder, rf.Sickorder, rf.SickTypeNo, rf.Chargeflag, rf.TestDest, rf.SLable, rf.zdy1, rf.zdy2, rf.zdy3, rf.zdy4, rf.zdy5, 
                   rf.inceptdate, rf.incepttime, rf.onlinedate, rf.incepter, rf.onlinetime, rf.bmanno, rf.isReceive, rf.ReceiveMan, rf.ReceiveTime, 
                   rf.concessionNum, rf.resultstatus, rf.testaim, rf.resultprinttimes, rf.Sender2, rf.SenderTime2, rf.paritemname, rf.clientprint, 
                   rf.resultsend, rf.reportsend, rf.CountNodesFormSource, rf.commsendflag, rf.PrintDateTime, rf.PrintOper, rf.abnormityflag, 
                   rf.HISDateTime, rf.allowprint, rf.UrgentState, rf.RemoveFeesReason, rf.ZDY6, rf.ZDY7, rf.ZDY8, rf.ZDY9, rf.phoneCode, 
                   rf.IsNode, rf.PhoneNodeCount, rf.AutoNodeCount, rf.FormDesc, rf.EquipCommMemo, rf.ESampleNo, rf.EPosition, 
                   rf.ISUsePG, rf.OperMemo, rf.FromQCL, rf.ESend, rf.IsDel, rf.EModule, rf.IsRedo, rf.EAchivPosition, rf.FenzhuDatetime, 
                   rf.EQCLPosition, rf.EquipResult, rf.EResultToHIS, rf.NurseSender, rf.NurseSendTime, rf.NurseSendCarrier, 
                   rf.bGetAllResults, rf.bSendWjz, rf.CollecterID, rf.PrintDateTime1, rf.PrintOper1, rf.HisDoctorId, rf.HisDoctorPhoneCode, 
                   rf.LisDoctorNo, rf.LischeckerNo, rf.LastOperator, rf.LastOperDatetime, rf.I_Reportstatus, rf.AutoSended, rf.PatState, 
                   rf.EquipCommSend, rf.EquipSampleNo, pg.SectionType, CASE WHEN CONVERT(varchar(50), rf.clientno) IS NULL 
                   THEN '425' WHEN CONVERT(varchar(50), rf.clientno) = '' THEN '425' ELSE CONVERT(varchar(50), rf.clientno) 
                   END AS clientno, dbo.GetBarCodeItemList(rf.SerialNo, '0') AS ItemName, rf.PageCount, rf.PageName, 
                   CASE WHEN ((rf.SectionNo >= 2 AND rf.SectionNo <= 14) OR
                   rf.SectionNo = 41 OR
                   rf.SectionNo = 43) THEN rf.CheckDate ELSE rf.SenderTime2 END AS ZDY10, st.CName AS SICKTYPENAME, 
                   tt.CName AS TestTypeName, spt.CName AS SampletypeName, '' AS SecretType, '' AS InpatientNo, '' AS PatCardNo, 
                   au.CName AS AgeUnitName, dct.CName AS DistrictName, wt.CName AS WardName, dt.CName AS DeptName, 
                   dgs.CName AS DiagName, '' AS LabID, '' AS MainTesterId, '' AS ActiveFlag, rf.bTempReport 
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
                   dbo.PGroup AS pg ON pg.SectionNo = rf.SectionNo
where bTempReport=1 or bTempReport=2

GO


---ReportItemQueryDataSource
ALTER VIEW [dbo].[ReportItemQueryDataSource]
AS
SELECT  CONVERT(varchar(10), ri.ReceiveDate, 21) + ';' + CONVERT(varchar(30), ri.SectionNo) + ';' + CONVERT(varchar(30), 
                   ri.TestTypeNo) + ';' + CONVERT(varchar(30), ri.SampleNo) AS FormNo,
                       (SELECT  ParaValue
                        FROM       dbo.SysPara
                        WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, ri.SectionNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportItemID, '__' + LTRIM(RTRIM(CONVERT(char, 
                   ri.SectionNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportFormID, dbo.getitemrow(ri.ReceiveDate, 
                   ri.SectionNo, ri.TestTypeNo, ri.SampleNo, t1.DispOrder) AS rowId, ri.ReceiveDate, ri.SectionNo, ri.TestTypeNo, ri.SampleNo, 
                   ri.ParItemNo, ri.ItemNo, ri.OriginalValue, ri.ReportValue, ri.OriginalDesc, ri.ReportDesc, ri.StatusNo, ri.RefRange, ri.EquipNo, 
                   ri.Modified, ri.ItemDate, ri.ItemTime, ri.IsMatch, CASE WHEN ((LTRIM(RTRIM(ISNULL(ri.ReportDesc, ''))) = '阳性') AND 
                   ((t1.CName NOT LIKE '%RH%') AND (t1.CName NOT LIKE '%血型%'))) THEN 'H' ELSE ResultStatus END AS ResultStatus, 
                   ri.HisValue, ri.HisComp, ri.isReceive, ri.SerialNoParent, ri.zdy1, ri.zdy2, ri.zdy3, ri.zdy4, ri.zdy5, ri.CountNodesItemSource, 
                   ri.Unit AS ItemUnit, ri.PlateNo, ri.PositionNo, ri.EquipCommMemo, ri.EquipCommSend, ri.EValueState, ri.EModule, 
                   ri.EPosition, ri.ESend, ri.IsRedo, ri.IsDel, ri.HisReceiveDate, ri.FromRedoNo, ri.I_Reportstatus, ri.ItemTestMemo, 
                   ri.ItemDealWith, ri.I_EResult, CASE WHEN isnull(t1.Prec, 0) = 0 THEN ISNULL(CONVERT(varchar, 
                   CAST(ReportValue AS decimal(18, 0))), '') + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 1) 
                   = 1 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 1))), '') + ISNULL(ri.ReportDesc, '') 
                   WHEN isnull(t1.Prec, 0) = 2 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') 
                   + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 0) = 3 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 
                   3))), '') + ISNULL(ri.ReportDesc, '') ELSE ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') 
                   + ISNULL(ri.ReportDesc, '') END AS ItemValue, t1.DispOrder, t1.CName AS ItemCname, t1.EName AS itemename, 
                   t2.CName AS paritemname, t1.Secretgrade, t1.ShortName, t1.ShortCode, t1.OrderNo, t1.StandardCode, t1.Cuegrade, 
                   t1.DiagMethod, '' AS EquipName, t1.Prec, t1.ItemDesc, t1.Visible, t1.EName AS TESTITEMSNAME, 
                   t1.CName AS TESTITEMNAME
FROM      dbo.ReportItem AS ri LEFT OUTER JOIN
                   dbo.TestItem AS t1 ON t1.ItemNo = ri.ItemNo LEFT OUTER JOIN
                   dbo.TestItem AS t2 ON t2.ItemNo = ri.ParItemNo
union all
SELECT  CONVERT(varchar(10), ri.ReceiveDate, 21) + ';' + CONVERT(varchar(30), ri.SectionNo) + ';' + CONVERT(varchar(30), 
                   ri.TestTypeNo) + ';' + CONVERT(varchar(30), ri.SampleNo) AS FormNo,
                       (SELECT  ParaValue
                        FROM       dbo.SysPara
                        WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, ri.SectionNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportItemID, '__' + LTRIM(RTRIM(CONVERT(char, 
                   ri.SectionNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportFormID, dbo.getitemrow(ri.ReceiveDate, 
                   ri.SectionNo, ri.TestTypeNo, ri.SampleNo, t1.DispOrder) AS rowId, ri.ReceiveDate, ri.SectionNo, ri.TestTypeNo, ri.SampleNo, 
                   ri.ParItemNo, ri.ItemNo, ri.OriginalValue, ri.ReportValue, ri.OriginalDesc, ri.ReportDesc, ri.StatusNo, ri.RefRange, ri.EquipNo, 
                   ri.Modified, ri.ItemDate, ri.ItemTime, ri.IsMatch, CASE WHEN ((LTRIM(RTRIM(ISNULL(ri.ReportDesc, ''))) = '阳性') AND 
                   ((t1.CName NOT LIKE '%RH%') AND (t1.CName NOT LIKE '%血型%'))) THEN 'H' ELSE ResultStatus END AS ResultStatus, 
                   ri.HisValue, ri.HisComp, ri.isReceive, ri.SerialNoParent, ri.zdy1, ri.zdy2, ri.zdy3, ri.zdy4, ri.zdy5, ri.CountNodesItemSource, 
                   ri.Unit AS ItemUnit, ri.PlateNo, ri.PositionNo, ri.EquipCommMemo, ri.EquipCommSend, ri.EValueState, ri.EModule, 
                   ri.EPosition, ri.ESend, ri.IsRedo, ri.IsDel, ri.HisReceiveDate, ri.FromRedoNo, ri.I_Reportstatus, ri.ItemTestMemo, 
                   ri.ItemDealWith, ri.I_EResult, CASE WHEN isnull(t1.Prec, 0) = 0 THEN ISNULL(CONVERT(varchar, 
                   CAST(ReportValue AS decimal(18, 0))), '') + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 1) 
                   = 1 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 1))), '') + ISNULL(ri.ReportDesc, '') 
                   WHEN isnull(t1.Prec, 0) = 2 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') 
                   + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 0) = 3 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 
                   3))), '') + ISNULL(ri.ReportDesc, '') ELSE ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') 
                   + ISNULL(ri.ReportDesc, '') END AS ItemValue, t1.DispOrder, t1.CName AS ItemCname, t1.EName AS itemename, 
                   t2.CName AS paritemname, t1.Secretgrade, t1.ShortName, t1.ShortCode, t1.OrderNo, t1.StandardCode, t1.Cuegrade, 
                   t1.DiagMethod, '' AS EquipName, t1.Prec, t1.ItemDesc, t1.Visible, t1.EName AS TESTITEMSNAME, 
                   t1.CName AS TESTITEMNAME
FROM      dbo.RequestItem AS ri LEFT OUTER JOIN
                   dbo.TestItem AS t1 ON t1.ItemNo = ri.ItemNo LEFT OUTER JOIN
                   dbo.TestItem AS t2 ON t2.ItemNo = ri.ParItemNo

GO





