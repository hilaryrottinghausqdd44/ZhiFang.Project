---------------------------------------------------------------
--报告浏览打印程序

print '增加ReportForm表字段PageName,PageCount'
    
if not exists(select * from syscolumns where id=object_id('ReportForm') and name='PageName') 
begin
    Alter Table ReportForm add PageName varchar(10)
end

if not exists(select * from syscolumns where id=object_id('ReportForm') and name='PageCount')
 begin
	Alter Table ReportForm add PageCount varchar(10)
 end
go

print '增加RequestForm表字段PageName,PageCount'
    
if not exists(select * from syscolumns where id=object_id('RequestForm') and name='PageName') 
begin
    Alter Table RequestForm add PageName varchar(10)
end

if not exists(select * from syscolumns where id=object_id('RequestForm') and name='PageCount')
 begin
	Alter Table RequestForm add PageCount varchar(10)
 end
go

print '增加BackupRForm表字段PageName,PageCount'
    
if not exists(select * from syscolumns where id=object_id('BackupRForm') and name='PageName') 
begin
    Alter Table BackupRForm add PageName varchar(10)
end

if not exists(select * from syscolumns where id=object_id('BackupRForm') and name='PageCount')
 begin
	Alter Table BackupRForm add PageCount varchar(10)
 end
go


PRINT '创建函数getitemrow'
go

IF OBJECT_ID('getitemrow') IS NOT NULL
begin
drop FUNCTION  getitemrow
end
go
create FUNCTION [dbo].[getitemrow] (@ReceiveDate VarChar(20),@SectionNo int,@TestTypeNo int,@SampleNo VarChar(20),@displayid int)  
RETURNS int AS  
BEGIN 
  declare @dnum int
--必须保证displayid不重复
   set @dnum=(select count(*) as a from reportitem,testitem 
      where reportitem.itemno=testitem.itemno and
        ReceiveDate=@ReceiveDate 
        and SectionNo=@SectionNo
        and TestTypeNo=@TestTypeNo
        and SampleNo=@SampleNo
         and testitem.disporder<=@displayid )

  -- set @dnum=select count(*) from reportitem 
   --   where  formno=@formno and itemno>=@displayid ;
    Return @dnum
end
GO

PRINT '创建存储过程GetReportFormFullList'
go

IF OBJECT_ID('GetReportFormFullList') IS NOT NULL
begin
drop PROCEDURE  GetReportFormFullList
end
go

go
CREATE PROCEDURE [dbo].[GetReportFormFullList]
	@ReceiveDate varchar(10), --核收时间
@SectionNo int, --小组号
@TestTypeNo int, --检验类型号
@SampleNo varchar(50) --样本号
AS
BEGIN
SELECT     Convert(varchar(10), a.ReceiveDate, 21)+';'+Convert(varchar(30), a.SectionNo)+';'+Convert(varchar(30), a.TestTypeNo)+';'+Convert(varchar(30), a.SampleNo) as FormNo,
a.ReceiveDate, 
a.SectionNo, a.TestTypeNo, a.SampleNo, a.StatusNo, a.SampleTypeNo,case WHEN a.PATNO is null then a.zdy3 ELSE PATNO end as PATNO , a.CName, a.GenderNo, a.Birthday, a.Age, a.AgeUnitNo, a.FolkNo, a.DistrictNo, a.WardNo, a.Bed, a.DeptNo, a.Doctor, a.ChargeNo, a.Charge, a.Collecter, 
Convert(varchar(10), a.CollectDate, 21) as CollectDate, 
Convert(varchar(10), a.CollectTime, 24) as CollectTime, 
a.FormMemo, a.Technician, 
Convert(varchar(10), a.TestDate, 21) as TestDate , 
Convert(varchar(10), a.TestTime, 24) as TestTime, 
a.Operator, 
Convert(varchar(10), a.OperDate, 21) as OperDate, 
Convert(varchar(10), a.OperTime, 24) as OperTime, 
a.Checker, 
Convert(varchar(10), a.CheckDate, 21) as CheckDate, 
Convert(varchar(10), a.CheckTime, 24) as CheckTime, 
a.SerialNo, a.SerialNo as BarCode, a.RequestSource, a.DiagNo, a.PrintTimes, a.SickTypeNo, a.FormComment, a.zdy1, a.zdy2, a.zdy3, a.zdy4, a.zdy5, 
Convert(varchar(10), a.inceptdate, 21) as inceptdate, 
Convert(varchar(10), a.incepttime, 24) as incepttime,
a.incepter, 
Convert(varchar(10), a.onlinedate, 21) as onlinedate,
Convert(varchar(10), a.onlinetime, 24) as onlinetime,
a.bmanno , a.clientno , a.chargeflag,
a.chargeflag AS DistrictName,

 a.resultprinttimes, a.paritemname, a.clientprint, a.resultsend, a.reportsend, a.CountNodesFormSource, a.commsendflag, a.PrintDateTime, 
 a.PrintOper, a.isReceive, a.ReceiveMan, a.ReceiveTime, a.concessionNum,a.resultstatus, 
a.testaim, a.ZDY6, a.abnormityflag,    a.HISDateTime, a.allowprint, 
a.RemoveFeesReason, a.UrgentState, a.ZDY7, a.ZDY8, a.ZDY9, a.ZDY10, a.phoneCode, 
a.IsNode, a.PhoneNodeCount, a.AutoNodeCount, a.FormDesc, 
a.EquipCommMemo, a.ESampleNo, a.EPosition, a.ISUsePG, a.OperMemo, 
a.FromQCL, a.ESend, a.IsDel, a.EModule, a.IsRedo, a.Sickorder, a.SickType,
                          (SELECT     CName
                            FROM          dbo.AgeUnit
                            WHERE      (AgeUnitNo = a.AgeUnitNo)) AS AgeUnitName,
                          (SELECT     CName
                            FROM          dbo.GenderType
                            WHERE      (GenderNo = a.GenderNo)) AS GenderName,
                          (SELECT     CName
                            FROM          dbo.Department
                            WHERE      (DeptNo = a.DeptNo)) AS DeptName, a.Doctor AS DoctorName,
						  (SELECT     CName
                            FROM          dbo.Diagnosis
                            WHERE      (DiagNo = a.DiagNo)) AS DiagDescribe,
                          (SELECT     CName
                            FROM          dbo.District
                            WHERE      (DistrictNo = a.DistrictNo)) AS DistrictName,
                          (SELECT     CName
                            FROM          dbo.WardType
                            WHERE      (WardNo = a.WardNo)) AS WardName,
                          (SELECT     CName
                            FROM          dbo.FolkType
                            WHERE      (FolkNo = a.FolkNo)) AS FolkName,
                            (SELECT     CName
                            FROM          dbo.PGroup
                            WHERE      (SectionNo = a.SectionNo)) as SectionName,
                          (SELECT     CName
                            FROM          dbo.SickType
                            WHERE      (SickTypeNo = a.SickTypeNo)) AS SickTypeName,
                          (SELECT     CName
                            FROM          dbo.SampleType
                            WHERE      (SampleTypeNo = a.SampleTypeNo)) AS SampleTypeName, dbo.CLIENTELE.CNAME AS ClientName
FROM         dbo.ReportForm AS a LEFT OUTER JOIN
                      dbo.CLIENTELE ON a.clientno = dbo.CLIENTELE.ClIENTNO
where a.ReceiveDate=@ReceiveDate and a.SectionNo=@SectionNo and TestTypeNo=@TestTypeNo and SampleNo=@SampleNo
END
GO

PRINT '创建存储过程GetReportItemFullList'
go

IF OBJECT_ID('GetReportItemFullList') IS NOT NULL
begin
drop PROCEDURE  GetReportItemFullList
end
go

CREATE PROCEDURE [dbo].[GetReportItemFullList] 
	@ReceiveDate varchar(10), --核收时间
@SectionNo varchar(50), --小组号
@TestTypeNo varchar(50), --检验类型号
@SampleNo varchar(50) --样本号
AS
BEGIN
SELECT     Convert(varchar(10),dbo.ReportForm.ReceiveDate,21)+';'+Convert(varchar(30),dbo.ReportForm.SectionNo)+';'+Convert(varchar(30),dbo.ReportForm.TestTypeNo)+';'+Convert(varchar(30),dbo.ReportForm.SampleNo) as FormNo, dbo.ReportForm.ClientNo, TestItem_2.CName AS TestItemName, TestItem_2.ShortName AS TestItemSName, dbo.ReportForm.ReceiveDate, 
                      dbo.ReportForm.SectionNo, dbo.ReportForm.TestTypeNo, dbo.ReportForm.SampleNo, dbo.ReportItem.ParItemNo, dbo.ReportItem.ItemNo, 
                      dbo.ReportItem.OriginalValue, dbo.ReportItem.ReportValue, dbo.ReportItem.OriginalDesc, dbo.ReportItem.ReportDesc, dbo.ReportItem.StatusNo, dbo.ReportItem.HisValue,dbo.ReportItem.HisComp,
                      dbo.ReportItem.EquipNo, dbo.ReportItem.Modified, dbo.ReportItem.RefRange, dbo.ReportItem.ItemDate, dbo.ReportItem.ItemTime, 
                      dbo.ReportItem.IsMatch, dbo.ReportItem.ResultStatus, CONVERT(varchar(10), dbo.ReportItem.ItemDate, 120) + ' ' + CONVERT(varchar(8), 
                      dbo.ReportItem.ItemTime, 114) AS TestItemDateTime, ISNULL(dbo.ReportItem.ReportDesc, '') + ISNULL(CONVERT(VARCHAR(50), 
                      dbo.ReportItem.ReportValue), '') AS ReportValueAll, TestItem_1.CName AS ParItemName, TestItem_1.ShortName AS ParItemSName, 
                      TestItem_2.DispOrder, TestItem_2.DispOrder AS ItemOrder, TestItem_2.Unit, dbo.ReportForm.SerialNo, dbo.ReportForm.zdy1, 
                      dbo.ReportForm.zdy2 AS OldSerialNlo, dbo.ReportForm.zdy3, dbo.ReportForm.zdy5, dbo.ReportForm.zdy4, TestItem_2.OrderNo AS HisOrderNo, 
                      dbo.ReportForm.Technician, dbo.ReportForm.Checker, CONVERT(varchar(10), dbo.ReportForm.CheckDate, 120) + ' ' + CONVERT(varchar(8), 
                      dbo.ReportForm.CheckTime, 114) AS checkdatetime,replace(CONVERT(varchar(12) , ReportForm.ReceiveDate, 111 ),'/','-') SJ,case WHEN ReportForm.PATNO is null then ReportForm.zdy3 ELSE PATNO end as PATNO  
FROM         dbo.ReportItem INNER JOIN
                      dbo.ReportForm ON dbo.ReportItem.ReceiveDate = dbo.ReportForm.ReceiveDate AND dbo.ReportItem.SectionNo = dbo.ReportForm.SectionNo AND 
                      dbo.ReportItem.TestTypeNo = dbo.ReportForm.TestTypeNo AND dbo.ReportItem.SampleNo = dbo.ReportForm.SampleNo
 LEFT OUTER JOIN
                      dbo.TestItem AS TestItem_1 ON dbo.ReportItem.ParItemNo = TestItem_1.ItemNo LEFT OUTER JOIN
                      dbo.TestItem AS TestItem_2 ON dbo.ReportItem.ItemNo = TestItem_2.ItemNo
where --ReportItem.FormNo=@FormNo
ReportItem.ReceiveDate=@ReceiveDate and ReportItem.SectionNo=@SectionNo and ReportItem.TestTypeNo=@TestTypeNo and ReportItem.SampleNo=@SampleNo 
END
GO


PRINT '创建存储过程GetReportMarrowFullList'
go
IF OBJECT_ID('GetReportMarrowFullList') IS NOT NULL
begin
drop PROCEDURE  GetReportMarrowFullList
end
go

CREATE PROCEDURE [dbo].[GetReportMarrowFullList] 
	@ReceiveDate varchar(10), --核收时间
@SectionNo varchar(50), --小组号
@TestTypeNo varchar(50), --检验类型号
@SampleNo varchar(50) --样本号
AS
BEGIN
SELECT     TOP 100 PERCENT rp.ParItemNo, rp.ItemNo, rp.BloodNum, rp.BloodPercent, rp.MarrowNum, rp.MarrowPercent, rp.BloodDesc, rp.MarrowDesc, 
                      rp.StatusNo, rp.RefRange, rp.EquipNo, rp.IsCale, rp.Modified, rp.ItemDate, rp.ItemTime, rp.IsMatch, rp.ResultStatus,
                      mitem.CName AS itemcname, mitem.modalname
FROM         dbo.ReportMarrow AS rp INNER JOIN
                          (SELECT     TOP 100 PERCENT vi.ItemNo, vi.CName, md.CName AS modalname, mi.DispOrder
                            FROM          dbo.S_MarrowModal AS md INNER JOIN
                                                       (SELECT     ItemNo, ModalTypeNo, ValueBM, IsCalc, Calcformula, IsVisible, BRefValue, BRefMin, BRefMax, BRefRange, 
                                                                                DispOrder
                                                         FROM          dbo.S_MarrowItem
                                                         WHERE      (ModalTypeNo NOT IN (8, 9))) AS mi ON md.ModalTypeNo = mi.ModalTypeNo INNER JOIN
                                                   dbo.S_ValueItem AS vi ON mi.ItemNo = vi.ItemNo) AS mitem ON rp.ItemNo = mitem.ItemNo
where 
rp.ReceiveDate=@ReceiveDate and rp.SectionNo=@SectionNo and rp.TestTypeNo=@TestTypeNo and rp.SampleNo=@SampleNo 
ORDER BY mitem.DispOrder
END
GO

PRINT '创建存储过程GetReportMicroFullList'
go
IF OBJECT_ID('GetReportMicroFullList') IS NOT NULL
begin
drop PROCEDURE  GetReportMicroFullList
end
go

CREATE PROCEDURE [dbo].[GetReportMicroFullList] 
	@ReceiveDate varchar(10), --核收时间
@SectionNo varchar(50), --小组号
@TestTypeNo varchar(50), --检验类型号
@SampleNo varchar(50) --样本号
AS
BEGIN
SELECT     CONVERT(varchar(10), dbo.ReportMicro.ReceiveDate, 21) + ';' + CONVERT(varchar(30), dbo.ReportMicro.SectionNo) + ';' + CONVERT(varchar(30), 
                      dbo.ReportMicro.TestTypeNo) + ';' + CONVERT(varchar(30), dbo.ReportMicro.SampleNo) AS FormNo, dbo.ReportMicro.ReceiveDate, 
                      dbo.ReportMicro.SectionNo, dbo.ReportMicro.TestTypeNo, dbo.ReportMicro.SampleNo, dbo.ReportMicro.ResultNo, dbo.ReportMicro.ItemNo, 
                      dbo.TestItem.CName AS ItemName, dbo.ReportMicro.DescNo, dbo.MicroPhrase.CName AS DescName, dbo.ReportMicro.MicroNo, 
                      dbo.ReportMicro.MicroDesc, dbo.MicroItem.CName AS MicroName, dbo.ReportMicro.AntiNo, dbo.AntiBiotic.CName AS AntiName, 
                      CASE WHEN dbo.ReportMicro.Suscept = 'S' THEN '敏感' WHEN dbo.ReportMicro.Suscept = 'R' THEN '耐药' WHEN dbo.ReportMicro.Suscept = 'I' THEN '中介'
                       ELSE dbo.ReportMicro.Suscept END AS Suscept, dbo.ReportMicro.SusQuan, dbo.ReportMicro.RefRange, dbo.ReportMicro.SusDesc, 
                      dbo.AntiGroup.AntiUnit, dbo.ReportMicro.ItemDate, dbo.ReportMicro.ItemTime, dbo.ReportMicro.ItemDesc, dbo.ReportMicro.EquipNo, 
                      dbo.ReportMicro.Modified, dbo.ReportMicro.IsMatch, dbo.ReportMicro.CheckType, dbo.ReportForm.SerialNo, dbo.TestItem.ShortName, 
                      dbo.ReportForm.zdy4,replace(CONVERT(varchar(12) , ReportForm.ReceiveDate, 111 ),'/','-') SJ,case WHEN ReportForm.PATNO is null then ReportForm.zdy3 ELSE PATNO end as PATNO ,
                          (SELECT     CName
                            FROM          dbo.Equipment
                            WHERE      (EquipNo = dbo.ReportMicro.EquipNo)) AS EquipName
FROM         dbo.AntiGroup RIGHT OUTER JOIN
                      dbo.ReportMicro ON dbo.AntiGroup.MicroNo = dbo.ReportMicro.MicroNo AND dbo.AntiGroup.Range = dbo.ReportMicro.RefRange AND 
                      dbo.AntiGroup.AntiNo = dbo.ReportMicro.AntiNo LEFT OUTER JOIN
                      dbo.AntiBiotic ON dbo.ReportMicro.AntiNo = dbo.AntiBiotic.AntiNo LEFT OUTER JOIN
                      dbo.MicroItem ON dbo.ReportMicro.MicroNo = dbo.MicroItem.MicroNo LEFT OUTER JOIN
                      dbo.MicroPhrase ON dbo.ReportMicro.DescNo = dbo.MicroPhrase.DescNo LEFT OUTER JOIN
                      dbo.TestItem ON dbo.ReportMicro.ItemNo = dbo.TestItem.ItemNo INNER JOIN
                      dbo.ReportForm ON dbo.ReportForm.ReceiveDate = dbo.ReportMicro.ReceiveDate AND dbo.ReportForm.SectionNo = dbo.ReportMicro.SectionNo AND 
                      dbo.ReportForm.TestTypeNo = dbo.ReportMicro.TestTypeNo AND dbo.ReportForm.SampleNo = dbo.ReportMicro.SampleNo
where 
ReportMicro.ReceiveDate=@ReceiveDate and ReportMicro.SectionNo=@SectionNo and ReportMicro.TestTypeNo=@TestTypeNo and ReportMicro.SampleNo=@SampleNo
order by ReportMicro.MicroNo
END
GO

PRINT '创建存储过程GetReportMicroGroupFullList'
go
IF OBJECT_ID('GetReportMicroGroupFullList') IS NOT NULL
begin
drop PROCEDURE  GetReportMicroGroupFullList
end
go

CREATE PROCEDURE [dbo].[GetReportMicroGroupFullList]
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
select COUNT(rm.ReceiveDate) cn,rm.ReceiveDate, rm.ItemNo,''MicroNo,'' DescNo,''AntiNo,ti.CName itemname ,NULL MicroName,'' DescName ,NULL AntiName,NULL Suscept,NULL SusDesc,	NULL SusQuan,replace(CONVERT(varchar(12) , rm.ReceiveDate, 111 ),'/','-') SJ,case WHEN rf.PATNO is null then rf.zdy3 ELSE PATNO end as PATNO  From ReportMicro rm
LEFT OUTER JOIN  dbo.TestItem ti ON rm.ItemNo = ti.ItemNo
INNER JOIN  dbo.ReportForm rf ON rf.ReceiveDate = rm.ReceiveDate AND rf.SectionNo = rm.SectionNo AND 
                      rf.TestTypeNo = rm.TestTypeNo AND rf.SampleNo = rm.SampleNo
where 
rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo
group by rm.ReceiveDate, rf.zdy3,rf.PatNo,rm.ItemNo,ti.CName
union
select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,'' DescNo,''AntiNo,NULL itemname,mi.CName MicroName,'' DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo From ReportMicro rm
LEFT OUTER JOIN dbo.MicroItem mi ON rm.MicroNo = mi.MicroNo
where 
rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.MicroNo is not null 
group by ReceiveDate,ItemNo,rm.MicroNo,mi.CName
union

select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,''AntiNo,NULL itemname,NUll MicroName,mp.CName DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo From ReportMicro rm
left outer join dbo.MicroPhrase mp on rm.DescNo = mp.DescNo
where 
rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.DescNo is not null 
group by ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,mp.CName
union
select COUNT(ReceiveDate) cn,ReceiveDate, ItemNo,MicroNo,rm.AntiNo,rm.DescNo,NULL itemname,NULL MicroName,NULL DescName,ab.CName AntiName,CASE WHEN rm.Suscept = 'S' THEN '敏感' WHEN rm.Suscept = 'R' THEN '耐药' WHEN rm.Suscept = 'I' THEN '中介'   ELSE rm.Suscept END AS Suscept,rm.SusDesc,rm.SusQuan ,NULL SJ , NULL PatNo
     From ReportMicro rm LEFT OUTER JOIN dbo.AntiBiotic ab ON rm.AntiNo = ab.AntiNo 
where 
rm.ReceiveDate=@ReceiveDate and rm.SectionNo=@SectionNo and rm.TestTypeNo=@TestTypeNo and rm.SampleNo=@SampleNo AND rm.MicroNo is not null and rm.AntiNo is not null
group by  rm.ReceiveDate,ItemNo,MicroNo,rm.AntiNo,ab.CName,Suscept,rm.SusDesc,rm.SusQuan,rm.DescNo
) b order by ItemNo,DescNo,MicroNo,AntiNo
END

GO




PRINT '创建存储过程GetReportValue'
go
IF OBJECT_ID('GetReportValue') IS NOT NULL
begin
drop PROCEDURE  GetReportValue
end
go

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
		exec ('select isnull(ReportValue,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate from dbo.ReportItem ri INNER JOIN
		dbo.ReportForm rf ON ri.ReceiveDate = rf.ReceiveDate AND ri.SectionNo = rf.SectionNo AND 
		ri.TestTypeNo = rf.TestTypeNo AND ri.SampleNo = rf.SampleNo
		where -- ReportItem.FormNo=@FormNo
		rf.PatNo='''+@PatNo+''' and ri.ITEMNO = '''+@ItemNo +'''' +@Where)
		--rf.PatNo=@PatNo and ri.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate
	
    end
 else if @Check='micro'
    begin
	    exec ('select isnull(SusQuan,0) as ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate from dbo.ReportMicro rm INNER JOIN
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

PRINT '创建视图ReportFormQueryDataSource'
go

IF OBJECT_ID('ReportFormQueryDataSource') IS NOT NULL
begin
drop VIEW  ReportFormQueryDataSource
end
go

CREATE VIEW [dbo].[ReportFormQueryDataSource]
AS
SELECT     CONVERT(varchar(10), rf.ReceiveDate, 21) + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), rf.TestTypeNo) + ';' + CONVERT(varchar(30),rf.SampleNo) AS ReportFormID, 
                      CONVERT(varchar(10), rf.ReceiveDate, 21) + ';' + CONVERT(varchar(30), rf.SectionNo) + ';' + CONVERT(varchar(30), rf.TestTypeNo) + ';' + CONVERT(varchar(30), 
                      rf.SampleNo) AS FormNo, gt.CName AS Gender, gt.ShortCode AS GenderEName, st.CName AS SickType, st.ShortCode AS SickEname, spt.CName AS sampletype, 
                      spt.ShortCode AS sampletypeename, ft.CName AS folk, ft.ShortCode AS folkename, dt.CName AS Dept, dt.ShortCode AS Deptename, dct.CName AS district, 
                      dct.ShortCode AS districtename, wt.CName AS ward, au.CName AS AgeUnit, au.ShortCode AS AgeUnitename, tt.CName AS TestType, tt.ShortCode AS TestTypeename, 
                      dgs.CName AS diag, clt.CNAME AS client, clt.ADDRESS, pg.CName AS sectionname, pg.SectionDesc, pg.ShortCode AS sectionename, '' AS PSender2, rf.ReceiveDate, 
                      rf.SectionNo, rf.TestTypeNo, rf.SampleNo, rf.StatusNo, rf.SampleTypeNo, rf.PatNo, rf.CName, rf.GenderNo, rf.Birthday, rf.Age, rf.AgeUnitNo, rf.FolkNo, rf.DistrictNo, 
                      rf.WardNo, rf.Bed, rf.DeptNo, rf.Doctor, rf.ChargeNo, rf.Charge, rf.Collecter, rf.CollectDate, rf.CollectTime, rf.FormMemo, rf.TestDate, rf.TestTime, rf.Technician, 
                      rf.Operator, rf.OperDate, rf.OperTime, rf.CheckDate, rf.CheckTime, rf.Checker, rf.SerialNo, rf.RequestSource, rf.DiagNo, rf.PrintTimes, rf.FormComment, rf.Artificerorder, 
                      rf.Sickorder, rf.SickTypeNo, rf.Chargeflag, rf.TestDest, rf.SLable, rf.zdy1, rf.zdy2, rf.zdy3, rf.zdy4, rf.zdy5, rf.inceptdate, rf.incepttime, rf.onlinedate, rf.incepter, 
                      rf.onlinetime, rf.bmanno, rf.isReceive, rf.ReceiveMan, rf.ReceiveTime, rf.concessionNum, rf.resultstatus, rf.testaim, rf.resultprinttimes, rf.Sender2, rf.SenderTime2, 
                      rf.paritemname, rf.clientprint, rf.resultsend, rf.reportsend, rf.CountNodesFormSource, rf.commsendflag, rf.PrintDateTime, rf.PrintOper, rf.abnormityflag, rf.HISDateTime, 
                      rf.allowprint, rf.UrgentState, rf.RemoveFeesReason, rf.ZDY6, rf.ZDY7, rf.ZDY8, rf.ZDY9, rf.ZDY10, rf.phoneCode, rf.IsNode, rf.PhoneNodeCount, rf.AutoNodeCount, 
                      rf.FormDesc, rf.EquipCommMemo, rf.ESampleNo, rf.EPosition, rf.ISUsePG, rf.OperMemo, rf.FromQCL, rf.ESend, rf.IsDel, rf.EModule, rf.IsRedo, rf.EAchivPosition, 
                      rf.FenzhuDatetime, rf.EQCLPosition, rf.EquipResult, rf.EResultToHIS, rf.NurseSender, rf.NurseSendTime, rf.NurseSendCarrier, rf.bGetAllResults, rf.bSendWjz, 
                      rf.CollecterID, rf.PrintDateTime1, rf.PrintOper1, rf.HisDoctorId, rf.HisDoctorPhoneCode, rf.LisDoctorNo, rf.LischeckerNo, rf.LastOperator, rf.LastOperDatetime, 
                      rf.I_Reportstatus, rf.AutoSended, rf.PatState, rf.EquipCommSend, rf.EquipSampleNo, pg.SectionType, 
                      CASE WHEN CONVERT(varchar(50), rf.clientno) IS NULL THEN '425' WHEN CONVERT(varchar(50), rf.clientno) = '' THEN '425' ELSE CONVERT(varchar(50), rf.clientno) 
                      END AS clientno, dbo.GetBarCodeItemList(rf.SerialNo, '0') AS ItemName, rf.PageCount,rf.PageName
FROM         dbo.ReportForm AS rf LEFT OUTER JOIN
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

PRINT '创建视图ReportItemQueryDataSource'
go
IF OBJECT_ID('ReportItemQueryDataSource') IS NOT NULL
begin
drop VIEW  ReportItemQueryDataSource
end
go
CREATE VIEW [dbo].[ReportItemQueryDataSource]
AS
SELECT     CONVERT(varchar(10), ri.ReceiveDate, 21) + ';' + CONVERT(varchar(30), ri.SectionNo) + ';' + CONVERT(varchar(30), ri.TestTypeNo) + ';' + CONVERT(varchar(30),ri.SampleNo) AS FormNo,
                         CONVERT(varchar(10), ri.ReceiveDate, 21) + ';' + CONVERT(varchar(30), ri.SectionNo) + ';' + CONVERT(varchar(30), ri.TestTypeNo) + ';' + CONVERT(varchar(30),ri.SampleNo)  AS ReportFormID, dbo.getitemrow(ri.ReceiveDate, ri.SectionNo, ri.TestTypeNo, ri.SampleNo, 
                      t1.DispOrder) AS rowId, ri.ReceiveDate, ri.SectionNo, ri.TestTypeNo, ri.SampleNo, ri.ParItemNo, ri.ItemNo, ri.OriginalValue, ri.ReportValue, ri.OriginalDesc, 
                      ri.ReportDesc, ri.StatusNo, ri.RefRange, ri.EquipNo, ri.Modified, ri.ItemDate, ri.ItemTime, ri.IsMatch, CASE WHEN ((LTRIM(RTRIM(ISNULL(ri.ReportDesc, ''))) = '阳性') 
                      AND ((t1.CName NOT LIKE '%RH%') AND (t1.CName NOT LIKE '%血型%'))) THEN 'H' ELSE ResultStatus END AS ResultStatus, ri.HisValue, ri.HisComp, ri.isReceive, 
                      ri.SerialNoParent, ri.zdy1, ri.zdy2, ri.zdy3, ri.zdy4, ri.zdy5, ri.CountNodesItemSource, ri.Unit AS ItemUnit, ri.PlateNo, ri.PositionNo, ri.EquipCommMemo, 
                      ri.EquipCommSend, ri.EValueState, ri.EModule, ri.EPosition, ri.ESend, ri.IsRedo, ri.IsDel, ri.HisReceiveDate, ri.FromRedoNo, ri.I_Reportstatus, ri.ItemTestMemo, 
                      ri.ItemDealWith, ri.I_EResult, CASE WHEN isnull(t1.Prec, 0) = 0 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 0))), '')
                       + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 1) = 1 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 1))), '') + ISNULL(ri.ReportDesc, '') 
                      WHEN isnull(t1.Prec, 0) = 2 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 2))), '') + ISNULL(ri.ReportDesc, '') WHEN isnull(t1.Prec, 0) 
                      = 3 THEN ISNULL(CONVERT(varchar, CAST(ReportValue AS decimal(18, 3))), '') + ISNULL(ri.ReportDesc, '') ELSE ISNULL(CONVERT(varchar, 
                      CAST(ReportValue AS decimal(18, 2))), '') + ISNULL(ri.ReportDesc, '') END AS ItemValue, t1.DispOrder, t1.CName AS ItemCname, t1.EName AS itemename, 
                      t2.CName AS paritemname, t1.Secretgrade, t1.ShortName, t1.ShortCode, t1.OrderNo, t1.StandardCode, t1.Cuegrade, t1.DiagMethod
FROM         dbo.ReportItem AS ri LEFT OUTER JOIN
                      dbo.TestItem AS t1 ON t1.ItemNo = ri.ItemNo LEFT OUTER JOIN
                      dbo.TestItem AS t2 ON t2.ItemNo = ri.ParItemNo
GO

PRINT '创建视图ReportMarrowQueryDataSource'
go
IF OBJECT_ID('ReportMarrowQueryDataSource') IS NOT NULL
begin
drop VIEW  ReportMarrowQueryDataSource
end
go
CREATE VIEW [dbo].[ReportMarrowQueryDataSource]
AS
SELECT   CONVERT(varchar(10), dbo.ReportForm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), dbo.ReportForm.SectionNo) 
                + ';' + CONVERT(varchar(30), dbo.ReportForm.TestTypeNo) + ';' + CONVERT(varchar(30), dbo.ReportForm.SampleNo) 
                AS FormNo, dbo.ReportMarrow.ReceiveDate, dbo.ReportMarrow.SectionNo, dbo.ReportMarrow.TestTypeNo, 
                dbo.ReportMarrow.SampleNo, dbo.ReportMarrow.ParItemNo, dbo.ReportMarrow.ItemNo, dbo.ReportMarrow.BloodNum, 
                dbo.ReportMarrow.BloodPercent, dbo.ReportMarrow.MarrowNum, dbo.ReportMarrow.MarrowPercent, 
                dbo.ReportMarrow.BloodDesc, dbo.ReportMarrow.MarrowDesc, dbo.ReportMarrow.StatusNo, 
                dbo.ReportMarrow.RefRange, dbo.ReportMarrow.EquipNo, dbo.ReportMarrow.IsCale, dbo.ReportMarrow.Modified, 
               CONVERT(varchar(10), dbo.ReportForm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), dbo.ReportForm.SectionNo) 
                + ';' + CONVERT(varchar(30), dbo.ReportForm.TestTypeNo) + ';' + CONVERT(varchar(30), dbo.ReportForm.SampleNo)  AS ReportFormID, dbo.ReportMarrow.ItemDate, dbo.ReportMarrow.ItemTime, dbo.ReportMarrow.IsMatch, 
                dbo.ReportMarrow.ResultStatus, dbo.TestItem.CName AS ItemCName, dbo.TestItem.EName AS ItemEName, 
                TestItem_1.CName AS ParItemCName, TestItem_1.EName AS ParItemEName, dbo.S_RequestVItem.ParItemNo AS Expr1, 
                dbo.S_RequestVItem.ItemNo AS Expr2, dbo.S_RequestVItem.OrgValue, dbo.S_RequestVItem.ReportValue, 
                dbo.S_RequestVItem.OrgDesc, dbo.S_RequestVItem.ReportDesc, dbo.S_RequestVItem.ReportText, 
                dbo.S_RequestVItem.ReportImage, dbo.S_RequestVItem.RefRange AS Expr3, dbo.S_RequestVItem.EquipNo AS Expr4, 
                dbo.S_RequestVItem.Modified AS Expr5, dbo.S_RequestVItem.ItemDate AS Expr6, 
                dbo.S_RequestVItem.ItemTime AS Expr7, dbo.S_RequestVItem.ResultStatus AS Expr8, dbo.S_RequestVItem.IsPrint, 
                dbo.S_RequestVItem.PrintOrder, dbo.S_RequestVItem_C.CItemNo,
                    (SELECT   CName
                     FROM      dbo.Equipment
                     WHERE   (EquipNo = dbo.ReportMarrow.EquipNo)) AS EquipName
FROM      dbo.ReportForm INNER JOIN
                dbo.ReportMarrow ON dbo.ReportForm.ReceiveDate = dbo.ReportMarrow.ReceiveDate AND 
                dbo.ReportForm.SectionNo = dbo.ReportMarrow.SectionNo AND 
                dbo.ReportForm.TestTypeNo = dbo.ReportMarrow.TestTypeNo AND 
                dbo.ReportForm.SampleNo = dbo.ReportMarrow.SampleNo INNER JOIN
                dbo.TestItem ON dbo.ReportMarrow.ItemNo = dbo.TestItem.ItemNo INNER JOIN
                dbo.TestItem AS TestItem_1 ON dbo.ReportMarrow.ParItemNo = TestItem_1.ItemNo LEFT OUTER JOIN
                dbo.S_RequestVItem ON dbo.ReportMarrow.ReceiveDate = dbo.S_RequestVItem.ReceiveDate AND 
                dbo.ReportMarrow.SectionNo = dbo.S_RequestVItem.SectionNo AND 
                dbo.ReportMarrow.TestTypeNo = dbo.S_RequestVItem.TestTypeNo AND 
                dbo.ReportMarrow.SampleNo = dbo.S_RequestVItem.SampleNo LEFT OUTER JOIN
                dbo.S_RequestVItem_C ON dbo.ReportMarrow.ReceiveDate = dbo.S_RequestVItem_C.ReceiveDate AND 
                dbo.ReportMarrow.SectionNo = dbo.S_RequestVItem_C.SectionNo AND 
                dbo.ReportMarrow.TestTypeNo = dbo.S_RequestVItem_C.TestTypeNo AND 
                dbo.ReportMarrow.SampleNo = dbo.S_RequestVItem_C.SampleNo
GO

PRINT '创建视图ReportMicroQueryDataSource'
go
IF OBJECT_ID('ReportMicroQueryDataSource') IS NOT NULL
begin
drop VIEW  ReportMicroQueryDataSource
end
go
CREATE VIEW [dbo].[ReportMicroQueryDataSource]
AS
SELECT     CONVERT(varchar(10), rm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), rm.SectionNo) + ';' + CONVERT(varchar(30), rm.TestTypeNo) + ';' + CONVERT(varchar(30), 
                      rm.SampleNo) AS ReportFormID, CONVERT(varchar(10), rm.ReceiveDate, 21) + ';' + CONVERT(varchar(30), rm.SectionNo) + ';' + CONVERT(varchar(30), rm.TestTypeNo) 
                      + ';' + CONVERT(varchar(30), rm.SampleNo) AS FormNo,
                          (SELECT     ParaValue
                            FROM          dbo.SysPara
                            WHERE      (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + + '__' + LTRIM(RTRIM(CONVERT(char, rm.SectionNo))) + '_' + LTRIM(RTRIM(CONVERT(char, 
                      rm.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, rm.SampleNo))) + '_' + LTRIM(RTRIM(CONVERT(varchar(20), rm.ReceiveDate, 20))) AS Expr1, rm.ReceiveDate, 
                      rm.SectionNo, rm.TestTypeNo, rm.SampleNo, rm.ResultNo, rm.ItemNo, rm.DescNo, rm.MicroNo, rm.MicroDesc, rm.AntiNo, rm.Suscept, rm.SusQuan, rm.SusDesc, 
                      rm.RefRange, rm.ItemDate, rm.ItemTime, rm.ItemDesc, rm.EquipNo, rm.Modified, rm.IsMatch, rm.CheckType, rm.isReceive, rm.serialnoparent, rm.Zdy1, rm.Zdy2, 
                      rm.Zdy3, rm.Zdy4, rm.Zdy5, rm.AntiDesc, rm.I_EResult, rm.AntiGroupType, rm.ExpertDesc, rm.ResultState, rm.MicroResultDesc, t1.CName AS itemcname, 
                      t1.EName AS itemename, mci.CName AS Microcname, mci.EName AS Microename, mip.CName AS DescName, ab.CName AS AntiName, ab.EName AS Antiename, 
                      ab.ShortName AS Antishortname, ab.ShortCode AS antishortcode, ab.AntiUnit, mci.MicroDesc AS microcountdesc
FROM         dbo.ReportMicro AS rm LEFT OUTER JOIN
                      dbo.TestItem AS t1 ON t1.ItemNo = rm.ItemNo LEFT OUTER JOIN
                      dbo.MicroItem AS mci ON mci.MicroNo = rm.MicroNo LEFT OUTER JOIN
                      dbo.MicroPhrase AS mip ON mip.DescNo = rm.DescNo LEFT OUTER JOIN
                      dbo.AntiBiotic AS ab ON ab.AntiNo = rm.AntiNo
GO

print '创建触发器ReportFormQueryPrint_CleanFlag用于重审'
go
IF OBJECT_ID('ReportFormQueryPrint_CleanFlag') IS NOT NULL
begin
drop trigger  ReportFormQueryPrint_CleanFlag
end
go
create trigger  ReportFormQueryPrint_CleanFlag
on ReportForm 
for insert as
begin
declare @receivedate datetime
declare @sectionno int
declare @testtypeno int
declare @sampleno varchar(20)
select @receivedate  = receivedate from inserted
select @sectionno  = sectionno from inserted
select @testtypeno  = testtypeno from inserted
select @sampleno  = sampleno from inserted

	update ReportForm set resultsend=0 where receivedate=@receivedate 
	and SectionNo=@sectionno 
	and TestTypeNo = @testtypeno
 and SampleNo=@sampleno
end