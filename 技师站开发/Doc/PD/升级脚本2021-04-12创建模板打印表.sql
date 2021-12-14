

if not exists (select 1
            from  sysobjects
           where  id = object_id('B_PrintModel')
            and   type = 'U')
  create table dbo.B_PrintModel (
   LabID                bigint               not null,
   PrintModelID         bigint               not null,
   BusinessTypeName     nvarchar(100)        null,
   ModelTypeName        nvarchar(200)        null,
   FileName             nvarchar(200)        null,
   FileData             image                null,
   FileComment          nvarchar(500)        null,
   DispOrder            int                  null,
   OperaterID           bigint               null,
   Operater             nvarchar(50)         null,
   FinalOperaterID      bigint               null,
   FinalOperater        nvarchar(50)         null,
   IsProtect            bit                  null,
   IsUse                bit                  null,
   FileUploadTime       datetime             null,
   UploadComputer       nvarchar(100)        null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   BusinessTypeCode     nvarchar(50)         null,
   ModelTypeCode        nvarchar(50)         null,
   constraint PK_B_PRINTMODEL primary key (PrintModelID)
)
go