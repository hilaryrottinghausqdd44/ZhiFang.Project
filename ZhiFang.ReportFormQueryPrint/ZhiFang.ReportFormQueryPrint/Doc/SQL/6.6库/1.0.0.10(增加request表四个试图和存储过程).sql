---郭海祥 增加request表四个试图RequestFormQueryDataSource，RequestItemQueryDataSource，RequestMarrowQueryDataSource，RequestMicroQueryDataSource
---增加一个存储过程GetRequestMicroGroupFullList

CREATE VIEW [dbo].[RequestFormQueryDataSource]
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
                   rf.SectionNo = 43) THEN rf.CheckDate ELSE rf.SenderTime2 END AS Expr3, st.CName AS SICKTYPENAME, rf.clientno
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

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
               Top = 7
               Left = 48
               Bottom = 170
               Right = 324
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "gt"
            Begin Extent = 
               Top = 7
               Left = 372
               Bottom = 170
               Right = 572
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "st"
            Begin Extent = 
               Top = 7
               Left = 620
               Bottom = 170
               Right = 820
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "spt"
            Begin Extent = 
               Top = 7
               Left = 868
               Bottom = 170
               Right = 1075
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ft"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 248
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dt"
            Begin Extent = 
               Top = 175
               Left = 296
               Bottom = 338
               Right = 496
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dct"
            Begin Extent = 
               Top = 175
               Left = 544
               Bottom = 338
               Right = 744
            End
            DisplayFlags = 280
            TopColumn = 0
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestFormQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  End
         Begin Table = "wt"
            Begin Extent = 
               Top = 175
               Left = 792
               Bottom = 338
               Right = 992
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "au"
            Begin Extent = 
               Top = 175
               Left = 1040
               Bottom = 338
               Right = 1240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tt"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 248
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dgs"
            Begin Extent = 
               Top = 343
               Left = 296
               Bottom = 506
               Right = 496
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "clt"
            Begin Extent = 
               Top = 343
               Left = 544
               Bottom = 506
               Right = 739
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pg"
            Begin Extent = 
               Top = 343
               Left = 787
               Bottom = 506
               Right = 994
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
      Begin ColumnWidths = 10
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestFormQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestFormQueryDataSource'
GO

CREATE VIEW [dbo].[RequestItemQueryDataSource]
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
                   t1.DiagMethod
FROM      dbo.RequestItem AS ri LEFT OUTER JOIN
                   dbo.TestItem AS t1 ON t1.ItemNo = ri.ItemNo LEFT OUTER JOIN
                   dbo.TestItem AS t2 ON t2.ItemNo = ri.ParItemNo

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "t1"
            Begin Extent = 
               Top = 7
               Left = 367
               Bottom = 170
               Right = 595
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t2"
            Begin Extent = 
               Top = 7
               Left = 643
               Bottom = 170
               Right = 871
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ri"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 319
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestItemQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestItemQueryDataSource'
GO


CREATE VIEW [dbo].[RequestMarrowQueryDataSource]
AS
SELECT  CONVERT(varchar(10), dbo.RequestForm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), dbo.RequestForm.SectionNo) 
                   + ';' + CONVERT(varchar(30), dbo.RequestForm.TestTypeNo) + ';' + CONVERT(varchar(30), dbo.RequestForm.SampleNo) 
                   AS FormNo, dbo.RequestMarrow.ReceiveDate, dbo.RequestMarrow.SectionNo, dbo.RequestMarrow.TestTypeNo, 
                   dbo.RequestMarrow.SampleNo, dbo.RequestMarrow.ParItemNo, dbo.RequestMarrow.ItemNo, dbo.RequestMarrow.BloodNum, 
                   dbo.RequestMarrow.BloodPercent, dbo.RequestMarrow.MarrowNum, dbo.RequestMarrow.MarrowPercent, 
                   dbo.RequestMarrow.BloodDesc, dbo.RequestMarrow.MarrowDesc, dbo.RequestMarrow.StatusNo, 
                   dbo.RequestMarrow.RefRange, dbo.RequestMarrow.EquipNo, dbo.RequestMarrow.IsCale, dbo.RequestMarrow.Modified, 
                   '__' + LTRIM(RTRIM(CONVERT(char, dbo.RequestMarrow.SectionNo))) + '_' + LTRIM(RTRIM(CONVERT(char, 
                   dbo.RequestMarrow.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, dbo.RequestMarrow.SampleNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), dbo.RequestMarrow.ReceiveDate, 20))) AS RequestFormID, 
                   dbo.RequestMarrow.ItemDate, dbo.RequestMarrow.ItemTime, dbo.RequestMarrow.IsMatch, dbo.RequestMarrow.ResultStatus, 
                   dbo.TestItem.CName AS ItemCName, dbo.TestItem.EName AS ItemEName, TestItem_1.CName AS ParItemCName, 
                   TestItem_1.EName AS ParItemEName, dbo.S_RequestVItem.ParItemNo AS Expr1, dbo.S_RequestVItem.ItemNo AS Expr2, 
                   dbo.S_RequestVItem.OrgValue, dbo.S_RequestVItem.ReportValue, dbo.S_RequestVItem.OrgDesc, 
                   dbo.S_RequestVItem.ReportDesc, dbo.S_RequestVItem.ReportText, dbo.S_RequestVItem.ReportImage, 
                   dbo.S_RequestVItem.RefRange AS Expr3, dbo.S_RequestVItem.EquipNo AS Expr4, 
                   dbo.S_RequestVItem.Modified AS Expr5, dbo.S_RequestVItem.ItemDate AS Expr6, 
                   dbo.S_RequestVItem.ItemTime AS Expr7, dbo.S_RequestVItem.ResultStatus AS Expr8, dbo.S_RequestVItem.IsPrint, 
                   dbo.S_RequestVItem.PrintOrder, dbo.S_RequestVItem_C.CItemNo,
                       (SELECT  CName
                        FROM       dbo.Equipment
                        WHERE    (EquipNo = dbo.RequestMarrow.EquipNo)) AS EquipName
FROM      dbo.RequestForm INNER JOIN
                   dbo.RequestMarrow ON dbo.RequestForm.ReceiveDate = dbo.RequestMarrow.ReceiveDate AND 
                   dbo.RequestForm.SectionNo = dbo.RequestMarrow.SectionNo AND 
                   dbo.RequestForm.TestTypeNo = dbo.RequestMarrow.TestTypeNo AND 
                   dbo.RequestForm.SampleNo = dbo.RequestMarrow.SampleNo INNER JOIN
                   dbo.TestItem ON dbo.RequestMarrow.ItemNo = dbo.TestItem.ItemNo INNER JOIN
                   dbo.TestItem AS TestItem_1 ON dbo.RequestMarrow.ParItemNo = TestItem_1.ItemNo LEFT OUTER JOIN
                   dbo.S_RequestVItem ON dbo.RequestMarrow.ReceiveDate = dbo.S_RequestVItem.ReceiveDate AND 
                   dbo.RequestMarrow.SectionNo = dbo.S_RequestVItem.SectionNo AND 
                   dbo.RequestMarrow.TestTypeNo = dbo.S_RequestVItem.TestTypeNo AND 
                   dbo.RequestMarrow.SampleNo = dbo.S_RequestVItem.SampleNo LEFT OUTER JOIN
                   dbo.S_RequestVItem_C ON dbo.RequestMarrow.ReceiveDate = dbo.S_RequestVItem_C.ReceiveDate AND 
                   dbo.RequestMarrow.SectionNo = dbo.S_RequestVItem_C.SectionNo AND 
                   dbo.RequestMarrow.TestTypeNo = dbo.S_RequestVItem_C.TestTypeNo AND 
                   dbo.RequestMarrow.SampleNo = dbo.S_RequestVItem_C.SampleNo


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "ReportForm"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 324
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ReportMarrow"
            Begin Extent = 
               Top = 7
               Left = 372
               Bottom = 170
               Right = 580
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TestItem"
            Begin Extent = 
               Top = 7
               Left = 628
               Bottom = 170
               Right = 856
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TestItem_1"
            Begin Extent = 
               Top = 7
               Left = 904
               Bottom = 170
               Right = 1132
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "S_RequestVItem"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "S_RequestVItem_C"
            Begin Extent = 
               Top = 175
               Left = 303
               Bottom = 338
               Right = 489
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
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Colu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestMarrowQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'mn = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestMarrowQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestMarrowQueryDataSource'
GO


CREATE VIEW [dbo].[RequestMicroQueryDataSource]
AS
SELECT  CONVERT(varchar(10), rm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), rm.SectionNo) + ';' + CONVERT(varchar(30), 
                   rm.TestTypeNo) + ';' + CONVERT(varchar(30), rm.SampleNo) AS ReportFormID, CONVERT(varchar(10), rm.ReceiveDate, 21) 
                   + ';' + CONVERT(varchar(30), rm.SectionNo) + ';' + CONVERT(varchar(30), rm.TestTypeNo) + ';' + CONVERT(varchar(30), 
                   rm.SampleNo) AS FormNo,
                       (SELECT  ParaValue
                        FROM       dbo.SysPara
                        WHERE    (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, rm.SectionNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(char, rm.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, rm.SampleNo))) 
                   + '_' + LTRIM(RTRIM(CONVERT(varchar(20), rm.ReceiveDate, 20))) AS Expr1, rm.ReceiveDate, rm.SectionNo, 
                   rm.TestTypeNo, rm.SampleNo, rm.ResultNo, rm.ItemNo, rm.DescNo, rm.MicroNo, rm.MicroDesc, rm.AntiNo, rm.Suscept, 
                   rm.SusQuan, rm.SusDesc, rm.RefRange, rm.ItemDate, rm.ItemTime, rm.ItemDesc, rm.EquipNo, rm.Modified, rm.IsMatch, 
                   rm.CheckType, rm.isReceive, rm.SerialNoParent, rm.Zdy1, rm.Zdy2, rm.Zdy3, rm.Zdy4, rm.Zdy5, rm.AntiDesc, rm.I_EResult, 
                   rm.AntiGroupType, rm.ExpertDesc, rm.ResultState, rm.MicroResultDesc, t1.CName AS itemcname, 
                   t1.EName AS itemename, mci.CName AS Microcname, mci.EName AS Microename, mip.CName AS DescName, 
                   ab.CName AS AntiName, ab.EName AS Antiename, ab.ShortName AS Antishortname, ab.ShortCode AS antishortcode, 
                   ab.AntiUnit, mci.MicroDesc AS microcountdesc
FROM      dbo.RequestMicro AS rm LEFT OUTER JOIN
                   dbo.TestItem AS t1 ON t1.ItemNo = rm.ItemNo LEFT OUTER JOIN
                   dbo.MicroItem AS mci ON mci.MicroNo = rm.MicroNo LEFT OUTER JOIN
                   dbo.MicroPhrase AS mip ON mip.DescNo = rm.DescNo LEFT OUTER JOIN
                   dbo.AntiBiotic AS ab ON ab.AntiNo = rm.AntiNo

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "t1"
            Begin Extent = 
               Top = 7
               Left = 314
               Bottom = 170
               Right = 542
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "mci"
            Begin Extent = 
               Top = 7
               Left = 590
               Bottom = 170
               Right = 786
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "mip"
            Begin Extent = 
               Top = 7
               Left = 834
               Bottom = 170
               Right = 1008
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ab"
            Begin Extent = 
               Top = 7
               Left = 1056
               Bottom = 170
               Right = 1252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rm"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 266
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
End' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestMicroQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestMicroQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequestMicroQueryDataSource'
GO


CREATE PROCEDURE [dbo].[GetRequestMicroGroupFullList]
	-- Add the parameters for the stored procedure here
@ReceiveDate varchar(10), --核收时间
@SectionNo varchar(50), --小组号
@TestTypeNo varchar(50), --检验类型号
@SampleNo varchar(50) --样本号
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from(
select COUNT(rm.ReceiveDate) cn,rm.ReceiveDate, rm.ItemNo,''MicroNo,'' DescNo,''AntiNo,ti.CName itemname ,NULL MicroName,'' DescName ,NULL AntiName,NULL Suscept,NULL SusDesc,	NULL SusQuan,
replace(CONVERT(varchar(12) , rm.ReceiveDate, 111 ),'/','-') SJ,case WHEN rf.PATNO is null then rf.zdy3 ELSE PATNO end as PATNO  
--添加英文字段
,ti.EName itemename ,NULL MicroEName,'' DescEName ,NULL AntiEName

From RequestMicro rm
LEFT OUTER JOIN  dbo.TestItem ti ON rm.ItemNo = ti.ItemNo
INNER JOIN  dbo.RequestForm rf ON rf.ReceiveDate = rm.ReceiveDate AND rf.SectionNo = rm.SectionNo AND 
                      rf.TestTypeNo = rm.TestTypeNo AND rf.SampleNo = rm.SampleNo
where 
rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo
group by rm.ReceiveDate, rf.zdy3,rf.PatNo,rm.ItemNo,ti.CName,ti.EName
union
select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,'' DescNo,''AntiNo,
NULL itemname,mi.CName MicroName,'' DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo 
--添加英文字段
,NULL itemename,mi.EName MicroEName ,'' DescEName ,NULL AntiEName

From RequestMicro rm
LEFT OUTER JOIN dbo.MicroItem mi ON rm.MicroNo = mi.MicroNo
where 
rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.MicroNo is not null 
group by ReceiveDate,ItemNo,rm.MicroNo,mi.CName,mi.EName
union

select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,''AntiNo,NULL itemname,NUll MicroName,
mp.CName DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo 
--添加英文字段
,NULL itemename,NULL MicroEName ,mp.ShortCode DescEName ,NULL AntiEName

From RequestMicro rm
left outer join dbo.MicroPhrase mp on rm.DescNo = mp.DescNo
where 
rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.DescNo is not null 
group by ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,mp.CName,mp.ShortCode
union
select COUNT(ReceiveDate) cn,ReceiveDate, ItemNo,MicroNo,rm.AntiNo,rm.DescNo,NULL itemname,NULL MicroName,
NULL DescName,ab.CName AntiName,CASE WHEN rm.Suscept = 'S' THEN '敏感' WHEN rm.Suscept = 'R' THEN '耐药' WHEN rm.Suscept = 'I' THEN '中介'   ELSE rm.Suscept END AS Suscept,rm.SusDesc,rm.SusQuan ,NULL SJ , NULL PatNo
--添加英文字段
,NULL itemename,NULL MicroEName ,NULL DescEName ,AB.EName AntiEName

     From RequestMicro rm LEFT OUTER JOIN dbo.AntiBiotic ab ON rm.AntiNo = ab.AntiNo 
where 
rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.MicroNo is not null and rm.AntiNo is not null
group by  rm.ReceiveDate,ItemNo,MicroNo,rm.AntiNo,ab.CName,ab.EName,Suscept,rm.SusDesc,rm.SusQuan,rm.DescNo
) b order by ItemNo,DescNo,MicroNo,AntiNo
END



GO
