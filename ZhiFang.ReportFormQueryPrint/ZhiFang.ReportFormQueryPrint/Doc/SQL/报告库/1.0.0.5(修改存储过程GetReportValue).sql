-- 2018/11/28 郭海祥   
--解决报告库检验项目历史对比不显示检验项目名称和显示重复检验项目的问题 GetReportValue存储过程
--在@Check='item'处增加了“,ItemCName”和“ActiveFlag=1”

ALTER PROCEDURE [dbo].[GetReportValue] 
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
		exec ('select isnull(ReportValue,0) ReportValue,CONVERT(varchar(10) , ReportItemFull.ReceiveDate, 111 ) ReceiveDate,
                       (SELECT  CONVERT(varchar(10) , ReportFormFull.CheckDate, 111 )
                        FROM       dbo.ReportFormFull
                        WHERE    (ReportItemFull.ReportPublicationID = ReportFormFull.ReportPublicationID)) AS CheckDate,
                       (SELECT  CONVERT(varchar(10) , ReportFormFull.CheckTime, 108 )
                        FROM       dbo.ReportFormFull
                        WHERE    (ReportItemFull.ReportPublicationID = ReportFormFull.ReportPublicationID)) AS CheckTime,
					    ItemCName
 from ReportItemFull  
 where ReportItemFull.ReportPublicationID in (select ReportPublicationID from ReportFormFull rf where  ActiveFlag=1 and PatNo='''+@PatNo+''''+@Where+') and ITEMNO='''+@ItemNo +'''  order by CheckDate,CheckTime ')
    end
 else if @Check='micro'
    begin
	    exec ('select isnull(SusQuan,0) as ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate,CONVERT(varchar(100), rf.checkdate, 23) Checkdate,CONVERT(varchar(100), rf.checktime, 24) checktime,rm.ItemCName from dbo.ReportMicro rm INNER JOIN
		dbo.ReportForm rf ON rm.ReceiveDate = rf.ReceiveDate AND rm.SectionNo = rf.SectionNo AND 
		rm.TestTypeNo = rf.TestTypeNo AND rm.SampleNo = rf.SampleNo
		where --ReportItem.FormNo=@FormNo
		rf.PatNo ='''+@PatNo+''' and rm.ITEMNO ='''+ @ItemNo +'''' + @Where)
		--rf.PatNo=@PatNo and rm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate
    end
else if @Check='marrow'
	begin
	    exec ('select isnull(BloodPercent,0) ReportValue,CONVERT(varchar(10) , rf.ReceiveDate, 111 ) ReceiveDate,CONVERT(varchar(100), rf.checkdate, 23) Checkdate,CONVERT(varchar(100), rf.checktime, 24) checktime,rmm.ItemCName from dbo.ReportMarrow rmm INNER JOIN
        dbo.ReportForm rf ON rmm.ReceiveDate = rf.ReceiveDate AND rmm.SectionNo = rf.SectionNo AND 
        rmm.TestTypeNo = rf.TestTypeNo AND rmm.SampleNo = rf.SampleNo
        where --ReportItem.FormNo=@FormNo
        rf.PatNo ='''+ @PatNo+''' and rmm.ITEMNO = '''+@ItemNo +'''' + @Where)
		--rf.PatNo=@PatNo and rmm.ItemNo=@ItemNo and rf.ReceiveDate between @StarRd and @EndRd order by rf.ReceiveDate
	end
END


GO

