IF exists(select * from syscolumns where id=object_id('Lis_BarCodeItem') and name='ReportDateDesc')
ALTER TABLE Lis_BarCodeItem ALTER COLUMN ReportDateDesc nvarchar(max)
GO

IF exists(select * from syscolumns where id=object_id('Lis_Queue') and name='QueueNoNoHeader')
begin
 EXEC('sp_rename ''Lis_Queue.QueueNoNoHeader'',''QueueNoHeader''')
end
GO
