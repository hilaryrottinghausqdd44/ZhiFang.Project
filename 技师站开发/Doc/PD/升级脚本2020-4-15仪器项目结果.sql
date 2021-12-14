if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_EquipItem')
            and   type = 'U')
   drop table Lis_EquipItem
go

/*==============================================================*/
/* Table: Lis_EquipItem                                         */
/*==============================================================*/
create table Lis_EquipItem (
   LabID                bigint               not null,
   EquipResultID        bigint               not null,
   EquipFormID          bigint               null,
   TestItemID           bigint               null,
   ItemID               bigint               null,
   ETestDate            datetime             null,
   iExamine             int                  null,
   bRedo                bit                  null,
   TestType             int                  null,
   ItemChannel          nvarchar(50)         null,
   EReportValue         nvarchar(300)        null,
   EResultStatus        nvarchar(50)         null,
   EResultAlarm         nvarchar(200)        null,
   ETestComment         ntext                null,
   ESend                nvarchar(100)        null,
   EOriginalValue       nvarchar(300)        null,
   EOriginalResultStatus nvarchar(50)         null,
   EOriginalResultAlarm nvarchar(200)        null,
   EOriginalSend        nvarchar(100)        null,
   EOriginalTestComment ntext                null,
   EReagentInfo         nvarchar(100)        null,
   ETestState           int                  null,
   EAlarmState          int                  null,
   bItemResultFlag      bit                  null,
   bResultDowith        bit                  null,
   ZDY1                 nvarchar(200)        null,
   ZDY2                 nvarchar(200)        null,
   ZDY3                 nvarchar(200)        null,
   DispOrder            int                  null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   TestTime             datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_Lis_EquipItem primary key (EquipResultID),
   constraint FK_EquipSampleItem_Form foreign key (EquipFormID)
      references Lis_EquipForm (EquipFormID),
   constraint FK_LIS_EquipItem_ItemID foreign key (ItemID)
      references dbo.LB_Item (ItemID)
)
go

--检验结果表增加信息仪器结果ID等
Alter Table Lis_TestItem Add EquipResultID        bigint    null        
Alter Table Lis_TestItem Add   EReportValue         nvarchar(300)                     
Alter Table Lis_TestItem Add   ETestComment         ntext                      
Alter Table Lis_TestItem Add   EReagentInfo         nvarchar(100)       
Alter Table Lis_TestItem Add   EAlarmState          int          
Alter Table Lis_TestItem  Add    iDoWith      int     null --仪器结果后处理



--检验表增加仪器单ID
Alter Table Lis_TestForm Add EquipFormID          bigint               null 

--仪器样本单增加字段，样本号排序字段,根据样本号生成
Alter Table Lis_EquipForm  Add ESampleNoForOrder    nvarchar(50)         null