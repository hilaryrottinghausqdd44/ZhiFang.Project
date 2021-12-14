SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Wuhu_StatisticsAge
@startDateTime varchar(50), --开始时间
@endDateTime varchar(50) --结束时间
AS
BEGIN
declare @sqlstr varchar(5000);
set @sqlstr='select 
sum(case when ageunitname<>''岁'' or (ageunitname=''岁'' and convert(float,age)<5) then 1 else 0 end)*1.0/count(*)*100 as age5,
sum(case when ageunitname=''岁'' and convert(float,age)<=5 and convert(float,age)<=14 then 1 else 0 end)*1.0/count(*)*100 as age5_14,
sum(case when ageunitname=''岁'' and convert(float,age)<=15 and convert(float,age)<=44 then 1 else 0 end)*1.0/count(*)*100 as age15_44,
sum(case when ageunitname=''岁'' and convert(float,age)<=45 and convert(float,age)<=59 then 1 else 0 end)*1.0/count(*)*100 as age45_59,
sum(case when ageunitname=''岁'' and convert(float,age)>=60 then 1 else 0 end)*1.0/count(*)*100 as age60
from ReportFormFull where CollectDate>=''' + @startDateTime + ''' and CollectDate<=''' + @endDateTime + '''';
END
	exec (@sqlstr);
    print(@sqlstr);
GO