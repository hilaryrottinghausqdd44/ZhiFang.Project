if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_EquipComLog') and o.name = 'FK_LisEquipComLog_LisEquipComFile')
alter table Lis_EquipComLog
   drop constraint FK_LisEquipComLog_LisEquipComFile
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_EquipComLog')
            and   type = 'U')
   drop table Lis_EquipComLog
go

/*==============================================================*/
/* Table: Lis_EquipComLog                                       */
/*==============================================================*/
create table Lis_EquipComLog (
   LabID                bigint               not null,
   ComLogID             bigint               not null,
   ComFileID            bigint               null,
   ComLogType           int                  null,
   ComLogInfo           ntext                null,
   ComLogTime           datetime             null,
   EquipID              bigint               null,
   EquipName            nvarchar(100)        null,
   SectionID            bigint               null,
   SectionName          nvarchar(100)        null,
   ClientComputer       nvarchar(100)        null,
   ClientMac            nvarchar(100)        null,
   ClientIP             nvarchar(100)        null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_EquipComLog primary key (ComLogID),
   constraint FK_LisEquipComLog_LisEquipComFile foreign key (ComFileID)
      references Lis_EquipComFile (ComFileID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Lis_EquipComLog') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Lis_EquipComLog' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '仪器通讯数据详细日志', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'LabID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实验室ID',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ComLogID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'ComLogID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日志ID',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'ComLogID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EquipName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'EquipName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '仪器名称',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'EquipName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SectionID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'SectionID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '小组ID',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'SectionID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SectionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'SectionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '小组名称',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'SectionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientComputer')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'ClientComputer'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户端计算机名',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'ClientComputer'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientMac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'ClientMac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户端网卡号',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'ClientMac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientIP')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'ClientIP'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户端IP',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'ClientIP'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'DataAddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'DataUpdateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数据更新时间',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'DataTimeStamp'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', @CurrentUser, 'table', 'Lis_EquipComLog', 'column', 'DataTimeStamp'
go
