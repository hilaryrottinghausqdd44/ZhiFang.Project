if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_OperateASection') and o.name = 'FK_LIS_OperateASection_OperateA')
alter table Lis_OperateASection
   drop constraint FK_LIS_OperateASection_OperateA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Lis_OperateASection') and o.name = 'FK_LIS_OperateASection_Section')
alter table Lis_OperateASection
   drop constraint FK_LIS_OperateASection_Section
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_OperateASection')
            and   type = 'U')
   drop table Lis_OperateASection
go

/*==============================================================*/
/* Table: Lis_OperateASection                                   */
/*==============================================================*/
create table Lis_OperateASection (
   LabID                bigint               not null,
   OperateASectionID    bigint               not null,
   OperateAuthorizeID   bigint               not null,
   SectionID            bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LIS_OPERATEASECTION primary key (OperateASectionID),
   constraint FK_LIS_OperateASection_OperateA foreign key (OperateAuthorizeID)
      references Lis_OperateAuthorize (OperateAuthorizeID),
   constraint FK_LIS_OperateASection_Section foreign key (SectionID)
      references LB_Section (SectionID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Lis_OperateASection') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Lis_OperateASection' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Lis_OperateASection 操作授权对应小组', 
   'user', @CurrentUser, 'table', 'Lis_OperateASection'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_OperateASection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'LabID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实验室ID',
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_OperateASection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OperateASectionID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'OperateASectionID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作授权小组ID',
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'OperateASectionID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_OperateASection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OperateAuthorizeID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'OperateAuthorizeID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作授权ID',
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'OperateAuthorizeID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_OperateASection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SectionID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'SectionID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '小组ID',
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'SectionID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_OperateASection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'DataAddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_OperateASection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'DataUpdateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数据更新时间',
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Lis_OperateASection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'DataTimeStamp'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', @CurrentUser, 'table', 'Lis_OperateASection', 'column', 'DataTimeStamp'
go
