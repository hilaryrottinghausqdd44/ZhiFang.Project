if exists (select 1
            from  sysobjects
           where  id = object_id('LB_DictType')
            and   type = 'U')
   drop table LB_DictType
go

/*==============================================================*/
/* Table: LB_DictType                                           */
/*==============================================================*/
create table LB_DictType (
   DictTypeID           bigint               not null,
   DictTypeCode         nvarchar(100)        not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_DICTTYPE primary key nonclustered (DictTypeID)
)
go


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
   DictID               bigint               not null,
   DictTypeID           bigint               not null,
   CName                nvarchar(50)         null,
   DictCode             nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   DictInfo             nvarchar(100)        null,
   ilevel               int                  null,
   iColor               int                  null,
   iColorDefault        int                  null,
   DictValue            nvarchar(50)         null,
   DictValueDefault     nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_DICT primary key nonclustered (DictID)
)
go