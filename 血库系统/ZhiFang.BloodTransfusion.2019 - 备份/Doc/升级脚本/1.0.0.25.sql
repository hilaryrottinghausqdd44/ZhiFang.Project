
IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5000278512090896900) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5000278512090896900,N'实验室数据升级版本',N'系统',N'CONFIG',N'BL-ULAB-DATA-0007',N'1.0.0.1',N'实验室数据升级版本',20,1,0,N'2020-02-15 10:16:52');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5081884485807967905) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5081884485807967905,N'登录后升级数据库',N'系统',N'CONFIG',N'BL-SYSE-UDAL-0001',N'1',N'1:是;0:否;',10,1,1,N'2020-02-15 10:13:44');

 IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5085688375696300791) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5085688375696300791,N'列表默认分页记录数',N'UI',N'CONFIG',N'BL-LRMP-UIPA-0003',N'10',N'列表默认分页记录数',10,1,1,N'2020-02-15 10:15:29');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5101233254823494116) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5101233254823494116,N'血袋接收是否需要护工完成取血确认',N'护士站',N'CONFIG',N'BL-BBAG-ISNC-0010',N'0',N'1:是;0:否;',10,1,1,N'2020-02-15 10:17:25');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260801) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260801,N'集成平台服务访问URL',N'集成平台',N'CONFIG',N'BL-SYSE-LURL-0002',N'http://localhost/ZhiFang.LabInformationIntegratePlatform',N'集成平台',10,1,1,N'2020-02-15 10:14:38');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5524339378542032882) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5524339378542032882,N'启用用户UI配置',N'UI',N'CONFIG',N'BL-EUSE-UICF-0008',N'0',N'1:是;0:否;',1,1,1,N'2020-02-15 10:15:59');

IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5691564273202278074) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[DataUpdateTime]) VALUES ( 0,5691564273202278074,N'输血申请单当前流水号',N'输血申请',N'CONFIG',N'BL-BRQF-CURN-0009',N'14',N'申请单号生成规则:年月日+4位顺序流水号(按天重新生成)',11,1,0,N'2019-10-08 16:06:52',N'2020-02-06 16:10:04');

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4703071337826980944) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4703071337826980944,4780816011182130210,N'10',N'立即停止输血,保持静脉通路',N'10',10,1,N'2020-02-14 17:19:27'); 

IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5215863475154392192) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5215863475154392192,4780816011182130210,N'20',N'对症处理',N'30',30,1,N'2020-02-14 17:20:27');
