

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.B_Dict') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'B_Dict' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '�ֵ�', 
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
   'ʵ����ID',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'LabID'
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
   '����',
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
   '���',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'SName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.B_Dict')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Shortcode')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'B_Dict', 'column', 'Shortcode'

end


execute sp_addextendedproperty 'MS_Description', 
   '�����',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'Shortcode'
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
   '����ƴ����ͷ',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'PinYinZiTou'
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
   '��ʾ����',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DispOrder'
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
   '��ע',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'Memo'
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
   '�Ƿ�ʹ��',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'IsUse'
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
   '����ʱ��',
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
   'ʱ���',
   'user', 'dbo', 'table', 'B_Dict', 'column', 'DataTimeStamp'
go
