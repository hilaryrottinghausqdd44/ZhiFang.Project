 IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5691564273202278074) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[DataUpdateTime]) VALUES ( 0,5691564273202278074,N'输血申请单当前流水号',N'输血申请',N'CONFIG',N'BL-BRQF-CURN-0009',N'0',N'申请单号生成规则:年月日+4位顺序流水号(按天重新生成)',11,1,0,N'2019-10-08 16:06:52',N'2019-10-08 16:06:52');

IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 5586711528094669431) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,5586711528094669431,N'用血方式',260,N'用血方式',1,N'2019-10-08 16:06:52',N'BloodWay'); 

IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 10001) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,10001,5586711528094669431,N'异型输血',1,N'异型输血',1,N'2019-10-08 16:06:52'); 

IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 10002) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,10002,5586711528094669431,N'自体输血',2,N'自体输血',1,N'2019-10-08 16:06:52'); 