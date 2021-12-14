--外键

ALTER TABLE [dbo].[LB_EquipItem]  WITH CHECK ADD  CONSTRAINT [FK_LB_EQUIPItem_LB_ITEM] FOREIGN KEY([ItemID])
REFERENCES [dbo].[LB_Item] ([ItemID])
GO

ALTER TABLE [dbo].LB_EquipResultTH  WITH CHECK ADD  CONSTRAINT FK_LB_EquipResultTH_LB_ITEM FOREIGN KEY([ItemID])
REFERENCES [dbo].[LB_Item] ([ItemID])
GO

ALTER TABLE [dbo].LB_EquipResultTH  WITH CHECK ADD  CONSTRAINT FK_LB_EquipResultTH_LB_EQUIP FOREIGN KEY(EquipID)
REFERENCES [dbo].LB_Equip (EquipID)
GO


--是否特殊提示颜色
if not Exists(Select * from SysColumns where [Name]= 'bAlarmColor'  and ID = (Select [ID] from SysObjects 
where Name = 'LB_ItemRangeExp')) 
  alter table LB_ItemRangeExp add  bAlarmColor  bit 
go

if not Exists(Select * from SysColumns where [Name]= 'bAlarmColor'  and ID = (Select [ID] from SysObjects 
where Name = 'Lis_TestItem')) 
  alter table Lis_TestItem add  bAlarmColor  bit 
go

if not Exists(Select * from SysColumns where [Name]= 'AlarmColor'  and ID = (Select [ID] from SysObjects 
where Name = 'Lis_TestItem')) 
  alter table Lis_TestItem add  AlarmColor  int 
go

