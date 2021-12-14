
IF COL_LENGTH('Blood_BInForm', 'LabID') IS NULL ALTER TABLE Blood_BInForm ADD LabID bigint;

IF COL_LENGTH('Blood_BInForm', 'DispOrder') IS NULL ALTER TABLE Blood_BInForm ADD DispOrder int;

IF COL_LENGTH('Blood_BInForm', 'DataAddTime') IS NULL ALTER TABLE Blood_BInForm ADD DataAddTime datetime;

 IF COL_LENGTH('Blood_BInForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BInForm ADD DataTimeStamp timestamp;

IF COL_LENGTH('Blood_BInForm', 'Visible') IS NULL ALTER TABLE Blood_BInForm ADD Visible bit;

update Blood_BInForm set LabID=0 where LabID is null;

update Blood_BInForm set Visible=1 where Visible is null;

update Blood_BInForm set DispOrder=0 where DispOrder is null;

IF COL_LENGTH('Blood_BInItemState', 'LabID') IS NULL ALTER TABLE Blood_BInItemState ADD LabID bigint; 

 IF COL_LENGTH('Blood_BInItemState', 'DispOrder') IS NULL ALTER TABLE Blood_BInItemState ADD DispOrder int;

 IF COL_LENGTH('Blood_BInItemState', 'DataAddTime') IS NULL ALTER TABLE Blood_BInItemState ADD DataAddTime datetime;

IF COL_LENGTH('Blood_BInItemState', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BInItemState ADD DataTimeStamp timestamp;

 IF COL_LENGTH('Blood_BInItemState', 'Visible') IS NULL ALTER TABLE Blood_BInItemState ADD Visible bit;

update Blood_BInItemState set LabID=0 where LabID is null;

 update Blood_BInItemState set Visible=1 where Visible is null;

 update Blood_BInItemState set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_Recei', 'LabID') IS NULL ALTER TABLE Blood_Recei ADD LabID bigint;

IF COL_LENGTH('Blood_Recei', 'DispOrder') IS NULL ALTER TABLE Blood_Recei ADD DispOrder int; 

IF COL_LENGTH('Blood_Recei', 'DataAddTime') IS NULL ALTER TABLE Blood_Recei ADD DataAddTime datetime; 

IF COL_LENGTH('Blood_Recei', 'DataTimeStamp') IS NULL ALTER TABLE Blood_Recei ADD DataTimeStamp timestamp;

 IF COL_LENGTH('Blood_Recei', 'Visible') IS NULL ALTER TABLE Blood_Recei ADD Visible bit;

 update Blood_Recei set LabID=0 where LabID is null; 

 update Blood_Recei set Visible=1 where Visible is null;

 update Blood_Recei set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_ReceiItem', 'LabID') IS NULL ALTER TABLE Blood_ReceiItem ADD LabID bigint; 

 IF COL_LENGTH('Blood_ReceiItem', 'DispOrder') IS NULL ALTER TABLE Blood_ReceiItem ADD DispOrder int; 

IF COL_LENGTH('Blood_ReceiItem', 'DataAddTime') IS NULL ALTER TABLE Blood_ReceiItem ADD DataAddTime datetime;

IF COL_LENGTH('Blood_ReceiItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_ReceiItem ADD DataTimeStamp timestamp; 

 IF COL_LENGTH('Blood_ReceiItem', 'Visible') IS NULL ALTER TABLE Blood_ReceiItem ADD Visible bit; 

 update Blood_ReceiItem set LabID=0 where LabID is null; 

 update Blood_ReceiItem set Visible=1 where Visible is null; 

 update Blood_ReceiItem set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_refuse', 'LabID') IS NULL ALTER TABLE Blood_refuse ADD LabID bigint; 

 IF COL_LENGTH('Blood_refuse', 'DispOrder') IS NULL ALTER TABLE Blood_refuse ADD DispOrder int; 

 IF COL_LENGTH('Blood_refuse', 'DataAddTime') IS NULL ALTER TABLE Blood_refuse ADD DataAddTime datetime; 

 IF COL_LENGTH('Blood_refuse', 'DataTimeStamp') IS NULL ALTER TABLE Blood_refuse ADD DataTimeStamp timestamp; 

 IF COL_LENGTH('Blood_refuse', 'Visible') IS NULL ALTER TABLE Blood_refuse ADD Visible bit;

 update Blood_refuse set LabID=0 where LabID is null;

update Blood_refuse set Visible=1 where Visible is null; 

 update Blood_refuse set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_refuseDispose', 'LabID') IS NULL ALTER TABLE Blood_refuseDispose ADD LabID bigint; 

 IF COL_LENGTH('Blood_refuseDispose', 'DispOrder') IS NULL ALTER TABLE Blood_refuseDispose ADD DispOrder int; 

 IF COL_LENGTH('Blood_refuseDispose', 'DataAddTime') IS NULL ALTER TABLE Blood_refuseDispose ADD DataAddTime datetime; 

 IF COL_LENGTH('Blood_refuseDispose', 'DataTimeStamp') IS NULL ALTER TABLE Blood_refuseDispose ADD DataTimeStamp timestamp; 

 IF COL_LENGTH('Blood_refuseDispose', 'Visible') IS NULL ALTER TABLE Blood_refuseDispose ADD Visible bit; 

 update Blood_refuseDispose set LabID=0 where LabID is null; 

 update Blood_refuseDispose set Visible=1 where Visible is null;

 update Blood_refuseDispose set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_BagABOCheck', 'LabID') IS NULL ALTER TABLE Blood_BagABOCheck ADD LabID bigint; 

 IF COL_LENGTH('Blood_BagABOCheck', 'DispOrder') IS NULL ALTER TABLE Blood_BagABOCheck ADD DispOrder int; 

IF COL_LENGTH('Blood_BagABOCheck', 'DataAddTime') IS NULL ALTER TABLE Blood_BagABOCheck ADD DataAddTime datetime;

 IF COL_LENGTH('Blood_BagABOCheck', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BagABOCheck ADD DataTimeStamp timestamp; 

 IF COL_LENGTH('Blood_BagABOCheck', 'Visible') IS NULL ALTER TABLE Blood_BagABOCheck ADD Visible bit; 

 update Blood_BagABOCheck set LabID=0 where LabID is null; 

 update Blood_BagABOCheck set Visible=1 where Visible is null; 

 update Blood_BagABOCheck set DispOrder=0 where DispOrder is null; 

 IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'LabID') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD LabID bigint;

IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'DispOrder') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD DispOrder int; 

 IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD DataAddTime datetime; 

 IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD DataTimeStamp timestamp; 

 IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'Visible') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD Visible bit; 

 update Blood_BagABOCheck_LisItem set LabID=0 where LabID is null; 

 update Blood_BagABOCheck_LisItem set Visible=1 where Visible is null; 

 update Blood_BagABOCheck_LisItem set DispOrder=0 where DispOrder is null; 

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 4780816011182130210) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES( 0,4780816011182130210,3,N'输血临床处理措施',N'ClinicalMeasures',100000,1,N'ClinicalMeasures',N'2020-02-13 10:54:00'); 

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 4712222272559814781) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,4712222272559814781,1,N'输血前',N'1001',1001,1,N'输血记录项的输血前分类',N'2020-02-13 10:52:32'); 

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5510188224824882839) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5510188224824882839,1,N'输血15分钟',N'1002',1002,1,N'输血记录项的输血15分钟分类',N'2020-02-13 11:00:07');

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5109775278160421817) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5109775278160421817,1,N'输血60分钟',N'1003',1003,1,N'输血记录项的输血60分钟分类',N'2020-02-13 11:01:04');

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5660775821937029735) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5660775821937029735,1,N'输血2小时',N'1004',1004,1,N'输血记录项的输血2小时分类',N'2020-02-13 11:01:38'); 

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 4959979330910815717) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,4959979330910815717,1,N'输血3小时',N'1005',1005,1,N'输血记录项的输血3小时分类',N'2020-02-13 11:02:06'); 

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 4748574105933710009) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,4748574105933710009,1,N'输血4小时',N'1006',1006,1,N'输血记录项的输血4小时分类',N'2020-02-13 11:02:39');

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20002) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20002,2,N'输血15分钟',N'20002',20002,1,N'输血不良反应的输血15分钟分类',N'2020-02-13 11:00:07');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20003) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20003,2,N'输血60分钟',N'20003',20003,1,N'输血不良反应的输血60分钟分类',N'2020-02-13 11:01:04'); 

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20004) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20004,2,N'输血2小时',N'20004',20004,1,N'输血不良反应的输血2小时分类',N'2020-02-13 11:01:38');

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20005) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20005,2,N'输血3小时',N'20005',20005,1,N'输血不良反应的输血3小时分类',N'2020-02-13 11:02:06');

 IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20006) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20006,2,N'输血4小时',N'20006',20006,1,N'输血不良反应的输血4小时分类',N'2020-02-13 11:02:39'); 
