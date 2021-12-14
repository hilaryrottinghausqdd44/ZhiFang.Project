if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SamplingItem')
            and   type = 'U')
   drop table LB_SamplingItem
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
   IsMustItem           int                  null,
   VirtualItemNo        int                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SAMPLINGITEM primary key (SamplingItemID),
   constraint FK_LB_SAMPL_REFERENCE_LB_SAMPL foreign key (SamplingGroupID)
      references LB_SamplingGroup (SamplingGroupID),
   constraint FK_LB_SAMPL_REFERENCE_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go
if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SamplingGroup')
            and   type = 'U')
   drop table LB_SamplingGroup
go

/*==============================================================*/
/* Table: LB_SamplingGroup                                      */
/*==============================================================*/
create table LB_SamplingGroup (
   SamplingGroupID      bigint               not null,
   CName                nvarchar(50)         null,
   SampleTypeID         bigint               null,
   CuveteID             bigint               null,
   SpecialtyID          bigint               null,
   SName                nvarchar(50)         null,
   SCode                nvarchar(50)         null,
   DestinationID        bigint               null,
   Synopsis             nvarchar(50)         null,
   PrintCount           int                  null,
   AffixTubeFlag        nvarchar(50)         null,
   PrepInfo             nvarchar(300)        null,
   VirtualNo            int                  null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SAMPLINGGROUP primary key (SamplingGroupID),
   constraint FK_LB_SAMPL_REFERENCE_LB_TCUVE foreign key (CuveteID)
      references LB_Tcuvete (CuveteID)
)
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ReportDate')
            and   type = 'U')
   drop table LB_ReportDate
go

/*==============================================================*/
/* Table: LB_ReportDate                                         */
/*==============================================================*/
create table LB_ReportDate (
   ReportDateID         bigint               not null,
   CName                nvarchar(100)        null,
   DateMemo             nvarchar(100)        null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_REPORTDATE primary key (ReportDateID)
)
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ReportDateDesc')
            and   type = 'U')
   drop table LB_ReportDateDesc
go

/*==============================================================*/
/* Table: LB_ReportDateDesc                                     */
/*==============================================================*/
create table LB_ReportDateDesc (
   ReportDateDescID     bigint               not null,
   ReportDateID         bigint               not null,
   ReportDateDesc       nvarchar(max)        null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_REPORTDATEDESC primary key (ReportDateDescID) 
)
go
if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ReportDateRule')
            and   type = 'U')
   drop table LB_ReportDateRule
go

/*==============================================================*/
/* Table: LB_ReportDateRule                                     */
/*==============================================================*/
create table LB_ReportDateRule (
   ReportDateRuleID     bigint               not null,
   ReportDateDescID     bigint               null,
   BeginWeekDay         int                  null,
   EndWeekDay           int                  null,
   BeginTime            datetime             null,
   EndTime              datetime             null,
   GetValue             nvarchar(200)        null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_REPORTDATERULE primary key (ReportDateRuleID) 
)
go
