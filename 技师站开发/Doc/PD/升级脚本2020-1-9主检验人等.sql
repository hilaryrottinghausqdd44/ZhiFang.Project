
--增加 HIS临床信息备注，病人信息备注
  alter table Lis_Patient  add  HISComment           nvarchar(500)        null
  alter table Lis_Patient  add  PatComment           nvarchar(500)        null
  alter table Lis_Patient  add  SickType             nvarchar(50)         null


  --增加主检验人
  alter Table  Lis_TestForm add MainTester   nvarchar(50)   null
  alter Table  Lis_TestForm add MainTesterId   bigint  null
  alter Table  Lis_TestForm add  GSampleType          nvarchar(50) 
  alter Table  Lis_TestForm  Alter Column SickTypeID   bigint 

   --增加采样时间和签收时间
  alter Table LB_QCItem add    IsUse                bit                  null
