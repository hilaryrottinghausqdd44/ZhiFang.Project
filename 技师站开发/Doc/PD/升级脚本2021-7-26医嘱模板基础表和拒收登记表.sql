if exists (select 1
            from  sysobjects
           where  id = object_id('Lis_BarCodeRefuseRecord')
            and   type = 'U')
   drop table Lis_BarCodeRefuseRecord
go

/*==============================================================*/
/* Table: Lis_BarCodeRefuseRecord                               */
/*==============================================================*/
create table Lis_BarCodeRefuseRecord (
   LabID                bigint               not null,
   RefuseRecordID       bigint               not null,
   PhrasesWatchClassItemID bigint               not null,
   PhrasesWatchClassID  bigint               null,
   RefuseID             bigint               null,
   RefuseValue          nvarchar(200)        null,
   RefuseOperate        nvarchar(200)        null,
   Memo                 nvarchar(500)        null,
   OperateUserID        bigint               null,
   OperateUser          nvarchar(100)        null,
   OperateHost          nvarchar(100)        null,
   OperateAddress       nvarchar(100)        null,
   OperateHostType      nvarchar(100)        null,
   RelationUserID       bigint               null,
   RelationUser         nvarchar(100)        null,
   DeptID               bigint               null,
   Dept                 nvarchar(100)        null,
   DeptTelNo            nvarchar(100)        null,
   BarCode              nvarchar(100)        null,
   IOFlag               int                  null,
   IOUserID             bigint               null,
   IOUserName           nvarchar(100)        null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LIS_BARCODEREFUSERECORD primary key nonclustered (RefuseRecordID)
)
go
if exists (select 1
            from  sysobjects
           where  id = object_id('LB_OrderModel')
            and   type = 'U')
   drop table LB_OrderModel
go

/*==============================================================*/
/* Table: LB_OrderModel                                         */
/*==============================================================*/
create table LB_OrderModel (
   OrderModelID         bigint               not null,
   POrderModelID        bigint               null,
   OrderModelTypeID     bigint               null,
   OrderModelTypeName   nvarchar(50)         null,
   CName                nvarchar(100)        null,
   DeptID               bigint               null,
   UserID               bigint               null,
   SName                nvarchar(100)        null,
   SCode                nvarchar(100)        null,
   OrderModelDesc       nvarchar(500)        null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ORDERMODEL primary key (OrderModelID)
)
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LB_OrderModelItem')
            and   type = 'U')
   drop table LB_OrderModelItem
go

/*==============================================================*/
/* Table: LB_OrderModelItem                                     */
/*==============================================================*/
create table LB_OrderModelItem (
   OrderModelItemID     bigint               not null,
   OrderModelID         bigint               not null,
   ItemID               bigint               not null,
   IsUse                bit                  null,
   DispOrder            int                  null,
   LabID                bigint               not null,
   DataAddTime          datetime             null,
   DataUpdateTime       datetime             null,
   DataTimeStamp        timestamp            null,
   constraint PK_LB_ORDERMODELITEM primary key (OrderModelItemID)
)
go