
/****** Object:  StoredProcedure [dbo].[Wuhu_StatisticsDataAnalysis]    Script Date: 2020/12/29 17:51:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Wuhu_StatisticsDataAnalysis] 
@startDateTime varchar(50), --开始时间
@endDateTime varchar(50) --结束时间
AS
BEGIN
--就诊类型区分样本数量
declare @sicktypesqlstr varchar(5000);
set @sicktypesqlstr='select SICKTYPENAME,
COUNT(*) as sicktypecount ,
sum(count(*)) over() as allcount from (
select BARCODE,SICKTYPENAME from ReportFormFull where CollectDate >='''+@startDateTime+''' and CollectDate <='''+@endDateTime+'''  group by BARCODE, SICKTYPENAME
)d group by SICKTYPENAME
';
--检验标本总数
declare @smaplesqlstr varchar(5000);
set @smaplesqlstr='select count(*) as sampleall from (select count(*) as rc from ReportFormFull where CollectDate >='''+@startDateTime+''' and CollectDate <='''+@endDateTime+'''  group by BARCODE)d';
--就诊人次数
declare @sickpopsqlstr varchar(5000);
set @sickpopsqlstr='select count(*) as sickcount from (select count(*) as rc from ReportFormFull where CollectDate >='''+@startDateTime+''' and CollectDate <='''+@endDateTime+'''  group by receivedate,patno)d';
--检测标本数曲线图
declare @sampletusqlstr varchar(5000);
if(DATEPART(year,@startDateTime)=DATEPART(year,@endDateTime))
begin
set @sampletusqlstr='select  Times,sum(clientcount) as count from (
select cast(datepart(YEAR,CollectDate) as varchar(4))+'+'''-'''+'+RIGHT('+'''00'''+'+CAST(MONTH(CollectDate) AS VARCHAR(2)),2) as Times,
count(BARCODE) as clientcount
from ReportFormFull where CollectDate >='''+@startDateTime+''' and CollectDate <='''+@endDateTime+'''
group by cast(datepart(YEAR,CollectDate) as varchar(4))+'+'''-'''+'+RIGHT('+'''00'''+'+CAST(MONTH(CollectDate) AS VARCHAR(2)),2),BARCODE
)d group by Times
order by Times';
end
else
begin
set @sampletusqlstr='select  Times,sum(clientcount) as count from (
select cast(datepart(YEAR,CollectDate) as varchar(4)) as Times,
count(BARCODE) as clientcount
from ReportFormFull where CollectDate >='''+@startDateTime+''' and CollectDate <='''+@endDateTime+'''
group by cast(datepart(YEAR,CollectDate) as varchar(4)),BARCODE
)d group by Times
order by Times';
end
--就诊人次数曲线图
declare @popsqlstr varchar(5000);
if(DATEPART(year,@startDateTime)=DATEPART(year,@endDateTime))
begin
set @popsqlstr='select  Times,sum(clientcount) as count from (
select cast(datepart(YEAR,CollectDate) as varchar(4))+'+'''-'''+'+RIGHT('+'''00'''+'+CAST(MONTH(CollectDate) AS VARCHAR(2)),2) as Times,
count(*) as clientcount
from ReportFormFull where CollectDate >='''+@startDateTime+''' and CollectDate <='''+@endDateTime+'''
group by cast(datepart(YEAR,CollectDate) as varchar(4))+'+'''-'''+'+RIGHT('+'''00'''+'+CAST(MONTH(CollectDate) AS VARCHAR(2)),2),receivedate,patno
)d group by Times
order by Times';
end
else
begin
set @popsqlstr='select  Times,sum(clientcount) as count from (
select cast(datepart(YEAR,CollectDate) as varchar(4)) as Times,
count(*) as clientcount
from ReportFormFull where CollectDate >='''+@startDateTime+''' and CollectDate <='''+@endDateTime+'''
group by cast(datepart(YEAR,CollectDate) as varchar(4)),receivedate,patno
)d group by Times
order by Times';
end

END
	exec (@sicktypesqlstr);
    print(@sicktypesqlstr);
	exec (@smaplesqlstr);
    print(@smaplesqlstr);
	exec (@sickpopsqlstr);
    print(@sickpopsqlstr);
	exec (@sampletusqlstr);
    print(@sampletusqlstr);
	exec (@popsqlstr);
    print(@popsqlstr);
GO


