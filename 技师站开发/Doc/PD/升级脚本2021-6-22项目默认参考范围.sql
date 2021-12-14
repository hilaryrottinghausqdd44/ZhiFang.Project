


--Ä¬ÈÏ²Î¿¼·¶Î§	DefaultRange	Ä¬ÈÏ²Î¿¼·¶Î§	nvarchar(400)	400		FALSE	FALSE	FALSE
--²Î¿¼·¶Î§ÏêÏ¸ÐÅÏ¢	RangeAllInfo	²Î¿¼·¶Î§ÏêÏ¸ÐÅÏ¢	nvarchar(1000)	1,000		FALSE	FALSE	FALSE

IF COL_LENGTH('LB_Item', 'DefaultRange') IS NULL  
  alter table LB_Item Add DefaultRange nvarchar(400)
go
execute sp_addextendedproperty 'MS_Description', 'Ä¬ÈÏ²Î¿¼·¶Î§', 'SCHEMA', 'dbo', 'table', 'LB_Item', 'column', 'DefaultRange'
go


  IF COL_LENGTH('LB_Item', 'RangeAllInfo') IS NULL  
  alter table LB_Item Add RangeAllInfo nVarchar(1000)
 go
execute sp_addextendedproperty 'MS_Description', '²Î¿¼·¶Î§ÏêÏ¸ÐÅÏ¢', 'SCHEMA', 'dbo', 'table', 'LB_Item', 'column', 'RangeAllInfo'
go

