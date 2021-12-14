if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_Host')
            and   type = 'U')
   drop table dbo.B_Host
go

/*==============================================================*/
/* Table: B_Host                                                */
/*==============================================================*/
create table dbo.B_Host (
   LabID                bigint               not null,
   HostID               bigint               not null,
   HostName             nvarchar(200)        null,
   ShortCode            nvarchar(100)        null,
   IPAddress            nvarchar(50)         null,
   HostDesc             nvarchar(200)        null,
   DispOrder            int                  null,
   HostTypeID           bigint               null,
   HostType             nvarchar(200)        null,
   DeptID               bigint               null,
   PinYinZiTou          nvarchar(100)        null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_HOST primary key (HostID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_Host') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_Host' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '站点表', 
   'user', 'dbo', 'table', 'B_Host'
go
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.B_HostTypePara') and o.name = 'FK_B_HOSTTY_REFERENCE_B_PARA')
alter table dbo.B_HostTypePara
   drop constraint FK_B_HOSTTY_REFERENCE_B_PARA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.B_ParaGroupItem') and o.name = 'FK_B_PARAGR_REFERENCE_B_PARA')
alter table dbo.B_ParaGroupItem
   drop constraint FK_B_PARAGR_REFERENCE_B_PARA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.B_SectionPara') and o.name = 'FK_B_SECTIO_REFERENCE_B_PARA')
alter table dbo.B_SectionPara
   drop constraint FK_B_SECTIO_REFERENCE_B_PARA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.B_UserPara') and o.name = 'FK_B_USERPA_REFERENCE_B_PARA')
alter table dbo.B_UserPara
   drop constraint FK_B_USERPA_REFERENCE_B_PARA
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
   Shortcode            nvarchar(100)        null,
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
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.B_ParaGroupItem') and o.name = 'FK_B_PARAGR_REFERENCE_B_PARAGR')
alter table dbo.B_ParaGroupItem
   drop constraint FK_B_PARAGR_REFERENCE_B_PARAGR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_ParaGroup')
            and   type = 'U')
   drop table dbo.B_ParaGroup
go

/*==============================================================*/
/* Table: B_ParaGroup                                           */
/*==============================================================*/
create table dbo.B_ParaGroup (
   LabID                bigint               not null,
   ParaGroupID          bigint               not null,
   ParaGroupNo          nvarchar(100)        null,
   CName                nvarchar(200)        null,
   SName                nvarchar(200)        null,
   ParaGroupDesc        nvarchar(2000)       null,
   Shortcode            nvarchar(100)        null,
   DispOrder            int                  null,
   PinYinZiTou          nvarchar(100)        null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_PARAGROUP primary key (ParaGroupID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_ParaGroup') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_ParaGroup' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '系统参数分组表，用于成组归类参数，方便维护', 
   'user', 'dbo', 'table', 'B_ParaGroup'
go
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_ParaGroupItem')
            and   type = 'U')
   drop table dbo.B_ParaGroupItem
go

/*==============================================================*/
/* Table: B_ParaGroupItem                                       */
/*==============================================================*/
create table dbo.B_ParaGroupItem (
   LabID                bigint               not null,
   ParaGroupItemID      bigint               not null,
   ParaID               bigint               null,
   ParaGroupID          bigint               null,
   DispOrder            bigint               null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_PARAGROUPITEM primary key (ParaGroupItemID),
   constraint FK_B_PARAGR_REFERENCE_B_PARAGR foreign key (ParaGroupID)
      references dbo.B_ParaGroup (ParaGroupID),
   constraint FK_B_PARAGR_REFERENCE_B_PARA foreign key (ParaID)
      references dbo.B_Para (ParaID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_ParaGroupItem') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_ParaGroupItem' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '参数分组对应参数', 
   'user', 'dbo', 'table', 'B_ParaGroupItem'
go
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.B_HostTypePara') and o.name = 'FK_B_HOSTTY_REFERENCE_B_HOSTTY')
alter table dbo.B_HostTypePara
   drop constraint FK_B_HOSTTY_REFERENCE_B_HOSTTY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_HostType')
            and   type = 'U')
   drop table dbo.B_HostType
go

/*==============================================================*/
/* Table: B_HostType                                            */
/*==============================================================*/
create table dbo.B_HostType (
   LabID                bigint               not null,
   HostTypeID           bigint               not null,
   CName                nvarchar(200)        null,
   SName                nvarchar(200)        null,
   Shortcode            nvarchar(100)        null,
   PinYinZiTou          nvarchar(100)        null,
   HostTypeDesc         nvarchar(2000)       null,
   DispOrder            int                  null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_HOSTTYPE primary key (HostTypeID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_HostType') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_HostType' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '站点类型表', 
   'user', 'dbo', 'table', 'B_HostType'
go
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_HostTypePara')
            and   type = 'U')
   drop table dbo.B_HostTypePara
go

/*==============================================================*/
/* Table: B_HostTypePara                                        */
/*==============================================================*/
create table dbo.B_HostTypePara (
   LabID                bigint               not null,
   HostTypeParaID       bigint               not null,
   ParaID               bigint               null,
   HostTypeID           bigint               null,
   ParaValue            nvarchar(Max)        null,
   ParaDesc             nvarchar(2000)       null,
   DispOrder            bigint               null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_HOSTTYPEPARA primary key (HostTypeParaID),
   constraint FK_B_HOSTTY_REFERENCE_B_HOSTTY foreign key (HostTypeID)
      references dbo.B_HostType (HostTypeID),
   constraint FK_B_HOSTTY_REFERENCE_B_PARA foreign key (ParaID)
      references dbo.B_Para (ParaID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_HostTypePara') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_HostTypePara' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '站点类型和参数关系', 
   'user', 'dbo', 'table', 'B_HostTypePara'
go
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_SectionPara')
            and   type = 'U')
   drop table dbo.B_SectionPara
go

/*==============================================================*/
/* Table: B_SectionPara                                         */
/*==============================================================*/
create table dbo.B_SectionPara (
   LabID                bigint               not null,
   SectionParaID        bigint               not null,
   SectionID            bigint               null,
   ParaID               bigint               null,
   ParaValue            nvarchar(Max)        null,
   ParaDesc             nvarchar(2000)       null,
   DispOrder            int                  null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_SECTIONPARA primary key (SectionParaID),
   constraint FK_B_SECTIO_REFERENCE_B_PARA foreign key (ParaID)
      references dbo.B_Para (ParaID),
   constraint FK_B_SECTIO_REFERENCE_LB_SECTI foreign key (SectionID)
      references LB_Section (SectionID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_SectionPara') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_SectionPara' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '检验小组和参数关系', 
   'user', 'dbo', 'table', 'B_SectionPara'
go
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_UserPara')
            and   type = 'U')
   drop table dbo.B_UserPara
go

/*==============================================================*/
/* Table: B_UserPara                                            */
/*==============================================================*/
create table dbo.B_UserPara (
   LabID                bigint               not null,
   UserParaID           bigint               not null,
   UserID               bigint               null,
   ParaID               bigint               null,
   ParaValue            nvarchar(Max)        null,
   ParaDesc             nvarchar(2000)       null,
   DispOrder            int                  null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_USERPARA primary key (UserParaID),
   constraint FK_B_USERPA_REFERENCE_RBAC_USE foreign key (UserID)
      references dbo.RBAC_User (UserID),
   constraint FK_B_USERPA_REFERENCE_B_PARA foreign key (ParaID)
      references dbo.B_Para (ParaID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_UserPara') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_UserPara' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '人员和参数关系', 
   'user', 'dbo', 'table', 'B_UserPara'
go
