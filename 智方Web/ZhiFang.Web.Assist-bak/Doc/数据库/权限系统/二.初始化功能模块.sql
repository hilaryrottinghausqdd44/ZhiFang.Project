

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 20) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20,0,0,0,0,0,N'package.PNG',N'����ά��',N'JCWH',N'JCWH',1,-200,N'2020-11-02 15:34:25');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30,0,0,0,0,3,N'package.PNG',N'Ժ��ϵͳ',N'YGXT',N'YGXT',1,30,N'2020-11-02 15:36:43');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100,0,0,0,0,3,N'package.PNG',N'Ѫ��ϵͳ',N'XKXT',N'XKXT',1,100,N'2020-04-02 10:36:15');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 1000) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,1000,0,0,0,0,3,N'package.PNG',N'Ȩ��ϵͳ',N'QXXT',N'QXXT',1,10000,N'2016-03-22 17:54:17');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2000)
INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2000,100,0,0,0,3,N'package.PNG',N'ҽ��վ',1,500,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7000)
INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7000,20,0,0,0,3,N'package.PNG',N'ϵͳ����',N'XTCS',N'XTCS',1,100,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7100,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.assist.bparameters.runparams.App',N'���в���',N'YXCS',N'YXCS',1,20,N'2020-04-03 14:04:34');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7200,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.bparameter.App?issys=1',N'ȫ�ֲ���',N'QJCS',N'QJCS',1,10,N'2020-04-03 14:04:02');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7300,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.assist.bparameters.setparams.Form',N'�û�����',N'YHSZ',N'YHSZ',1,30,N'2020-04-03 14:05:10');



IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8000,20,0,0,0,3,N'package.PNG',N'����ά��6.6',N'JCWH6.6',N'JCWH6.6',1,200,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8010,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.doctor.App',N'ҽ��ά��',N'YSWH',N'YSWH',1,30,N'2020-04-03 14:07:51');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8020,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.puser.App',N'��Աά��',N'RYWH',N'RYWH',1,20,N'2020-04-03 14:07:29');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8030,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.deptuser.App',N'������Աά��',N'KSRYWH',N'KSRYWH',1,40,N'2020-04-03 14:08:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8100,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.department.App',N'����ά��',N'KSWH',N'KSWH',1,10,N'2020-04-03 14:06:58');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9000,20,0,0,0,3,N'package.PNG',N'����ά��',N'JCWH',N'JCWH',1,300,N'2020-04-03 10:21:18')

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9010,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dicttype.App',N'�����ֵ�����',N'GGZDLX',N'GGZDLX',1,10,N'2020-04-03 14:08:54');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9020,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dict.App',N'�����ֵ�ά��',N'GGZDWH',N'GGZDWH',1,20,N'2020-04-03 14:09:17');



 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30000,30,0,0,0,3,N'package.PNG',N'����ά��',N'JCWH',N'JCWH',1,30000,N'2020-11-11 10:18:29');

 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30001) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30001,30000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordtype.App',N'�������ά��',N'JCLXWH',N'JCLXWH',1,30001,N'2020-11-11 10:19:44');

 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30002) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30002,30000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordtypeitem.App',N'��Ʒ��Ϣά��',N'XYJLXWH',N'XYJLXWH',1,30002,N'2020-11-11 10:20:21');

 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30003) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30003,30000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordphrase.ofdept.App',N'��¼�����-������',N'JLXDY-AKS',N'JLXDY-AKS',1,30003,N'2020-11-11 10:21:00');

 IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30004) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30004,30000,0,0,0,0,N'list.PNG',N'#Shell.class.sysbase.deptautochecklink.App',N'�����Զ�����ά��',N'KSZDHS',N'KSZDHS',1,30004,N'2020-11-11 10:21:51');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] =30005) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30005,30000,0,0,0,0,N'list.PNG',N'#Shell.class.sysbase.screcorditemlink.App',N'��¼�����͹�ϵ',N'JLXLXGX',N'JLXLXGX',1,30002,N'2020-11-24 17:08:12');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30010,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordtype.App',N'��¼������',N'JLXLX',N'JLXLX',1,30010,N'2020-11-02 15:46:32');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30020,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordtypeitem.App',N'��¼���ֵ�',N'JLXZD',N'JLXZD',1,30020,N'2020-11-02 15:46:48');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30100,30,0,0,0,0,N'list.PNG',N'#Shell.class.assist.infection.apply.App',N'Ժ�еǼ�',N'YGDJ',N'YGDJ',1,100,N'2020-11-02 15:44:49');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30200,30,0,0,0,0,N'list.PNG',N'#Shell.class.assist.infection.assess.App',N'Ժ������',N'YGPG',N'YGPG',1,30200,N'2020-11-02 15:45:19');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30211) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30211,30,0,0,0,0,N'search.PNG',N'#Shell.class.assist.infection.alertinfo.App',N'Ժ�д�����',N'YGDCL',N'YGDCL',1,30211,N'2020-12-11 16:23:11');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30300,30,0,0,0,0,N'list.PNG',N'#Shell.class.assist.statistics.infection.App',N'����ͳ��(ȫ������)',N'YGPG',N'YGPG',1,30300,N'2020-11-02 15:45:19');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100010,1000,0,0,0,3,N'package.PNG',N'#Shell.class.sysbase.module.App',N'����ģ��',N'XTJCSZ',N'XTJCSZ',1,10,N'2016-03-22 17:54:17');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100020,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.modulerole.App',N'ģ���ɫ',1,30,N'2020-04-02 16:31:36');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100030,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.App',N'��ɫ����',1,20,N'2020-04-02 16:31:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100040,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.userrole.App',N'Ա����ɫ',1,40,N'2020-04-02 16:33:52');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100050) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100050,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.roleuser.App',N'��ɫԱ��',1,50,N'2020-04-02 16:34:22');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100060) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100060,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.ModuleApp',N'��ɫȨ��',1,60,N'2020-04-02 16:35:11');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30015) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30015,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordphrase.App1',N'��¼�����Ͷ���',N'JLXLXDY',N'JLXLXDY',1,30015,N'2020-11-03 15:06:50');
 
IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30025) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30025,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordphrase.App2',N'��¼�����',N'JLXDY',N'JLXDY',1,30025,N'2020-11-03 15:07:36');
 
IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30030,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcorditemlink.App',N'��¼�����͹�ϵ',N'JLXLXGX',N'JLXLXGX',1,30030,N'2020-11-04 14:27:22'); 

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] =30040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30040,9000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordphrase.ofdept.App',N'��¼�����-������',N'JLXJY-AKS',N'JLXJY-AKS',1,30040,N'2020-11-10 14:21:26');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 31000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,31000,30,0,0,0,3,N'package.PNG',N'��¼����',N'31000',N'DLKS',N'DLKS',1,30010,N'2020-12-14 14:39:45');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30400) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30400,31000,0,0,0,0,N'list.PNG',N'#Shell.class.assist.statistics.infection.ofcurdept.App',N'����ͳ��',N'BBTJ',N'BBTJ',1,30400,N'2020-11-25 16:19:15');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30410) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30410,31000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordphrase.ofcurdept.App',N'��¼��������',N'30410',N'JLXJGDY',N'JLXJGDY',1,30410,N'2020-12-15 15:01:06');