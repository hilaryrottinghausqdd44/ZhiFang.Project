
/****** Object:  StoredProcedure [dbo].[Wuhu_StatisticsHosptalGrade]    Script Date: 2020/12/29 10:06:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Wuhu_StatisticsHosptalGrade]
@startDateTime varchar(50), --开始时间
@endDateTime varchar(50) --结束时间
AS
BEGIN
declare @sqlstr varchar(5000);
set @sqlstr='select czdy1,clientcount,
   cast(sum(clientcount) over (partition by czdy1)*1.0  / sum(clientcount) over ()*100 as decimal) as clientpercentage
  from (
select czdy1,count(*) as clientcount from ReportFormFull  where CollectDate>=''' + @startDateTime + ''' and CollectDate<=''' + @endDateTime + ''' group by czdy1)clinetgroup';
END
	exec (@sqlstr);
    print(@sqlstr);
GO


