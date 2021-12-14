/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2019-05-27 19:43:19                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_EquipItem') and o.name = 'FK_EquipSampleItem_Form')
alter table Lis_EquipItem
   drop constraint FK_EquipSampleItem_Form
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Lis_OrderForm') and o.name = 'FK_LIS_ORDE_FK_ORDERF_LIS_PATI')
alter table dbo.Lis_OrderForm
   drop constraint FK_LIS_ORDE_FK_ORDERF_LIS_PATI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_TestItem') and o.name = 'FK_LIS_TEST_FK_TESTIT_LIS_TEST')
alter table Lis_TestItem
   drop constraint FK_LIS_TEST_FK_TESTIT_LIS_TEST
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_BarCodeForm')
            and   type = 'U')
   drop table Lis_BarCodeForm
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_BarCodeItem')
            and   type = 'U')
   drop table Lis_BarCodeItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_EquipForm')
            and   type = 'U')
   drop table Lis_EquipForm
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_EquipItem')
            and   type = 'U')
   drop table Lis_EquipItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_OrderForm')
            and   type = 'U')
   drop table dbo.Lis_OrderForm
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_OrderItem')
            and   type = 'U')
   drop table dbo.Lis_OrderItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_Patient')
            and   type = 'U')
   drop table dbo.Lis_Patient
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_TestForm')
            and   type = 'U')
   drop table Lis_TestForm
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_TestItem')
            and   type = 'U')
   drop table Lis_TestItem
go

/*==============================================================*/
/* Table: Lis_Patient                                           */
/*==============================================================*/
create table dbo.Lis_Patient (
   PatID                bigint               not null,
   PartitionDate        datetime             null,
   CName                varchar(40)          null,
   GenderID             int                  null,
   GenderName           varchar(50)          null,
   Age                  float                null,
   AgeUnitID            int                  null,
   AgeUnitName          varchar(50)          null,
   Birthday             datetime             null,
   AgeDesc              varchar(100)         null,
   PatHeight            int                  null,
   PatWeight            Float                null,
   FolkID               bigint               null,
   FolkName             varchar(100)         null,
   PatAddress           varchar(200)         null,
   PatPhoto             image                null,
   PhoneCode            varchar(100)         null,
   WeChatNo             varchar(100)         null,
   EMailAddress         varchar(100)         null,
   PatType              varchar(100)         null,
   SickTypeID           int                  null,
   DiagID               bigint               null,
   DiagName             varchar(1000)        null,
   DoctorID             bigint               null,
   DoctorName           varchar(100)         null,
   DoctorTell           varchar(500)         null,
   ExecDeptID           bigint               null,
   DeptID               bigint               null,
   DeptName             varchar(100)         null,
   VisitTimes           int                  null,
   VisitDate            datetime             null,
   DistrictID           bigint               null,
   DistrictName         varchar(100)         null,
   WardID               bigint               null,
   WardName             varchar(100)         null,
   Bed                  varchar(100)         null,
   IDCardNo             varchar(40)          null,
   HisPatNo             varchar(100)         null,
   PatNo                varchar(20)          null,
   PatCardNo            varchar(100)         null,
   InPatNo              varchar(100)         null,
   ExamNo               varchar(100)         null,
   MedicareNo           varchar(100)         null,
   UnionPayNo           varchar(100)         null,
   HealthCardNo         varchar(100)         null,
   PowerCardNo          varchar(100)         null,
   InOutSerialNo        varchar(100)         null,
   InvoiceNo            varchar(100)         null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   LabID                bigint               not null,
   DateTimeStamp        timestamp            null,
   constraint PK_Lis_Patient primary key (PatID)
         on "PRIMARY"
)
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
   ExecDeptID           bigint               null,
   DestinationID        bigint               null,
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

/*==============================================================*/
/* Table: Lis_BarCodeForm                                       */
/*==============================================================*/
create table Lis_BarCodeForm (
   PartitionDate        datetime             null,
   OrderFormID          bigint               null,
   PatID                bigint               null,
   BarCodeFormID        bigint               not null,
   BarCode              varchar(100)         null,
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

/*==============================================================*/
/* Table: Lis_OrderItem                                         */
/*==============================================================*/
create table dbo.Lis_OrderItem (
   OrderItemID          bigint               not null,
   Order_ItemID         bigint               null,
   OrderFormID          bigint               null,
   PartitionDate        datetime             not null,
   OrderItemExecFlag    int                  null,
   IsPriceItem          int                  null,
   IsUrgent             int                  null,
   IsCheckFee           int                  null,
   Charge               float                null,
   RemoveHost           varchar(100)         null,
   Remover              varchar(100)         null,
   RemoveTime           datetime             null,
   SampleTypeID         bigint               null,
   CollectPart          varchar(100)         null,
   HisItemNo            varchar(100)         null,
   HisItemName          varchar(100)         null,
   HisSampleTypeNo      varchar(100)         null,
   HisSampleTypeName    varchar(100)         null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   LabID                bigint               null,
   DateTimeStamp        timestamp            null,
   constraint PK_Lis_OrderItem primary key (OrderItemID)
         with (fillfactor= 90)
   on "PRIMARY",
   constraint FK_LIS_ORDE_FK_ORDERI_LIS_ORDE foreign key (OrderFormID)
      references dbo.Lis_OrderForm (OrderFormID)
)
go

/*==============================================================*/
/* Table: Lis_BarCodeItem                                       */
/*==============================================================*/
create table Lis_BarCodeItem (
   PartitionDate        datetime             null,
   BarCodeItemID        bigint               not null,
   BarCodeFormID        bigint               null,
   BarCode_ItemID       bigint               null,
   OrderItemID          bigint               null,
   Order_ItemID         bigint               null,
   BarCodeItemFlag      int                  null,
   ReceiveFlag          int                  null,
   ReportDateDesc       datetime             null,
   ItemDispenseFlag     int                  null,
   IsItemSplitReceive   int                  null,
   ItemStatusID         bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   LabID                bigint               null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_BarCodeItem primary key (BarCodeItemID),
   constraint FK_LIS_BARC_FK_BARCOD_LIS_BARC foreign key (BarCodeFormID)
      references Lis_BarCodeForm (BarCodeFormID),
   constraint FK_LIS_BARC_FK_BARCODEITEM_LIS_ORDE foreign key (OrderItemID)
      references dbo.Lis_OrderItem (OrderItemID)
)
go

/*==============================================================*/
/* Table: Lis_EquipForm                                         */
/*==============================================================*/
create table Lis_EquipForm (
   EquipFormID          bigint               not null,
   EBarCode             varchar(20)          null,
   EquipModuleCode      varchar(20)          null,
   ETestDate            datetime             null,
   ESampleNo            varchar(20)          null,
   ERack                varchar(20)          null,
   EPosition            int                  null,
   EFinishCode          int                  null,
   EFinishInfo          varchar(50)          null,
   EResultComment       ntext                null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_EquipForm primary key nonclustered (EquipFormID)
)
go

/*==============================================================*/
/* Table: Lis_EquipItem                                         */
/*==============================================================*/
create table Lis_EquipItem (
   EquipResultID        bigint               not null,
   EquipFormID          bigint               null,
   TestType             int                  null,
   ItemChannel          varchar(50)          null,
   EReportValue         varchar(300)         null,
   EResultStatus        varchar(20)          null,
   EResultAlarm         varchar(20)          null,
   ETestComment         text                 null,
   EOriginalValue       varchar(300)         null,
   EOriginalResultStatus varchar(20)          null,
   EOriginalResultAlarm varchar(20)          null,
   EOriginalTestComment text                 null,
   ETestState           int                  null,
   ItemResultFlag       int                  null,
   ESend                varchar(100)         null,
   ZDY1                 varchar(200)         null,
   ZDY2                 varchar(200)         null,
   ZDY3                 varchar(200)         null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_EquipItem primary key (EquipResultID),
   constraint FK_EquipSampleItem_Form foreign key (EquipFormID)
      references Lis_EquipForm (EquipFormID)
)
go

/*==============================================================*/
/* Table: Lis_TestForm                                          */
/*==============================================================*/
create table Lis_TestForm (
   TestFormID           bigint               not null,
   OrderFormID          bigint               null,
   SampleFormID         bigint               null,
   PatID                bigint               null,
   GTestDate            datetime             null,
   GSampleNo            nvarchar(20)         null,
   GSampleNoForOrder    nvarchar(20)         null,
   MainState            int                  null,
   GSampleInfo          nvarchar(200)        null,
   SampleSpecialDesc    nvarchar(200)        null,
   FormMemo             text                 null,
   SickTypeID           int                  null,
   GSampleTypeID        bigint               null,
   Age                  float                null,
   AgeUnitID            bigint               null,
   BarCode              nvarchar(20)         null,
   TestType             int                  null,
   PatNo                nvarchar(20)         null,
   CName                nvarchar(40)         null,
   GenderID             bigint               null,
   PatWeight            Float                null,
   DeptID               bigint               null,
   DistrictID           bigint               null,
   UrgentState          nvarchar(50)         null,
   Testaim              nvarchar(200)        null,
   TestComment          nvarchar(500)        null,
   TestInfo             nvarchar(500)        null,
   iDevelop             int                  null,
   IsTestOutTime        bit                  null,
   IsExamineOutTime     bit                  null,
   iWarningTime         int                  null,
   DeleteFlag           int                  null,
   PositiveFlag         int                  null,
   DistributeFlag       int                  null,
   ReceiveFlag          int                  null,
   ReceiveMan           nvarchar(50)         null,
   ReceiveManID         bigint               null,
   ReceiveTime          datetime             null,
   Charge               float                null,
   ChargeFlag           int                  null,
   ChargeInfo           nvarchar(200)        null,
   RemoveFeesReason     nvarchar(200)        null,
   RemoveReason         nvarchar(200)        null,
   CommState            int                  null,
   IsOnLine             bit                  null,
   OnLineTime           datetime             null,
   IsRedo               bit                  null,
   ESend                nvarchar(100)        null,
   AlarmLevel           int                  null,
   AlarmInfo            nvarchar(50)         null,
   TestTime             datetime             null,
   IsAllResultTest      bit                  null,
   IsAllTest            bit                  null,
   ZFSysCheckFlag       int                  null,
   ZFSysCheckInfo       nvarchar(1000)       null,
   MainTester           nvarchar(50)         null,
   MainTesterId         bigint               null,
   OtherTester          nvarchar(50)         null,
   OtherTesterId        bigint               null,
   ReviseFlag           int                  null,
   BackUpDesc           varchar(200)         null,
   BackUpDescID         bigint               null,
   afteRevisedFlag      int                  null,
   Confirmer            nvarchar(50)         null,
   ConfirmerId          bigint               null,
   ConfirmeDate         datetime             null,
   afterConfirmeFlag    int                  null,
   ConfirmInfo          nvarchar(500)        null,
   IsConfirmBySys       bit                  null,
   Examiner             nvarchar(50)         null,
   ExaminerID           bigint               null,
   ExamineDate          datetime             null,
   ExamineInfo          nvarchar(500)        null,
   afteExamineFlag      int                  null,
   IsExamineBySys       bit                  null,
   MigrationFlag        bit                  null,
   PrintFlag            int                  null,
   PrintCount           int                  null,
   IsUpload             int                  null,
   OperaterID           bigint               null,
   FinalOperaterID      bigint               null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_TestForm primary key (TestFormID),
   constraint FK_LIS_TEST_FK_TESTFO_LIS_PATI foreign key (PatID)
      references dbo.Lis_Patient (PatID),
   constraint FK_LIS_TEST_FK_TESTFO_LIS_BARC foreign key (SampleFormID)
      references Lis_BarCodeForm (BarCodeFormID),
   constraint FK_LIS_TEST_FK_TESTFO_LIS_ORDE foreign key (OrderFormID)
      references dbo.Lis_OrderForm (OrderFormID)
)
go

/*==============================================================*/
/* Table: Lis_TestItem                                          */
/*==============================================================*/
create table Lis_TestItem (
   TestItemID           bigint               not null,
   OrderItemID          bigint               null,
   BarCodeItemID        bigint               null,
   Order_ItemID         bigint               not null,
   BarCode_ItemID       bigint               null,
   ItemID               bigint               null,
   GTestDate            datetime             null,
   TestType             int                  null,
   TestFormID           bigint               null,
   OriglValue           nvarchar(300)        null,
   ValueType            int                  null,
   ReportValue          nvarchar(300)        null,
   ResultComment        nvarchar(1000)       null,
   ResultStatus         nvarchar(20)         null,
   QuanValue            float                null,
   ESend                nvarchar(100)        null,
   QuanValue2           float                null,
   QuanValue3           float                null,
   TestMethod           nvarchar(50)         null,
   Unit                 nvarchar(50)         null,
   RefRange             nvarchar(400)        null,
   EResultStatus        nvarchar(50)         null,
   EResultAlarm         nvarchar(50)         null,
   PreResultID          bigint               null,
   PreGTestDate         DateTime             null,
   PreValue             nvarchar(400)        null,
   PreValueComp         nvarchar(50)         null,
   PreCompStatus        nvarchar(50)         null,
   HisResultCount       int                  null,
   PreTestItemID2       bigint               null,
   PreGTestDate2        DateTime             null,
   PreValue2            nvarchar(400)        null,
   PreTestItemID3       bigint               null,
   PreGTestDate3        DateTime             null,
   PreValue3            nvarchar(400)        null,
   AlarmLevel           int                  null,
   AlarmInfo            nvarchar(50)         null,
   ResultLinkType       int                  null,
   ResultLink           nvarchar(400)        null,
   DeleteFlag           int                  null,
   IsDuplicate          bit                  null,
   IsMatch              bit                  null,
   IsHandEditStatus     bit                  null,
   OperaterID           bigint               null,
   TestFlag             int                  null,
   CheckFlag            int                  null,
   iItemSource          int                  null,
   ReportFlag           int                  null,
   CommFlag             int                  null,
   DispOrder            int                  null,
   LabID                bigint               null,
   TestTime             datetime             null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_TestItem primary key (TestItemID),
   constraint FK_LIS_TEST_FK_TESTIT_LIS_TEST foreign key (TestFormID)
      references Lis_TestForm (TestFormID),
   constraint FK_LIS_TEST_FK_TESTIT_LIS_ORDE foreign key (OrderItemID)
      references dbo.Lis_OrderItem (OrderItemID),
   constraint FK_LIS_TEST_FK_TESTIT_LIS_BARC foreign key (BarCodeItemID)
      references Lis_BarCodeItem (BarCodeItemID)
)
go

