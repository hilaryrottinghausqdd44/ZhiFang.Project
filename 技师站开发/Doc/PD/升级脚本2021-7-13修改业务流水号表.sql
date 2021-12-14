if exists (select 1
            from  sysobjects
           where  id = object_id('LB_TGetMaxNo')
            and   type = 'U')
   drop table LB_TGetMaxNo
go

/*==============================================================*/
/* Table: LB_TGetMaxNo                                          */
/*==============================================================*/
create table LB_TGetMaxNo (
   BmsTypeItemID        bigint               not null,
   LabID                bigint               not null,
   BmsTypeID            bigint               null,
   BmsType              nvarchar(100)        not null,
   BmsDate              datetime             not null,
   MaxID                ntext                not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_TGETMAXNO_BmsTypeItemID primary key (BmsTypeItemID),
   constraint AK_KEY_LB_TGetMaxNo unique (LabID, BmsTypeID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LB_TGetMaxNo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '业务流水号表获取流水号', 
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_TGetMaxNo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'LabID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实验室ID',
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_TGetMaxNo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BmsType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'BmsType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类型ID',
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'BmsType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_TGetMaxNo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BmsDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'BmsDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名称',
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'BmsDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_TGetMaxNo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MaxID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'MaxID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'MaxID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_TGetMaxNo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'DataAddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_TGetMaxNo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'DataUpdateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数据更新时间',
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LB_TGetMaxNo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'DataTimeStamp'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', @CurrentUser, 'table', 'LB_TGetMaxNo', 'column', 'DataTimeStamp'
go
