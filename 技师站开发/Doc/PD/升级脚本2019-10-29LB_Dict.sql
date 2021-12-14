create table LB_Dict (
   LabID                bigint               not null,
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
   ColorValue           nvarchar(50)         null,
   ColorDefault         nvarchar(50)         null,
   DictValue            nvarchar(50)         null,
   DictValueDefault     nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_DICT primary key nonclustered (DictID),
   constraint FK_LB_DICT_DICTType foreign key (DictTypeID)
      references LB_DictType (DictTypeID)
)
go