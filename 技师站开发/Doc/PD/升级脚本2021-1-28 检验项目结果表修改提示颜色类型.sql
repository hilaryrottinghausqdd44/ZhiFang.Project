

--�����������״̬����
IF COL_LENGTH('Lis_TestItem', 'AlarmColor') IS NOT NULL  
  alter table Lis_TestItem alter column AlarmColor Varchar(50)
go


--LB_ItemRangeExp����ĿID�޸�Ϊ����Ϊ��
IF COL_LENGTH('LB_ItemRangeExp', 'ItemID') IS NOT NULL  
  alter table LB_ItemRangeExp alter column ItemID bigint
go

--LB_ItemRange���Ա�ID�޸�Ϊ����Ϊ��
IF COL_LENGTH('LB_ItemRange', 'GenderID') IS NOT NULL  
  alter table LB_ItemRange alter column GenderID bigint
go




