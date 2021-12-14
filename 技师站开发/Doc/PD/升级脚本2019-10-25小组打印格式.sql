create table dbo.LB_SectionPrint (
   LabID                bigint               not null,
   SPID                 bigint               not null,
   SectionID            bigint               null,
   SampleTypeID         bigint               null,
   SickTypeID           bigint               null,
   bDefPrint            bit                  null,
   PrintFormat          nvarchar(50)         null,
   PrintProgram         nvarchar(50)         null,
   DefPrinter           nvarchar(100)        null,
   FormatPara           text                 null,
   ItemID               bigint               null,
   ClientID             bigint               null,
   ItemCountMax         int                  null,
   ItemCountMin         int                  null,
   PrintOrder           int                  null,
   nodename             nvarchar(50)         null,
   microattribute       nvarchar(20)         null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_R_SectionPrint primary key (SPID),
   constraint FK_SectionPrint_Section foreign key (SectionID)
      references LB_Section (SectionID),
   constraint FK_SectionPrint_Item foreign key (ItemID)
      references LB_Item (ItemID)
)
go