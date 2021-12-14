if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_BarCodeForm') and o.name = 'FK_LIS_BARC_FK_BARCOD_LIS_ORDE')
alter table Lis_BarCodeForm
   drop constraint FK_LIS_BARC_FK_BARCOD_LIS_ORDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Lis_OrderItem') and o.name = 'FK_LIS_ORDE_FK_ORDERI_LIS_ORDE')
alter table dbo.Lis_OrderItem
   drop constraint FK_LIS_ORDE_FK_ORDERI_LIS_ORDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_TestForm') and o.name = 'FK_LIS_TEST_FK_TESTFO_LIS_ORDE')
alter table Lis_TestForm
   drop constraint FK_LIS_TEST_FK_TESTFO_LIS_ORDE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_OrderForm')
            and   type = 'U')
   drop table dbo.Lis_OrderForm
go

/*==============================================================*/
/* Table: Lis_OrderForm                                         */
/*==============================================================*/
create table dbo.Lis_OrderForm (
   OrderFormID          bigint               not null,
   PatID                bigint               null,
   PartitionDate        datetime             null,
   OrderFormNo          varchar(50)          null,
   OrderTypeID          bigint               null,
   OrderTime            datetime             null,
   OrderExecTime        datetime             null,
   OrderExecFlag        int                  null,
   HospitalID           bigint               null,
   ExecDeptID           bigint               null,
   DestinationID        bigint               null,
   IsUrgent             int                  null,
   ClientID             bigint               null,
   IsByHand             int                  null,
   IsAffirm             int                  null,
   FormMemo             varchar(200)         null,
   ChargeID             varchar(10)          null,
   ChargeOrderName      varchar(50)          null,
   Charge               float                null,
   IsCheckFee           int                  null,
   Balance              float                null,
   OperHost             varchar(100)         null,
   OperatorID           bigint               null,
   OperatorName         varchar(100)         null,
   CheckerID            bigint               null,
   CheckerName          varchar(100)         null,
   ParItemCName         varchar(1000)        null,
   PhotoURL             varchar(100)         null,
   RequestSource        varchar(100)         null,
   PrintTimes           int                  null,
   HisHospitalNo        varchar(50)          null,
   HisDeptNo            varchar(50)          null,
   HisDeptName          varchar(100)         null,
   HisDoctorNo          varchar(50)          null,
   HisDoctor            varchar(100)         null,
   HisDoctorPhoneCode   varchar(100)         null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   LabID                bigint               null,
   DateTimeStamp        timestamp            null,
   constraint PK_Lis_OrderForm primary key (OrderFormID)
         on "PRIMARY",
   constraint FK_LIS_ORDE_FK_ORDERF_LIS_PATI foreign key (PatID)
      references dbo.Lis_Patient (PatID)
)
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_BarCodeItem') and o.name = 'FK_LIS_BARC_FK_BARCOD_LIS_BARC')
alter table Lis_BarCodeItem
   drop constraint FK_LIS_BARC_FK_BARCOD_LIS_BARC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_TestForm') and o.name = 'FK_LIS_TEST_FK_TESTFO_LIS_BARC')
alter table Lis_TestForm
   drop constraint FK_LIS_TEST_FK_TESTFO_LIS_BARC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_BarCodeForm')
            and   type = 'U')
   drop table Lis_BarCodeForm
go

/*==============================================================*/
/* Table: Lis_BarCodeForm                                       */
/*==============================================================*/
create table Lis_BarCodeForm (
   PartitionDate        datetime             null,
   OrderFormID          bigint               null,
   PatID                bigint               null,
   BarCodeFormID        bigint               not null,
   BarCode              varchar(100)         null,
   OrderExecTime        datetime             null,
   HospitalID           bigint               null,
   SampleStatusID       bigint               null,
   DestinationID        bigint               null,
   PrintTimes           int                  null,
   IsAffirm             int                  null,
   IsPrep               int                  null,
   BarCodeFlag          int                  null,
   BarCodeStatusID      bigint               null,
   BarCodeCurrentStatus varchar(100)         null,
   IsSendPreSystem      int                  null,
   ReceiveFlag          int                  null,
   DispenseFlag         int                  null,
   SamplingGroupID      bigint               null,
   Color                varchar(100)         null,
   ColorValue           int                  null,
   SampleTypeID         bigint               null,
   IsUrgent             int                  null,
   ParItemCName         varchar(2000)        null,
   SampleCap            float                null,
   CollectPart          varchar(200)         null,
   CollectTimes         int                  null,
   ClientID             bigint               null,
   CollectPackNo        varchar(100)         null,
   AutoUnionSNo         varchar(100)         null,
   IsAutoUnion          int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   LabID                bigint               null,
   DateTimeStamp        timestamp            null,
   constraint PK_Lis_BarCodeForm primary key (BarCodeFormID),
   constraint FK_LIS_BARC_FK_BARCOD_LIS_PATI foreign key (PatID)
      references dbo.Lis_Patient (PatID),
   constraint FK_LIS_BARC_FK_BARCOD_LIS_ORDE foreign key (OrderFormID)
      references dbo.Lis_OrderForm (OrderFormID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Lis_BarCodeForm') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Lis_BarCodeForm' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '采样样本单', 
   'user', @CurrentUser, 'table', 'Lis_BarCodeForm'
go
