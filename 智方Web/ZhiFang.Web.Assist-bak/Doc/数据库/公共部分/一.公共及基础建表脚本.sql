if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SC_Operation')
            and   type = 'U')
   drop table dbo.SC_Operation
go

create table dbo.SC_Operation (
   LabID                bigint               not null,
   SCOperationID        bigint               not null,
   BobjectID            bigint               not null,
   Type                 bigint               null,
   TypeName             varchar(50)          collate Chinese_PRC_CI_AS null,
   BusinessModuleCode   varchar(50)          collate Chinese_PRC_CI_AS null,
   Memo                 text                 collate Chinese_PRC_CI_AS null,
   IsUse                bit                  null,
   CreatorID            bigint               null,
   CreatorName          varchar(50)          collate Chinese_PRC_CI_AS null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_SC_OPERATION primary key (SCOperationID)
         on "PRIMARY"
)
on "PRIMARY"
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.SC_Operation') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'SC_Operation' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '公共操作记录表
   如申请审核操作，过程记录修改操作等', 
   'user', 'dbo', 'table', 'SC_Operation'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'LabID'

end


execute sp_addextendedproperty 'MS_Description', 
   '平台客户ID',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SCOperationID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'SCOperationID'

end


execute sp_addextendedproperty 'MS_Description', 
   '公共操作记录主键ID',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'SCOperationID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BobjectID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'BobjectID'

end


execute sp_addextendedproperty 'MS_Description', 
   '业务对象ID',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'BobjectID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Type')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'Type'

end


execute sp_addextendedproperty 'MS_Description', 
   '操作类型',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'Type'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TypeName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'TypeName'

end


execute sp_addextendedproperty 'MS_Description', 
   '操作类型名',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'TypeName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BusinessModuleCode')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'BusinessModuleCode'

end


execute sp_addextendedproperty 'MS_Description', 
   '业务模块代码',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'BusinessModuleCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'Memo'

end


execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUse')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'IsUse'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否使用',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'IsUse'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'CreatorID'

end


execute sp_addextendedproperty 'MS_Description', 
   '创建者',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'CreatorID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'CreatorName'

end


execute sp_addextendedproperty 'MS_Description', 
   '创建者姓名',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'CreatorName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'DispOrder'

end


execute sp_addextendedproperty 'MS_Description', 
   '显示次序',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'DataAddTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'DataUpdateTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '数据修改时间',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.SC_Operation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'DataTimeStamp'

end


execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', 'dbo', 'table', 'SC_Operation', 'column', 'DataTimeStamp'
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.B_Dict') and o.name = 'FK_B_Dict_B_DictType')
alter table dbo.B_Dict
   drop constraint FK_B_Dict_B_DictType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_DictType')
            and   type = 'U')
   drop table dbo.B_DictType
go

create table dbo.B_DictType (
   LabID                bigint               null,
   DCId                 bigint               not null,
   DictTypeCode         varchar(100)         collate Chinese_PRC_CI_AS null,
   CName                varchar(40)          collate Chinese_PRC_CI_AS null,
   SName                varchar(80)          collate Chinese_PRC_CI_AS null,
   ShortCode            varchar(40)          collate Chinese_PRC_CI_AS null,
   PinYinZiTou          varchar(50)          collate Chinese_PRC_CI_AS null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   Memo                 ntext                collate Chinese_PRC_CI_AS null,
   DataAddTime          datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_DictType primary key nonclustered (DCId)
         on "PRIMARY"
)
on "PRIMARY"
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_DictType') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_DictType' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '字典类型', 
   'user', 'dbo', 'table', 'B_DictType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'LabID'

end


execute sp_addextendedproperty 'MS_Description', 
   'LabID',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DCId')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DCId'

end


execute sp_addextendedproperty 'MS_Description', 
   '字典类型主键',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DCId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DictTypeCode')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DictTypeCode'

end


execute sp_addextendedproperty 'MS_Description', 
   '类型编码',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DictTypeCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'CName'

end


execute sp_addextendedproperty 'MS_Description', 
   '类型名称',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'CName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'SName'

end


execute sp_addextendedproperty 'MS_Description', 
   '简称',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'SName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ShortCode')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'ShortCode'

end


execute sp_addextendedproperty 'MS_Description', 
   '快捷码',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'ShortCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PinYinZiTou')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'PinYinZiTou'

end


execute sp_addextendedproperty 'MS_Description', 
   '拼音字头',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'PinYinZiTou'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUse')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'IsUse'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否使用',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'IsUse'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DispOrder'

end


execute sp_addextendedproperty 'MS_Description', 
   '显示次序',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'Memo'

end


execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DataAddTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '加入时间',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_DictType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DataTimeStamp'

end


execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DataTimeStamp'
go
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.B_Dict') and o.name = 'FK_B_Dict_B_DictType')
alter table dbo.B_Dict
   drop constraint FK_B_Dict_B_DictType
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_Dict')
            and   type = 'U')
   drop table dbo.B_Dict
go

/*==============================================================*/
/* Table: B_Dict                                                */
/*==============================================================*/
create table dbo.B_Dict (
   LabID                bigint               null,
   DID                  bigint               not null,
   DCId                 bigint               null,
   CName                varchar(80)          collate Chinese_PRC_CI_AS null,
   SName                varchar(80)          collate Chinese_PRC_CI_AS null,
   ShortCode            varchar(40)          collate Chinese_PRC_CI_AS null,
   PinYinZiTou          varchar(50)          collate Chinese_PRC_CI_AS null,
   HisOrderCode         varchar(100)         collate Chinese_PRC_CI_AS null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataTimeStamp        timestamp            null,
   Memo                 ntext                collate Chinese_PRC_CI_AS null,
   constraint PK_B_Dict primary key nonclustered (DID)
         on "PRIMARY"
)
on "PRIMARY"
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_Dict') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_Dict' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '字典信息', 
   'user', 'dbo', 'table', 'B_Dict'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'LabID'

end


execute sp_addextendedproperty 'MS_Description', 
   'LabID',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DID'

end


execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DCId')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DCId'

end


execute sp_addextendedproperty 'MS_Description', 
   '字典类型ID，关联B_DictType',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DCId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'CName'

end


execute sp_addextendedproperty 'MS_Description', 
   '字典名称',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'CName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'SName'

end


execute sp_addextendedproperty 'MS_Description', 
   '简称',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'SName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ShortCode')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'ShortCode'

end


execute sp_addextendedproperty 'MS_Description', 
   '快捷码',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'ShortCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PinYinZiTou')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'PinYinZiTou'

end


execute sp_addextendedproperty 'MS_Description', 
   '拼音字头',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'PinYinZiTou'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HisOrderCode')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'HisOrderCode'

end


execute sp_addextendedproperty 'MS_Description', 
   '对照转换码',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'HisOrderCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUse')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'IsUse'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否使用',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'IsUse'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DispOrder'

end


execute sp_addextendedproperty 'MS_Description', 
   '显示次序',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DataAddTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '加入时间',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DataTimeStamp'

end


execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DataTimeStamp'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'Memo'

end


execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'Memo'
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_Parameter')
            and   type = 'U')
   drop table dbo.B_Parameter
go

/*==============================================================*/
/* Table: B_Parameter                                           */
/*==============================================================*/
create table dbo.B_Parameter (
   LabID                bigint               null,
   ParameterID          bigint               not null,
   Name                 varchar(100)         collate Chinese_PRC_CI_AS null,
   SName                varchar(100)         collate Chinese_PRC_CI_AS null,
   ParaType             varchar(100)         collate Chinese_PRC_CI_AS null,
   ParaNo               varchar(50)          collate Chinese_PRC_CI_AS null,
   ParaValue            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ParaDesc             nvarchar(1000)       collate Chinese_PRC_CI_AS null,
   Shortcode            varchar(100)         collate Chinese_PRC_CI_AS null,
   PinYinZiTou          varchar(100)         collate Chinese_PRC_CI_AS null,
   ItemEditInfo         ntext                collate Chinese_PRC_CI_AS null,
   DispOrder            int                  null,
   IsUse                bit                  null,
   IsUserSet            bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_Parameter primary key nonclustered (ParameterID)
         on "PRIMARY"
)
on "PRIMARY"
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_Parameter') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_Parameter' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '系统参数表', 
   'user', 'dbo', 'table', 'B_Parameter'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'LabID'

end


execute sp_addextendedproperty 'MS_Description', 
   'LabID',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ParameterID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParameterID'

end


execute sp_addextendedproperty 'MS_Description', 
   '参数主键',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParameterID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'Name'

end


execute sp_addextendedproperty 'MS_Description', 
   '参数名称',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'Name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'SName'

end


execute sp_addextendedproperty 'MS_Description', 
   '简称',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'SName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ParaType')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParaType'

end


execute sp_addextendedproperty 'MS_Description', 
   '参数类型',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParaType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ParaNo')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParaNo'

end


execute sp_addextendedproperty 'MS_Description', 
   '参数编码',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParaNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ParaValue')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParaValue'

end


execute sp_addextendedproperty 'MS_Description', 
   '参数值',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParaValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ParaDesc')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParaDesc'

end


execute sp_addextendedproperty 'MS_Description', 
   '参数说明',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ParaDesc'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Shortcode')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'Shortcode'

end


execute sp_addextendedproperty 'MS_Description', 
   '快捷码',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'Shortcode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PinYinZiTou')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'PinYinZiTou'

end


execute sp_addextendedproperty 'MS_Description', 
   '拼音字头',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'PinYinZiTou'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ItemEditInfo')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ItemEditInfo'

end


execute sp_addextendedproperty 'MS_Description', 
   '供用户配置的信息',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'ItemEditInfo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'DispOrder'

end


execute sp_addextendedproperty 'MS_Description', 
   '显示次序',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUse')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'IsUse'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否使用',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'IsUse'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUserSet')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'IsUserSet'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否允许用户配置',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'IsUserSet'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'DataAddTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '加入时间',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'DataUpdateTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Parameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'DataTimeStamp'

end


execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', 'dbo', 'table', 'B_Parameter', 'column', 'DataTimeStamp'
go
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_Template')
            and   type = 'U')
   drop table dbo.B_Template
go

create table dbo.B_Template (
   LabID                bigint               null,
   TemplateID           bigint               not null,
   CName                varchar(100)         collate Chinese_PRC_CI_AS null,
   SName                varchar(80)          collate Chinese_PRC_CI_AS null,
   ShortCode            varchar(80)          collate Chinese_PRC_CI_AS null,
   PinYinZiTou          varchar(50)          collate Chinese_PRC_CI_AS null,
   TypeID               bigint               null,
   TypeName             varchar(50)          collate Chinese_PRC_CI_AS null,
   TemplateType         varchar(40)          collate Chinese_PRC_CI_AS null,
   FilePath             varchar(500)         collate Chinese_PRC_CI_AS null,
   FileName             varchar(100)         collate Chinese_PRC_CI_AS null,
   FileExt              varchar(50)          collate Chinese_PRC_CI_AS null,
   ContentType          varchar(100)         collate Chinese_PRC_CI_AS null,
   FileSize             float                null,
   Comment              ntext                collate Chinese_PRC_CI_AS null,
   ExcelRuleInfo        ntext                collate Chinese_PRC_CI_AS null,
   DispOrder            int                  null,
   IsUse                bit                  null,
   IsDefault            bit                  null,
   DataAddTime          datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_Template primary key nonclustered (TemplateID)
         on "PRIMARY"
)
on "PRIMARY"
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_Template') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_Template' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '报表模板信息表', 
   'user', 'dbo', 'table', 'B_Template'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'LabID'

end


execute sp_addextendedproperty 'MS_Description', 
   'LabID',
   'user', 'dbo', 'table', 'B_Template', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TemplateID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'TemplateID'

end


execute sp_addextendedproperty 'MS_Description', 
   '模版ID',
   'user', 'dbo', 'table', 'B_Template', 'column', 'TemplateID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'CName'

end


execute sp_addextendedproperty 'MS_Description', 
   '模板名称',
   'user', 'dbo', 'table', 'B_Template', 'column', 'CName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'SName'

end


execute sp_addextendedproperty 'MS_Description', 
   '简称',
   'user', 'dbo', 'table', 'B_Template', 'column', 'SName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ShortCode')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'ShortCode'

end


execute sp_addextendedproperty 'MS_Description', 
   '快捷码',
   'user', 'dbo', 'table', 'B_Template', 'column', 'ShortCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PinYinZiTou')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'PinYinZiTou'

end


execute sp_addextendedproperty 'MS_Description', 
   '拼音字头',
   'user', 'dbo', 'table', 'B_Template', 'column', 'PinYinZiTou'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TypeID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'TypeID'

end


execute sp_addextendedproperty 'MS_Description', 
   '模板类型ID',
   'user', 'dbo', 'table', 'B_Template', 'column', 'TypeID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TypeName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'TypeName'

end


execute sp_addextendedproperty 'MS_Description', 
   '模板类型名称',
   'user', 'dbo', 'table', 'B_Template', 'column', 'TypeName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TemplateType')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'TemplateType'

end


execute sp_addextendedproperty 'MS_Description', 
   '模板分类',
   'user', 'dbo', 'table', 'B_Template', 'column', 'TemplateType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FilePath')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'FilePath'

end


execute sp_addextendedproperty 'MS_Description', 
   '模板存放路径',
   'user', 'dbo', 'table', 'B_Template', 'column', 'FilePath'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FileName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'FileName'

end


execute sp_addextendedproperty 'MS_Description', 
   '文件名称',
   'user', 'dbo', 'table', 'B_Template', 'column', 'FileName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FileExt')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'FileExt'

end


execute sp_addextendedproperty 'MS_Description', 
   '文件扩展名',
   'user', 'dbo', 'table', 'B_Template', 'column', 'FileExt'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ContentType')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'ContentType'

end


execute sp_addextendedproperty 'MS_Description', 
   '内容类型',
   'user', 'dbo', 'table', 'B_Template', 'column', 'ContentType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FileSize')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'FileSize'

end


execute sp_addextendedproperty 'MS_Description', 
   '文件大小',
   'user', 'dbo', 'table', 'B_Template', 'column', 'FileSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Comment')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'Comment'

end


execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'B_Template', 'column', 'Comment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ExcelRuleInfo')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'ExcelRuleInfo'

end


execute sp_addextendedproperty 'MS_Description', 
   'Excel模板规则信息',
   'user', 'dbo', 'table', 'B_Template', 'column', 'ExcelRuleInfo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'DispOrder'

end


execute sp_addextendedproperty 'MS_Description', 
   '显示次序',
   'user', 'dbo', 'table', 'B_Template', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUse')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'IsUse'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否使用',
   'user', 'dbo', 'table', 'B_Template', 'column', 'IsUse'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDefault')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'IsDefault'

end


execute sp_addextendedproperty 'MS_Description', 
   '默认模板',
   'user', 'dbo', 'table', 'B_Template', 'column', 'IsDefault'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'DataAddTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'B_Template', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Template')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Template', 'column', 'DataTimeStamp'

end


execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', 'dbo', 'table', 'B_Template', 'column', 'DataTimeStamp'
go
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.B_UserUIConfig')
            and   type = 'U')
   drop table dbo.B_UserUIConfig
go

/*==============================================================*/
/* Table: B_UserUIConfig                                        */
/*==============================================================*/
create table dbo.B_UserUIConfig (
   LabID                bigint               null,
   UserUIID             bigint               not null,
   UserUIKey            varchar(100)         collate Chinese_PRC_CI_AS null,
   UserUIName           varchar(100)         collate Chinese_PRC_CI_AS null,
   TemplateTypeID       bigint               null,
   TemplateTypeCName    varchar(100)         collate Chinese_PRC_CI_AS null,
   UITypeID             bigint               null,
   UITypeName           varchar(100)         collate Chinese_PRC_CI_AS null,
   EmpID                bigint               null,
   IsDefault            bit                  null,
   Comment              ntext                collate Chinese_PRC_CI_AS null,
   DispOrder            int                  null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_UserUIConfig primary key nonclustered (UserUIID)
         on "PRIMARY"
)
on "PRIMARY"
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_UserUIConfig') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_UserUIConfig' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '用户UI配置信息', 
   'user', 'dbo', 'table', 'B_UserUIConfig'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'LabID'

end


execute sp_addextendedproperty 'MS_Description', 
   'LabID',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserUIID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UserUIID'

end


execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UserUIID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserUIKey')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UserUIKey'

end


execute sp_addextendedproperty 'MS_Description', 
   '配置编码',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UserUIKey'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserUIName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UserUIName'

end


execute sp_addextendedproperty 'MS_Description', 
   '配置名称',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UserUIName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TemplateTypeID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'TemplateTypeID'

end


execute sp_addextendedproperty 'MS_Description', 
   '模板类型',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'TemplateTypeID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TemplateTypeCName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'TemplateTypeCName'

end


execute sp_addextendedproperty 'MS_Description', 
   '模板类型名称',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'TemplateTypeCName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UITypeID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UITypeID'

end


execute sp_addextendedproperty 'MS_Description', 
   'UI配置类型ID',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UITypeID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UITypeName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UITypeName'

end


execute sp_addextendedproperty 'MS_Description', 
   'UI配置类型名称',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'UITypeName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EmpID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'EmpID'

end


execute sp_addextendedproperty 'MS_Description', 
   '用户ID',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'EmpID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDefault')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'IsDefault'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否默认',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'IsDefault'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Comment')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'Comment'

end


execute sp_addextendedproperty 'MS_Description', 
   '配置内容',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'Comment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'DispOrder'

end


execute sp_addextendedproperty 'MS_Description', 
   '显示次序',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUse')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'IsUse'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否使用',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'IsUse'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'DataAddTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '加入时间',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'DataUpdateTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'DataTimeStamp'

end


execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', 'dbo', 'table', 'B_UserUIConfig', 'column', 'DataTimeStamp'
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.DepartmentUser')
            and   type = 'U')
   drop table dbo.DepartmentUser
go

/*==============================================================*/
/* Table: DepartmentUser                                        */
/*==============================================================*/
create table dbo.DepartmentUser (
   LabID                bigint               null,
   DeptEmpID            bigint               not null,
   DeptNo               bigint               null,
   UserNo               bigint               null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_DepartmentUser primary key (DeptEmpID)
         on "PRIMARY"
)
on "PRIMARY"
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.DepartmentUser') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'DepartmentUser' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '科室人员关系表', 
   'user', 'dbo', 'table', 'DepartmentUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.DepartmentUser')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'LabID'

end


execute sp_addextendedproperty 'MS_Description', 
   'LabID',
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.DepartmentUser')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeptEmpID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DeptEmpID'

end


execute sp_addextendedproperty 'MS_Description', 
   '关系Id',
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DeptEmpID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.DepartmentUser')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeptNo')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DeptNo'

end


execute sp_addextendedproperty 'MS_Description', 
   '科室ID',
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DeptNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.DepartmentUser')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserNo')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'UserNo'

end


execute sp_addextendedproperty 'MS_Description', 
   '人员ID',
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'UserNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.DepartmentUser')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUse')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'IsUse'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否使用',
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'IsUse'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.DepartmentUser')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DispOrder'

end


execute sp_addextendedproperty 'MS_Description', 
   '显示次序',
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.DepartmentUser')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DataAddTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '加入时间',
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.DepartmentUser')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DataUpdateTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.DepartmentUser')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DataTimeStamp'

end


execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', 'dbo', 'table', 'DepartmentUser', 'column', 'DataTimeStamp'
go




 IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_InterfaceMaping]') AND type in(N'U')) CREATE TABLE [dbo].[SC_InterfaceMaping](
	[LabID] [bigint] NULL,
	[MappingId] [bigint] NOT NULL,
	[BobjectType] [bigint] NOT NULL,
	[BobjectID] [bigint] NOT NULL,
	[MapingCode] [varchar](60) NULL,
	[IsUse] [bit] NULL,
	[DispOrder] [int] NULL,
	[DataAddTime] [datetime] NULL,
	[DataTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_SC_InterfaceMaping] PRIMARY KEY CLUSTERED 
(
	[MappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SC_InterfaceMaping]  WITH CHECK ADD  CONSTRAINT [FK_SC_InterfaceMaping_B_Dict] FOREIGN KEY([BobjectType])
REFERENCES [dbo].[B_Dict] ([DID])
GO

ALTER TABLE [dbo].[SC_InterfaceMaping] CHECK CONSTRAINT [FK_SC_InterfaceMaping_B_Dict]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LabID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping', @level2type=N'COLUMN',@level2name=N'LabID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'映射关系Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping', @level2type=N'COLUMN',@level2name=N'MappingId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务类型
以公共字典方式维护定义，如
300130:科室对照;
300120:人员对照;
300110:费用项目对照;
300140:血制品血站对照;
300150:血制品HIS对照;
300160:检验项目LIS对照;
300170:加工类型HIS对照;
300180:血制品单位HIS对照;
300190:血型ABO血站对照;
300200:申请类型HIS对照;
300210:就诊类型HIS对照;
300220:用血类型HIS对照;
300230:收费类型HIS对照;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping', @level2type=N'COLUMN',@level2name=N'BobjectType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务对象ID
按业务对照类型存储不同的业务字典表主键ID ,如
科室信息表的Id,人员信息表的Id,费用项目的Id;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping', @level2type=N'COLUMN',@level2name=N'BobjectID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对照码
按业务对照类型存储不同的业务字典对照码 ,如
科室信息对照码,人员信息对照码,费用项目对照码;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping', @level2type=N'COLUMN',@level2name=N'MapingCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping', @level2type=N'COLUMN',@level2name=N'IsUse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping', @level2type=N'COLUMN',@level2name=N'DispOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping', @level2type=N'COLUMN',@level2name=N'DataAddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接口映射(对照)关系表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_InterfaceMaping'
GO


