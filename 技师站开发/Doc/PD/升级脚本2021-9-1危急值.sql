create table Lis_TestFormMsg (
   LabID                bigint               not null,
   TestFormMsgID        bigint               not null,
   MsgType              int                  not null,
   BarCode              nvarchar(100)        null,
   PatNo                nvarchar(100)        null,
   TestFormID           bigint               null,
   GTestDate            datetime             null,
   MasterDesc           nvarchar(2000)       null,
   DetailDesc           nvarchar(2000)       null,
   ReaderID             bigint               null,
   Reader               nvarchar(50)         null,
   ReadTime             DateTime             null,
   ReportStatus         int                  null,
   ReporterID           bigint               null,
   Reporter             nvarchar(50)         null,
   ReportTime           DateTime             null,
   ReportInfo           nvarchar(200)        null,
   PhoneStatus          int                  null,
   PhoneCallerID        bigint               null,
   PhoneCaller          nvarchar(50)         null,
   PhoneTime            datetime             null,
   PhoneNum             nvarchar(50)         null,
   PhoneReceiver        nvarchar(50)         null,
   PhoneDesc            nvarchar(200)        null,
   ReportEditStatus     int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_TestFormMsg primary key nonclustered (TestFormMsgID),
   constraint FK_LIS_TestFormMag foreign key (TestFormID)
      references Lis_TestForm (TestFormID)
)
go


create table Lis_TestFormMsgItem (
   LabID                bigint               not null,
   TestFormMsgItemID    bigint               not null,
   TestFormMsgID        bigint               not null,
   TestFormID           bigint               null,
   GTestDate            datetime             null,
   ItemID               bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_TestFormMsgItem primary key nonclustered (TestFormMsgItemID),
   constraint FK_LIS_TestFormMsgItem foreign key (TestFormMsgID)
      references Lis_TestFormMsg (TestFormMsgID)
)
go