if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_BarCodeForm') and o.name = 'FK_LIS_BARC_FK_BARCOD_LIS_ORDE')
alter table Lis_BarCodeForm
   drop constraint FK_LIS_BARC_FK_BARCOD_LIS_ORDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_BarCodeForm') and o.name = 'FK_LIS_BARC_FK_BARCOD_LIS_PATI')
alter table Lis_BarCodeForm
   drop constraint FK_LIS_BARC_FK_BARCOD_LIS_PATI
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
   LabID                bigint               null,
   PartitionDate        datetime             null,
   OrderFormID          bigint               null,
   PatID                bigint               null,
   BarCodeFormID        bigint               not null,
   BarCode              nvarchar(100)        null,
   OrderExecTime        datetime             null,
   HospitalID           bigint               null,
   ExecDeptID           bigint               null,
   DestinationID        bigint               null,
   PrintTimes           int                  null,
   IsAffirm             int                  null,
   IsPrep               int                  null,
   BarCodeFlag          int                  null,
   BarCodeStatusID      bigint               null,
   BarCodeCurrentStatus nvarchar(100)        null,
   IsSendPreSystem      int                  null,
   ReceiveFlag          int                  null,
   DispenseFlag         int                  null,
   AffirmTime           datetime             null,
   PrintTime            datetime             null,
   CollectTime          datetime             null,
   SendTime             datetime             null,
   ArriveTime           datetime             null,
   InceptTime           datetime             null,
   GroupInceptTime      datetime             null,
   RejectTime           datetime             null,
   AllowTime            datetime             null,
   ReceiveTime          datetime             null,
   TranTime             datetime             null,
   ReportConfirmTime    datetime             null,
   ReportCheckTime      datetime             null,
   ReportSendTime       datetime             null,
   RepotPrintTimeLab    datetime             null,
   RepotPrintTimeSelf   datetime             null,
   RepotPrintTimeClinical datetime             null,
   AbortTime            datetime             null,
   SamplingGroupID      bigint               null,
   Color                nvarchar(100)        null,
   ColorValue           nvarchar(50)         null,
   SampleTypeID         bigint               null,
   IsUrgent             int                  null,
   ParItemCName         nvarchar(2000)       null,
   SampleCap            float                null,
   CollectPart          nvarchar(200)        null,
   CollectTimes         int                  null,
   ClientID             bigint               null,
   CollectPackNo        nvarchar(100)        null,
   AutoUnionSNo         nvarchar(100)        null,
   IsAutoUnion          int                  null,
   Charge               float                null,
   ChargeFlag           int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_BarCodeForm primary key nonclustered (BarCodeFormID),
   constraint FK_LIS_BARC_FK_BARCOD_LIS_PATI foreign key (PatID)
      references dbo.Lis_Patient (PatID),
   constraint FK_LIS_BARC_FK_BARCOD_LIS_ORDE foreign key (OrderFormID)
      references dbo.Lis_OrderForm (OrderFormID)
)
go