--2018/11/27 郭海祥  
--修改了6.6数据库，检验项目历史对比不显示检验项目名称和日期的问题  GetReportValue存储过程
--在@Check='item'中添加语句“CONVERT(varchar(100), rf.checkdate, 23)Checkdate,CONVERT(varchar(100), rf.checktime, 24)checktime,ti.cname as ItemCName”
ALTER PROCEDURE [dbo].[GetReportValue] 

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

