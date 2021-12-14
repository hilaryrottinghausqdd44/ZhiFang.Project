
if exists (select 1 from  sys.extended_properties
           where major_id = object_id('B_LodopTemplet') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'B_LodopTemplet' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Lodop��ӡģ��ά����Ϣ', 
   'user', @CurrentUser, 'table', 'B_LodopTemplet'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_LodopTemplet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'LabID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ʵ����ID',
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_LodopTemplet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'CName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'CName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_LodopTemplet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'DispOrder'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ʾ����',
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_LodopTemplet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_LodopTemplet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUse')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'IsUse'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ�ʹ��',
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'IsUse'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_LodopTemplet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'DataAddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_LodopTemplet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'DataTimeStamp'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ʱ���',
   'user', @CurrentUser, 'table', 'B_LodopTemplet', 'column', 'DataTimeStamp'
go
