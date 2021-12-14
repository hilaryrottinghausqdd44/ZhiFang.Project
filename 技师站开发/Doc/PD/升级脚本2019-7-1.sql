
--LB_ItemRange表 2019-06-28
if not Exists(Select * from SysColumns where [Name]= 'DeptID'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemRange')) 
  alter table LB_ItemRange add  DeptID  bigint 
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室' , @level0type=N'SCHEMA',
@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LB_ItemRange', @level2type=N'COLUMN',@level2name=N'DeptID'
GO
if not Exists(Select * from SysColumns where [Name]= 'SectionID'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemRange')) 
  alter table LB_ItemRange add  SectionID  bigint 
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检验小组' , @level0type=N'SCHEMA',
@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LB_ItemRange', @level2type=N'COLUMN',@level2name=N'SectionID'
GO

if not Exists(Select * from SysColumns where [Name]= 'EquipID'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemRange')) 
  alter table LB_ItemRange add  EquipID  bigint  
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检验仪器' , @level0type=N'SCHEMA',
@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LB_ItemRange', @level2type=N'COLUMN',@level2name=N'EquipID'
GO

if not Exists(Select * from SysColumns where [Name]= 'AgeUnitID'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemRange')) 
  alter table LB_ItemRange add  AgeUnitID  bigint  
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年龄单位' , @level0type=N'SCHEMA',
@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LB_ItemRange', @level2type=N'COLUMN',@level2name=N'AgeUnitID'
GO

--创建表
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LB_EquipItem') and o.name = 'FK_ITEM_EQU_EP_B_EQU')
alter table LB_EquipItem
   drop constraint FK_ITEM_EQU_EP_B_EQU
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LB_Equip')
            and   type = 'U')
   drop table dbo.LB_Equip
go
 
 create table dbo.LB_Equip (
   EquipID              bigint               not null,
   CName                nvarchar(50)         null,
   EName                nvarchar(50)         null,
   SName                nvarchar(50)         null,
   UseCode              nvarchar(50)         null,
   StandCode            nvarchar(50)         null,
   DeveCode             nvarchar(50)         null,
   Shortcode            nvarchar(50)         null,
   PinYinZiTou          nvarchar(50)         null,
   Comment              ntext                null,
   SectionID            bigint               null,
   Computer             nvarchar(50)         null,
   ComProgram           nvarchar(50)         null,
   Doubleflag           int                  null,
   EquipResultType      nvarchar(50)         null,
   CommInfo             nvarchar(500)        null,
   CommPara             ntext                null,
   Commsys              image                null,
   LicenceKey           nvarchar(50)         null,
   LicenceType          nvarchar(50)         null,
   SQH                  nvarchar(50)         null,
   LicenceDate          datetime             null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   SampleNoStart        nvarchar(50)         null,
   SampleNoEnd          nvarchar(50)         null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_Equip primary key nonclustered (EquipID)
         on "PRIMARY"
)
go


if exists (select 1
            from  sysobjects
           where  id = object_id('LB_EquipItem')
            and   type = 'U')
   drop table LB_EquipItem
go

/*==============================================================*/
/* Table: LB_EquipItem                                          */
/*==============================================================*/
create table LB_EquipItem (
   EquipItemID          bigint               not null,
   EquipID              bigint               null,
   ItemID               bigint               not null,
   DispOrder            int                  null,
   DispOrderComm        int                  null,
   DispOrderQC          int                  null,
   CompCode             nvarchar(50)         null,
   IsReserve            bit                  null,
   PItemID              bigint               null,
   SectionID            bigint               null,
   DilutionMultiple     float                null,
   IsUse                bit                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_EQUIPITEM primary key nonclustered (EquipItemID),
   constraint FK_ITEM_EQU_EP_B_EQU foreign key (EquipID)
      references dbo.LB_Equip (EquipID)
)
go

--仪器结果替换
if exists (select 1
            from  sysobjects
           where  id = object_id('LB_EquipResultTH')
            and   type = 'U')
   drop table LB_EquipResultTH
go

/*==============================================================*/
/* Table: LB_EquipResultTH                                      */
/*==============================================================*/
create table LB_EquipResultTH (
   EquipResultTHID      bigint               not null,
   EquipID              bigint               not null,
   ItemID               bigint               null,
   CompCode             nvarchar(50)         null,
   IsUse                bit                  null,
   SampleTypeID         bigint               null,
   GenderID             bigint               null,
   LowAge               float                null,
   HighAge              float                null,
   bCollectTime         datetime             null,
   eCollectTime         datetime             null,
   AgeUnitID            bigint               null,
   CalcType             nvarchar(50)         null,
   SourceValue          nvarchar(200)        null,
   ReportValue          nvarchar(200)        null,
   AppValue             nvarchar(200)        null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_EQUIPRESULTTH primary key nonclustered (EquipResultTHID)
)
go

