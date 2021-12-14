
if not exists (select 1
            from  sysobjects
           where  id = object_id('LB_ChargeType')
            and   type = 'U')
create table LB_ChargeType (
   LabID                bigint               not null,
   ChargeTypeID         bigint               not null,
   CName                nvarchar(50)         null,
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
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ChargeType primary key (ChargeTypeID)
)
go

if not exists (select 1
            from  sysobjects
           where  id = object_id('LB_ItemCharge')
            and   type = 'U')
create table LB_ItemCharge (
   LabID                bigint               not null,
   ItemChargeID         bigint               not null,
   ItemID               bigint               not null,
   ChargeTypeID         bigint               null,
   SickTypeID           bigint               null,
   DeptID               bigint               null,
   SampleTypeID         bigint               null,
   ItemCharge           decimal              not null,
   Comment              nvarchar(500)        null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ItemCharge primary key (ItemChargeID),
   constraint FK_ItemCharge_Item foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go
