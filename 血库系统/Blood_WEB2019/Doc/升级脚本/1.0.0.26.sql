

if Exists(Select * from SysColumns where [Name]='BloodNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodNo int;

if Exists(Select * from SysColumns where [Name]='BloodABONo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodABONo nvarchar(20);

if Exists(Select * from SysColumns where [Name]='BloodUnitNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodUnitNo int;

if Exists(Select * from SysColumns where [Name]='BloodABONo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BInItem')) alter table Blood_BInItem ALTER COLUMN BloodABONo nvarchar(20); 

IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5485690321920592783) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5485690321920592783,4,N'不良反应选择项',N'AdverseReactionOptions',30000,1,N'不良反应选择项',N'2020-02-18 10:41:43');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5118439131802773757) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5118439131802773757,5,N'临床处理结果',N'ClinicalResults',40000,1,N'临床处理结果',N'2020-02-18 10:54:59');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5065045818603329234) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5065045818603329234,6,N'临床处理结果描述',N'ClinicalResultsDesc',50000,1,N'临床处理结果描述',N'2020-02-18 10:55:23');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4803836600047891481) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4803836600047891481,5485690321920592783,N'10',N'发热',N'10',10,1,N'2020-02-18 10:43:29');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5531342248897198408) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5531342248897198408,5485690321920592783,N'20',N'恶心呕吐',N'20',20,1,N'2020-02-18 10:43:50');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5555439810679451060) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5555439810679451060,5485690321920592783,N'30',N'血压升高',N'30',30,1,N'2020-02-18 10:45:08');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5098846963842011802) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5098846963842011802,5118439131802773757,N'10',N'对症处理',N'10',10,1,N'2020-02-18 10:57:50');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5321905214369573002) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5321905214369573002,5065045818603329234,N'10',N'临床处理结果描述',N'10',10,1,N'2020-02-18 10:58:19');


