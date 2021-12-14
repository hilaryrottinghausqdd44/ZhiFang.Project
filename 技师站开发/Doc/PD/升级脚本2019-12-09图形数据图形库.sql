
USE [LabGraphData]
GO
/*==============================================================*/
/* Table: Lis_GraphData                                         */
/*==============================================================*/
create table dbo.Lis_GraphData (
   LabID                bigint               not null,
   GraphDataID          bigint               not null,
   GraphData            ntext                null,
   ThumbData            ntext                null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_GraphData primary key (GraphDataID)
)
go
