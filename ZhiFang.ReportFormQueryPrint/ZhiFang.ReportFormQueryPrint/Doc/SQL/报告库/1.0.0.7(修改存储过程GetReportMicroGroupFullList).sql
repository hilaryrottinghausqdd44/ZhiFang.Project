--- 2018-12-6 郭海祥  微生物脚本  解决ReportPublicationID=@ReportFormID对应问题
-- 增加TPF3和PYJDF7字段
ALTER PROCEDURE [dbo].[GetReportMicroGroupFullList]
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

