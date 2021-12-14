

/****** Object:  StoredProcedure [dbo].[GetReportMicroGroupFullList]    Script Date: 01/13/2015 15:16:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetReportMicroGroupFullList]
	-- Add the parameters for the stored procedure here
@ReportFormID varchar(100) --报告单号
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from(
select COUNT(rm.ReceiveDate) cn,rm.ReceiveDate, rm.ItemNo,''MicroNo,'' DescNo,''AntiNo,ti.CName itemname ,NULL MicroName,'' DescName ,NULL AntiName,NULL Suscept,NULL SusDesc,	NULL SusQuan,'' SJ,case WHEN rf.PATNO is null then rf.zdy3 ELSE PATNO end as PATNO  From ReportMicroFull rm
LEFT OUTER JOIN  dbo.TestItem ti ON rm.ItemNo = ti.ItemNo
INNER JOIN  dbo.ReportFormFull rf ON rf.ReportFormID = rm.ReportFormID
where rm.ReportFormID=@ReportFormID 
group by rm.ReceiveDate, rf.zdy3,rf.PatNo,rm.ItemNo,ti.CName
union
select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,'' DescNo,''AntiNo,NULL itemname,mi.CName MicroName,'' DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo From ReportMicroFull rm
LEFT OUTER JOIN dbo.MicroItem mi ON rm.MicroNo = mi.MicroNo
where rm.ReportFormID=@ReportFormID AND rm.MicroNo is not null 
group by ReceiveDate,ItemNo,rm.MicroNo,mi.CName
union

select COUNT(ReceiveDate) cn,rm.ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,''AntiNo,NULL itemname,NUll MicroName,mp.CName DescName,NULL AntiName,NULL Suscept,NULL SusDesc,NULL SusQuan,NULL SJ ,NULL PatNo From ReportMicroFull rm
left outer join dbo.MicroPhrase mp on rm.DescNo = mp.DescNo
where rm.ReportFormID=@ReportFormID AND rm.DescNo is not null 
group by ReceiveDate,ItemNo,rm.MicroNo,rm.DescNo,mp.CName
union
select COUNT(ReceiveDate) cn,ReceiveDate, ItemNo,MicroNo,rm.AntiNo,rm.DescNo,NULL itemname,NULL MicroName,NULL DescName,ab.CName AntiName,CASE WHEN rm.Suscept = 'S' THEN '敏感' WHEN rm.Suscept = 'R' THEN '耐药' WHEN rm.Suscept = 'I' THEN '中介'   ELSE rm.Suscept END AS Suscept,rm.SusDesc,rm.SusQuan ,NULL SJ , NULL PatNo
     From ReportMicroFull rm LEFT OUTER JOIN dbo.AntiBiotic ab ON rm.AntiNo = ab.AntiNo 
where rm.ReportFormID=@ReportFormID AND rm.MicroNo is not null and rm.AntiNo is not null
group by  rm.ReceiveDate,ItemNo,MicroNo,rm.AntiNo,ab.CName,Suscept,rm.SusDesc,rm.SusQuan,rm.DescNo
) b order by ItemNo,DescNo,MicroNo,AntiNo
END


GO


