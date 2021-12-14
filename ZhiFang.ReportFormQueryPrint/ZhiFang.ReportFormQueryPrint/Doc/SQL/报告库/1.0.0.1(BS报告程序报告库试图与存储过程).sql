--郭海祥 创建BS报告打印程序报告库中所用的试图和存储过程
--试图：(ReportFormQueryDataSource，ReportItemQueryDataSource，ReportMarrowQueryDataSource，ReportMicroQueryDataSource)，存储过程:(GetReportValue)    
--注：如已经存在以上所列试图和存储过程则不需要执行

CREATE VIEW [dbo].[ReportFormQueryDataSource]
AS
SELECT
	ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, SectionName, TestTypeName, SampleTypeNo, SampletypeName, 
	SecretType, PatNo, CName, InpatientNo, PatCardNo, GenderNo, GenderName, Age, AgeUnitNo, AgeUnitName, Birthday, DistrictNo, DistrictName, 
	WardNo, WardName, Bed, DeptNo, DeptName, Doctor, SerialNo, ParitemName, Collecter, CollectDate, CollectTime, Incepter, InceptDate, InceptTime, 
	Technician, TestDate, TestTime, Operator, OperDate, OperTime, Checker, CheckDate, CheckTime, FormComment, FormMemo, SickTypeNo, 
	SickTypeName, DiagNo, DiagName, ClientNo, ClientName, Sender2, PrintTimes, ClientPrint, PrintOper, PrintDateTime, PrintOper1, PrintDateTime1, 
	resultsend, reportsend, PageName, PageCount, ZDY1, ZDY2, ZDY3, ZDY4, ZDY5, ZDY6, ZDY7, ZDY8, ReportFormID AS formno, 
	SecretType AS SectionType, LabID, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp, MainTesterId, PatientID, ExaminerId, 
	CollectPart, ReportPublicationID AS ReportFormID, ActiveFlag, DoctorItemName AS ItemName
FROM dbo.ReportFormFull
WHERE (ActiveFlag = 1)


GO



CREATE VIEW [dbo].[ReportItemQueryDataSource]
AS
SELECT     
	ReportFormID AS RFID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, OrderNo, ParItemNo, ItemNo, ParitemName, ItemCname, ItemEname, 
	ReportValue, ReportDesc, ItemValue, ItemUnit, ResultStatus, RefRange, EquipNo, EquipName, DiagMethod, Prec, StandardCode, ItemDesc, 
	SecretGrade, Visible, ZDY1, zdy2, zdy3, ItemUnit AS UNIT, ItemEname AS TESTITEMSNAME, ItemCname AS TESTITEMNAME, 
	ReportPublicationID AS ReportFormID, LabID, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp
FROM dbo.ReportItemFull


GO


CREATE VIEW [dbo].[ReportMarrowQueryDataSource]
AS
SELECT 
	ReportFormID, ReceiveDate, SectionNo, TestTypeNo, SampleNo, OrderNo, ParItemNo, 
	ItemNo, ParitemName, ItemCname, ItemEname, BloodNum, BloodPercent, BloodDesc, 
	MarrowNum, MarrowPercent, MarrowDesc, RefRange, EquipNo, EquipName, ResultStatus, 
	DiagMethod
FROM dbo.ReportMarrowFull

GO

CREATE VIEW [dbo].[ReportMicroQueryDataSource]
AS
SELECT     LabID, ReportPublicationID, ReportFormID AS RFID, OrderNo, ReceiveDate, SectionNo, TestTypeNo, SampleNo, ItemNo, ItemCname, ItemEname, DescNo, 
                      TPF3 AS DescName, MicroStepID, MicroStepName, ResultID, ReportValue, MicroNo, MicroName, MicroEame, MicroDesc, MicroResultDesc, ItemDesc, AntiNo, 
                      AntiName, AntiEName, Suscept, SusQuan, SusDesc, AntiUnit, RefRange, ResultState, EquipNo, EquipName, AntiGroupType, MethodName, SerumcenTration, 
                      EmictioncenTration, Microdisplayid, Antidisplayid, CheckType, DataAddTime, DataUpdateTime, DataMigrationTime, DataTimeStamp, 
                      ReportPublicationID AS ReportFormID, DSTType, PYJDF7, ResistancePhenotypeName AS RPName
FROM         dbo.ReportMicroFull

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[21] 4[40] 2[20] 3) )"
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
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ReportMicroFull"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 207
               Right = 254
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
         Column = 1905
         Alias = 2220
         Table = 2445
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportMicroQueryDataSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportMicroQueryDataSource'
GO



CREATE PROCEDURE [dbo].[GetReportValue] 
	@PatNo varchar(50), --病历号
	@ItemNo varchar(50), --项目号
	@Check varchar(50), --SectionType
	@Where varchar(max) --where
	--ReportFormFull rf
	--ReportItemFull ri
	--ReportMicroFull rm
	--ReportMarrowFull rmm
AS
BEGIN
if @Check='item'
    begin
		--exec ('select isnull(ReportValue,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate from dbo.ReportItemFull ri INNER JOIN
		--dbo.ReportFormFull rf ON ri.ReportFormID = rf.ReportFormID 
		--where -- ReportItem.FormNo=@FormNo
		--rf.PatNo='''+@PatNo+''' and ri.ITEMNO = '''+@ItemNo +'''' +@Where)
		--rf.PatNo=@PatNo and ri.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate

		exec ('select isnull(ReportValue,0) ReportValue,CONVERT(varchar(10) , ReportItemFull.ReceiveDate, 111 ) ReceiveDate,
                       (SELECT  CONVERT(varchar(10) , ReportFormFull.CheckDate, 111 )
                        FROM       dbo.ReportFormFull
                        WHERE    (ReportItemFull.ReportPublicationID = ReportFormFull.ReportPublicationID)) AS CheckDate,
                       (SELECT  CONVERT(varchar(10) , ReportFormFull.CheckTime, 108 )
                        FROM       dbo.ReportFormFull
                        WHERE    (ReportItemFull.ReportPublicationID = ReportFormFull.ReportPublicationID)) AS CheckTime,
						ItemCName
 from ReportItemFull  
 where ReportItemFull.ReportPublicationID in (select ReportPublicationID from ReportFormFull rf where PatNo='''+@PatNo+''''+@Where+') and ITEMNO='''+@ItemNo +''' order by CheckDate,CheckTime ')
    end
 else if @Check='micro'
    begin
	    print ('select isnull(SusQuan,0) as ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate from dbo.ReportMicro rm INNER JOIN
		dbo.ReportForm rf ON rm.ReceiveDate = rf.ReceiveDate AND rm.SectionNo = rf.SectionNo AND 
		rm.TestTypeNo = rf.TestTypeNo AND rm.SampleNo = rf.SampleNo
		where --ReportItem.FormNo=@FormNo
		rf.PatNo ='''+@PatNo+''' and rm.ITEMNO ='''+ @ItemNo +'''' + @Where)
		--rf.PatNo=@PatNo and rm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate
    end
else if @Check='marrow'
	begin
	    exec ('select isnull(BloodPercent,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate from dbo.ReportMarrow rmm INNER JOIN
        dbo.ReportForm rf ON rmm.ReceiveDate = rf.ReceiveDate AND rmm.SectionNo = rf.SectionNo AND 
        rmm.TestTypeNo = rf.TestTypeNo AND rmm.SampleNo = rf.SampleNo
        where --ReportItem.FormNo=@FormNo
        rf.PatNo ='''+ @PatNo+''' and rmm.ITEMNO = '''+@ItemNo +'''' + @Where)
		--rf.PatNo=@PatNo and rmm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate
	end
END

GO


CREATE PROCEDURE [dbo].[GetReportMicroGroupFullList]
-- Add the parameters for the stored procedure here
@ReportFormID varchar(50) --报告单ID
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
-- Insert statements for procedure here
select * from(
select COUNT(rm.ReceiveDate) cn,rm.ReceiveDate, rm.ItemNo,''MicroNo,''MicroDesc,'' DescNo,
''AntiNo,rm.ItemCname itemname ,NULL MicroName,'' DescName ,NULL AntiName,NULL Suscept,NULL SusDesc,
NULL SusQuan,replace(CONVERT(varchar(12) , rm.ReceiveDate, 111 ),'/','-') SJ,rm.TPF3,rm.PYJDF7
  From ReportMicroFull rm
INNER JOIN  dbo.ReportFormFull rf ON rf.ReportFormID = rm.ReportFormID 
where rm.ReportPublicationID=@ReportFormID and ItemCname is not null and ItemCname <>''
group by rm.ReceiveDate,rm.ItemNo,rm.ItemCname,rm.TPF3,rm.PYJDF7
union
select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,rm.MicroDesc,'' DescNo,''AntiNo,NULL itemname,rm.MicroName MicroName,'' DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ,NULL TPF3,NULL PYJDF7  From ReportMicroFull rm
where rm.ReportPublicationID=@ReportFormID AND rm.MicroNo is not null 
group by ReceiveDate,ItemNo,rm.MicroNo,rm.MicroName,rm.MicroDesc
union

select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,''MicroDesc,rm.DescNo,''AntiNo,NULL itemname,NUll MicroName,rm.DescName DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ,NULL TPF3,NULL PYJDF7 From ReportMicroFull rm
where rm.ReportPublicationID=@ReportFormID AND rm.DescNo is not null and rm.DescName is not null and rm.DescName <>''
group by ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,rm.DescName
union
select COUNT(ReceiveDate) cn,ReceiveDate, ItemNo,MicroNo,''MicroDesc,rm.AntiNo,rm.DescNo,NULL itemname,NULL MicroName,NULL DescName,rm.AntiName AntiName,
CASE WHEN rm.Suscept = 'S' THEN '敏感' WHEN rm.Suscept = 'R' THEN '耐药' WHEN rm.Suscept = 'I' THEN '中介'   ELSE rm.Suscept END AS Suscept,rm.SusDesc,rm.SusQuan ,NULL SJ,NULL TPF3,NULL PYJDF7
     From ReportMicroFull rm  
where rm.ReportPublicationID=@ReportFormID AND rm.MicroNo is not null and rm.AntiNo is not null
group by  rm.ReceiveDate,ItemNo,MicroNo,rm.AntiNo,rm.AntiName,Suscept,rm.SusDesc,rm.SusQuan,rm.DescNo
) b order by ItemNo desc,DescNo ,MicroNo ,AntiNo 

END


GO

