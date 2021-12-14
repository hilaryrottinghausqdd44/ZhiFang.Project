

--字典类型名称
IF COL_LENGTH('LB_Dict', 'DictTypeName') IS NULL  
  alter table LB_Dict Add DictTypeName Varchar(50)
go
execute sp_addextendedproperty 'MS_Description', '字典类型名称', 'SCHEMA', 'dbo', 'table', 'LB_Dict', 'column', 'DictTypeName'
go
  --DictType=字典类型：ResultStatus  DictTypeName=字典类型名称：结果状态 
  --注SName 是显示名称

 
  --检验结果表，结果状态编码
  IF COL_LENGTH('Lis_TestItem', 'ResultStatusCode') IS NULL  
  alter table Lis_TestItem Add ResultStatusCode Varchar(50)
 go
execute sp_addextendedproperty 'MS_Description', '结果状态编码', 'SCHEMA', 'dbo', 'table', 'Lis_TestItem', 'column', 'ResultStatusCode'
go

  --注，ResultStatusCode对应DictCode	字典编码，不允许修改
  --注，ResultStatus对应SName	状态显示名称

  --删除调整过为枚举类型，不用的表
  --删除删除LB_DictType字典表类型
  drop table LB_DictType
