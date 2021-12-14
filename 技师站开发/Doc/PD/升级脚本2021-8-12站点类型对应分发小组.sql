if exists (select 1
            from  sysobjects
           where  id = object_id('LB_TranRuleHostSection')
            and   type = 'U')
   drop table LB_TranRuleHostSection
go

/*==============================================================*/
/* Table: LB_TranRuleHostSection                                */
/*==============================================================*/
create table LB_TranRuleHostSection (
   HostSectionID        bigint               not null,
   HostTypeID           bigint               null,
   SectionID            bigint               null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_TRANRULEHOSTSECTION primary key (HostSectionID)
)
go