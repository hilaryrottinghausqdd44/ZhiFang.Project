


--Ĭ�ϲο���Χ	DefaultRange	Ĭ�ϲο���Χ	nvarchar(400)	400		FALSE	FALSE	FALSE
--�ο���Χ��ϸ��Ϣ	RangeAllInfo	�ο���Χ��ϸ��Ϣ	nvarchar(1000)	1,000		FALSE	FALSE	FALSE

IF COL_LENGTH('LB_Item', 'DefaultRange') IS NULL  
  alter table LB_Item Add DefaultRange nvarchar(400)
go
execute sp_addextendedproperty 'MS_Description', 'Ĭ�ϲο���Χ', 'SCHEMA', 'dbo', 'table', 'LB_Item', 'column', 'DefaultRange'
go


  IF COL_LENGTH('LB_Item', 'RangeAllInfo') IS NULL  
  alter table LB_Item Add RangeAllInfo nVarchar(1000)
 go
execute sp_addextendedproperty 'MS_Description', '�ο���Χ��ϸ��Ϣ', 'SCHEMA', 'dbo', 'table', 'LB_Item', 'column', 'RangeAllInfo'
go

