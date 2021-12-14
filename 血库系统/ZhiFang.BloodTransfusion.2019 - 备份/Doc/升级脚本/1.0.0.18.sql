

IF COL_LENGTH('Blood_BInItem', 'LabID') IS NULL ALTER TABLE Blood_BInItem ADD LabID bigint; 

 IF COL_LENGTH('Blood_BInItem', 'DispOrder') IS NULL ALTER TABLE Blood_BInItem ADD DispOrder int; 

 IF COL_LENGTH('Blood_BInItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BInItem ADD DataAddTime datetime; 

 IF COL_LENGTH('Blood_BInItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BInItem ADD DataTimeStamp timestamp; 

 IF COL_LENGTH('Blood_BInItem', 'Visible') IS NULL ALTER TABLE Blood_BInItem ADD Visible bit; 

 update Blood_BInItem set LabID=0 where LabID is null; 

 update Blood_BInItem set Visible=1 where Visible is null; 

 update Blood_BInItem set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_BPreForm', 'LabID') IS NULL ALTER TABLE Blood_BPreForm ADD LabID bigint; 

IF COL_LENGTH('Blood_BPreForm', 'DispOrder') IS NULL ALTER TABLE Blood_BPreForm ADD DispOrder int; 

IF COL_LENGTH('Blood_BPreForm', 'DataAddTime') IS NULL ALTER TABLE Blood_BPreForm ADD DataAddTime datetime; 

IF COL_LENGTH('Blood_BPreForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BPreForm ADD DataTimeStamp timestamp; 

IF COL_LENGTH('Blood_BPreForm', 'Visible') IS NULL ALTER TABLE Blood_BPreForm ADD Visible bit;

 update Blood_BPreForm set LabID=0 where LabID is null; 

update Blood_BPreForm set DataAddTime=getdate() where DataAddTime is null; 

update Blood_BPreForm set Visible=1 where Visible is null; 

update Blood_BPreForm set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_BPreItem', 'LabID') IS NULL ALTER TABLE Blood_BPreItem ADD LabID bigint; 

IF COL_LENGTH('Blood_BPreItem', 'DispOrder') IS NULL ALTER TABLE Blood_BPreItem ADD DispOrder int; 

IF COL_LENGTH('Blood_BPreItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BPreItem ADD DataAddTime datetime; 

IF COL_LENGTH('Blood_BPreItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BPreItem ADD DataTimeStamp timestamp; 

IF COL_LENGTH('Blood_BPreItem', 'Visible') IS NULL ALTER TABLE Blood_BPreItem ADD Visible bit; 

update Blood_BPreItem set LabID=0 where LabID is null; 

 update Blood_BPreItem set DataAddTime=getdate() where DataAddTime is null; 

 update Blood_BPreItem set Visible=1 where Visible is null; 

 update Blood_BPreItem set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_BOutForm', 'LabID') IS NULL ALTER TABLE Blood_BOutForm ADD LabID bigint; 

 IF COL_LENGTH('Blood_BOutForm', 'DispOrder') IS NULL ALTER TABLE Blood_BOutForm ADD DispOrder int; 

IF COL_LENGTH('Blood_BOutForm', 'DataAddTime') IS NULL ALTER TABLE Blood_BOutForm ADD DataAddTime datetime; 

IF COL_LENGTH('Blood_BOutForm', 'DataUpdateTime') IS NULL ALTER TABLE Blood_BOutForm ADD DataUpdateTime datetime; 

IF COL_LENGTH('Blood_BOutForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BOutForm ADD DataTimeStamp timestamp; 

IF COL_LENGTH('Blood_BOutForm', 'Visible') IS NULL ALTER TABLE Blood_BOutForm ADD Visible bit;

IF COL_LENGTH('Blood_BOutForm', 'ConfirmCompletion') IS NULL ALTER TABLE Blood_BOutForm ADD ConfirmCompletion int; 

 IF COL_LENGTH('Blood_BOutForm', 'HandoverCompletion') IS NULL ALTER TABLE Blood_BOutForm ADD HandoverCompletion int;

IF COL_LENGTH('Blood_BOutForm', 'CourseCompletion') IS NULL ALTER TABLE Blood_BOutForm ADD CourseCompletion int; 

IF COL_LENGTH('Blood_BOutForm', 'RecoverCompletion') IS NULL ALTER TABLE Blood_BOutForm ADD RecoverCompletion int; 

update Blood_BOutForm set LabID=0 where LabID is null;

update Blood_BOutForm set DataAddTime=getdate() where DataAddTime is null; 

update Blood_BOutForm set DataUpdateTime=getdate() where DataUpdateTime is null; 

update Blood_BOutForm set Visible=1 where Visible is null; 

update Blood_BOutForm set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_BOutItem', 'LabID') IS NULL ALTER TABLE Blood_BOutItem ADD LabID bigint; 

IF COL_LENGTH('Blood_BOutItem', 'DispOrder') IS NULL ALTER TABLE Blood_BOutItem ADD DispOrder int; 

IF COL_LENGTH('Blood_BOutItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BOutItem ADD DataAddTime datetime; 

 IF COL_LENGTH('Blood_BOutItem', 'DataUpdateTime') IS NULL ALTER TABLE Blood_BOutItem ADD DataUpdateTime datetime;

IF COL_LENGTH('Blood_BOutItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BOutItem ADD DataTimeStamp timestamp;

IF COL_LENGTH('Blood_BOutItem', 'Visible') IS NULL ALTER TABLE Blood_BOutItem ADD Visible bit; 

IF COL_LENGTH('Blood_BOutItem', 'ConfirmCompletion') IS NULL ALTER TABLE Blood_BOutItem ADD ConfirmCompletion int; 

IF COL_LENGTH('Blood_BOutItem', 'HandoverCompletion') IS NULL ALTER TABLE Blood_BOutItem ADD HandoverCompletion int;

IF COL_LENGTH('Blood_BOutItem', 'CourseCompletion') IS NULL ALTER TABLE Blood_BOutItem ADD CourseCompletion int; 

IF COL_LENGTH('Blood_BOutItem', 'RecoverCompletion') IS NULL ALTER TABLE Blood_BOutItem ADD RecoverCompletion int; 

 update Blood_BOutItem set LabID=0 where LabID is null;

update Blood_BOutItem set DataAddTime=BODate where DataAddTime is null; 

update Blood_BOutItem set DataUpdateTime=getdate() where DataUpdateTime is null; 

update Blood_BOutItem set Visible=1 where Visible is null; 

update Blood_BOutItem set DispOrder=0 where DispOrder is null; 

IF COL_LENGTH('Blood_UsePlace', 'LabID') IS NULL ALTER TABLE Blood_UsePlace ADD LabID bigint; 

IF COL_LENGTH('Blood_UsePlace', 'DispOrder') IS NULL ALTER TABLE Blood_UsePlace ADD DispOrder int; 

IF COL_LENGTH('Blood_UsePlace', 'DataAddTime') IS NULL ALTER TABLE Blood_UsePlace ADD DataAddTime datetime; 

IF COL_LENGTH('Blood_UsePlace', 'DataTimeStamp') IS NULL ALTER TABLE Blood_UsePlace ADD DataTimeStamp timestamp; 

IF COL_LENGTH('Blood_UsePlace', 'Visible') IS NULL ALTER TABLE Blood_UsePlace ADD Visible bit; 

update Blood_UsePlace set LabID=0 where LabID is null; 

update Blood_UsePlace set DataAddTime=getdate() where DataAddTime is null; 

update Blood_UsePlace set Visible=1 where Visible is null; 

 update Blood_UsePlace set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_ABO', 'LabID') IS NULL ALTER TABLE Blood_ABO ADD LabID bigint; 

IF COL_LENGTH('Blood_ABO', 'DispOrder') IS NULL ALTER TABLE Blood_ABO ADD DispOrder int;

IF COL_LENGTH('Blood_ABO', 'DataAddTime') IS NULL ALTER TABLE Blood_ABO ADD DataAddTime datetime; 

IF COL_LENGTH('Blood_ABO', 'DataTimeStamp') IS NULL ALTER TABLE Blood_ABO ADD DataTimeStamp timestamp; 

IF COL_LENGTH('Blood_ABO', 'Visible') IS NULL ALTER TABLE Blood_ABO ADD Visible bit; 

update Blood_ABO set LabID=0 where LabID is null; 

 update Blood_ABO set DataAddTime=getdate() where DataAddTime is null; 

update Blood_ABO set Visible=1 where Visible is null;

update Blood_ABO set Visible=1 where Visible is null;

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BLOOD_BA_REFERENCE_BLOOD_BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperationDtl]')) ALTER TABLE [dbo].[Blood_BagOperationDtl] DROP CONSTRAINT [FK_BLOOD_BA_REFERENCE_BLOOD_BA]; 

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransItem_Blood_TransForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransItem]')) ALTER TABLE [dbo].[Blood_TransItem] DROP CONSTRAINT [FK_Blood_TransItem_Blood_TransForm]; 

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransItem_Blood_TransRecordTypeItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransItem]')) ALTER TABLE [dbo].[Blood_TransItem] DROP CONSTRAINT [FK_Blood_TransItem_Blood_TransRecordTypeItem];

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BLOOD_TR_REFERENCE_BLOOD_TR]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransRecordTypeItem]')) ALTER TABLE [dbo].[Blood_TransRecordTypeItem] DROP CONSTRAINT [FK_BLOOD_TR_REFERENCE_BLOOD_TR];

 IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperation_Blood_BReqForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperation]')) ALTER TABLE [dbo].[Blood_BagOperation] DROP CONSTRAINT [FK_Blood_BagOperation_Blood_BReqForm];

 IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperationDtl_Blood_BagOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperationDtl]')) ALTER TABLE [dbo].[Blood_BagOperationDtl] DROP CONSTRAINT [FK_Blood_BagOperationDtl_Blood_BagOperation];

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransForm_Blood_BReqForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransForm]')) ALTER TABLE [dbo].[Blood_TransForm] DROP CONSTRAINT [FK_Blood_TransForm_Blood_BReqForm]; 

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransForm_Blood_Style]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransForm]')) ALTER TABLE [dbo].[Blood_TransForm] DROP CONSTRAINT [FK_Blood_TransForm_Blood_Style]; 

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperation_Blood_BReqForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperation]')) ALTER TABLE [dbo].[Blood_BagOperation] DROP CONSTRAINT [FK_Blood_BagOperation_Blood_BReqForm]; 

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperation_Blood_Style]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperation]')) ALTER TABLE [dbo].[Blood_BagOperation] DROP CONSTRAINT [FK_Blood_BagOperation_Blood_Style]; 

IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperationDtl_B_Dict]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperationDtl]')) ALTER TABLE [dbo].[Blood_BagOperationDtl] DROP CONSTRAINT [FK_Blood_BagOperationDtl_B_Dict];

 if exists(select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransItem') and o.name = 'FK_BLOOD_TR_REFERENCE_BLOOD_TR') alter table Blood_TransItem drop constraint FK_BLOOD_TR_REFERENCE_BLOOD_TR; if exists (select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransItem') and o.name = 'FK_BLOOD_TR_REFERENCE_BLOOD_TR') alter table Blood_TransItem drop constraint FK_BLOOD_TR_REFERENCE_BLOOD_TR; if exists (select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransRecordTypeItem') and o.name = 'FK_BLOOD_TR_REFERENCE_BLOOD_TR') alter table Blood_TransRecordTypeItem drop constraint FK_BLOOD_TR_REFERENCE_BLOOD_TR; if exists (select 1 from sysobjects where id = object_id('Blood_TransRecordType') and type = 'U') drop table Blood_TransRecordType; create table Blood_TransRecordType ( LabID bigint null, TransRecordTypeID bigint not null, ContentTypeID int null, TransRecordType varchar(50) null, TypeCode varchar(50) null, DispOrder int null, IsVisible bit null, Memo ntext null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_TRANSRECORDTYPE primary key (TransRecordTypeID)); 

 if exists(select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransRecordTypeItem') and o.name = 'FK_BLOOD_TR_REFERENCE_BLOOD_TR') alter table Blood_TransRecordTypeItem drop constraint FK_BLOOD_TR_REFERENCE_BLOOD_TR; if exists (select 1 from sysobjects where id = object_id('Blood_TransRecordTypeItem') and type = 'U') drop table Blood_TransRecordTypeItem; create table Blood_TransRecordTypeItem ( LabID bigint null, TransRecordTypeItemID bigint not null, TransRecordTypeID bigint null, TransItemCode varchar(50) null, TransItemName varchar(50) null, SName varchar(80) null, Shortcode varchar(50) null, PinYinZiTou varchar(50) null, TransItemEditInfo ntext null, DispOrder int null, IsVisible bit null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_TRANSRECORDTYPEITEM primary key (TransRecordTypeItemID));

 IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BLOOD_TR_REFERENCE_BLOOD_TR]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransRecordTypeItem]')) ALTER TABLE [dbo].[Blood_TransRecordTypeItem] DROP CONSTRAINT [FK_BLOOD_TR_REFERENCE_BLOOD_TR]; ALTER TABLE [dbo].[Blood_TransRecordTypeItem] WITH CHECK ADD CONSTRAINT [FK_BLOOD_TR_REFERENCE_BLOOD_TR] FOREIGN KEY([TransRecordTypeID]) REFERENCES [dbo].[Blood_TransRecordType] ([TransRecordTypeID]); ALTER TABLE [dbo].[Blood_TransRecordTypeItem] CHECK CONSTRAINT [FK_BLOOD_TR_REFERENCE_BLOOD_TR];



