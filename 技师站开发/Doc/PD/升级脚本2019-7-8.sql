if exists (select 1
            from  sysobjects
           where  id = object_id('LB_ItemRangeExp')
            and   type = 'U')
   drop table LB_ItemRangeExp
go

/*==============================================================*/
/* Table: LB_ItemRangeExp                                       */
/*==============================================================*/
create table LB_ItemRangeExp (
   ItemRangeExpID       bigint               not null,
   ItemID               bigint               not null,
   JudgeType            int                  null,
   JudgeValue           nvarchar(300)        null,
   ResultStatus         nvarchar(20)         null,
   ResultReport         nvarchar(500)        null,
   ResultComment        nvarchar(1000)       null,
   IsAddReport          bit                  null,
   expReport            int                  null,
   expComment           nvarchar(500)        null,
   AlarmColor           int                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemRangeExp primary key (ItemRangeExpID)
)
go
