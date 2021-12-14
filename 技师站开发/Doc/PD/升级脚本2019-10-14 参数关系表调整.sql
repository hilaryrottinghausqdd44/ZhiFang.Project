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
   ParaID               bigint               null,
   ParaValue            nvarchar(Max)        null,
   ParaDesc             nvarchar(2000)       null,
   OrderInfo            nvarchar(2000)       null,
   EmpID                bigint               null,
   EmpName              nvarchar(200)        null,
   HostTypeID           bigint               null,
   HostTypeName         nvarchar(200)        null,
   SectionID            bigint               null,
   SectionName          nvarchar(200)        null,
   DispOrder            int                  null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_PARAITEM primary key (ParaItemID),
   constraint FK_B_PARAIT_REFERENCE_B_PARA foreign key (ParaID)
      references dbo.B_Para (ParaID)
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
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_HostTypeUser')
            and   type = 'U')
   drop table dbo.B_HostTypeUser
go

/*==============================================================*/
/* Table: B_HostTypeUser                                        */
/*==============================================================*/
create table dbo.B_HostTypeUser (
   LabID                bigint               not null,
   HostTypeUserID       bigint               not null,
   HostTypeID           bigint               null,
   EmpID                bigint               null,
   DispOrder            int                  null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_HOSTTYPEUSER primary key (HostTypeUserID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_HostTypeUser') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_HostTypeUser' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '站点类型和人员关系', 
   'user', 'dbo', 'table', 'B_HostTypeUser'
go
