



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
                   ft.ShortCode AS folkename, dt.CName AS Dept, dt.CName AS DeptName, dt.ShortCode AS Deptename, dct.CName AS district, 
                   dct.ShortCode AS districtename, wt.CName AS ward, au.CName AS AgeUnit, au.ShortCode AS AgeUnitename, 
                   tt.CName AS TestType, tt.ShortCode AS TestTypeename, dgs.CName DGSCName, clt.CNAME AS client, clt.ADDRESS, 
                   pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate, 
                   rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName, rf.GenderNo, 
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
                   pg.SuperGroupNo, pg.CName AS SuperGroupName, spt.CName AS SampletypeName,null as ReportStatus
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


CREATE VIEW [dbo].[NRequestFormQueryDataSource]
AS
SELECT  PatNo, CName, Bed, DeptNo, CollectDate, CollectTime, OperDate, OperTime, DeptName, SickTypeNo, SickTypeName
FROM      (SELECT  dbo.NRequestForm.PatNo, dbo.NRequestForm.CName, dbo.NRequestForm.Bed, dbo.NRequestForm.DeptNo, 
                                      dbo.NRequestForm.CollectDate, dbo.NRequestForm.CollectTime, dbo.NRequestForm.OperDate, 
                                      dbo.NRequestForm.OperTime, dbo.Department.CName AS DeptName, dbo.NRequestForm.jztype AS SickTypeNo, 
                                      dbo.SickType.CName AS SickTypeName
                   FROM       dbo.NRequestForm INNER JOIN
                                      dbo.Department ON dbo.NRequestForm.DeptNo = dbo.Department.DeptNo INNER JOIN
                                      dbo.SickType ON dbo.NRequestForm.jztype = dbo.SickType.SickTypeNo) AS aaa

GO

