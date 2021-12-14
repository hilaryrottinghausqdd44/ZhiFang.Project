if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_Para')
            and   type = 'U')
   drop table dbo.B_Para
go

create table dbo.B_Para (
   LabID                bigint               not null,
   ParaID               bigint               not null,
   ParaNo               nvarchar(100)        null,
   CName                nvarchar(200)        null,
   SName                nvarchar(200)        null,
   TypeCode             nvarchar(100)        null,
   ParaType             nvarchar(100)        null,
   ParaValue            nvarchar(Max)        null,
   ParaDesc             nvarchar(2000)       null,
   ParaEditInfo         nvarchar(Max)        null,
   SystemCode           nvarchar(200)        null,
   ShortCode            nvarchar(100)        null,
   DispOrder            int                  null,
   PinYinZiTou          nvarchar(100)        null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_PARA primary key (ParaID)
)
go

  alter table LIS_TESTItem Add constraint FK_LIS_TESTItem_PITEMID foreign key (PItemID)
      references dbo.LB_Item (ItemID)