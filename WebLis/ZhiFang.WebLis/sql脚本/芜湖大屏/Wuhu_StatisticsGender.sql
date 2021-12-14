SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Wuhu_StatisticsGender]
@startDateTime varchar(50), --开始时间
@endDateTime varchar(50) --结束时间
AS
BEGIN
declare @sqlstr varchar(5000);
set @sqlstr='select sum(case when gendername=''男'' then 1 else 0 end)*1.0/count(*)*100 as man,
sum(case when gendername=''女'' then 1 else 0 end)*1.0 /count(*)*100 as wuman
from ReportFormFull where CollectDate>=''' + @startDateTime + ''' and CollectDate<=''' + @endDateTime + '''';
END
	exec (@sqlstr);
    print(@sqlstr);
GO