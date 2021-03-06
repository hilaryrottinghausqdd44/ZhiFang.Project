
create table dbo.Lis_QCData (
   LabID                bigint               not null,
   QCDataID             bigint               not null,
   QCItemID             bigint               null,
   ReceiveTime          datetime             not null,
   ReportValue          nvarchar(100)        null,
   QuanValue            float                null,
   QCValueMemo          nvarchar(100)        null,
   EValue               nvarchar(300)        null,
   EResultStatus        nvarchar(50)         null,
   EResultAlarm         nvarchar(50)         null,
   bUse                 bit                  null,
   bAnaly               bit                  null,
   loserule             nvarchar(50)         null,
   loseType             nvarchar(10)         null,
   LoseReason           nvarchar(200)        null,
   CorrectMeasure       nvarchar(200)        null,
   CorrectValue         nvarchar(100)        null,
   CorrectDesc          nvarchar(200)        null,
   Precaution           nvarchar(200)        null,
   LoseMemo             nvarchar(200)        null,
   ClinicalEffects      nvarchar(200)        null,
   OperateInfo          nvarchar(200)        null,
   LoseOperator         nvarchar(50)         null,
   LoseOperatorID       nvarchar(50)         null,
   loseOperateTime      datetime             null,
   LoseExaminer         nvarchar(50)         null,
   LoseExaminerID       int                  null,
   LoseExamineTime      datetime             null,
   LoseExamineState     int                  null,
   ExaminInfo           nvarchar(200)        null,
   MainStatusID         int                  null,
   iSource              int                  null,
   StatusID             bigint               null,
   IsUpload             bit                  null,
   PlateNo              int                  null,
   PositionNo           int                  null,
   OperatorID           bigint               null,
   Operator             nvarchar(20)         null,
   DispOrder            int                  null,
   TestTime             datetime             null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCData primary key (QCDataID) ON [PRIMARY],
   constraint FK_LIS_QCData_QCItem foreign key (QCItemID)
      references dbo.LB_QCItem (QCItemID)
)
go
