if exists (select 1
            from  sysobjects
           where  id = object_id('LB_TGetMaxNo')
            and   type = 'U')
   drop table LB_TGetMaxNo
go

/*==============================================================*/
/* Table: LB_TGetMaxNo                                          */
/*==============================================================*/
create table LB_TGetMaxNo (
   LabID                bigint               not null,
   BmsType              nvarchar(100)        not null,
   BmsDate              datetime             not null,
   MaxID                ntext                not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_TGETMAXNO primary key (LabID, BmsType, BmsDate)
)
go
