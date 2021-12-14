drop table dbo.B_ParaItem
drop table dbo.B_Para


create table dbo.B_Para (
   LabID                bigint               not null,
   ParaID               bigint               not null,
   ParaNo               nvarchar(100)        null,
   CName                nvarchar(200)        null,
   SName                nvarchar(200)        null,
   TypeCode             nvarchar(100)        null,
   ParaType             nvarchar(100)        null,
   ParaGroup            nvarchar(100)        null,
   ParaValue            nvarchar(Max)        null,
   ParaDesc             nvarchar(2000)       null,
   ParaEditInfo         nvarchar(Max)        null,
   SystemCode           nvarchar(200)        null,
   ShortCode            nvarchar(100)        null,
   DispOrder            int                  null,
   PinYinZiTou          nvarchar(100)        null,
   bVisible             bit                  null,
   IsUse                bit                  null,
   ParaVER              nvarchar(50)         null,
   OperaterID           bigint               null,
   Operater             nvarchar(50)         null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_PARA primary key (ParaID)
)
go



create table dbo.B_ParaItem (
   LabID                bigint               not null,
   ParaItemID           bigint               not null,
   ParaID               bigint               null,
   ParaNo               nvarchar(100)        null,
   ObjectID             bigint               null,
   ObjectName           nvarchar(200)        null,
   ParaValue            nvarchar(Max)        null,
   DispOrder            int                  null,
   OperaterID           bigint               null,
   Operater             nvarchar(50)         null,
   FinalOperaterID      bigint               null,
   FinalOperater        nvarchar(50)         null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_PARAITEM primary key (ParaItemID),
   constraint FK_B_PARAIT_REFERENCE_B_PARA foreign key (ParaID)
      references dbo.B_Para (ParaID)
)
go