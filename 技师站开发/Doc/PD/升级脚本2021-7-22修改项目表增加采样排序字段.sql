
IF COL_LENGTH('LB_Item', 'CollectSort') IS NULL  
  alter table LB_Item Add CollectSort int
go

execute sp_addextendedproperty 'MS_Description', '采样排序', 'SCHEMA', 'dbo', 'table', 'LB_Item', 'column', 'CollectSort'
go
