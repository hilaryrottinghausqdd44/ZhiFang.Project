

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('B_UserUIConfig') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'B_UserUIConfig' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '�û�UI������Ϣ', 
   'user', @CurrentUser, 'table', 'B_UserUIConfig'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'LabID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ʵ����ID',
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EmpID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'EmpID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Ա��ID',
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'EmpID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Comment')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'Comment'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'Comment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DispOrder')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'DispOrder'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ʾ����',
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'DispOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsUse')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'IsUse'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ�ʹ��',
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'IsUse'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'DataAddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('B_UserUIConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'DataTimeStamp'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ʱ���',
   'user', @CurrentUser, 'table', 'B_UserUIConfig', 'column', 'DataTimeStamp'
go
