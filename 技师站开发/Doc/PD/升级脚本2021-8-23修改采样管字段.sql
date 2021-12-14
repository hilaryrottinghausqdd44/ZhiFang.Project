IF exists(select * from syscolumns where id=object_id('LB_Tcuvete') and name='ColorValue')
ALTER TABLE LB_Tcuvete ALTER COLUMN ColorValue nvarchar(50)
GO

IF exists(select * from syscolumns where id=object_id('Lis_BarCodeForm') and name='ColorValue')
ALTER TABLE Lis_BarCodeForm ALTER COLUMN ColorValue nvarchar(50)
GO

