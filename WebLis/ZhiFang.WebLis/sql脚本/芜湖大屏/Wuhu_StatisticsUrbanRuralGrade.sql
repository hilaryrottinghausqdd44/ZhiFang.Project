-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Wuhu_StatisticsUrbanRuralGrade
@startDateTime varchar(50), --开始时间
@endDateTime varchar(50) --结束时间
AS
BEGIN
declare @sqlstr varchar(5000);
set @sqlstr='select czdy2,clientcount,
   cast(sum(clientcount) over (partition by czdy2)*1.0  / sum(clientcount) over ()*100 as decimal) as clientpercentage
  from (
select czdy2,count(*) as clientcount from ReportFormFull  where CollectDate>=''' + @startDateTime + ''' and CollectDate<=''' + @endDateTime + ''' group by czdy2)clinetgroup';
END
	exec (@sqlstr);
    print(@sqlstr);