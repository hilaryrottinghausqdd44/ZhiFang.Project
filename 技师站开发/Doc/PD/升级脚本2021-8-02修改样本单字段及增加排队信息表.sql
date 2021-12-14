IF exists(select * from syscolumns where id=object_id('Lis_BarCodeForm') and name='SampleStatusID')
ALTER TABLE Lis_BarCodeForm Drop COLUMN SampleStatusID
GO

IF not exists(select * from syscolumns where id=object_id('Lis_BarCodeForm') and name='ExecDeptID')
ALTER TABLE Lis_BarCodeForm add ExecDeptID bigint
GO

if exists (select 1 from  sysobjects where  id = object_id('Lis_Queue') and   type = 'U')
   drop table Lis_Queue
go
create table Lis_Queue (
   QueueID              bigint               not null,
   LineDate             datetime             null,
   QueueNo              int                  null,
   QueueNoNoHeader      nvarchar(100)        null,
   BarCode              nvarchar(100)        null,
   PatName              nvarchar(100)        null,
   HisPatNo             nvarchar(100)        null,
   OrderFormNo          nvarchar(100)        null,
   ExecFlag             int                  null,
   PatTypeID            int                  null,
   LabID                bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LIS_QUEUE primary key (QueueID)
)
go
