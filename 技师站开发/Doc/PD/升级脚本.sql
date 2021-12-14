
--LB_Item±í 2019-06-20
if not Exists(Select * from SysColumns where [Name]= 'SpecialtyID'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_Item')) 
  alter table LB_Item add  SpecialtyID  bigint 
go

if not Exists(Select * from SysColumns where [Name]= 'HintFlag'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_Item')) 
  alter table LB_Item add  HintFlag  int 
go


if not Exists(Select * from SysColumns where [Name]= 'FWorkLoad'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_Item')) 
  alter table LB_Item add  FWorkLoad  float 
go

if not Exists(Select * from SysColumns where [Name]= 'SectionFun'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_Item')) 
  alter table LB_Item add  SectionFun  nvarchar(50) 
go


if Exists(Select * from SysColumns where [Name]= 'ItemType'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_Item')) 
  alter table LB_Item drop column ItemType
go

if Exists(Select * from SysColumns where [Name]= 'RefRange'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_Item')) 
  alter table LB_Item drop column RefRange
go

--LB_ItemCalcFormula 2019-06-20
if not Exists(Select * from SysColumns where [Name]= 'FormulaConditionInfo'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemCalcFormula')) 
  alter table LB_ItemCalcFormula add  FormulaConditionInfo  nvarchar(300) 
go

if not Exists(Select * from SysColumns where [Name]= 'CalcFormulaInfo'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemCalcFormula')) 
  alter table LB_ItemCalcFormula add  CalcFormulaInfo  nvarchar(300) 
go



--LB_ItemRange±í 2019-06-28
if not Exists(Select * from SysColumns where [Name]= 'DeptID'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemRange')) 
  alter table LB_ItemRange add  DeptID  bigint 
go

if not Exists(Select * from SysColumns where [Name]= 'SectionID'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemRange')) 
  alter table LB_ItemRange add  SectionID  bigint 
go


if not Exists(Select * from SysColumns where [Name]= 'EquipID'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemRange')) 
  alter table LB_ItemRange add  EquipID  bigint  
go



--LB_EquipItem 2019-06-24
if not Exists(Select * from SysColumns where [Name]= 'ItemID'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_EquipItem')) 
  alter table LB_EquipItem add  ItemID  bigint
go

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LBITEMID_LB_EQUIPITEM]') and 
parent_object_id = OBJECT_ID(N'[dbo].[LB_EquipItem]')) 
ALTER TABLE [dbo].[LB_EquipItem] DROP CONSTRAINT [FK_LBITEMID_LB_EQUIPITEM]
GO

ALTER TABLE [dbo].[LB_EquipItem]  WITH CHECK ADD  CONSTRAINT [FK_LBITEMID_LB_EQUIPITEM] FOREIGN KEY([ItemID])
REFERENCES [dbo].[LB_Item] ([ItemID])
GO

ALTER TABLE [dbo].[LB_EquipItem] CHECK CONSTRAINT [FK_LBITEMID_LB_EQUIPITEM]
GO


