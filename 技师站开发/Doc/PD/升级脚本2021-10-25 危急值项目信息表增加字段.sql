--Σ��ֵ��Ŀ��
IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='ItemName')
ALTER TABLE Lis_TestFormMsgItem ADD ItemName nvarchar(200)
GO
execute sp_addextendedproperty 'MS_Description', 
   '��Ŀ����',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'ItemName'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='ReportValue')
ALTER TABLE Lis_TestFormMsgItem ADD ReportValue nvarchar(500)
GO
execute sp_addextendedproperty 'MS_Description', 
   '����ֵ',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'ReportValue'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='ResultStatus')
ALTER TABLE Lis_TestFormMsgItem ADD ResultStatus nvarchar(20)
GO
execute sp_addextendedproperty 'MS_Description', 
   '������״̬',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'ResultStatus'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='ResultStatusCode')
ALTER TABLE Lis_TestFormMsgItem ADD ResultStatusCode nvarchar(50)
GO
execute sp_addextendedproperty 'MS_Description', 
   '������״̬����',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'ResultStatusCode'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='AlarmLevel')
ALTER TABLE Lis_TestFormMsgItem ADD AlarmLevel int
GO
execute sp_addextendedproperty 'MS_Description', 
   '�����ʾ����',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'AlarmLevel'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='AlarmInfo')
ALTER TABLE Lis_TestFormMsgItem ADD AlarmInfo nvarchar(50)
GO
execute sp_addextendedproperty 'MS_Description', 
   '�����ʾ��Ϣ',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'AlarmInfo'
go