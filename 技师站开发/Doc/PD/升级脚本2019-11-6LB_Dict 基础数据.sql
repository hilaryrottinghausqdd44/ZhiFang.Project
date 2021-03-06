if exists (select 1
            from  sysobjects
           where  id = object_id('LB_Dict')
            and   type = 'U')
   drop table LB_Dict
go

/*==============================================================*/
/* Table: LB_Dict                                               */
/*==============================================================*/
create table LB_Dict (
   LabID                bigint               not null,
   DictID               bigint               not null,
   DictType             nvarchar(50)         null,
   CName                nvarchar(50)         null,
   DictCode             nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   DictInfo             nvarchar(100)        null,
   ilevel               int                  null,
   ColorValue           nvarchar(50)         null,
   ColorDefault         nvarchar(50)         null,
   DictValue            nvarchar(50)         null,
   DictValueDefault     nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   IsDefault            bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_DICT primary key nonclustered (DictID)
)
go