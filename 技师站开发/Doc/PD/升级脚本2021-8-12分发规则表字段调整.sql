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
   CName                nvarchar(100)        null,
   TranRuleTypeID       bigint               null,
   SectionID            bigint               null,
   SampleNoMin          nvarchar(100)        null,
   SampleNoMax          nvarchar(100)        null,
   SampleNoRule         nvarchar(100)        null,
   SampleNoType         int                  null,
   NextSampleNo         nvarchar(100)        null,
   IsUseNextNo          bit                  null,
   IsFollow             bit                  null,
   UrgentState          nvarchar(100)        null,
   UseTimeMin           DATETIME             null,
   UseTimeMax           DATETIME             null,
   SickTypeID           bigint               null,
   TestTypeID           bigint               null,
   SampleTypeID         bigint               null,
   DeptID               bigint               null,
   SamplingGroupID      bigint               null,
   ClientID             bigint               null,
   ResetType            nvarchar(100)        null,
   ResetTime            DATETIME             null,
   TranWeek             nvarchar(100)        null,
   TranToWeek           nvarchar(100)        null,
   TestDelayDates       int                  null,
   IsAutoPrint          bit                  null,
   PrintCount           int                  null,
   IsPrintProce         bit                  null,
   ProceModel           nvarchar(100)        null,
   DispenseMemo         nvarchar(100)        null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_TRANRULE primary key (TranRuleID),
   constraint FK_LB_TRANR_REFERENCE_LB_TRANR foreign key (TranRuleTypeID)
      references LB_TranRuleType (LB_TranRuleTypeID)
)
go