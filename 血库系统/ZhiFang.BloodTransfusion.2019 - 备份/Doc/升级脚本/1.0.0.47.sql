

if not exists(select * from B_Parameter where ParameterID=4955399619796462166) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[Shortcode],[DispOrder],[PinYinZiTou],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,4955399619796462166,N'用血申请确认后是否自动完成审批',N'医生站',N'CONFIG',N'BL-BLCF-ISAC-0024',N'false',N'true:是;false:否;',N'5',5,N'5',1,0,N'2020-09-09 14:46:42',N'{"ItemXType":"radiogroup","ItemDefaultValue":"false","ItemUnit":"","ItemDataSet":"[{''true'':''是''},{''false'':''否''}]"}');
