

--检验结果表，结果状态编码
IF COL_LENGTH('Lis_TestItem', 'AlarmColor') IS NOT NULL  
  alter table Lis_TestItem alter column AlarmColor Varchar(50)
go


--LB_ItemRangeExp表，项目ID修改为可以为空
IF COL_LENGTH('LB_ItemRangeExp', 'ItemID') IS NOT NULL  
  alter table LB_ItemRangeExp alter column ItemID bigint
go

--LB_ItemRange表，性别ID修改为可以为空
IF COL_LENGTH('LB_ItemRange', 'GenderID') IS NOT NULL  
  alter table LB_ItemRange alter column GenderID bigint
go




