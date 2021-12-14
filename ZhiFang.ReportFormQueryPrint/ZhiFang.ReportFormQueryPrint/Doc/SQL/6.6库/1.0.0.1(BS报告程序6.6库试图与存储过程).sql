--郭海祥 创建BS报告打印程序6.6库中所用的试图和存储过程
--试图：(ReportFormQueryDataSource，ReportItemQueryDataSource，ReportMarrowQueryDataSource，ReportMicroQueryDataSource)，存储过程:(GetReportValue)    
--注：如已经存在以上所列试图和存储过程则不需要执行

CREATE VIEW [dbo].[ReportFormQueryDataSource]
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
                   rf.SectionNo = 43) THEN rf.CheckDate ELSE rf.SenderTime2 END AS ZDY10, st.CName AS SICKTYPENAME
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

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[22] 4[31] 2[30] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "rf"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "gt"
            Begin Extent = 
               Top = 150
               Left = 38
               Bottom = 290
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "st"
            Begin Extent = 
               Top = 150
               Left = 249
               Bottom = 290
               Right = 422
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "spt"
            Begin Extent = 
               Top = 294
               Left = 38
               Bottom = 434
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ft"
            Begin Extent = 
               Top = 294
               Left = 254
               Bottom = 434
               Right = 427
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dt"
            Begin Extent = 
               Top = 438
               Left = 38
               Bottom = 578
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dct"
            Begin Extent = 
               Top = 438
               Left = 249
               Bottom = 578
               Right = 422
            End
            DisplayFlags = 280
            TopColumn = 0
    ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportFormQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'     End
         Begin Table = "wt"
            Begin Extent = 
               Top = 582
               Left = 38
               Bottom = 722
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "au"
            Begin Extent = 
               Top = 582
               Left = 249
               Bottom = 722
               Right = 422
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tt"
            Begin Extent = 
               Top = 726
               Left = 38
               Bottom = 866
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dgs"
            Begin Extent = 
               Top = 726
               Left = 249
               Bottom = 866
               Right = 422
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "clt"
            Begin Extent = 
               Top = 870
               Left = 38
               Bottom = 1010
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pg"
            Begin Extent = 
               Top = 870
               Left = 246
               Bottom = 1010
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportFormQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportFormQueryDataSource'
GO




CREATE VIEW [dbo].[ReportItemQueryDataSource]
AS
SELECT 
	CONVERT(varchar(10), ri.ReceiveDate, 21) + ';' + 
	CONVERT(varchar(30), ri.SectionNo) + ';' + 
	CONVERT(varchar(30), ri.TestTypeNo) + ';' + 
	CONVERT(varchar(30), ri.SampleNo) AS FormNo,
	(SELECT ParaValue FROM dbo.SysPara WHERE (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + 
	LTRIM(RTRIM(CONVERT(char, ri.SectionNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportItemID, 
	'__' + LTRIM(RTRIM(CONVERT(char, ri.SectionNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(char, ri.TestTypeNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(char, ri.SampleNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(varchar(20), ri.ReceiveDate, 20))) AS ReportFormID, 
	dbo.getitemrow(ri.ReceiveDate, ri.SectionNo, ri.TestTypeNo, ri.SampleNo, t1.DispOrder) AS rowId, 
	ri.ReceiveDate, ri.SectionNo, ri.TestTypeNo, ri.SampleNo, ri.ParItemNo, ri.ItemNo, ri.OriginalValue, 
	ri.ReportValue, ri.OriginalDesc, ri.ReportDesc, ri.StatusNo, ri.RefRange, ri.EquipNo, ri.Modified, 
	ri.ItemDate, ri.ItemTime, ri.IsMatch, 
	CASE 
		WHEN ((LTRIM(RTRIM(ISNULL(ri.ReportDesc, ''))) = '阳性') 
			AND ((t1.CName NOT LIKE '%RH%') AND (t1.CName NOT LIKE '%血型%'))) 
			THEN 'H' 
		ELSE ResultStatus 
	END AS ResultStatus, 
	ri.HisValue, ri.HisComp, ri.isReceive, ri.SerialNoParent, ri.zdy1, ri.zdy2, ri.zdy3, ri.zdy4, ri.zdy5, 
	ri.CountNodesItemSource, ri.Unit AS ItemUnit, ri.PlateNo, ri.PositionNo, ri.EquipCommMemo, 
	ri.EquipCommSend, ri.EValueState, ri.EModule, ri.EPosition, ri.ESend, ri.IsRedo, ri.IsDel, 
	ri.HisReceiveDate, ri.FromRedoNo, ri.I_Reportstatus, ri.ItemTestMemo, ri.ItemDealWith, ri.I_EResult, 
	CASE 
		WHEN isnull(t1.Prec, 0) = 0 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 0))), '') + ISNULL(ri.ReportDesc, '') 
		WHEN isnull(t1.Prec, 1) = 1 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 1))), '') + ISNULL(ri.ReportDesc, '') 
		WHEN isnull(t1.Prec, 0) = 2 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') + ISNULL(ri.ReportDesc, '') 
		WHEN isnull(t1.Prec, 0) = 3 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 3))), '') + ISNULL(ri.ReportDesc, '') 
		ELSE ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') + ISNULL(ri.ReportDesc, '') 
	END AS ItemValue, 
	t1.DispOrder, t1.CName AS ItemCname, t1.EName AS itemename, t2.CName AS paritemname, t1.Secretgrade, 
	t1.ShortName, t1.ShortCode, t1.OrderNo, t1.StandardCode, t1.Cuegrade, t1.DiagMethod
FROM dbo.ReportItem AS ri 
LEFT OUTER JOIN
	dbo.TestItem AS t1 ON t1.ItemNo = ri.ItemNo 
LEFT OUTER JOIN
	dbo.TestItem AS t2 ON t2.ItemNo = ri.ParItemNo

GO






CREATE VIEW [dbo].[ReportMarrowQueryDataSource]
AS
SELECT 
	CONVERT(varchar(10), dbo.ReportForm.ReceiveDate, 21) + ';' + 
	CONVERT(varchar(30), dbo.ReportForm.SectionNo) + ';' + 
	CONVERT(varchar(30), dbo.ReportForm.TestTypeNo) + ';' + 
	CONVERT(varchar(30), dbo.ReportForm.SampleNo) AS FormNo, 
	dbo.ReportMarrow.ReceiveDate, dbo.ReportMarrow.SectionNo, dbo.ReportMarrow.TestTypeNo, 
	dbo.ReportMarrow.SampleNo, dbo.ReportMarrow.ParItemNo, dbo.ReportMarrow.ItemNo, dbo.ReportMarrow.BloodNum, 
	dbo.ReportMarrow.BloodPercent, dbo.ReportMarrow.MarrowNum, dbo.ReportMarrow.MarrowPercent, 
	dbo.ReportMarrow.BloodDesc, dbo.ReportMarrow.MarrowDesc, dbo.ReportMarrow.StatusNo, 
	dbo.ReportMarrow.RefRange, dbo.ReportMarrow.EquipNo, dbo.ReportMarrow.IsCale, dbo.ReportMarrow.Modified, 
	'__' + LTRIM(RTRIM(CONVERT(char, ReportMarrow.SectionNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(char, ReportMarrow.TestTypeNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(char, ReportMarrow.SampleNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(varchar(20), ReportMarrow.ReceiveDate, 20))) AS ReportFormID, 
	dbo.ReportMarrow.ItemDate, dbo.ReportMarrow.ItemTime, dbo.ReportMarrow.IsMatch, 
	dbo.ReportMarrow.ResultStatus, dbo.TestItem.CName AS ItemCName, dbo.TestItem.EName AS ItemEName, 
	TestItem_1.CName AS ParItemCName, TestItem_1.EName AS ParItemEName, dbo.S_RequestVItem.ParItemNo AS Expr1, 
	dbo.S_RequestVItem.ItemNo AS Expr2, dbo.S_RequestVItem.OrgValue, dbo.S_RequestVItem.ReportValue, 
	dbo.S_RequestVItem.OrgDesc, dbo.S_RequestVItem.ReportDesc, dbo.S_RequestVItem.ReportText, 
	dbo.S_RequestVItem.ReportImage, dbo.S_RequestVItem.RefRange AS Expr3, dbo.S_RequestVItem.EquipNo AS Expr4, 
	dbo.S_RequestVItem.Modified AS Expr5, dbo.S_RequestVItem.ItemDate AS Expr6, 
	dbo.S_RequestVItem.ItemTime AS Expr7, dbo.S_RequestVItem.ResultStatus AS Expr8, dbo.S_RequestVItem.IsPrint, 
	dbo.S_RequestVItem.PrintOrder, dbo.S_RequestVItem_C.CItemNo,
    (SELECT CName FROM dbo.Equipment WHERE (EquipNo = dbo.ReportMarrow.EquipNo)) AS EquipName
FROM dbo.ReportForm 
INNER JOIN
	dbo.ReportMarrow ON dbo.ReportForm.ReceiveDate = dbo.ReportMarrow.ReceiveDate AND 
		dbo.ReportForm.SectionNo = dbo.ReportMarrow.SectionNo AND 
		dbo.ReportForm.TestTypeNo = dbo.ReportMarrow.TestTypeNo AND 
		dbo.ReportForm.SampleNo = dbo.ReportMarrow.SampleNo 
INNER JOIN
	dbo.TestItem ON dbo.ReportMarrow.ItemNo = dbo.TestItem.ItemNo 
INNER JOIN
	dbo.TestItem AS TestItem_1 ON dbo.ReportMarrow.ParItemNo = TestItem_1.ItemNo 
LEFT OUTER JOIN
	dbo.S_RequestVItem ON dbo.ReportMarrow.ReceiveDate = dbo.S_RequestVItem.ReceiveDate AND 
		dbo.ReportMarrow.SectionNo = dbo.S_RequestVItem.SectionNo AND 
		dbo.ReportMarrow.TestTypeNo = dbo.S_RequestVItem.TestTypeNo AND 
		dbo.ReportMarrow.SampleNo = dbo.S_RequestVItem.SampleNo 
LEFT OUTER JOIN
	dbo.S_RequestVItem_C ON dbo.ReportMarrow.ReceiveDate = dbo.S_RequestVItem_C.ReceiveDate AND 
		dbo.ReportMarrow.SectionNo = dbo.S_RequestVItem_C.SectionNo AND 
		dbo.ReportMarrow.TestTypeNo = dbo.S_RequestVItem_C.TestTypeNo AND 
		dbo.ReportMarrow.SampleNo = dbo.S_RequestVItem_C.SampleNo

GO




CREATE VIEW [dbo].[ReportMicroQueryDataSource]
AS
SELECT 
	CONVERT(varchar(10), rm.ReceiveDate, 21) + ';' + 
	CONVERT(varchar(30), rm.SectionNo) + ';' + 
	CONVERT(varchar(30), rm.TestTypeNo) + ';' + 
	CONVERT(varchar(30), rm.SampleNo) AS ReportFormID, 
	CONVERT(varchar(10), rm.ReceiveDate, 21) + ';' + 
	CONVERT(varchar(30), rm.SectionNo) + ';' + 
	CONVERT(varchar(30), rm.TestTypeNo) + ';' + 
	CONVERT(varchar(30), rm.SampleNo) AS FormNo,
	(SELECT ParaValue FROM dbo.SysPara WHERE (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + 
	LTRIM(RTRIM(CONVERT(char, rm.SectionNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(char, rm.TestTypeNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(char, rm.SampleNo))) + '_' + 
	LTRIM(RTRIM(CONVERT(varchar(20), rm.ReceiveDate, 20))) AS Expr1, 
	rm.ReceiveDate, rm.SectionNo, rm.TestTypeNo, rm.SampleNo, rm.ResultNo, rm.ItemNo, rm.DescNo, 
	rm.MicroNo, rm.MicroDesc, rm.AntiNo, rm.Suscept, rm.SusQuan, rm.SusDesc, rm.RefRange, 
	rm.ItemDate, rm.ItemTime, rm.ItemDesc, rm.EquipNo, rm.Modified, rm.IsMatch, rm.CheckType, 
	rm.isReceive, rm.serialnoparent, rm.Zdy1, rm.Zdy2, rm.Zdy3, rm.Zdy4, rm.Zdy5, rm.AntiDesc, 
	rm.I_EResult, rm.AntiGroupType, rm.ExpertDesc, rm.ResultState, rm.MicroResultDesc, 
	t1.CName AS itemcname, t1.EName AS itemename, mci.CName AS Microcname, mci.EName AS Microename, 
	mip.CName AS DescName, ab.CName AS AntiName, ab.EName AS Antiename, ab.ShortName AS Antishortname, 
	ab.ShortCode AS antishortcode, ab.AntiUnit, mci.MicroDesc AS microcountdesc
FROM dbo.ReportMicro AS rm 
LEFT OUTER JOIN
	dbo.TestItem AS t1 ON t1.ItemNo = rm.ItemNo 
LEFT OUTER JOIN
	dbo.MicroItem AS mci ON mci.MicroNo = rm.MicroNo 
LEFT OUTER JOIN
	dbo.MicroPhrase AS mip ON mip.DescNo = rm.DescNo 
LEFT OUTER JOIN
	dbo.AntiBiotic AS ab ON ab.AntiNo = rm.AntiNo

GO








CREATE PROCEDURE [dbo].[GetReportValue] 

@PatNo varchar(50), --病历号
@ItemNo varchar(50), --项目号
@Check varchar(50), --SectionType
@Where varchar(max) --where
--ReportForm rf
--ReportItem ri
--ReportMicro rm
--ReportMarrow rmm
--testitem     ti

AS
BEGIN
if @Check='item'
    begin
		exec 
('select isnull(ReportValue,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate,CONVERT(varchar(100), rf.checkdate, 23) Checkdate,CONVERT(varchar(100), rf.checktime, 24) checktime,ti.cname as ItemCName
from dbo.ReportItem ri INNER JOIN
		dbo.ReportForm rf ON ri.ReceiveDate = rf.ReceiveDate AND ri.SectionNo = rf.SectionNo AND 
		ri.TestTypeNo = rf.TestTypeNo AND ri.SampleNo = rf.SampleNo LEFT OUTER JOIN    
         dbo.TestItem AS ti ON ti.ItemNo = ri.ItemNo
		where -- ReportItem.FormNo=@FormNo
		rf.PatNo='''+@PatNo+''' and ri.ITEMNO = '''+@ItemNo +'''' +@Where)
		--rf.PatNo=@PatNo and ri.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate
	
    end
 else if @Check='micro'
    begin
	    exec ('select isnull(SusQuan,0) as ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate,CONVERT(varchar(100), rf.checkdate, 23) Checkdate,CONVERT(varchar(100), rf.checktime, 24) checktime,ti.cname as ItemCName from dbo.ReportMicro rm INNER JOIN
		dbo.ReportForm rf ON rm.ReceiveDate = rf.ReceiveDate AND rm.SectionNo = rf.SectionNo AND 
		rm.TestTypeNo = rf.TestTypeNo AND rm.SampleNo = rf.SampleNo
		LEFT OUTER JOIN    
 dbo.TestItem AS ti ON ti.ItemNo = rm.ItemNo
		where --ReportItem.FormNo=@FormNo
		rf.PatNo ='''+@PatNo+''' and rm.ITEMNO ='''+ @ItemNo +'''' + @Where)
		--rf.PatNo=@PatNo and rm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate
    end
else if @Check='marrow'
	begin
	    exec ('select isnull(BloodPercent,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate,CONVERT(varchar(100), rf.checkdate, 23) Checkdate,CONVERT(varchar(100), rf.checktime, 24) checktime,ti.cname as ItemCName from dbo.ReportMarrow rmm INNER JOIN
        dbo.ReportForm rf ON rmm.ReceiveDate = rf.ReceiveDate AND rmm.SectionNo = rf.SectionNo AND 
        rmm.TestTypeNo = rf.TestTypeNo AND rmm.SampleNo = rf.SampleNo
        LEFT OUTER JOIN    
 dbo.TestItem AS ti ON ti.ItemNo = rmm.ItemNo
        where --ReportItem.FormNo=@FormNo
        rf.PatNo ='''+ @PatNo+''' and rmm.ITEMNO = '''+@ItemNo +'''' + @Where)
		--rf.PatNo=@PatNo and rmm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate
	end
END

GO







