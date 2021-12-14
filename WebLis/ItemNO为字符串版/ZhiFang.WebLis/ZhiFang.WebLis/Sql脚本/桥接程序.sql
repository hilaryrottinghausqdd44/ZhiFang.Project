use master
declare 
@msg varchar(1000)
use master
if exists (select * from sys.databases where name = 'WeblisClient')  
drop database WeblisClient
create database WeblisClient
if exists (select * from sys.databases where name = 'WeblisClient') 
set @msg= '1.WeblisClient 数据库创建成功|'
else
set @msg= '1.WeblisClient 数据库创建失败|'

use WeblisClient
if exists (select * from sysobjects where id = object_id('SendSampleList')) 
drop table SendSampleList 
/*==============================================================*/
/* Table: SendSampleList                                        */
/*==============================================================*/
create table SendSampleList 
(
   SendSampleFormNo     varchar(30)                    primary key,
   SendOrgBarCode       varchar(20)                    null,
   ReceiveOrgBarCode    varchar(20)                    null,
   SampleNo             varchar(20)                    null,
   NrequestNo           varchar(40)                    null,
   UpLoadDate           datetime                       null,
   UpLoadStatus         int                            null,
   ExportDate           datetime                       null,
   ExportStatus         int                            null
);

EXEC sp_addextendedproperty 'MS_Description', '1-已上传 ,2未上传,3上传失败', 'user', dbo, 'table', SendSampleList, 'column', UpLoadStatus
EXEC sp_addextendedproperty 'MS_Description', '0已导出、1已上传、4已打印、2未上传、3上传失败', 'user', dbo, 'table', SendSampleList, 'column', ExportStatus

if exists (select * from sysobjects where id = object_id('SendSampleList')) 
 set  @msg +='2.SendSampleList 表创建成功|'
 else
 set  @msg +='2.SendSampleList 表创建失败|'

 
 
 
if exists (select * from sysobjects where id = object_id('SendSampleForm')) 
drop table SendSampleForm 
/*==============================================================*/
/* Table: SendSampleForm                                        */
/*==============================================================*/
create table SendSampleForm 
(
   SendSampleFormNo     varchar(30)                    primary key,
   WebLisSourceOrgID    varchar(10)                    null,
   WebLisSourceOrgName  varchar(40)                    null,
   WebLisOrgID          varchar(10)                    null,
   WebLisOrgName        varchar(40)                    null,
   OrderCreateMan       varchar(20)                    null,
   OrderCreateDate      datetime                       null,
   PrintDate            datetime                       null,
   PirntTimes           int                            null,
   SampleNum            int                            null,
   UpLoadDate           datetime                       null,
   UpLoadStatus         int                            null,
   ExportDate           datetime                       null,
   ExportStatus         int                            null,
   OrderPrice           float                          null
);

EXEC sp_addextendedproperty 'MS_Description', '1-已上传 ,2未上传,3上传失败', 'user', dbo, 'table', SendSampleForm, 'column', UpLoadStatus
EXEC sp_addextendedproperty 'MS_Description', '0已导出、1已上传、4已打印、2未上传、3上传失败', 'user', dbo, 'table', SendSampleForm, 'column', ExportStatus

 if exists (select * from sysobjects where id = object_id('SendSampleList')) 
 set  @msg +='3.SendSampleForm 表创建成功|'
 else
 set  @msg +='3.SendSampleForm 表创建失败|'
 

if exists (select * from sysobjects where id = object_id('ReportMicro')) 
drop table ReportMicro 
/*==============================================================*/
/* Table: ReportMicro                                           */
/*==============================================================*/
create table ReportMicro 
(
   ReportFormID         varchar(50)                    null,
   FormNo               varchar(50)                    null,
   Barcode              varchar(20)                    null,
   ReceiveDate          datetime                       null,
   SectionNo            int                            null,
   TestTypeNo           int                            null,
   SampleNo             varchar(20)                    null,
   ItemNo               int                            null,
   ItemName             varchar(40)                    null,
   DescNo               int                            null,
   DescName             varchar(250)                   null,
   MicroNo              int                            null,
   MicroDesc            varchar(100)                   null,
   MicroName            varchar(40)                    null,
   AntiNo               int                            null,
   AntiName             varchar(40)                    null,
   Suscept              varchar(8)                     null,
   SusQuan              float                          null,
   RefRange             varchar(30)                    null,
   SusDesc              varchar(8)                     null,
   AntiUnit             varchar(10)                    null,
   ItemDesc             varchar(10)                    null,
   EquipNo              int                            null,
   EquipName            varchar(40)                    null,
   Modified             int                            null,
   ItemDate             datetime                       null,
   ItemTime             datetime                       null,
   CheckType            int                            null
);

EXEC sp_addextendedproperty 'MS_Description', '0-不修改,1-修改、2-作废', 'user', dbo, 'table', ReportMicro, 'column', Modified

 if exists (select * from sysobjects where id = object_id('SendSampleList')) 
 set  @msg +='4.ReportMicro 表创建成功|'
 else
 set  @msg +='4.ReportMicro 表创建失败|'

if exists (select * from sysobjects where id = object_id('ReportForm')) 
drop table ReportForm 
/*==============================================================*/
/* Table: ReportForm                                            */
/*==============================================================*/
create table ReportForm 
(
   ReportFormID         varchar(50)                    null,
   FormNo               varchar(100)                   null,
   Barcode              varchar(20)                    null,
   PersonID             varchar(20)                    null,
   ReceiveDate          datetime                       null,
   SectionNo            int                            null,
   SectionName          varchar(20)                    null,
   TestTypeNo           int                            null,
   SampleNo             int                            null,
   StatusNo             int                            null,
   StatusType           varchar(10)                    null,
   SampleTypeNo         int                            null,
   CName                varchar(30)                    null,
   GenderNo             int                            null,
   Age                  varchar(40)                    null,
   AgeUnitNo            int                            null,
   FolkNo               int                            null,
   TelNo                varchar(30)                    null,
   Doctor               varchar(30)                    null,
   Collecter            varchar(10)                    null,
   CollectDate          datetime                       null,
   CollectTime          datetime                       null,
   Technician           varchar(10)                    null,
   TestDate             datetime                       null,
   TestTime             datetime                       null,
   Operator             varchar(10)                    null,
   OperDate             datetime                       null,
   OperTime             datetime                       null,
   Checker              varchar(10)                    null,
   CheckDate            datetime                       null,
   CheckTime            datetime                       null,
   SerialNo             varchar(40)                    null,
   SickTypeNo           int                            null,
   WebLisSourceOrgID    varchar(50)                    null,
   WebLisSourceOrgName  varchar(50)                    null,
   接检单位组织机构代码           varchar(50)                    null,
   WebLisOrgName        varchar(150)                   null,
   AgeUnitName          varchar(10)                    null,
   GenderName           varchar(10)                    null,
   DeptName             varchar(40)                    null,
   DoctorName           varchar(10)                    null,
   DistrictName         varchar(20)                    null,
   WardName             varchar(40)                    null,
   FolkName             varchar(20)                    null,
   SickTypeName         varchar(20)                    null,
   SampleTypeName       varchar(20)                    null,
   TestTypeName         varchar(20)                    null
);

EXEC sp_addextendedproperty 'MS_Description', '报告单样本信息', 'user', 'dbo', 'table', 'ReportForm', NULL, NULL

EXEC sp_addextendedproperty 'MS_Description', 'WebLisOrgID', 'user', dbo, 'table', ReportForm, 'column', 接检单位组织机构代码

 if exists (select * from sysobjects where id = object_id('ReportForm')) 
 set  @msg +='5.ReportForm 表创建成功|'
 else
 set  @msg +='5.ReportForm 表创建失败|'



if exists (select * from sysobjects where id = object_id('NRequestForm')) 
drop table NRequestForm 
/*==============================================================*/
/* Table: NRequestForm                                          */
/*==============================================================*/
create table NRequestForm 
(
   SerialNo             varchar(30)                    not null,
   PersonID             varchar(20)                    not null,
   BarCode              varchar(12)                    not null,
   GenderNo             int                            null,
   GenderName           varchar(10)                    null,
   SampleType           varchar(10)                    null,
   SampleTypeNo         int                            null,
   CName                varchar(30)                    null,
   TelNo                varchar(20)                    null,
   Age                  varchar(30)                    null,
   AgeUnit              varchar(30)                    null,
   AgeUnitNo            varchar(20)                    null,
   FOLKNAME             varchar(20)                    null,
   CollectDate          datetime                       null,
   CollectTime          datetime                       null,
   WebLisSourceOrgID    varchar(50)                    null,
   WebLisSourceOrgName  varchar(100)                   null,
   WebLisOrgID          varchar(50)                    null,
   WebLisOrgName        varchar(150)                   null,
   TESTTYPENO           int                            null,
   TESTTYPEName         varchar(30)                    null,
   StatusNo             int                            null,
   StatusName           varchar(30)                    null,
   jztype               int                            null,
   jztypeName           varchar(30)                    null,
   ItemNo               varchar(20)                    null,
   ItemName             varchar(50)                    null,
   CheckTypeName        varchar(40)                    null
);

EXEC sp_addextendedproperty 'MS_Description', '人员信息表', 'user', 'dbo', 'table', 'NRequestForm', NULL, NULL

EXEC sp_addextendedproperty 'MS_Description', '申请单号', 'user', dbo, 'table', NRequestForm, 'column', SerialNo

EXEC sp_addextendedproperty 'MS_Description', '病人ID', 'user', dbo, 'table', NRequestForm, 'column', PersonID

EXEC sp_addextendedproperty 'MS_Description', '条码号', 'user', dbo, 'table', NRequestForm, 'column', BarCode

EXEC sp_addextendedproperty 'MS_Description', '性别编号', 'user', dbo, 'table', NRequestForm, 'column', GenderNo

 if exists (select * from sysobjects where id = object_id('NRequestForm')) 
 set  @msg +='6.NRequestForm 表创建成功|'
 else
 set  @msg +='6.NRequestForm 表创建失败|'



if exists (select * from sysobjects where id = object_id('ReportItem')) 
drop table ReportItem 
/*==============================================================*/
/* Table: ReportItem                                            */
/*==============================================================*/
create table ReportItem 
(
   ReportFormID             varchar(50)            null,
   FormNo               varchar(100)                   null,
   Barcode              varchar(20)                    null,
   ReceiveDate          datetime                       null,
   SectionNo            int                            null,
   SectionName          varchar(20)                    null,
   TestTypeNo           int                            null,
   SampleNo             varchar(20)                    null,
   ParItemNo            int                            null,
   ItemNo               int                            null,
   TestItemName         varchar(40)                    null,
   TestItemSName        varchar(40)                    null,
   OriginalValue        float                          null,
   ReportValue          varchar(100)                   null,
   Unit                 varchar(10)                    null,
   OriginalDesc         varchar(100)                   null,
   ReportDesc           varchar(100)                   null,
   StatusNo             int                            null,
   EquipNo              int                            null,
   EquipName            varchar(40)                    null,
   CheckType            int                            null,
   CheckTypeName        varchar(40)                    null,
   Modified             int                            null,
   RefRange             varchar(150)                   null,
   ItemDate             datetime                       null,
   ItemTime             datetime                       null,
   IsMatch              int                            null,
   ResultStatus         varchar(10)                    null,
   ParItemName          varchar(40)                    null,
   ParItemSName         varchar(40)                    null
);

EXEC sp_addextendedproperty 'MS_Description', '0-不修改,1-修改、2-作废', 'user', dbo, 'table', ReportItem, 'column', Modified

EXEC sp_addextendedproperty 'MS_Description', '0-不核对,1-核对', 'user', dbo, 'table', ReportItem, 'column', IsMatch

EXEC sp_addextendedproperty 'MS_Description', 'H-高,L-低', 'user', dbo, 'table', ReportItem, 'column', ResultStatus


 if exists (select * from sysobjects where id = object_id('ReportItem')) 
 set  @msg +='7.ReportItem 表创建成功|'
 else
 set  @msg +='7.ReportItem 表创建失败|'



/*==============================================================*/
/* Table: ReportMarrow                                          */
/*==============================================================*/
create table ReportMarrow 
(
   ReportFormID         varchar(50)                    null,
   ReportFormID2        varchar(50)                    null,
   FormNo               varchar(50)                    null,
   Barcode              varchar(20)                    null,
   ReceiveDate          datetime                       null,
   SectionNo            int                            null,
   TestTypeNo           int                            null,
   SampleNo             varchar(20)                    null,
   ParItemNo            int                            null,
   ItemNo               int                            null,
   ItemCName            varchar(40)                    null,
   ItemEName            varchar(10)                    null,
   ParItemCName         varchar(40)                    null,
   ParItemEName         varchar(40)                    null,
   BloodNum             int                            null,
   BloodPercent         varchar(250)                   null,
   MarrowNum            int                            null,
   MarrowPercent        varchar(100)                   null,
   BloodDesc            varchar(100)                   null,
   MarrowDesc           varchar(100)                   null,
   RefRange             varchar(100)                   null,
   EquipNo              int                            null,
   EquipName            varchar(40)                    null,
   ItemDate             datetime                       null,
   ItemTime             datetime                       null,
   ResultStatus         varchar(10)                    null
);

EXEC sp_addextendedproperty 'MS_Description', 'H-高,L-低', 'user', dbo, 'table', ReportMarrow, 'column', ResultStatus

 if exists (select * from sysobjects where id = object_id('ReportMarrow')) 
 set  @msg +='8.ReportMarrow 表创建成功|'
 else
 set  @msg +='8.ReportMarrow 表创建失败|'

 print replace(@msg,'|',char(10))
