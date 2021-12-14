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
   ParaEditInfo         nvarchar(Max)        null,
   ShortCode            nvarchar(100)        null,
   DispOrder            int                  null,
   PinYinZiTou          nvarchar(100)        null,
   ParaLevel            int                  null,
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
