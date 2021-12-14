create table LB_EquipSection (
   LabID                bigint               not null,
   EquipSectionID       bigint               not null,
   EquipID              bigint               null,
   SectionID            bigint               null,
   ItemID               bigint               null,
   DispOrder            int                  null,
   CompValue1           nvarchar(50)         null,
   CompValue2           nvarchar(50)         null,
   SampleNoCode         nvarchar(200)        null,
   IsUse                bit                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_EP_EQUIPSECTION primary key (EquipSectionID),
   constraint FK_EquipSection_Equip foreign key (EquipID)
      references dbo.LB_Equip (EquipID),
   constraint FK_EquipSection_Section foreign key (SectionID)
      references LB_Section (SectionID),
   constraint FK_EquipSection_Item foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go