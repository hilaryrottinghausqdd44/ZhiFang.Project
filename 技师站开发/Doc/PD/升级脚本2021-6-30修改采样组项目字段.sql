if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LB_SamplingItem') and o.name = 'FK_LB_SAMPL_REFERENCE_LB_SAMPL')
alter table LB_SamplingItem
   drop constraint FK_LB_SAMPL_REFERENCE_LB_SAMPL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LB_SamplingItem') and o.name = 'FK_LB_SAMPL_REFERENCE_LB_ITEM')
alter table LB_SamplingItem
   drop constraint FK_LB_SAMPL_REFERENCE_LB_ITEM
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_SamplingItem')
            and   type = 'U')
   drop table LB_SamplingItem
go

/*==============================================================*/
/* Table: LB_SamplingItem                                       */
/*==============================================================*/
create table LB_SamplingItem (
   SamplingItemID       bigint               not null,
   SamplingGroupID      bigint               null,
   ItemID               bigint               null,
   IsDefault            bit                  null,
   MinItemCount         int                  null,
   MustItemID           bigint               null,
   VirtualItemNo        int                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_SAMPLINGITEM primary key (SamplingItemID),
   constraint FK_LB_SAMPL_REFERENCE_LB_SAMPL foreign key (SamplingGroupID)
      references LB_SamplingGroup (SamplingGroupID),
   constraint FK_LB_SAMPL_REFERENCE_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_SamplingItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_SamplingItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '采样组项目', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SamplingItemID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'SamplingItemID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采样组项目ID',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'SamplingItemID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SamplingGroupID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'SamplingGroupID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '采样组ID',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'SamplingGroupID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ItemID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'ItemID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '项目ID',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'ItemID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDefault')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'IsDefault'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否缺省',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'IsDefault'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MinItemCount')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'MinItemCount'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最小项目数',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'MinItemCount'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MustItemID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'MustItemID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '必须项目',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'MustItemID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'VirtualItemNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'VirtualItemNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '虚拟项目数',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'VirtualItemNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'DispOrder'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '优先次序',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'LabID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实验室ID',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'DataAddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'DataUpdateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数据更新时间',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_SamplingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'DataTimeStamp'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', @CurrentUser, 'table', 'LB_SamplingItem', 'column', 'DataTimeStamp'
go
