if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.B_ParaItem') and o.name = 'FK_B_PARAIT_REFERENCE_B_PARA')
alter table dbo.B_ParaItem
   drop constraint FK_B_PARAIT_REFERENCE_B_PARA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_Para')
            and   type = 'U')
   drop table dbo.B_Para
go

/*==============================================================*/
/* Table: B_Para                                                */
/*==============================================================*/
create table dbo.B_Para (
   LabID                bigint               not null,
   ParaID               bigint               not null,
   ParaNo               nvarchar(100)        null,
   CName                nvarchar(200)        null,
   SName                nvarchar(200)        null,
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

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_Para') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_Para' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '系统参数表', 
   'user', 'dbo', 'table', 'B_Para'
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_ParaItem')
            and   type = 'U')
   drop table dbo.B_ParaItem
go

/*==============================================================*/
/* Table: B_ParaItem                                            */
/*==============================================================*/
create table dbo.B_ParaItem (
   LabID                bigint               not null,
   ParaItemID           bigint               not null,
   HostTypeID           bigint               null,
   SectionID            bigint               null,
   UserID               bigint               null,
   ParaID               bigint               null,
   ParaValue            nvarchar(Max)        null,
   ParaDesc             nvarchar(2000)       null,
   OrderInfo            nvarchar(2000)       null,
   DispOrder            int                  null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_PARAITEM primary key (ParaItemID),
   constraint FK_B_PARAIT_REFERENCE_B_PARA foreign key (ParaID)
      references dbo.B_Para (ParaID),
   constraint FK_B_PARAIT_REFERENCE_LB_SECTI foreign key (SectionID)
      references LB_Section (SectionID),
   constraint FK_B_PARAIT_REFERENCE_RBAC_USE foreign key (UserID)
      references dbo.RBAC_User (UserID),
   constraint FK_B_PARAIT_REFERENCE_B_HOSTTY foreign key (HostTypeID)
      references dbo.B_HostType (HostTypeID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_ParaItem') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_ParaItem' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '参数关系', 
   'user', 'dbo', 'table', 'B_ParaItem'
go
