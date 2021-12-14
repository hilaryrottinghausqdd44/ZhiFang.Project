IF COL_LENGTH('Blood_TransItem', 'TransRecordTypeID') IS NULL ALTER TABLE Blood_TransItem ADD TransRecordTypeID bigint;
ALTER TABLE Blood_TransItem ALTER COLUMN [LabID] bigint NULL;
      
 if not Exists(Select * from SysColumns where [Name]='ID' and ID =(Select [ID] from SysObjects where Name = 'blood_bagProcessTypeQry')) ALTER TABLE blood_bagProcessTypeQry ADD ID BIGINT IDENTITY NOT NULL;
ALTER TABLE blood_bagProcessTypeQry ALTER COLUMN [LabID] bigint NULL;
        
 if not Exists(Select * from SysColumns where [Name]='ID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BagABOCheck_LisItem')) ALTER TABLE Blood_BagABOCheck_LisItem ADD ID BIGINT IDENTITY NOT NULL;
 ALTER TABLE Blood_BagABOCheck_LisItem ALTER COLUMN [LabID] bigint NULL;
        
ALTER TABLE Blood_BagProcess ALTER COLUMN [LabID] bigint NULL; 

ALTER TABLE blood_BagProcessType ALTER COLUMN [LabID] bigint NULL; 

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260811) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260811,N'CS�������URL',N'CS�ӿ�',N'CONFIG',N'BL-SYSE-CSRL-0011',N'http://localhost',N'���õ���CS�����URL',11,1,1,N'2020-03-16 10:14:38');

 IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260900) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260900,N'�Ƿ���Ѫ���̼�¼�ǼǺ����Ѫ�����յǼ�',N'��ʿվ',N'CONFIG',N'BL-BBBO-BBRR-0012',N'1',N'1:��;0:��;',12,1,1,N'2020-03-19 10:14:38');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260901) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260901,N'������Ѫ���̵Ǽ�ʱ�Ƿ���Ӳ�����¼',N'��ʿվ',N'CONFIG',N'BL-BBBO-BBTF-0013',N'1',N'1:��;0:��;',13,1,1,N'2020-03-21 12:01:38');

if exists(select 1 from sysobjects where id = object_id('Blood_TransOperation') and type = 'U') drop table Blood_TransOperation; create table Blood_TransOperation ( LabID bigint null, OperationID bigint not null, BOutItemID varchar(20) null, TransFormID bigint null, BloodNo int null, ContentTypeID int null, TransRecordTypeID bigint null, BusinessCode varchar(40) null, Memo varchar(5000) null, DispOrder int null, IsUse bit null, CreatorID bigint null, CreatorName varchar(50) null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_TRANSOPERATION primary key (OperationID));