
--2021-10-26 LB_ItemExp��Ŀ������Ϣ��չ
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
   '��Ŀ������ϢID',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'ItemExpID'
go

execute sp_addextendedproperty 'MS_Description', 
   '���� ���� 0�����ı�',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'ExpType'
go

execute sp_addextendedproperty 'MS_Description', 
   '�������±곬�ı�',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'IsHyperText'
go

execute sp_addextendedproperty 'MS_Description', 
   '���ÿ��ģ��',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'IsTemplate'
go

execute sp_addextendedproperty 'MS_Description', 
   '���ģ������',
   'user', 'dbo', 'table', 'LB_ItemExp', 'column', 'TemplateInfo'
go


--��Ŀ��������
IF not exists(select * from syscolumns where id=object_id('LB_Item') and name='SpecialType')
ALTER TABLE LB_Item ADD SpecialType int
GO
execute sp_addextendedproperty 'MS_Description', 
   '��Ŀ�������� 0��������Ŀ 1�����ı� 2�������ı�',
   'user', 'dbo', 'table', 'LB_Item', 'column', 'SpecialType'
go
