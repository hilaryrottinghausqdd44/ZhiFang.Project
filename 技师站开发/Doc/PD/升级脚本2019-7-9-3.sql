if exists (select 1
            from  sysobjects
           where  id = object_id('LB_GetReportDate')
            and   type = 'U')
   drop table LB_GetReportDate
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_GetReportDateItem')
            and   type = 'U')
   drop table LB_GetReportDateItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ParItemSplit')
            and   type = 'U')
   drop table LB_ParItemSplit
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_PhrasesWatch')
            and   type = 'U')
   drop table LB_PhrasesWatch
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_PhrasesWatchClass')
            and   type = 'U')
   drop table LB_PhrasesWatchClass
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_PhrasesWatchClassItem')
            and   type = 'U')
   drop table LB_PhrasesWatchClassItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ReportDateItem')
            and   type = 'U')
   drop table LB_ReportDateItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SampleItem')
            and   type = 'U')
   drop table LB_SampleItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SamplingChargeItem')
            and   type = 'U')
   drop table LB_SamplingChargeItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SamplingGroup')
            and   type = 'U')
   drop table LB_SamplingGroup
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SamplingItem')
            and   type = 'U')
   drop table LB_SamplingItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_Tcuvete')
            and   type = 'U')
   drop table LB_Tcuvete
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_TranRule')
            and   type = 'U')
   drop table LB_TranRule
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_TranRuleItem')
            and   type = 'U')
   drop table LB_TranRuleItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_TranRuleType')
            and   type = 'U')
   drop table LB_TranRuleType
go

/*==============================================================*/
/* Table: LB_GetReportDate                                      */
/*==============================================================*/
create table LB_GetReportDate (
   ReportDateID         bigint               not null,
   CName                nvarchar(100)        null,
   Datememo             nvarchar(100)        null,
   ReportDateDesc       nvarchar(4000)       null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_GETREPORTDATE primary key (ReportDateID)
)
go

/*==============================================================*/
/* Table: LB_GetReportDateItem                                  */
/*==============================================================*/
create table LB_GetReportDateItem (
   ReportDateRuleID     bigint               not null,
   ReportDateID         bigint               null,
   BeginWeekDay         int                  null,
   EndWeekDay           int                  null,
   BeginTime            datetime             null,
   EndTime              datetime             null,
   GetValue             nvarchar(200)        null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_GETREPORTDATEITEM primary key (ReportDateRuleID)
)
go

/*==============================================================*/
/* Table: LB_ParItemSplit                                       */
/*==============================================================*/
create table LB_ParItemSplit (
   ParItemSplitID       bigint               not null,
   ParItemID            bigint               not null,
   ItemID               bigint               not null,
   NewBarCode           int                  not null,
   SamplingGroupNo      int                  null,
   IsAutoUnion          bit                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_PARITEMSPLIT primary key (ParItemSplitID)
)
go

/*==============================================================*/
/* Table: LB_PhrasesWatch                                       */
/*==============================================================*/
create table LB_PhrasesWatch (
   RefuseID             bigint               not null,
   CName                nvarchar(100)        null,
   SName                nvarchar(50)         null,
   SCode                nvarchar(50)         null,
   PhrasesType          int                  null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_PHRASESWATCH primary key (RefuseID)
)
go

/*==============================================================*/
/* Table: LB_PhrasesWatchClass                                  */
/*==============================================================*/
create table LB_PhrasesWatchClass (
   PhrasesWatchClassID  bigint               not null,
   CName                nvarchar(100)        null,
   EName                nvarchar(100)        null,
   ShortCode            nvarchar(50)         null,
   Memo                 varchar(50)          null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_PHRASESWATCHCLASS primary key (PhrasesWatchClassID)
)
go

/*==============================================================*/
/* Table: LB_PhrasesWatchClassItem                              */
/*==============================================================*/
create table LB_PhrasesWatchClassItem (
   PhrasesWatchClassItemID bigint               not null,
   PhrasesWatchClassID  bigint               null,
   RefuseID             bigint               null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_PHRASESWATCHCLASSITEM primary key (PhrasesWatchClassItemID)
)
go

/*==============================================================*/
/* Table: LB_ReportDateItem                                     */
/*==============================================================*/
create table LB_ReportDateItem (
   ReportDateItemID     bigint               not null,
   ReportDateID         bigint               null,
   ItemID               bigint               null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_REPORTDATEITEM primary key (ReportDateItemID)
)
go

/*==============================================================*/
/* Table: LB_SampleItem                                         */
/*==============================================================*/
create table LB_SampleItem (
   SampleItemID         bigint               not null,
   SampleTypeID         bigint               null,
   ItemID               bigint               null,
   DataUpdateTime       datetime             null,
   DataAddTime          datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SAMPLEITEM primary key (SampleItemID)
)
go

/*==============================================================*/
/* Table: LB_SamplingChargeItem                                 */
/*==============================================================*/
create table LB_SamplingChargeItem (
   SamplingChargeItemID bigint               not null,
   SamplingGroupID      bigint               null,
   ItemID               bigint               null,
   ChargeTimes          int                  null,
   IsBatchCharge        bit                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SAMPLINGCHARGEITEM primary key (SamplingChargeItemID)
)
go

/*==============================================================*/
/* Table: LB_SamplingGroup                                      */
/*==============================================================*/
create table LB_SamplingGroup (
   SamplingGroupID      bigint               not null,
   CName                nvarchar(50)         null,
   SampleTypeID         bigint               null,
   CuveteID             bigint               null,
   SuperGroupID         bigint               null,
   SName                nvarchar(50)         null,
   SCode                nvarchar(50)         null,
   Destination          nvarchar(50)         null,
   Capability           decimal(18,2)        null,
   Unit                 nvarchar(50)         null,
   MinCapability        decimal(18,2)        null,
   Synopsis             nvarchar(50)         null,
   PrintCount           int                  null,
   AffixTubeFlag        nvarchar(50)         null,
   VirtualNo            int                  null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SAMPLINGGROUP primary key (SamplingGroupID)
)
go

/*==============================================================*/
/* Table: LB_SamplingItem                                       */
/*==============================================================*/
create table LB_SamplingItem (
   SamplingItemID       bigint               not null,
   SamplingGroupID      bigint               null,
   ItemID               bigint               null,
   IsDefault            bit                  null,
   MinItemCount         int                  null,
   MustItem             int                  null,
   VirtualItemNo        int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SAMPLINGITEM primary key (SamplingItemID)
)
go

/*==============================================================*/
/* Table: LB_Tcuvete                                            */
/*==============================================================*/
create table LB_Tcuvete (
   CuveteID             bigint               not null,
   Color                nvarchar(50)         null,
   Capacity             decimal(18,2)        null,
   Synopsis             nvarchar(50)         null,
   Unit                 nvarchar(50)         null,
   CName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   Code                 nvarchar(50)         null,
   SCode                nvarchar(50)         null,
   minCapability        decimal(18,2)        null,
   ColorValue           int                  null,
   IsPrep               bit                  null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_TCUVETE primary key (CuveteID)
)
go

/*==============================================================*/
/* Table: LB_TranRule                                           */
/*==============================================================*/
create table LB_TranRule (
   TranRuleID           bigint               not null,
   TranRuleTypeID       bigint               null,
   SectionID            bigint               null,
   SampleNoMin          nvarchar(100)        null,
   SampleNoMax          nvarchar(100)        null,
   SampleNoRule         nvarchar(100)        null,
   NextSampleNo         nvarchar(100)        null,
   IsFollow             bit                  null,
   UrgentState          nvarchar(100)        null,
   UseTimeMin           DATETIME             null,
   UseTimeMax           DATETIME             null,
   SickTypeID           bigint               null,
   TestTypeID           bigint               null,
   SampleTypeID         bigint               null,
   SamplingGroupID      bigint               null,
   DispenseMemo         nvarchar(100)        null,
   ReceiveDateMin       int                  null,
   IsAutoPrint          bit                  null,
   ReNextNo             nvarchar(100)        null,
   PrintCount           int                  null,
   IsUseNextNo          bit                  null,
   ResetTime            DATETIME             null,
   ReceiveWeek          nvarchar(100)        null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_TRANRULE primary key (TranRuleID)
)
go

/*==============================================================*/
/* Table: LB_TranRuleItem                                       */
/*==============================================================*/
create table LB_TranRuleItem (
   TranRuleItemID       bigint               not null,
   TranRuleID           bigint               null,
   ItemID               bigint               null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_TRANRULEITEM primary key (TranRuleItemID)
)
go

/*==============================================================*/
/* Table: LB_TranRuleType                                       */
/*==============================================================*/
create table LB_TranRuleType (
   LB_TranRuleTypeID    bigint               not null,
   CName                nvarchar(100)        null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_TRANRULETYPE primary key (LB_TranRuleTypeID)
)
go
