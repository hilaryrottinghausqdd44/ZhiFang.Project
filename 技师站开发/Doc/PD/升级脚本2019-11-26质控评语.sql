
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Lis_QCComments]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin
   drop table Lis_QCComments
end

/*==============================================================*/
/* Table: Lis_QCComments                                        */
/*==============================================================*/
create table Lis_QCComments (
   LabID                bigint               not null,
   QCCommentID          bigint               not null,
   TypeName             nvarchar(50)         null,
   QCMatID              bigint               null,
   QCItemID             bigint               null,
   EquipID              bigint               null,
   ItemID               bigint               null,
   BeginDate            datetime             null,
   EndDate              datetime             null,
   QCInfo               nvarchar(1000)       null,
   QCComment            nvarchar(1000)       null,
   Operator             nvarchar(50)         null,
   OperatorID           bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QC_Comments primary key (QCCommentID) ON [PRIMARY],
   constraint FK_Lis_QCComments2 foreign key (QCMatID)
      references dbo.LB_QCMaterial (QCMatID),
   constraint FK_Lis_QCComments1 foreign key (QCItemID)
      references dbo.LB_QCItem (QCItemID)
)
go

alter Table LB_Phrase Alter Column ObjectID bigint  Null
