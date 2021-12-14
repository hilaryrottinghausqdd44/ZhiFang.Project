
CREATE VIEW [dbo].[ReportFormAllQueryDataSource]
AS
SELECT   CONVERT(varchar(10), rf.ReceiveDate, 21) + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), 
                rf.TestTypeNo) + ';' + CONVERT(varchar(30), rf.SampleNo) AS ReportFormID, CONVERT(varchar(10), rf.ReceiveDate, 21) 
                + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), rf.TestTypeNo) + ';' + CONVERT(varchar(30), 
                rf.SampleNo) AS FormNo, gt.CName AS GenderName, gt.ShortCode AS GenderEName, st.CName AS SickType, 
                st.ShortCode AS SickEname, spt.CName AS sampletype, spt.ShortCode AS sampletypeename, ft.CName AS folk, 
                ft.ShortCode AS folkename, dt.CName AS DeptName, dt.ShortCode AS Deptename, dct.CName AS district, 
                dct.ShortCode AS districtename, wt.CName AS ward, au.CName AS AgeUnitName, au.ShortCode AS AgeUnitename, 
                tt.CName AS TestType, tt.ShortCode AS TestTypeename, dgs.CName AS diag, clt.CNAME AS client, clt.ADDRESS, 
                pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate, 
                rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName, rf.GenderNo, rf.Birthday, 
                rf.Age, rf.AgeUnitNo, rf.FolkNo, rf.DistrictNo, rf.WardNo, rf.Bed, rf.DeptNo, rf.Doctor, rf.ChargeNo, rf.Charge, rf.Collecter, 
                rf.CollectDate, rf.CollectTime, rf.FormMemo, rf.TestDate, rf.TestTime, rf.Technician, rf.Operator, rf.OperDate, rf.OperTime, 
                rf.CheckDate, rf.CheckTime, rf.Checker, rf.SerialNo, rf.RequestSource, rf.DiagNo, rf.PrintTimes, rf.FormComment, 
                rf.Artificerorder, rf.Sickorder, rf.SickTypeNo, rf.Chargeflag, rf.TestDest, rf.SLable, rf.zdy1, rf.zdy2, rf.zdy3, rf.zdy4, rf.zdy5, 
                rf.inceptdate, rf.incepttime, rf.onlinedate, rf.incepter, rf.onlinetime, rf.bmanno, rf.isReceive, rf.ReceiveMan, 
                rf.ReceiveTime, rf.concessionNum, rf.testaim, rf.resultprinttimes, rf.Sender2, rf.SenderTime2, rf.paritemname, 
                rf.clientprint, rf.resultsend, rf.reportsend, rf.CountNodesFormSource, rf.commsendflag, rf.PrintDateTime, rf.PrintOper, 
                rf.abnormityflag, rf.HISDateTime, rf.allowprint, rf.UrgentState, rf.RemoveFeesReason, rf.ZDY6, rf.ZDY7, rf.ZDY8, rf.ZDY9, 
                rf.ZDY10, rf.phoneCode, rf.IsNode, rf.PhoneNodeCount, rf.AutoNodeCount, rf.FormDesc, rf.EquipCommMemo, 
                rf.ESampleNo, rf.EPosition, rf.ISUsePG, rf.OperMemo, rf.FromQCL, rf.ESend, rf.IsDel, rf.EModule, rf.IsRedo, 
                rf.EAchivPosition, rf.FenzhuDatetime, rf.EQCLPosition, rf.EquipResult, rf.EResultToHIS, rf.NurseSender, 
                rf.NurseSendTime, rf.NurseSendCarrier, rf.bGetAllResults, rf.bSendWjz, rf.CollecterID, rf.PrintDateTime1, rf.PrintOper1, 
                rf.HisDoctorId, rf.HisDoctorPhoneCode, rf.LisDoctorNo, rf.LischeckerNo, rf.LastOperator, rf.LastOperDatetime, 
                rf.I_Reportstatus, rf.AutoSended, rf.PatState, rf.EquipCommSend, rf.EquipSampleNo, pg.SectionType, 
                dbo.GetBarCodeItemList(rf.SerialNo, '0') AS DoctorItemName, rf.PageCount, rf.PageName, dbo.ReportItem.ParItemNo, 
                dbo.ReportItem.ItemNo, dbo.ReportItem.OriginalValue, dbo.ReportItem.ReportValue, dbo.ReportItem.OriginalDesc, 
                dbo.ReportItem.ReportDesc, dbo.ReportItem.RefRange, dbo.ReportItem.EquipNo, dbo.ReportItem.Modified, 
                dbo.ReportItem.ItemDate, dbo.ReportItem.ItemTime, dbo.ReportItem.IsMatch, dbo.ReportItem.ResultStatus, 
                dbo.ReportItem.HisValue, dbo.ReportItem.HisComp, dbo.ReportItem.SerialNoParent, 
                dbo.ReportItem.CountNodesItemSource, dbo.ReportItem.Unit, dbo.ReportItem.PlateNo, dbo.ReportItem.PositionNo, 
                dbo.ReportItem.HisReceiveDate, dbo.ReportItem.FromRedoNo, dbo.ReportItem.ItemTestMemo, 
                dbo.ReportItem.ItemDealWith, dbo.ReportItem.Mergeno, dbo.ReportItem.OldParItemNo, dbo.ReportItem.EErrorInfo, 
                dbo.ReportItem.DilutionMultiple, dbo.ReportItem.ResultStatusDesc, dbo.TestItem.CName AS ItemCname, 
                dbo.TestItem.EName, dbo.TestItem.ShortName, dbo.TestItem.ShortCode
FROM      dbo.ReportForm AS rf INNER JOIN
                dbo.ReportItem ON rf.ReceiveDate = dbo.ReportItem.ReceiveDate AND rf.SectionNo = dbo.ReportItem.SectionNo AND 
                rf.TestTypeNo = dbo.ReportItem.TestTypeNo AND rf.SampleNo = dbo.ReportItem.SampleNo INNER JOIN
                dbo.TestItem ON dbo.ReportItem.ItemNo = dbo.TestItem.ItemNo LEFT OUTER JOIN
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


