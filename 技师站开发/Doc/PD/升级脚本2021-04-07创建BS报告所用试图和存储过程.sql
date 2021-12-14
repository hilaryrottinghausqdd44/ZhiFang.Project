--guohx 2020-04-22 创建BS报告查询所用的视图与存储过程

CREATE VIEW [dbo].[TestFormQueryDataSource]
AS
SELECT  tf.TestFormID AS ReportFormID, tf.GTestDate, tf.CheckTime, tf.CName, tf.SectionID, tf.PatNo, tf.CheckTime AS CheckDate, 
                   tf.ReceiveTime AS ReceiveDate, st.CName AS SectionName, tf.GSampleTypeID
FROM      dbo.Lis_TestForm AS tf LEFT OUTER JOIN
                   dbo.LB_Section AS st ON tf.SectionID = st.SectionID

GO



CREATE VIEW [dbo].[TestItemQueryDataSource]
AS
SELECT  ti.TestItemID, ti.TestFormID AS ReportFormID, ti.ItemID AS ItemNo, it.CName AS ItemCname, ti.ReportValue AS ItemValue, 
                   ti.Unit AS ItemUnit, ti.ResultStatus
FROM      dbo.Lis_TestItem AS ti LEFT OUTER JOIN
                   dbo.LB_Item AS it ON ti.ItemID = it.ItemID

GO



CREATE PROCEDURE [dbo].[GetReportValue] 
	@PatNo varchar(50), --病历号
	@ItemNo varchar(50), --项目号
	@Check varchar(50), 
	@Where varchar(max) --where
AS
BEGIN
if @Check='item'
    begin
		exec ('select isnull(QuanValue,0) ReportValue,
						(SELECT  CONVERT(varchar(10) , Lis_TestForm.ReceiveTime, 111 ) ReceiveDate
                        FROM       dbo.Lis_TestForm
                        WHERE    (Lis_TestItem.TestFormID = Lis_TestForm.TestFormID)) AS ReceiveDate,
                       (SELECT  CONVERT(varchar(10) , Lis_TestForm.CheckTime, 111 ) CheckDate
                        FROM       dbo.Lis_TestForm
                        WHERE    (Lis_TestItem.TestFormID = Lis_TestForm.TestFormID)) AS CheckDate,
                       (SELECT  CONVERT(varchar(10) , Lis_TestForm.CheckTime, 108 )
                        FROM       dbo.Lis_TestForm
                        WHERE    (Lis_TestItem.TestFormID = Lis_TestForm.TestFormID)) AS CheckTime,
						(SELECT  CName as ItemCName
                        FROM       dbo.LB_Item
                        WHERE    (Lis_TestItem.ITEMID = LB_Item.ITEMID)) AS ItemCName,
						Lis_TestItem.ItemID	as ItemNo					
 from Lis_TestItem  
 where Lis_TestItem.TestFormID in (select TestFormID from Lis_TestForm rf where PatNo='''+@PatNo+''''+@Where+') and ITEMID='''+@ItemNo +''' order by CheckTime ')
    end
END


GO