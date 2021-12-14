--基本质控规则
create table LB_QCRuleBase (
   LabID                bigint               not null,
   QCRuleBaseID         bigint               not null,
   IsDefault            int                  null,
   QCRuleBaseName       nvarchar(50)         null,
   RuleDesc             nvarchar(200)        null,
   CountN               int                  null,
   CountM               int                  null,
   MultX                float                null,
   MultY                float                null,
   RuleType             int                  null,
   Operator             nvarchar(50)         null,
   OperatorID           bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCRULEBASE primary key (QCRuleBaseID)
)
go


--质控规则

create table LB_QCRule (
   LabID                bigint               not null,
   QCRuleID             bigint               not null,
   QCRuleName           nvarchar(100)        null,
   RuleDesc             nvarchar(200)        null,
   bDefault             bit                  null,
   bWestGard            bit                  null,
   DispOrder            int                  null,
   LoseType             nvarchar(20)         null,
   bWarnState           bit                  null,
   Operator             nvarchar(50)         null,
   OperatorID           bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCRULEUSE primary key (QCRuleID)
)
go



--质控规则关系
create table LB_QCRulesCon (
   LabID                bigint               not null,
   QCRulesConID         bigint               not null,
   QCRuleID             bigint               null,
   QCRuleBaseID         bigint               null,
   DispOrder            int                  null,
   Operator             nvarchar(50)         null,
   OperatorID           bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCRULESCON primary key (QCRulesConID),
   constraint FK_LB_QCRulesCon1 foreign key (QCRuleBaseID)
      references LB_QCRuleBase (QCRuleBaseID),
   constraint FK_LB_QCRulesCon2 foreign key (QCRuleID)
      references LB_QCRule (QCRuleID)
)
go

--项目质控规则
create table LB_QCItemRule (
   LabID                bigint               not null,
   QCItemRuleID         bigint               not null,
   QCItemID             bigint               null,
   QCRuleID             bigint               null,
   DispOrder            int                  null,
   Operator             nvarchar(50)         null,
   OperatorID           bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QCITEMRULES primary key (QCItemRuleID),
   constraint FK_LB_QCIItemRule_QCItem foreign key (QCItemID)
      references dbo.LB_QCItem (QCItemID),
   constraint FK_LB_QCItemRule_QCRule foreign key (QCRuleID)
      references LB_QCRule (QCRuleID)
)
go


--质控数据
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
   LoseExamineTime2     datetime             null,
   LoseExamineState     int                  null,
   ExaminInfo           nvarchar(200)        null,
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
   constraint PK_QCData primary key (QCDataID),
   constraint FK_LIS_QCData_QCItem foreign key (QCItemID)
      references dbo.LB_QCItem (QCItemID)
)
go



--质控评语
create table Lis_QCComments (
   LabID                bigint               not null,
   QCCommentID1         bigint               not null,
   QCCommentType        int                  null,
   QCMatID              bigint               null,
   QCItemID             bigint               null,
   EquipID              bigint               null,
   ItemID               bigint               null,
   BeginDate            datetime             null,
   EndDate              datetime             null,
   QCInfo               nvarchar(200)        null,
   QCComment            nvarchar(1000)       null,
   Operator             nvarchar(50)         null,
   OperatorID           bigint               null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_QC_Comments primary key (QCCommentID1),
   constraint FK_Lis_QCComments1 foreign key (QCItemID)
      references dbo.LB_QCItem (QCItemID),
   constraint FK_Lis_QCComments2 foreign key (QCMatID)
      references dbo.LB_QCMaterial (QCMatID)
)
go
