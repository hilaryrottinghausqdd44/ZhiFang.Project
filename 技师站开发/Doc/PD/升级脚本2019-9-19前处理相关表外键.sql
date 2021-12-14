/*==============================================================*/
/* 包括取单时间、采样组、拒收短语、项目对应样本类型、分发规则等基础表  */                                 
/*==============================================================*/

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ReportDateItem')
            and   type = 'U')
   drop table LB_ReportDateItem
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
   constraint PK_LB_REPORTDATEITEM primary key (ReportDateItemID),
   constraint FK_LB_REPOR_REFERENCE_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID),
   constraint FK_LB_REPOR_REFERENCE_LB_GETRE foreign key (ReportDateID)
      references LB_GetReportDate (ReportDateID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_ReportDateItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_ReportDateItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '取单分类项目明细', 
   'user', @CurrentUser, 'table', 'LB_ReportDateItem'
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LB_SamplingItem') and o.name = 'FK_LB_SAMPL_REFERENCE_LB_SAMPL')
alter table LB_SamplingItem
   drop constraint FK_LB_SAMPL_REFERENCE_LB_SAMPL
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
   SuperGroupID         bigint               null,
   SName                nvarchar(50)         null,
   SCode                nvarchar(50)         null,
   Destination          nvarchar(50)         null,
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

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_SamplingGroup') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_SamplingGroup' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '采样组', 
   'user', @CurrentUser, 'table', 'LB_SamplingGroup'
go

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
   MustItem             int                  null,
   VirtualItemNo        int                  null,
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

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_SamplingItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_SamplingItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '采样组项目', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem'
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LB_TranRuleItem') and o.name = 'FK_LB_TRANR_REFERENCE_LB_TRANR')
alter table LB_TranRuleItem
   drop constraint FK_LB_TRANR_REFERENCE_LB_TRANR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_TranRule')
            and   type = 'U')
   drop table LB_TranRule
go

/*==============================================================*/
/* Table: LB_TranRule                                           */
/*==============================================================*/
create table LB_TranRule (
   TranRuleID           bigint               not null,
   LB_TranRuleTypeID    bigint               null,
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
   constraint PK_LB_TRANRULE primary key (TranRuleID),
   constraint FK_LB_TRANR_REFERENCE_LB_TRANR foreign key (LB_TranRuleTypeID)
      references LB_TranRuleType (LB_TranRuleTypeID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_TranRule') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_TranRule' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '分发规则', 
   'user', @CurrentUser, 'table', 'LB_TranRule'
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_TranRuleItem')
            and   type = 'U')
   drop table LB_TranRuleItem
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
   constraint PK_LB_TRANRULEITEM primary key (TranRuleItemID),
   constraint FK_LB_TRANR_REFERENCE_TRANR foreign key (TranRuleID)
      references LB_TranRule (TranRuleID),
   constraint FK_LB_TRANR_REFERENCE_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_TranRuleItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_TranRuleItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '分发规则项目明细', 
   'user', @CurrentUser, 'table', 'LB_TranRuleItem'
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_PhrasesWatchClassItem')
            and   type = 'U')
   drop table LB_PhrasesWatchClassItem
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
   constraint PK_LB_PHRASESWATCHCLASSITEM primary key (PhrasesWatchClassItemID),
   constraint FK_LB_PHRAS_REFERENCE_PHRAS foreign key (PhrasesWatchClassID)
      references LB_PhrasesWatchClass (PhrasesWatchClassID),
   constraint FK_LB_PHRAS_REFERENCE_LB_PHRAS foreign key (RefuseID)
      references LB_PhrasesWatch (RefuseID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_PhrasesWatchClassItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_PhrasesWatchClassItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '拒收让步短语类型明细', 
   'user', @CurrentUser, 'table', 'LB_PhrasesWatchClassItem'
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SampleItem')
            and   type = 'U')
   drop table LB_SampleItem
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
   constraint PK_LB_SAMPLEITEM primary key (SampleItemID),
   constraint FK_LB_SAMPL_REFERENCE_SAMPLE foreign key (SampleTypeID)
      references LB_SampleType (SampleTypeID),
   constraint FK_LB_SAMPL_REFERENCE_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_SampleItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_SampleItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '项目样本类型', 
   'user', @CurrentUser, 'table', 'LB_SampleItem'
go

