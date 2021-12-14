IF exists(select * from syscolumns where id=object_id('LB_SamplingItem') and name='MinItemCount')
ALTER TABLE LB_SamplingItem DROP COLUMN MinItemCount
GO