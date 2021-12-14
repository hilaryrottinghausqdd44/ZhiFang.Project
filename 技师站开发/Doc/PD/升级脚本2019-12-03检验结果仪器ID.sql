
alter Table Lis_TestItem Add  EquipID bigint  Null
   
   
   
   
   drop table Lis_TestGraph

create table Lis_TestGraph (
   TestGraphID          bigint               not null,
   TestFormID           bigint               null,
   GTestDate            datetime             null,
   iExamine             int                  null,
   GraphNo              int                  null,
   GraphName            nvarchar(100)        null,
   GraphType            nvarchar(20)         null,
   GraphData            image                null,
   GraphInfo            ntext                null,
   GraphComment         ntext                null,
   MainStatusID         int                  null,
   StatusID             bigint               null,
   ReportStatusID       bigint               null,
   IsReport             bit                  null,
   GraphHeight          int                  null,
   GraphWidth           int                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_TestGraph primary key nonclustered (TestGraphID) ON [PRIMARY],
   constraint FK_LIS_TESTGraph_TESTForm foreign key (TestFormID)
      references Lis_TestForm (TestFormID)
)
go
