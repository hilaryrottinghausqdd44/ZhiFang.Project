--������
create table LB_PhyPeriod (
   PhyPeriodID          bigint               not null,
   CName                nvarchar(500)        null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_PHYPERIOD primary key nonclustered (PhyPeriodID)
)
go



--������λ
create table LB_CollectPart (
   CollectPartID       bigint               not null,
   CName                nvarchar(500)        null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_B_CollectPart primary key nonclustered (CollectPartID)
)
go



--������Ϣ��
IF not exists(select * from syscolumns where id=object_id('Lis_Patient') and name='PhyPeriodID')
ALTER TABLE Lis_Patient ADD PhyPeriodID bigint
GO
execute sp_addextendedproperty 'MS_Description', 
   '������ID',
   'user', 'dbo', 'table', 'Lis_Patient', 'column', 'PhyPeriodID'
go


IF not exists(select * from syscolumns where id=object_id('Lis_Patient') and name='PhyPeriodName')
ALTER TABLE Lis_Patient ADD PhyPeriodName nvarchar(500)
GO
execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', 'dbo', 'table', 'Lis_Patient', 'column', 'PhyPeriodName'
go


IF not exists(select * from syscolumns where id=object_id('Lis_Patient') and name='CollectPartID')
ALTER TABLE Lis_Patient ADD CollectPartID bigint
GO

execute sp_addextendedproperty 'MS_Description', 
   '������λID',
   'user', 'dbo', 'table', 'Lis_Patient', 'column', 'CollectPartID'
go

IF not exists(select * from syscolumns where id=object_id('Lis_Patient') and name='CollectPartName')
ALTER TABLE Lis_Patient ADD CollectPartName nvarchar(500)
GO
execute sp_addextendedproperty 'MS_Description', 
   '������λ',
   'user', 'dbo', 'table', 'Lis_Patient', 'column', 'CollectPartName'
go


--��Ŀ�ο���Χ
--   bCritical            bit                  null,
--   DiagID               bigint               null,
--   PhyPeriodID          bigint               null,
--   CollectPartID       bigint               null,

IF not exists(select * from syscolumns where id=object_id('LB_ItemRange') and name='bCritical')
ALTER TABLE LB_ItemRange ADD bCritical bit
GO
execute sp_addextendedproperty 'MS_Description', 
   '����Σ��ֵ',
   'user', 'dbo', 'table', 'LB_ItemRange', 'column', 'bCritical'
go

IF not exists(select * from syscolumns where id=object_id('LB_ItemRange') and name='DiagID')
ALTER TABLE LB_ItemRange ADD DiagID bigint
GO
execute sp_addextendedproperty 'MS_Description', 
   '�ٴ����ID',
   'user', 'dbo', 'table', 'LB_ItemRange', 'column', 'DiagID'
go

IF not exists(select * from syscolumns where id=object_id('LB_ItemRange') and name='PhyPeriodID')
ALTER TABLE LB_ItemRange ADD PhyPeriodID bigint
GO
execute sp_addextendedproperty 'MS_Description', 
   '������ID',
   'user', 'dbo', 'table', 'LB_ItemRange', 'column', 'PhyPeriodID'
go

IF not exists(select * from syscolumns where id=object_id('LB_ItemRange') and name='CollectPartID')
ALTER TABLE LB_ItemRange ADD CollectPartID bigint
GO
execute sp_addextendedproperty 'MS_Description', '������λID', 'SCHEMA', 'dbo', 'table', 'LB_ItemRange', 'column', 'CollectPartID'
go

--��Ŀ�ο���Χ��չ
IF not exists(select * from syscolumns where id=object_id('LB_ItemRangeExp') and name='AlarmLevel')
ALTER TABLE LB_ItemRangeExp ADD AlarmLevel int
GO
execute sp_addextendedproperty 'MS_Description', '��ʾ���� ö�٣�0������ 1����ʾ 2������ 3�����ؾ��� 4��Σ�� 5������', 'SCHEMA', 'dbo', 'table', 'LB_ItemRangeExp', 'column', 'AlarmLevel'
go
