
--运行参数升级脚本
IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5138720501622064189) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5138720501622064189,N'用血申请传入的患者参数非空字段',N'HIS',N'CONFIG',N'BL-HISN-FIED-0017',N'admId',N'用血申请传入的患者参数非空字段',10,1,0,N'2020-04-08 14:57:52',N'{"ItemXType":"textfield","ItemDefaultValue":"admId","ItemUnit":"","ItemDataSet":""}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5036215231652457320) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5036215231652457320,N'用血申请审核完成后是否返回给HIS',N'HIS',N'CONFIG',N'BL-ISTO-HISR-0018',N'true',N'用血申请审核完成后是否返回给HIS',20,1,0,N'2020-04-08 14:59:34',N'{"ItemXType":"radiogroup","ItemDefaultValue":"true","ItemUnit":"","ItemDataSet":"[{''true'':''是''},{''false'':''否''}]"}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5309967330537261186) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5309967330537261186,N'获取几天内的LIS检验结果',N'医生站',N'CONFIG',N'BL-LISG-DAYS-0015',N'7',N'获取几天内的LIS检验结果',10,1,0,N'2020-04-08 14:55:37',N'{"ItemXType":"numberfield","ItemDefaultValue":"7","ItemUnit":"","ItemDataSet":""}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 4664812460164271159) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,4664812460164271159,N'LIS结果为空时默认值',N'医生站',N'CONFIG',N'BL-LISR-DEVL-0016',N'检查中',N'新增用血申请时,检验项目LIS结果为空时,设置的默认值',20,1,0,N'2020-04-08 14:56:55',N'{"ItemXType":"textfield","ItemDefaultValue":"检查中","ItemUnit":"","ItemDataSet":""}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5000433943148799011) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5000433943148799011,N'是否允许手工选择患者ABO及患者Rh',N'医生站',N'CONFIG',N'BL-NULL-ISBH-0021',N'true',N'是否允许手工选择患者ABO及患者Rh',50,1,0,N'2020-04-08 15:19:35',N'{"ItemXType":"radiogroup","ItemDefaultValue":"true","ItemUnit":"","ItemDataSet":"[{''true'':''是''},{''false'':''否''}]"}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5481303247194050358) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5481303247194050358,N'紧急用血是否在用血申请确认提交时上传HIS',N'HIS',N'CONFIG',N'BL-ISTO-HISJ-0020',N'true',N'紧急用血是否在用血申请确认提交时上传HIS',40,1,0,N'2020-04-08 15:06:44',N'{"ItemXType":"radiogroup","ItemDefaultValue":"true","ItemUnit":"","ItemDataSet":"[{''true'':''是''},{''false'':''否''}]"}');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5686468594605305205) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5686468594605305205,N'申请作废时是否调用HIS作废接口',N'HIS',N'CONFIG',N'BL-ISTO-HISO-0019',N'true',N'申请作废时是否调用HIS作废接口',60,1,0,N'2020-04-08 15:09:34',N'{"ItemXType":"radiogroup","ItemDefaultValue":"true","ItemUnit":"","ItemDataSet":"[{''true'':''是''},{''false'':''否''}]"}');


--角色升级脚本
IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 1000) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,1000,0,0,N'系统管理员',N'系统',N'系统管理员',1,10,N'2020-04-02 11:03:13');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 10000) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,10000,0,0,N'智方管理',N'智方',N'ZFGL',N'ZFGL',1,-1000,N'2020-04-03 14:28:19');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20010) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20010,0,0,N'医生',N'医生站',N'YS',N'YS',1,20,N'2020-04-03 14:24:23');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20020) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20020,0,0,N'护士',N'护士站',N'HS',N'HS',1,30,N'2020-04-03 14:24:38');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20030) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20030,0,0,N'输血科',N'输血科',N'SXK',N'SXK',1,40,N'2020-04-03 14:25:49');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20040) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20040,0,0,N'护工角色',N'护工',N'HGJS',N'HGJS',1,30,N'2020-04-03 14:25:03');

IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20050) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20050,0,0,N'统计和报表',N'统计',N'TJHBB',N'TJHBB',1,50,N'2020-04-03 14:27:42');


--功能模块升级脚本

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100,0,0,0,0,3,N'package.PNG',N'血库系统',1,10,N'2020-04-02 10:36:15');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 1000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,1000,100,0,0,0,3,N'package.PNG',N'系统基础设置',N'XTJCSZ',N'XTJCSZ',1,10000,N'2016-03-22 17:54:17');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2000,100,0,0,0,3,N'package.PNG',N'医生站',1,500,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2010,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/director/index.html',N'科主任审核',N'KZRSH',N'KZRSH',1,400,N'2020-04-03 15:27:45');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2020,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/apply/index.html',N'用血申请',N'YXSQ',N'YXSQ',1,100,N'2020-04-03 15:27:04');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2030,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/senior/index.html',N'上级审核',N'SJSH',N'SJSH',1,300,N'2020-04-03 15:26:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2040,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/applyandreview/index.html',N'用库申请+',N'YKSQ',N'YKSQ',1,110,N'2020-04-03 15:25:04'); 

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2600) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2600,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/medical/index.html',N'医务处审批',N'YWCSP',N'YWCSP',1,500,N'2020-04-03 15:25:39'); 

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3000,100,0,0,0,3,N'package.PNG',N'护士站',N'HSZ',N'HSZ',1,600,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3010,3000,0,0,0,0,N'default.PNG',N'血袋领用确认',N'XDLYQR',N'XDLYQR',1,10,N'2020-04-03 14:10:33');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3020,3000,0,0,0,0,N'default.PNG',N'交叉配血条码打印',N'JCPXTMDY',N'JCPXTMDY',1,20,N'2020-04-03 14:10:45');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3030,3000,0,0,0,0,N'list.PNG',N'#Shell.class.blood.nursestation.bloodprohandover.App',N'血袋接收',N'XDJS',N'XDJS',1,40,N'2020-04-03 14:11:24');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3040,3000,0,0,0,0,N'list.PNG',N'#Shell.class.blood.nursestation.transrecord.App',N'输血过程记录',N'SXGCJL',N'SXGCJL',1,50,N'2020-04-03 14:11:49');



IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3050) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3050,3000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/nursestation/bloodbagretrieve/index.html',N'血袋回收',N'XDHS',N'XDHS',1,60,N'2020-04-03 14:12:54');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3060) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3060,3000,0,0,0,0,N'default.PNG',N'取血凭证',N'QXPZ',N'QXPZ',1,30,N'2020-04-03 14:11:06');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 4000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,4000,100,0,0,0,3,N'package.PNG',N'输血科',N'SXK',N'SXK',1,700,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 5000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,5000,100,0,0,0,3,N'package.PNG',N'库存管理',N'KCGL',N'KCGL',1,800,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 6000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,6000,100,0,0,0,3,N'package.PNG',N'统计和报表',N'TJHBB',N'TJHBB',1,900,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 6100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,6100,6000,0,0,0,0,N'list.PNG',N'#Shell.class.blood.statistics.reqntegrated.App',N'输血综合查询',N'SXZHCX',N'SXZHCX',1,10,N'2020-04-03 14:13:30');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7000,100,0,0,0,3,N'package.PNG',N'系统参数',N'XTCS',N'XTCS',1,100,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7100,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.blood.bparameters.runparams.App',N'运行参数',N'YXCS',N'YXCS',1,20,N'2020-04-03 14:04:34');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7200,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.bparameter.App?issys=1',N'全局参数',N'QJCS',N'QJCS',1,10,N'2020-04-03 14:04:02');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7300,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.blood.bparameters.setparams.Form',N'用户设置',N'YHSZ',N'YHSZ',1,30,N'2020-04-03 14:05:10');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8000,100,0,0,0,3,N'package.PNG',N'基础维护6.6',N'JCWH6.6',N'JCWH6.6',1,200,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8010,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.doctor.App',N'医生维护',N'YSWH',N'YSWH',1,30,N'2020-04-03 14:07:51');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8020,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.puser.App',N'人员维护',N'RYWH',N'RYWH',1,20,N'2020-04-03 14:07:29');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8030,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.deptuser.App',N'科室人员维护',N'KSRYWH',N'KSRYWH',1,40,N'2020-04-03 14:08:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8100,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.department.App',N'科室维护',N'KSWH',N'KSWH',1,10,N'2020-04-03 14:06:58');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9000,100,0,0,0,3,N'package.PNG',N'基础维护',N'JCWH',N'JCWH',1,300,N'2020-04-03 10:21:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9010,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dicttype.App',N'字典类型维护',N'ZDLXWH',N'ZDLXWH',1,10,N'2020-04-03 14:08:54');


IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9020,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dict.App',N'字典维护',N'ZDWH',N'ZDWH',1,20,N'2020-04-03 14:09:17');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9100,100,0,0,0,3,N'package.PNG',N'医生站设置',N'YSZSZ',N'YSZSZ',1,400,N'2020-04-03 14:01:25');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9110) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9110,9100,0,0,0,0,N'configuration.PNG',N'/layui/views/bloodtransfusion/sysbase/bloodusedesc/index.html',N'用血说明维护',N'YXSMWH',N'YXSMWH',1,10,N'2020-04-03 15:22:38');



IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9120) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9120,9100,0,0,0,0,N'configuration.PNG',N'就诊类型',N'JZLX',N'JZLX',1,20,N'2020-04-03 15:22:55');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9130) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9130,9100,0,0,0,0,N'configuration.PNG',N'申请类型',N'SQLX',N'SQLX',1,30,N'2020-04-03 15:23:10');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9200,100,0,0,0,3,N'package.PNG',N'护士站设置',N'HSZSZ',N'HSZSZ',1,410,N'2020-04-03 14:01:42');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9210) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9210,9200,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.transrecordtype.App',N'输血过程记录分类',N'SXGCJLFL',N'SXGCJLFL',1,10,N'2020-04-03 14:09:46');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9220) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9220,9200,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.transrecordtypeitem.App',N'输血过程记录项字典',N'SXGCJLXZD',N'SXGCJLXZD',1,20,N'2020-04-03 14:10:13');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9300,100,0,0,0,3,N'package.PNG',N'输血科设置',N'SXKSZ',N'SXKSZ',1,420,N'2020-04-03 14:02:10');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9310) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9310,9300,0,0,0,0,N'configuration.PNG',N'血制品单位',N'XZPDW',N'XZPDW',1,20,N'2020-04-03 15:23:57');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9320) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9320,9300,0,0,0,0,N'configuration.PNG',N'血制品维护',N'XZPWH',N'XZPWH',1,30,N'2020-04-03 15:24:12');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9330) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9330,9300,0,0,0,0,N'configuration.PNG',N'血库项目维护',N'XKXMWH',N'XKXMWH',1,40,N'2020-04-03 15:24:26');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9340) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9340,9300,0,0,0,0,N'dictionary.PNG',N'血制品类型',N'XZPLX',N'XZPLX',1,10,N'2020-04-03 15:23:43');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100010,1000,0,0,0,3,N'package.PNG',N'#Shell.class.sysbase.module.App',N'功能模块',N'XTJCSZ',N'XTJCSZ',1,10,N'2016-03-22 17:54:17');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100020,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.modulerole.App',N'模块角色',1,30,N'2020-04-02 16:31:36');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100030,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.App',N'角色管理',1,20,N'2020-04-02 16:31:18');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100040,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.userrole.App',N'员工角色',1,40,N'2020-04-02 16:33:52');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100050) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100050,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.roleuser.App',N'角色员工',1,50,N'2020-04-02 16:34:22');

IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100060) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100060,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.ModuleApp',N'角色权限',1,60,N'2020-04-02 16:35:11');

