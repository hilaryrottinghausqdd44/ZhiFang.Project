if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_QCPrintTemplate')
            and   type = 'U')
   drop table dbo.LB_QCPrintTemplate
go

/*==============================================================*/
/* Table: LB_QCPrintTemplate                                    */
/*==============================================================*/
create table dbo.LB_QCPrintTemplate (
   LabID                bigint               not null,
   QCPrintTemplateID    bigint               not null,
   TypeName             nvarchar(50)         not null,
   PrintTemplateName    nvarchar(50)         not null,
   EquipID              bigint               null,
   EquipModule          nvarchar(50)         null,
   QCDataType           int                  null,
   QCCountInDay         int                  null,
   LevelCount           int                  null,
   ItemID               bigint               null,
   QCMatID              bigint               null,
   DispOrder            int                  null,
   UserID               bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCPrintTemplate primary key (QCPrintTemplateID)
         WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('dbo.LB_QCPrintTemplate') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'user', 'dbo', 'table', 'LB_QCPrintTemplate' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '质控打印模板表', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LabID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'LabID'

end


execute sp_addextendedproperty 'MS_Description', 
   '实验室ID',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'LabID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'QCPrintTemplateID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'QCPrintTemplateID'

end


execute sp_addextendedproperty 'MS_Description', 
   '质控物时效ID',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'QCPrintTemplateID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TypeName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'TypeName'

end


execute sp_addextendedproperty 'MS_Description', 
   '日质控 仪器日质控 月质控 多浓度质控',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'TypeName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrintTemplateName')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'PrintTemplateName'

end


execute sp_addextendedproperty 'MS_Description', 
   '日质控* 仪器日质控* 月质控* 多浓度质控*',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'PrintTemplateName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EquipID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'EquipID'

end


execute sp_addextendedproperty 'MS_Description', 
   '仪器ID',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'EquipID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EquipModule')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'EquipModule'

end


execute sp_addextendedproperty 'MS_Description', 
   '仪器模块',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'EquipModule'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'QCDataType')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'QCDataType'

end


execute sp_addextendedproperty 'MS_Description', 
   '0：靶值标准差， 1：定性 2：值范围',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'QCDataType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'QCCountInDay')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'QCCountInDay'

end


execute sp_addextendedproperty 'MS_Description', 
   '不用， ',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'QCCountInDay'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ItemID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'ItemID'

end


execute sp_addextendedproperty 'MS_Description', 
   '项目ID',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'ItemID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'QCMatID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'QCMatID'

end


execute sp_addextendedproperty 'MS_Description', 
   '不用，  质控物ID',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'QCMatID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserID')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'UserID'

end


execute sp_addextendedproperty 'MS_Description', 
   '操作者',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'UserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataAddTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'DataAddTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'DataAddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataUpdateTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'DataUpdateTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '数据更新时间',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'DataUpdateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('dbo.LB_QCPrintTemplate')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DataTimeStamp')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'DataTimeStamp'

end


execute sp_addextendedproperty 'MS_Description', 
   '时间戳',
   'user', 'dbo', 'table', 'LB_QCPrintTemplate', 'column', 'DataTimeStamp'
go
