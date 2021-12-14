IF not exists(select * from syscolumns where id=object_id('Lis_Queue') and name='BarCodeFormID')
ALTER TABLE Lis_Queue ADD BarCodeFormID bigint
GO

