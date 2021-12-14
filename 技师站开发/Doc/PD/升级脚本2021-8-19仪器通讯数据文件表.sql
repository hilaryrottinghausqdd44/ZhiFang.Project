if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_EquipComFile')
            and   type = 'U')
   drop table Lis_EquipComFile
go

/*==============================================================*/
/* Table: Lis_EquipComFile                                      */
/*==============================================================*/
create table Lis_EquipComFile (
   LabID                bigint               not null,
   ComFileID            bigint               not null,
   ComFileComment       ntext                null,
   ComFileResultCount   int                  null,
   ComFileResultType    nvarchar(100)        null,
   ComFileTime          datetime             null,
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
   constraint PK_Lis_EquipComFile primary key (ComFileID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Lis_EquipComFile') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Lis_EquipComFile' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '仪器通讯数据文件', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'LabID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实验室ID',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ComFileID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通讯数据文件ID',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ComFileComment')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileComment'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通讯数据文件内容',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileComment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ComFileResultCount')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileResultCount'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通讯数据文记录数',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileResultCount'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ComFileResultType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileResultType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通讯数据文件类型',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileResultType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ComFileTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通讯数据文件时间',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ComFileTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EquipID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'EquipID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '仪器ID',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'EquipID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EquipName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'EquipName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '仪器名称',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'EquipName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SectionID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'SectionID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '小组ID',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'SectionID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SectionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'SectionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '小组名称',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'SectionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientComputer')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ClientComputer'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户端计算机名',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ClientComputer'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientMac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ClientMac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户端网卡号',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ClientMac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientIP')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ClientIP'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户端IP',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'ClientIP'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'DataAddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'DataUpdateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数据更新时间',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_EquipComFile')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'DataTimeStamp'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', @CurrentUser, 'table', 'Lis_EquipComFile', 'column', 'DataTimeStamp'
go
