create table Lis_TestItemReceive (
   LabID                bigint               not null,
   TestItemReceiveID    bigint               not null,
   OrderItemID          bigint               null,
   BarCodeItemID        bigint               null,
   OrdersItemID         bigint               null,
   BarCodesItemID       bigint               null,
   ItemID               bigint               null,
   PItemID              bigint               null,
   BarCode              nvarchar(100)        null,
   TestFormID           bigint               null,
   GTestDate            datetime             null,
   IsByHand             bit                  null,
   ReportTime           DateTime             null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_TestItem primary key nonclustered (TestItemReceiveID),
   constraint FK_LIS_TEST_REFERENCE_LIS_TEST foreign key (TestFormID)
      references Lis_TestForm (TestFormID)
)
go