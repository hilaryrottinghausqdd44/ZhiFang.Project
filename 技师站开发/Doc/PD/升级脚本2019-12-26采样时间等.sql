
--���ž�����Ϣ�����Ƿ�ȷ�ϱ�ǣ��Ѿ�ȷ�ϵĲ������޸�   
  alter table dbo.Lis_Patient add bConfirm bit

  --���Ӳ���ʱ���ǩ��ʱ��
  alter Table Lis_TestForm add CollectTime datetime    null
  alter Table Lis_TestForm add InceptTime datetime    null

  --���Ӳ���ʱ���ǩ��ʱ��
  alter Table Lis_BarCodeForm add CollectTime datetime    null
  alter Table Lis_BarCodeForm add InceptTime datetime    null

  --ʧ�������Ϊbigint
  alter Table  Lis_QCdata Alter Column LoseExaminerID bigint