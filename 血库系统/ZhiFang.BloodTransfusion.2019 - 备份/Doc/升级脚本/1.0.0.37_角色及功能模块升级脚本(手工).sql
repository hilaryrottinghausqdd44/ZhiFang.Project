
--���в��������ű�
IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5138720501622064189) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5138720501622064189,N'��Ѫ���봫��Ļ��߲����ǿ��ֶ�',N'HIS',N'CONFIG',N'BL-HISN-FIED-0017',N'admId',N'��Ѫ���봫��Ļ��߲����ǿ��ֶ�',10,1,0,N'2020-04-08 14:57:52',N'{"ItemXType":"textfield","ItemDefaultValue":"admId","ItemUnit":"","ItemDataSet":""}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5036215231652457320) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5036215231652457320,N'��Ѫ���������ɺ��Ƿ񷵻ظ�HIS',N'HIS',N'CONFIG',N'BL-ISTO-HISR-0018',N'true',N'��Ѫ���������ɺ��Ƿ񷵻ظ�HIS',20,1,0,N'2020-04-08 14:59:34',N'{"ItemXType":"radiogroup","ItemDefaultValue":"true","ItemUnit":"","ItemDataSet":"[{''true'':''��''},{''false'':''��''}]"}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5309967330537261186) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5309967330537261186,N'��ȡ�����ڵ�LIS������',N'ҽ��վ',N'CONFIG',N'BL-LISG-DAYS-0015',N'7',N'��ȡ�����ڵ�LIS������',10,1,0,N'2020-04-08 14:55:37',N'{"ItemXType":"numberfield","ItemDefaultValue":"7","ItemUnit":"","ItemDataSet":""}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 4664812460164271159) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,4664812460164271159,N'LIS���Ϊ��ʱĬ��ֵ',N'ҽ��վ',N'CONFIG',N'BL-LISR-DEVL-0016',N'�����',N'������Ѫ����ʱ,������ĿLIS���Ϊ��ʱ,���õ�Ĭ��ֵ',20,1,0,N'2020-04-08 14:56:55',N'{"ItemXType":"textfield","ItemDefaultValue":"�����","ItemUnit":"","ItemDataSet":""}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5000433943148799011) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5000433943148799011,N'�Ƿ������ֹ�ѡ����ABO������Rh',N'ҽ��վ',N'CONFIG',N'BL-NULL-ISBH-0021',N'true',N'�Ƿ������ֹ�ѡ����ABO������Rh',50,1,0,N'2020-04-08 15:19:35',N'{"ItemXType":"radiogroup","ItemDefaultValue":"true","ItemUnit":"","ItemDataSet":"[{''true'':''��''},{''false'':''��''}]"}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5481303247194050358) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5481303247194050358,N'������Ѫ�Ƿ�����Ѫ����ȷ���ύʱ�ϴ�HIS',N'HIS',N'CONFIG',N'BL-ISTO-HISJ-0020',N'true',N'������Ѫ�Ƿ�����Ѫ����ȷ���ύʱ�ϴ�HIS',40,1,0,N'2020-04-08 15:06:44',N'{"ItemXType":"radiogroup","ItemDefaultValue":"true","ItemUnit":"","ItemDataSet":"[{''true'':''��''},{''false'':''��''}]"}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5686468594605305205) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5686468594605305205,N'��������ʱ�Ƿ����HIS���Ͻӿ�',N'HIS',N'CONFIG',N'BL-ISTO-HISO-0019',N'true',N'��������ʱ�Ƿ����HIS���Ͻӿ�',60,1,0,N'2020-04-08 15:09:34',N'{"ItemXType":"radiogroup","ItemDefaultValue":"true","ItemUnit":"","ItemDataSet":"[{''true'':''��''},{''false'':''��''}]"}');


--��ɫ�����ű�
IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 1000) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,1000,0,0,N'ϵͳ����Ա',N'ϵͳ',N'ϵͳ����Ա',1,10,N'2020-04-02 11:03:13');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 10000) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,10000,0,0,N'�Ƿ�����',N'�Ƿ�',N'ZFGL',N'ZFGL',1,-1000,N'2020-04-03 14:28:19');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20010) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20010,0,0,N'ҽ��',N'ҽ��վ',N'YS',N'YS',1,20,N'2020-04-03 14:24:23');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20020) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20020,0,0,N'��ʿ',N'��ʿվ',N'HS',N'HS',1,30,N'2020-04-03 14:24:38');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20030) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20030,0,0,N'��Ѫ��',N'��Ѫ��',N'SXK',N'SXK',1,40,N'2020-04-03 14:25:49');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20040) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20040,0,0,N'������ɫ',N'����',N'HGJS',N'HGJS',1,30,N'2020-04-03 14:25:03');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20050) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20050,0,0,N'ͳ�ƺͱ���',N'ͳ��',N'TJHBB',N'TJHBB',1,50,N'2020-04-03 14:27:42');


--����ģ�������ű�

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100,0,0,0,0,3,N'package.PNG',N'Ѫ��ϵͳ',1,10,N'2020-04-02 10:36:15');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 1000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,1000,100,0,0,0,3,N'package.PNG',N'ϵͳ��������',N'XTJCSZ',N'XTJCSZ',1,10000,N'2016-03-22 17:54:17');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2000,100,0,0,0,3,N'package.PNG',N'ҽ��վ',1,500,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2010,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/director/index.html',N'���������',N'KZRSH',N'KZRSH',1,400,N'2020-04-03 15:27:45');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2020,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/apply/index.html',N'��Ѫ����',N'YXSQ',N'YXSQ',1,100,N'2020-04-03 15:27:04');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2030,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/senior/index.html',N'�ϼ����',N'SJSH',N'SJSH',1,300,N'2020-04-03 15:26:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2040,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/applyandreview/index.html',N'�ÿ�����+',N'YKSQ',N'YKSQ',1,110,N'2020-04-03 15:25:04'); 

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2600) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2600,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/medical/index.html',N'ҽ������',N'YWCSP',N'YWCSP',1,500,N'2020-04-03 15:25:39'); 

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3000,100,0,0,0,3,N'package.PNG',N'��ʿվ',N'HSZ',N'HSZ',1,600,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3010,3000,0,0,0,0,N'default.PNG',N'Ѫ������ȷ��',N'XDLYQR',N'XDLYQR',1,10,N'2020-04-03 14:10:33');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3020,3000,0,0,0,0,N'default.PNG',N'������Ѫ�����ӡ',N'JCPXTMDY',N'JCPXTMDY',1,20,N'2020-04-03 14:10:45');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3030,3000,0,0,0,0,N'list.PNG',N'#Shell.class.blood.nursestation.bloodprohandover.App',N'Ѫ������',N'XDJS',N'XDJS',1,40,N'2020-04-03 14:11:24');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3040,3000,0,0,0,0,N'list.PNG',N'#Shell.class.blood.nursestation.transrecord.App',N'��Ѫ���̼�¼',N'SXGCJL',N'SXGCJL',1,50,N'2020-04-03 14:11:49');



IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3050) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3050,3000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/nursestation/bloodbagretrieve/index.html',N'Ѫ������',N'XDHS',N'XDHS',1,60,N'2020-04-03 14:12:54');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3060) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3060,3000,0,0,0,0,N'default.PNG',N'ȡѪƾ֤',N'QXPZ',N'QXPZ',1,30,N'2020-04-03 14:11:06');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 4000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,4000,100,0,0,0,3,N'package.PNG',N'��Ѫ��',N'SXK',N'SXK',1,700,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 5000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,5000,100,0,0,0,3,N'package.PNG',N'������',N'KCGL',N'KCGL',1,800,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 6000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,6000,100,0,0,0,3,N'package.PNG',N'ͳ�ƺͱ���',N'TJHBB',N'TJHBB',1,900,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 6100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,6100,6000,0,0,0,0,N'list.PNG',N'#Shell.class.blood.statistics.reqntegrated.App',N'��Ѫ�ۺϲ�ѯ',N'SXZHCX',N'SXZHCX',1,10,N'2020-04-03 14:13:30');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7000,100,0,0,0,3,N'package.PNG',N'ϵͳ����',N'XTCS',N'XTCS',1,100,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7100,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.blood.bparameters.runparams.App',N'���в���',N'YXCS',N'YXCS',1,20,N'2020-04-03 14:04:34');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7200,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.bparameter.App?issys=1',N'ȫ�ֲ���',N'QJCS',N'QJCS',1,10,N'2020-04-03 14:04:02');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7300,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.blood.bparameters.setparams.Form',N'�û�����',N'YHSZ',N'YHSZ',1,30,N'2020-04-03 14:05:10');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8000,100,0,0,0,3,N'package.PNG',N'����ά��6.6',N'JCWH6.6',N'JCWH6.6',1,200,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8010,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.doctor.App',N'ҽ��ά��',N'YSWH',N'YSWH',1,30,N'2020-04-03 14:07:51');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8020,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.puser.App',N'��Աά��',N'RYWH',N'RYWH',1,20,N'2020-04-03 14:07:29');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8030,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.deptuser.App',N'������Աά��',N'KSRYWH',N'KSRYWH',1,40,N'2020-04-03 14:08:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8100,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.department.App',N'����ά��',N'KSWH',N'KSWH',1,10,N'2020-04-03 14:06:58');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9000,100,0,0,0,3,N'package.PNG',N'����ά��',N'JCWH',N'JCWH',1,300,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9010,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dicttype.App',N'�ֵ�����ά��',N'ZDLXWH',N'ZDLXWH',1,10,N'2020-04-03 14:08:54');


IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9020,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dict.App',N'�ֵ�ά��',N'ZDWH',N'ZDWH',1,20,N'2020-04-03 14:09:17');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9100,100,0,0,0,3,N'package.PNG',N'ҽ��վ����',N'YSZSZ',N'YSZSZ',1,400,N'2020-04-03 14:01:25');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9110) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9110,9100,0,0,0,0,N'configuration.PNG',N'/layui/views/bloodtransfusion/sysbase/bloodusedesc/index.html',N'��Ѫ˵��ά��',N'YXSMWH',N'YXSMWH',1,10,N'2020-04-03 15:22:38');



IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9120) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9120,9100,0,0,0,0,N'configuration.PNG',N'��������',N'JZLX',N'JZLX',1,20,N'2020-04-03 15:22:55');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9130) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9130,9100,0,0,0,0,N'configuration.PNG',N'��������',N'SQLX',N'SQLX',1,30,N'2020-04-03 15:23:10');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9200,100,0,0,0,3,N'package.PNG',N'��ʿվ����',N'HSZSZ',N'HSZSZ',1,410,N'2020-04-03 14:01:42');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9210) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9210,9200,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.transrecordtype.App',N'��Ѫ���̼�¼����',N'SXGCJLFL',N'SXGCJLFL',1,10,N'2020-04-03 14:09:46');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9220) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9220,9200,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.transrecordtypeitem.App',N'��Ѫ���̼�¼���ֵ�',N'SXGCJLXZD',N'SXGCJLXZD',1,20,N'2020-04-03 14:10:13');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9300,100,0,0,0,3,N'package.PNG',N'��Ѫ������',N'SXKSZ',N'SXKSZ',1,420,N'2020-04-03 14:02:10');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9310) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9310,9300,0,0,0,0,N'configuration.PNG',N'Ѫ��Ʒ��λ',N'XZPDW',N'XZPDW',1,20,N'2020-04-03 15:23:57');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9320) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9320,9300,0,0,0,0,N'configuration.PNG',N'Ѫ��Ʒά��',N'XZPWH',N'XZPWH',1,30,N'2020-04-03 15:24:12');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9330) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9330,9300,0,0,0,0,N'configuration.PNG',N'Ѫ����Ŀά��',N'XKXMWH',N'XKXMWH',1,40,N'2020-04-03 15:24:26');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9340) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9340,9300,0,0,0,0,N'dictionary.PNG',N'Ѫ��Ʒ����',N'XZPLX',N'XZPLX',1,10,N'2020-04-03 15:23:43');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100010,1000,0,0,0,3,N'package.PNG',N'#Shell.class.sysbase.module.App',N'����ģ��',N'XTJCSZ',N'XTJCSZ',1,10,N'2016-03-22 17:54:17');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100020,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.modulerole.App',N'ģ���ɫ',1,30,N'2020-04-02 16:31:36');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100030,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.App',N'��ɫ����',1,20,N'2020-04-02 16:31:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100040,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.userrole.App',N'Ա����ɫ',1,40,N'2020-04-02 16:33:52');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100050) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100050,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.roleuser.App',N'��ɫԱ��',1,50,N'2020-04-02 16:34:22');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100060) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100060,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.ModuleApp',N'��ɫȨ��',1,60,N'2020-04-02 16:35:11');

