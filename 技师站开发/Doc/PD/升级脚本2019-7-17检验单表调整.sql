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
   TestEndTime          datetime             null,
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
      references dbo.Lis_OrderForm (OrderFormID),
   constraint FK_LIS_TESTForm_Section foreign key (SectionID)
      references LB_Section (SectionID)
)
go


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
   Order_ItemID         bigint               not null,
   BarCode_ItemID       bigint               null,
   ItemID               bigint               null,
   PItemID              bigint               null,
   GTestDate            datetime             null,
   TestType             int                  null,
   TestFormID           bigint               null,
   OriglValue           nvarchar(300)        null,
   ValueType            int                  null,
   ReportValue          nvarchar(300)        null,
   ResultComment        nvarchar(1000)       null,
   ResultStatus         nvarchar(20)         null,
   QuanValue            float                null,
   bAlarmColor          bit                  null,
   AlarmColor           int                  null,
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
      references Lis_BarCodeItem (BarCodeItemID),
   constraint FK_LIS_TESTItem_ITEMID foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
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
   constraint PK_Lis_EquipForm primary key nonclustered (EquipFormID),
   constraint FK_LIS_EquipForm_EquipID foreign key (EquipID)
      references dbo.LB_Equip (EquipID)
)
go

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
