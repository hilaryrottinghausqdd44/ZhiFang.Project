
--���� HIS�ٴ���Ϣ��ע��������Ϣ��ע
  alter table Lis_Patient  add  HISComment           nvarchar(500)        null
  alter table Lis_Patient  add  PatComment           nvarchar(500)        null
  alter table Lis_Patient  add  SickType             nvarchar(50)         null


  --������������
  alter Table  Lis_TestForm add MainTester   nvarchar(50)   null
  alter Table  Lis_TestForm add MainTesterId   bigint  null
  alter Table  Lis_TestForm add  GSampleType          nvarchar(50) 
  alter Table  Lis_TestForm  Alter Column SickTypeID   bigint 

   --���Ӳ���ʱ���ǩ��ʱ��
  alter Table LB_QCItem add    IsUse                bit                  null
