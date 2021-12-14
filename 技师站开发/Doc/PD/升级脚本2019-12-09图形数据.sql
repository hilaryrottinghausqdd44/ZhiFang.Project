if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_TestGraph')
            and   type = 'U')
   drop table dbo.Lis_TestGraph
go
/*==============================================================*/
/* Table: Lis_TestGraph                                         */
/*==============================================================*/
create table dbo.Lis_TestGraph (
   LabID                bigint               not null,
   TestGraphID          bigint               not null,
   TestFormID           bigint               null,
   GTestDate            datetime             null,
   iExamine             int                  null,
   GraphNo              int                  null,
   GraphName            nvarchar(100)        null,
   GraphType            nvarchar(20)         null,
   GraphDataID          bigint               null,
   GraphInfo            ntext                null,
   GraphComment         ntext                null,
   MainStatusID         int                  null,
   StatusID             bigint               null,
   ReportStatusID       bigint               null,
   IsReport             bit                  null,
   GraphHeight          int                  null,
   GraphWidth           int                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_TestGraph primary key (TestGraphID),
   constraint FK_LIS_TESTGraph_TESTForm foreign key (TestFormID)
      references Lis_TestForm (TestFormID)
)
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Lis_EquipGraph')
            and   type = 'U')
   drop table dbo.Lis_EquipGraph
go

/*==============================================================*/
/* Table: Lis_EquipGraph                                        */
/*==============================================================*/
create table dbo.Lis_EquipGraph (
   LabID                bigint               not null,
   EquipGraphID         bigint               not null,
   EquipFormID          bigint               null,
   GTestDate            datetime             null,
   iExamine             int                  null,
   GraphNo              int                  null,
   GraphName            nvarchar(100)        null,
   GraphType            nvarchar(20)         null,
   GraphDataID          bigint               null,
   GraphInfo            ntext                null,
   GraphComment         ntext                null,
   IsReport             bit                  null,
   GraphHeight          int                  null,
   GraphWidth           int                  null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LIS_EQUIPGRAPH primary key (EquipGraphID),
   constraint FK_LIS_EquipGraph_Equip foreign key (EquipFormID)
      references Lis_EquipForm (EquipFormID)
)
go
