USE [LabStar]
GO
--患者就诊信息
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_BarCodeForm') and o.name = 'FK_LIS_BARC_FK_BARCOD_LIS_PATI')
alter table Lis_BarCodeForm
   drop constraint FK_LIS_BARC_FK_BARCOD_LIS_PATI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Lis_OrderForm') and o.name = 'FK_LIS_ORDE_FK_ORDERF_LIS_PATI')
alter table dbo.Lis_OrderForm
   drop constraint FK_LIS_ORDE_FK_ORDERF_LIS_PATI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_TestForm') and o.name = 'FK_LIS_TEST_FK_TESTFO_LIS_PATI')
alter table Lis_TestForm
   drop constraint FK_LIS_TEST_FK_TESTFO_LIS_PATI
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_Patient')
            and   type = 'U')
   drop table dbo.Lis_Patient
go

/*==============================================================*/
/* Table: Lis_Patient                                           */
/*==============================================================*/
create table dbo.Lis_Patient (
   PatID                bigint               not null,
   PartitionDate        datetime             null,
   CName                nvarchar(100)        null,
   GenderID             int                  null,
   GenderName           nvarchar(50)         null,
   Age                  float                null,
   AgeUnitID            bigint               null,
   AgeUnitName          nvarchar(50)         null,
   Birthday             datetime             null,
   AgeDesc              nvarchar(100)        null,
   PatHeight            int                  null,
   PatWeight            Float                null,
   FolkID               bigint               null,
   FolkName             nvarchar(100)        null,
   PatAddress           nvarchar(200)        null,
   PatPhoto             varbinary(Max)       null,
   PhoneCode            nvarchar(100)        null,
   WeChatNo             nvarchar(100)        null,
   EMailAddress         nvarchar(100)        null,
   PatType              nvarchar(100)        null,
   SickTypeID           bigint               null,
   DiagID               bigint               null,
   DiagName             nvarchar(2000)       null,
   DoctorID             bigint               null,
   DoctorName           nvarchar(100)        null,
   DoctorTell           nvarchar(500)        null,
   ExecDeptID           bigint               null,
   DeptID               bigint               null,
   DeptName             nvarchar(100)        null,
   VisitTimes           int                  null,
   VisitDate            datetime             null,
   DistrictID           bigint               null,
   DistrictName         nvarchar(100)        null,
   WardID               bigint               null,
   WardName             nvarchar(100)        null,
   Bed                  nvarchar(100)        null,
   IDCardNo             nvarchar(40)         null,
   HisPatNo             nvarchar(100)        null,
   PatNo                nvarchar(100)        null,
   PatCardNo            nvarchar(100)        null,
   InPatNo              nvarchar(100)        null,
   ExamNo               nvarchar(100)        null,
   MedicareNo           nvarchar(100)        null,
   UnionPayNo           nvarchar(100)        null,
   HealthCardNo         nvarchar(100)        null,
   PowerCardNo          nvarchar(100)        null,
   InOutSerialNo        nvarchar(100)        null,
   InvoiceNo            nvarchar(100)        null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   LabID                bigint               not null,
   DateTimeStamp        timestamp            null,
   constraint PK_Lis_Patient primary key (PatID)
         on "PRIMARY"
)
go


--医嘱单

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
--医嘱项目
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_BarCodeItem') and o.name = 'FK_LIS_BARC_FK_BARCODEITEM_LIS_ORDE')
alter table Lis_BarCodeItem
   drop constraint FK_LIS_BARC_FK_BARCODEITEM_LIS_ORDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_TestItem') and o.name = 'FK_LIS_TEST_FK_TESTIT_LIS_ORDE')
alter table Lis_TestItem
   drop constraint FK_LIS_TEST_FK_TESTIT_LIS_ORDE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_OrderItem')
            and   type = 'U')
   drop table dbo.Lis_OrderItem
go

/*==============================================================*/
/* Table: Lis_OrderItem                                         */
/*==============================================================*/
create table dbo.Lis_OrderItem (
   OrderItemID          bigint               not null,
   OrdersItemID         bigint               null,
   OrderFormID          bigint               null,
   OrderFormNo          nvarchar(100)        null,
   OrderDate            datetime             null,
   PartitionDate        datetime             not null,
   OrderItemExecFlag    int                  null,
   ItemStatusID         bigint               null,
   IsPriceItem          int                  null,
   IsUrgent             int                  null,
   IsCheckFee           int                  null,
   Charge               float                null,
   RemoveHost           nvarchar(100)        null,
   Remover              nvarchar(100)        null,
   RemoveTime           datetime             null,
   SampleTypeID         bigint               null,
   CollectPart          nvarchar(100)        null,
   HisItemNo            nvarchar(100)        null,
   HisItemName          nvarchar(100)        null,
   HisSampleTypeNo      nvarchar(100)        null,
   HisSampleTypeName    nvarchar(100)        null,
   LabID                bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DateTimeStamp        timestamp            null,
   constraint PK_Lis_OrderItem primary key (OrderItemID)
         with (fillfactor= 90)
   on "PRIMARY",
   constraint FK_LIS_ORDE_FK_ORDERI_LIS_ORDE foreign key (OrderFormID)
      references dbo.Lis_OrderForm (OrderFormID)
)
go

--采样单
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


--采样项目
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_TestItem') and o.name = 'FK_LIS_TEST_FK_TESTIT_LIS_BARC')
alter table Lis_TestItem
   drop constraint FK_LIS_TEST_FK_TESTIT_LIS_BARC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_BarCodeItem')
            and   type = 'U')
   drop table Lis_BarCodeItem
go

/*==============================================================*/
/* Table: Lis_BarCodeItem                                       */
/*==============================================================*/
create table Lis_BarCodeItem (
   PartitionDate        datetime             null,
   BarCodeItemID        bigint               not null,
   BarCodeFormID        bigint               null,
   BarCodesItemID       bigint               null,
   OrderItemID          bigint               null,
   OrdersItemID         bigint               null,
   BarCodeItemFlag      int                  null,
   ReportDateDesc       datetime             null,
   ReceiveFlag          int                  null,
   CollectTime          datetime             null,
   InceptTime           datetime             null,
   CheckTime            datetime             null,
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


--检验单
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_EquipForm') and o.name = 'FK_LIS_EquipForm_TestForm')
alter table Lis_EquipForm
   drop constraint FK_LIS_EquipForm_TestForm
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Lis_TestGraph') and o.name = 'FK_LIS_TESTGraph_TESTForm')
alter table dbo.Lis_TestGraph
   drop constraint FK_LIS_TESTGraph_TESTForm
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_TestItem') and o.name = 'FK_LIS_TEST_FK_TESTIT_LIS_TEST')
alter table Lis_TestItem
   drop constraint FK_LIS_TEST_FK_TESTIT_LIS_TEST
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_TestForm')
            and   type = 'U')
   drop table Lis_TestForm
go

/*==============================================================*/
/* Table: Lis_TestForm                                          */
/*==============================================================*/
create table Lis_TestForm (
   TestFormID           bigint               not null,
   OrderFormID          bigint               null,
   SampleFormID         bigint               null,
   PatID                bigint               null,
   SectionID            bigint               null,
   GTestDate            datetime             null,
   GSampleNo            nvarchar(20)         null,
   GSampleNoForOrder    nvarchar(20)         null,
   MainStatusID         int                  null,
   StatusID             bigint               null,
   ReportStatusID       bigint               null,
   iSource              int                  null,
   GSampleInfo          nvarchar(200)        null,
   SampleSpecialDesc    nvarchar(200)        null,
   FormMemo             ntext                null,
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
   ReceiveTime          datetime             null,
   OnLineTime           datetime             null,
   iExamine             int                  null,
   TestTime             datetime             null,
   TestEndTime          datetime             null,
   AlarmLevel           int                  null,
   AlarmInfo            nvarchar(50)         null,
   ZFSysCheckInfo       nvarchar(1000)       null,
   OtherTester          nvarchar(50)         null,
   OtherTesterId        bigint               null,
   Confirmer            nvarchar(50)         null,
   ConfirmerId          bigint               null,
   ConfirmTime          datetime             null,
   ConfirmInfo          nvarchar(500)        null,
   Checker              nvarchar(50)         null,
   CheckerID            bigint               null,
   CheckTime            datetime             null,
   CheckInfo            nvarchar(500)        null,
   BackUpDesc           varchar(200)         null,
   BackUpDescID         bigint               null,
   Charge               float                null,
   RemoveReason         nvarchar(200)        null,
   ESend                nvarchar(100)        null,
   PrintCount           int                  null,
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
      references dbo.Lis_OrderForm (OrderFormID),
   constraint FK_LIS_TESTForm_Section foreign key (SectionID)
      references LB_Section (SectionID)
)
go


--检验项目
if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_TestItem')
            and   type = 'U')
   drop table Lis_TestItem
go

/*==============================================================*/
/* Table: Lis_TestItem                                          */
/*==============================================================*/
create table Lis_TestItem (
   TestItemID           bigint               not null,
   OrderItemID          bigint               null,
   BarCodeItemID        bigint               null,
   OrdersItemID         bigint               not null,
   BarCodesItemID       bigint               null,
   ItemID               bigint               null,
   PItemID              bigint               null,
   TestFormID           bigint               null,
   GTestDate            datetime             null,
   MainStatusID         int                  null,
   StatusID             bigint               null,
   ReportStatusID       bigint               null,
   iSource              int                  null,
   iExamine             int                  null,
   OriglValue           nvarchar(300)        null,
   ValueType            int                  null,
   ReportValue          nvarchar(300)        null,
   ReportInfo           image                null,
   ResultComment        nvarchar(1000)       null,
   ResultStatus         nvarchar(20)         null,
   QuanValue            float                null,
   bAlarmColor          bit                  null,
   AlarmColor           int                  null,
   IsReport             bit                  null,
   ESend                nvarchar(100)        null,
   QuanValue2           float                null,
   QuanValue3           float                null,
   TestMethod           nvarchar(50)         null,
   Unit                 nvarchar(50)         null,
   RefRange             nvarchar(400)        null,
   EResultStatus        nvarchar(50)         null,
   EResultAlarm         nvarchar(50)         null,
   RedoDesc             varchar(200)         null,
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
   OperaterID           bigint               null,
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
   constraint FK_LIS_TESTItem_ITEMID foreign key (ItemID)
      references dbo.LB_Item (ItemID),
   constraint FK_LIS_TEST_FK_TESTIT_LIS_BARC foreign key (BarCodeItemID)
      references Lis_BarCodeItem (BarCodeItemID)
)
go



--检验图形
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_TestGraph')
            and   type = 'U')
   drop table dbo.Lis_TestGraph
go

/*==============================================================*/
/* Table: Lis_TestGraph                                         */
/*==============================================================*/
create table dbo.Lis_TestGraph (
   TestGraphID          bigint               not null,
   TestFormID           bigint               null,
   GTestDate            datetime             null,
   iExamine             int                  null,
   GraphNo              int                  null,
   GraphName            nvarchar(100)        null,
   GraphType            nvarchar(20)         null,
   GraphData            image                null,
   GraphComment         ntext                null,
   MainStatusID         int                  null,
   StatusID             bigint               null,
   ReportStatusID       bigint               null,
   IsReport             bit                  null,
   GraphHeight          int                  null,
   GraphWidth           int                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_TestGraph primary key nonclustered (TestGraphID)
         WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
   constraint FK_LIS_TESTGraph_TESTForm foreign key (TestFormID)
      references Lis_TestForm (TestFormID)
)
go



--仪器单
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Lis_EquipGraph') and o.name = 'FK_LIS_EquipGraph_Equip')
alter table dbo.Lis_EquipGraph
   drop constraint FK_LIS_EquipGraph_Equip
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_EquipItem') and o.name = 'FK_EquipSampleItem_Form')
alter table Lis_EquipItem
   drop constraint FK_EquipSampleItem_Form
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_EquipForm')
            and   type = 'U')
   drop table Lis_EquipForm
go

/*==============================================================*/
/* Table: Lis_EquipForm                                         */
/*==============================================================*/
create table Lis_EquipForm (
   EquipFormID          bigint               not null,
   EquipID              bigint               null,
   TestFormID           bigint               null,
   ETestDate            datetime             null,
   ESampleNo            nvarchar(20)         null,
   EBarCode             nvarchar(20)         null,
   EquipModuleCode      nvarchar(20)         null,
   ERack                nvarchar(20)         null,
   EPosition            int                  null,
   iExamine             int                  null,
   IsExamined           bit                  null,
   EFinishCode          int                  null,
   EFinishInfo          varchar(50)          null,
   EResultComment       ntext                null,
   ESend                nvarchar(100)        null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_EquipForm primary key nonclustered (EquipFormID),
   constraint FK_LIS_EquipForm_EquipID foreign key (EquipID)
      references dbo.LB_Equip (EquipID),
   constraint FK_LIS_EquipForm_TestForm foreign key (TestFormID)
      references Lis_TestForm (TestFormID)
)
go



--仪器项目
if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_EquipItem')
            and   type = 'U')
   drop table Lis_EquipItem
go

/*==============================================================*/
/* Table: Lis_EquipItem                                         */
/*==============================================================*/
create table Lis_EquipItem (
   EquipResultID        bigint               not null,
   EquipFormID          bigint               null,
   ItemID               bigint               null,
   ETestDate            datetime             null,
   iExamine             int                  null,
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
      references Lis_EquipForm (EquipFormID),
   constraint FK_LIS_EquipItem_ItemID foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go


--仪器图形
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_EquipGraph')
            and   type = 'U')
   drop table dbo.Lis_EquipGraph
go

/*==============================================================*/
/* Table: Lis_EquipGraph                                        */
/*==============================================================*/
create table dbo.Lis_EquipGraph (
   EquipGraphID         bigint               not null,
   EquipFormID          bigint               null,
   GTestDate            datetime             null,
   iExamine             int                  null,
   GraphNo              int                  null,
   GraphName            nvarchar(100)        null,
   GraphType            nvarchar(20)         null,
   GraphData            image                null,
   GraphComment         ntext                null,
   IsReport             bit                  null,
   GraphHeight          int                  null,
   GraphWidth           int                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LIS_EQUIPGRAPH primary key (EquipGraphID),
   constraint FK_LIS_EquipGraph_Equip foreign key (EquipFormID)
      references Lis_EquipForm (EquipFormID)
)
go


--操作表
if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_Operate')
            and   type = 'U')
   drop table Lis_Operate
go

/*==============================================================*/
/* Table: Lis_Operate                                           */
/*==============================================================*/
create table Lis_Operate (
   PartitionDate        datetime             null,
   OperateID            bigint               not null,
   OperateObject        nvarchar(100)        null,
   OperateObjectID      bigint               null,
   OperateFormID        bigint               null,
   OperateType          nvarchar(100)        null,
   OperateTypeID        bigint               null,
   OperateMemoAuto      ntext                null,
   OperateMemo          ntext                null,
   OperateUserID        bigint               null,
   OperateUser          nvarchar(100)        null,
   OperateHost          nvarchar(100)        null,
   OperateAddress       nvarchar(100)        null,
   OperateHostType      nvarchar(100)        null,
   RelationUserID       bigint               null,
   RelationUser         nvarchar(100)        null,
   OperateDeptID        bigint               null,
   OperateDept          nvarchar(100)        null,
   BarCode              nvarchar(100)        null,
   TranceTime           datetime             null,
   IsTrance             bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   LabID                bigint               not null,
   DateTimeStamp        timestamp            null,
   constraint PK_LIS_OPERATE primary key nonclustered (OperateID)
)
go

--操作授权表
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_OperateASection') and o.name = 'FK_LIS_OperateASection_OperateA')
alter table Lis_OperateASection
   drop constraint FK_LIS_OperateASection_OperateA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_OperateAuthorize')
            and   type = 'U')
   drop table Lis_OperateAuthorize
go

/*==============================================================*/
/* Table: Lis_OperateAuthorize                                  */
/*==============================================================*/
create table Lis_OperateAuthorize (
   OperateAuthorizeID   bigint               not null,
   OperateType          nvarchar(100)        null,
   OperateTypeID        bigint               null,
   AuthorizeType        int                  null,
   AuthorizeInfo        nvarchar(200)        null,
   AuthorizeUserID      bigint               null,
   OperateUserID        bigint               null,
   OperateHost          nvarchar(100)        null,
   OperateAddress       nvarchar(100)        null,
   IsUse                bit                  null,
   BeginTime            datetime             null,
   EndTime              datetime             null,
   IsOnlyUseTime        bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   LabID                bigint               not null,
   DateTimeStamp        timestamp            null,
   constraint PK_LIS_OPERATEAUTHORIZE primary key (OperateAuthorizeID)
)
go

--授权小组关系
if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_OperateASection')
            and   type = 'U')
   drop table Lis_OperateASection
go

/*==============================================================*/
/* Table: Lis_OperateASection                                   */
/*==============================================================*/
create table Lis_OperateASection (
   OperateASectionID    bigint               not null,
   OperateAuthorizeID   bigint               not null,
   SectionID            bigint               null,
   DataAddTime          datetime             null,
   constraint PK_LIS_OPERATEASECTION primary key (OperateASectionID),
   constraint FK_LIS_OperateASection_OperateA foreign key (OperateAuthorizeID)
      references Lis_OperateAuthorize (OperateAuthorizeID),
   constraint FK_LIS_OperateASection_Section foreign key (SectionID)
      references LB_Section (SectionID)
)
go
