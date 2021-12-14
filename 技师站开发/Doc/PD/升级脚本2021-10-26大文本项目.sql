
--2021-10-26 LB_ItemExp项目特殊信息扩展
create table LB_ItemExp (
   LabID                bigint               not null,
   ItemExpID            bigint               not null,
   ExpType              int                  null,
   SectionID            bigint               null,
   ItemID               bigint               null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DispHeight           int                  null,
   IsHyperText          bit                  null,
   IsTemplate           bit                  null,
   TemplateInfo         image                null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ITEMEXP primary key (ItemExpID),
   constraint FK_LB_ITEME_REFERENCE_LB_ITEM foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go


execute sp_addextendedproperty 'MS_Description', 
   '项目特殊信息ID',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'ItemExpID'
go

execute sp_addextendedproperty 'MS_Description', 
   '特殊 类型 0：大文本',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'ExpType'
go

execute sp_addextendedproperty 'MS_Description', 
   '采用上下标超文本',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'IsHyperText'
go

execute sp_addextendedproperty 'MS_Description', 
   '采用快捷模板',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'IsTemplate'
go

execute sp_addextendedproperty 'MS_Description', 
   '快捷模板内容',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'TemplateInfo'
go


--项目特殊属性
IF not exists(select * from syscolumns where id=object_id('LB_Item') and name='SpecialType')
ALTER TABLE LB_Item ADD SpecialType int
GO
execute sp_addextendedproperty 'MS_Description', 
   '项目特殊属性 0：常规项目 1：大文本 2：仅大文本',
   'user', 'dbo', 'table', 'LB_Item', 'column', 'SpecialType'
go
