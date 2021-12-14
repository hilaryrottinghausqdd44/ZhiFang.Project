IF not exists(select * from syscolumns where id=object_id('Lis_BarCodeForm') and name='Charge')
ALTER TABLE Lis_BarCodeForm add Charge float
GO

IF not exists(select * from syscolumns where id=object_id('Lis_BarCodeForm') and name='ChargeFlag')
ALTER TABLE Lis_BarCodeForm add ChargeFlag int
GO