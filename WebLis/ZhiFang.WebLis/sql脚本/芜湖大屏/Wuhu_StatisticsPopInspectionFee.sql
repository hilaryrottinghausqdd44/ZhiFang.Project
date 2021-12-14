
/****** Object:  StoredProcedure [dbo].[Wuhu_StatisticsPopInspectionFee]    Script Date: 2020/12/29 14:23:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Wuhu_StatisticsPopInspectionFee]
@startDateTime varchar(50), --开始时间
@endDateTime varchar(50) --结束时间
AS
BEGIN
declare @sqlstr varchar(5000);
if(DATEPART(year,@startDateTime)=DATEPART(year,@endDateTime))
begin
set @sqlstr='select cast(datepart(YEAR,CollectDate) as varchar(4))+'+'''-'''+'+RIGHT('+'''00'''+'+CAST(MONTH(CollectDate) AS VARCHAR(2)),2)  as Times,
count(*) as clientcount,
sum(CONVERT(float,zdy3)) as prose ,
sum(CONVERT(float,zdy3)) /count(*) as avgparse
from ReportFormFull where CollectDate >=''' + @startDateTime + ''' and CollectDate <=''' + @endDateTime + '''
group by cast(datepart(YEAR,CollectDate) as varchar(4))+'+'''-'''+'+RIGHT('+'''00'''+'+CAST(MONTH(CollectDate) AS VARCHAR(2)),2) 
order by cast(datepart(YEAR,CollectDate) as varchar(4))+'+'''-'''+'+RIGHT('+'''00'''+'+CAST(MONTH(CollectDate) AS VARCHAR(2)),2) 
';
end
else
begin
set @sqlstr='select cast(datepart(YEAR,CollectDate) as varchar(4)) as Times,
count(*) as clientcount,
sum(CONVERT(float,zdy3)) as prose ,
sum(CONVERT(float,zdy3)) /count(*) as avgparse
from ReportFormFull where CollectDate >=''' + @startDateTime + ''' and CollectDate <=''' + @endDateTime + '''
group by cast(datepart(YEAR,CollectDate) as varchar(4))
order by cast(datepart(YEAR,CollectDate) as varchar(4)) 
';
end
END
	exec (@sqlstr);
    print(@sqlstr);
GO


