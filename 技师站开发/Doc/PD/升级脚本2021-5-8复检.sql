

--复检
IF COL_LENGTH('Lis_TestItem', 'RedoStatus') IS NULL  
  alter table Lis_TestItem Add RedoStatus int
go
execute sp_addextendedproperty 'MS_Description', '复检标记', 'SCHEMA', 'dbo', 'table', 'Lis_TestItem', 'column', 'RedoStatus'
go


  IF COL_LENGTH('Lis_TestItem', 'RedoValues') IS NULL  
  alter table Lis_TestItem Add RedoValues nVarchar(200)
 go
execute sp_addextendedproperty 'MS_Description', '复检参考结果', 'SCHEMA', 'dbo', 'table', 'Lis_TestItem', 'column', 'RedoValues'
go

