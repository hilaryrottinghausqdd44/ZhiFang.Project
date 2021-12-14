
if not exists (select 1
            from  sysobjects
           where  id = object_id('LB_DicCodeLink')
            and   type = 'U')
   create table LB_DicCodeLink (
   LabID                bigint               not null,
   LBDicCodelinkID      bigint               not null,
   DicTypeCode          nvarchar(100)        null,
   DicTypeName          nvarchar(100)        null,
   LinkSystemCode       nvarchar(100)        null,
   LinkSystemName       nvarchar(100)        null,
   LinkSystemID         bigint               null,
   DicDataID            nvarchar(100)        null,
   DicDataCode          nvarchar(100)        null,
   DicDataName          nvarchar(100)        null,
   LinkDicDataCode      nvarchar(100)        null,
   LinkDicDataName      nvarchar(100)        null,
   TransTypeID          bigint               null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_DICCODELINK primary key (LBDicCodelinkID)
)
go

if not exists (select 1
            from  sysobjects
           where  id = object_id('LB_ItemCodeLink')
            and   type = 'U')
   create table LB_ItemCodeLink (
   LabID                bigint               not null,
   LBItemCodeLinkID     bigint               not null,
   LinkSystemCode       nvarchar(100)        null,
   LinkSystemName       nvarchar(100)        null,
   LinkSystemID         bigint               null,
   DicDataID            nvarchar(100)        null,
   DicDataCode          nvarchar(100)        null,
   DicDataName          nvarchar(100)        null,
   LinkDicDataCode      nvarchar(100)        null,
   LinkDicDataName      nvarchar(100)        null,
   TransTypeID          bigint               null,
   Comment              ntext                null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ITEMCODELINK primary key (LBItemCodeLinkID)
)
go
