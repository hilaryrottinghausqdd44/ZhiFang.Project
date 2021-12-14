IF not exists(select * from syscolumns where id=object_id('LB_Equip') and name='SpecialtyID')
ALTER TABLE LB_Equip ADD SpecialtyID bigint
GO

IF not exists(select * from syscolumns where id=object_id('LB_Phrase') and name='TypeCode')
ALTER TABLE LB_Phrase ADD TypeCode nvarchar(50)
GO