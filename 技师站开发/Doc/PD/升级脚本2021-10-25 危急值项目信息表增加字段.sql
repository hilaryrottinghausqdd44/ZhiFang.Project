--危急值项目表
IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='ItemName')
ALTER TABLE Lis_TestFormMsgItem ADD ItemName nvarchar(200)
GO
execute sp_addextendedproperty 'MS_Description', 
   '项目名称',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'ItemName'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='ReportValue')
ALTER TABLE Lis_TestFormMsgItem ADD ReportValue nvarchar(500)
GO
execute sp_addextendedproperty 'MS_Description', 
   '报告值',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'ReportValue'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='ResultStatus')
ALTER TABLE Lis_TestFormMsgItem ADD ResultStatus nvarchar(20)
GO
execute sp_addextendedproperty 'MS_Description', 
   '检验结果状态',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'ResultStatus'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='ResultStatusCode')
ALTER TABLE Lis_TestFormMsgItem ADD ResultStatusCode nvarchar(50)
GO
execute sp_addextendedproperty 'MS_Description', 
   '检验结果状态编码',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'ResultStatusCode'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='AlarmLevel')
ALTER TABLE Lis_TestFormMsgItem ADD AlarmLevel int
GO
execute sp_addextendedproperty 'MS_Description', 
   '结果警示级别',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'AlarmLevel'
go

IF not exists(select * from syscolumns where id=object_id('Lis_TestFormMsgItem') and name='AlarmInfo')
ALTER TABLE Lis_TestFormMsgItem ADD AlarmInfo nvarchar(50)
GO
execute sp_addextendedproperty 'MS_Description', 
   '结果警示信息',
   'user', 'dbo', 'table', 'Lis_TestFormMsgItem', 'column', 'AlarmInfo'
go