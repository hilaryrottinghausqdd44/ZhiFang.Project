

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_DictType') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_DictType' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '字典类别', 
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
   '实验室ID',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'LabID'
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
   '名称',
   'user', 'dbo', 'table', 'B_DictType', 'column', 'CName'
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
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_DictType', 'column', 'DataAddTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
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
