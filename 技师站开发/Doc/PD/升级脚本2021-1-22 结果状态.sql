

--�ֵ���������
IF COL_LENGTH('LB_Dict', 'DictTypeName') IS NULL  
  alter table LB_Dict Add DictTypeName Varchar(50)
go
execute sp_addextendedproperty 'MS_Description', '�ֵ���������', 'SCHEMA', 'dbo', 'table', 'LB_Dict', 'column', 'DictTypeName'
go
  --DictType=�ֵ����ͣ�ResultStatus  DictTypeName=�ֵ��������ƣ����״̬ 
  --עSName ����ʾ����

 
  --�����������״̬����
  IF COL_LENGTH('Lis_TestItem', 'ResultStatusCode') IS NULL  
  alter table Lis_TestItem Add ResultStatusCode Varchar(50)
 go
execute sp_addextendedproperty 'MS_Description', '���״̬����', 'SCHEMA', 'dbo', 'table', 'Lis_TestItem', 'column', 'ResultStatusCode'
go

  --ע��ResultStatusCode��ӦDictCode	�ֵ���룬�������޸�
  --ע��ResultStatus��ӦSName	״̬��ʾ����

  --ɾ��������Ϊö�����ͣ����õı�
  --ɾ��ɾ��LB_DictType�ֵ������
  drop table LB_DictType
