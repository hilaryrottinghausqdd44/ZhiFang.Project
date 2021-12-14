IF COL_LENGTH('Department', 'MonitorType') IS NULL ALTER TABLE Department ADD MonitorType bigint;
Update Department set MonitorType=0 where MonitorType is null;

 IF COL_LENGTH('B_DictType', 'UseCode') IS NULL ALTER TABLE B_DictType ADD UseCode varchar(60); 

 IF COL_LENGTH('B_DictType', 'DeveCode') IS NULL ALTER TABLE B_DictType ADD DeveCode varchar(60); 

 IF COL_LENGTH('B_DictType', 'StandCode') IS NULL ALTER TABLE B_DictType ADD StandCode varchar(60); 


 IF COL_LENGTH('B_Dict', 'UseCode') IS NULL ALTER TABLE B_Dict ADD UseCode varchar(60); 

 IF COL_LENGTH('B_Dict', 'DeveCode') IS NULL ALTER TABLE B_Dict ADD DeveCode varchar(60); 

 IF COL_LENGTH('B_Dict', 'StandCode') IS NULL ALTER TABLE B_Dict ADD StandCode varchar(60); 

 
 IF COL_LENGTH('AntiGroup', 'LabID') IS NULL ALTER TABLE AntiGroup ADD LabID bigint;
                
IF COL_LENGTH('AntiGroup', 'DataAddTime') IS NULL ALTER TABLE AntiGroup ADD DataAddTime datetime;
                
IF COL_LENGTH('AntiGroup', 'DataTimeStamp') IS NULL ALTER TABLE AntiGroup ADD DataTimeStamp timestamp;

IF COL_LENGTH('ChargeType', 'LabID') IS NULL ALTER TABLE ChargeType ADD LabID bigint;
                
IF COL_LENGTH('ChargeType', 'DataAddTime') IS NULL ALTER TABLE ChargeType ADD DataAddTime datetime;
                
IF COL_LENGTH('ChargeType', 'DataTimeStamp') IS NULL ALTER TABLE ChargeType ADD DataTimeStamp timestamp;

IF COL_LENGTH('SamplingGroup', 'LabID') IS NULL ALTER TABLE SamplingGroup ADD LabID bigint;
                
IF COL_LENGTH('SamplingGroup', 'DataAddTime') IS NULL ALTER TABLE SamplingGroup ADD DataAddTime datetime;
                
IF COL_LENGTH('SamplingGroup', 'DataTimeStamp') IS NULL ALTER TABLE SamplingGroup ADD DataTimeStamp timestamp;

IF COL_LENGTH('Samplingitem', 'LabID') IS NULL ALTER TABLE Samplingitem ADD LabID bigint;
                
IF COL_LENGTH('Samplingitem', 'DataAddTime') IS NULL ALTER TABLE Samplingitem ADD DataAddTime datetime;
                
IF COL_LENGTH('Samplingitem', 'DataTimeStamp') IS NULL ALTER TABLE Samplingitem ADD DataTimeStamp timestamp;

IF COL_LENGTH('SectionItem', 'LabID') IS NULL ALTER TABLE SectionItem ADD LabID bigint;
                
IF COL_LENGTH('SectionItem', 'DataAddTime') IS NULL ALTER TABLE SectionItem ADD DataAddTime datetime;
                
IF COL_LENGTH('SectionItem', 'DataTimeStamp') IS NULL ALTER TABLE SectionItem ADD DataTimeStamp timestamp;

IF COL_LENGTH('StatusType', 'LabID') IS NULL ALTER TABLE StatusType ADD LabID bigint;
                
IF COL_LENGTH('StatusType', 'DataAddTime') IS NULL ALTER TABLE StatusType ADD DataAddTime datetime;
                
IF COL_LENGTH('StatusType', 'DataTimeStamp') IS NULL ALTER TABLE StatusType ADD DataTimeStamp timestamp; 

 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30000,30,0,0,0,3,N'package.PNG',N'基础维护',N'JCWH',N'JCWH',1,30000,N'2020-11-11 10:18:29');

 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30001) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30001,30000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordtype.App',N'监测类型维护',N'JCLXWH',N'JCLXWH',1,30001,N'2020-11-11 10:19:44');

 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30002) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30002,30000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordtypeitem.App',N'型与记录项维护',N'XYJLXWH',N'XYJLXWH',1,30002,N'2020-11-11 10:20:21');

 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30003) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30003,30000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordphrase.ofdept.App',N'记录项短语-按科室',N'JLXDY-AKS',N'JLXDY-AKS',1,30003,N'2020-11-11 10:21:00');

 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30004) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30004,30000,0,0,0,0,N'list.PNG',N'#Shell.class.sysbase.screcorditemlink.App',N'科室自动核收',N'KSZDHS',N'KSZDHS',1,30004,N'2020-11-11 10:21:51');


 IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GK_DeptAutoCheckLink]') AND type in(N'U')) CREATE TABLE [dbo].[GK_DeptAutoCheckLink]( [LabID] [bigint] NULL, [LinkId] [bigint] NOT NULL, [DeptNo] [bigint] NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_GK_DEPTAUTOCHECKLINK] PRIMARY KEY CLUSTERED( [LinkId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY];


 IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_InterfaceMaping]') AND type in(N'U')) CREATE TABLE [dbo].[SC_InterfaceMaping]( [LabID] [bigint] NULL, [MappingId] [bigint] NOT NULL, [BobjectType] [bigint] NOT NULL, [BobjectID] [bigint] NOT NULL, [MapingCode] [varchar](60) NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_InterfaceMaping] PRIMARY KEY CLUSTERED( [MappingId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY];


IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 300) INSERT [B_DictType] ([LabID],[DCId],[DictTypeCode],[CName],[SName],[ShortCode],[PinYinZiTou],[IsUse],[DispOrder],[Memo],[DataAddTime]) VALUES ( 0,300,N'SCInterfaceMaping',N'接口对照映射类型',N'JKDZYSLX',N'JKDZYSLX',N'JKDZYSLX',1,300,N'在该类型下维护各种业务接口的对照',N'2020-07-30 10:07:34');

IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300120) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300120,300,N'人员HIS对照',N'RYHISDZ',N'RYHISDZ',N'RYHISDZ',N'PUser',1,30012000,N'2020-07-30 10:10:15',N'300120:人员对照;');

IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300130) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300130,300,N'科室HIS对照',N'KSHISDZ',N'KSHISDZ',N'KSHISDZ',N'Department',1,30013000,N'2020-07-30 10:10:37',N'300130:科室对照;');

IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300160) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300160,300,N'检验项目LIS对照',N'JYXMLISDZ',N'JYXMLISDZ',N'JYXMLISDZ',N'TestItem',1,300160,N'2020-07-30 10:13:55',N'300160:检验项目LIS对照;');


IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300200) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[UseCode],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300200,300,N'申请类型HIS对照',N'SQLXHISDZ',N'SQLXHISDZ',N'SQLXHISDZ',N'20',N'BDict',1,300200,N'2020-07-30 10:23:35',N'300200:申请类型HIS对照;');

IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300210) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[UseCode],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300210,300,N'就诊类型HIS对照',N'JZLXHISDZ',N'JZLXHISDZ',N'JZLXHISDZ',N'25',N'BDict',1,300210,N'2020-07-30 10:24:23',N'300210:就诊类型HIS对照;');


IF COL_LENGTH('GK_SampleRequestForm', 'CName') IS NULL ALTER TABLE GK_SampleRequestForm ADD CName varchar(40); 
IF COL_LENGTH('RBAC_Role', 'SySType') IS NULL ALTER TABLE RBAC_Role ADD SySType bigint;
IF NOT EXISTS(SELECT * FROM [SickType] WHERE [SickTypeNo] = 8) INSERT [SickType] ([SickTypeNo],[CName],[ShortCode],[DispOrder],[ContractCode]) VALUES ( 8,N'院感申请',N'8',8,N'Code_4');

 IF COL_LENGTH('SampleType', 'LabID') IS NULL ALTER TABLE SampleType ADD LabID bigint; 
                
 IF COL_LENGTH('SampleType', 'DataAddTime') IS NULL ALTER TABLE SampleType ADD DataAddTime datetime; 
                
IF COL_LENGTH('SampleType', 'DataTimeStamp') IS NULL ALTER TABLE SampleType ADD DataTimeStamp timestamp; 
                
                
IF COL_LENGTH('TestItem', 'LabID') IS NULL ALTER TABLE TestItem ADD LabID bigint; 
                
IF COL_LENGTH('TestItem', 'DataAddTime') IS NULL ALTER TABLE TestItem ADD DataAddTime datetime; 
                
IF COL_LENGTH('TestItem', 'DataTimeStamp') IS NULL ALTER TABLE TestItem ADD DataTimeStamp timestamp; 


IF COL_LENGTH('SickType', 'LabID') IS NULL ALTER TABLE SickType ADD LabID bigint; 
                
IF COL_LENGTH('SickType', 'DataAddTime') IS NULL ALTER TABLE SickType ADD DataAddTime datetime; 
                
IF COL_LENGTH('SickType', 'DataTimeStamp') IS NULL ALTER TABLE SickType ADD DataTimeStamp timestamp; 

IF COL_LENGTH('PGroup', 'LabID') IS NULL ALTER TABLE PGroup ADD LabID bigint;
                
IF COL_LENGTH('PGroup', 'DataAddTime') IS NULL ALTER TABLE PGroup ADD DataAddTime datetime;
                
IF COL_LENGTH('PGroup', 'DataTimeStamp') IS NULL ALTER TABLE PGroup ADD DataTimeStamp timestamp; 


update SampleType set LabID=0 where LabID is null;
update PGroup set LabID=0 where LabID is null;
update SickType set LabID=0 where LabID is null;
update TestItem set LabID=0 where LabID is null;

update SC_RecordItemLink set IsBillVisible=1