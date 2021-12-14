
/****** Object:  StoredProcedure [dbo].[Wuhu_StatisticsInspectionData]    Script Date: 2020/12/29 10:06:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Wuhu_StatisticsInspectionData]
@startDateTime varchar(50), --开始时间
@endDateTime varchar(50) --结束时间
AS
BEGIN
declare @sqlstr varchar(5000);
set @sqlstr='select clientzdy3,clientcount,
   cast(sum(clientcount) over (partition by clientzdy3)*1.0  / sum(clientcount) over ()*100 as decimal) as clientpercentage
  from (
select clientzdy3,count(*) as clientcount from ReportFormFull  where CollectDate>=''' + @startDateTime + ''' and CollectDate<=''' + @endDateTime + ''' group by clientzdy3)clinetgroup';
END
	exec (@sqlstr);
    print(@sqlstr);
GO


