 IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 1000) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,1000,0,0,N'1000',N'1000',N'1000',N'ϵͳ����Ա',N'ϵͳ',N'XTGLY',N'XTGLY',N'ϵͳ����Ա',1,1000,N'2020-04-02 11:03:13',1);

 IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 10000) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,10000,0,0,N'10000',N'10000',N'10000',N'�Ƿ�����',N'�Ƿ�',N'ZFGL',N'ZFGL',1,-1000,N'2020-04-03 14:28:19',1);

 IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20010) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20010,0,0,N'20010',N'20010',N'20010',N'ҽ����ɫ',N'ҽ��վ',N'YSJS',N'YSJS',N'20060',1,20010,N'2020-04-03 14:24:23',2);

 IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20020) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20020,0,0,N'20020',N'20020',N'20020',N'��ʿ��ɫ',N'��ʿվ',N'HSJS',N'HSJS',1,20020,N'2020-04-03 14:24:38',3);

 IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20030) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20030,0,0,N'20030',N'20030',N'20030',N'��Ѫ��',N'��Ѫ��',N'SXK',N'SXK',1,20030,N'2020-04-03 14:25:49',7);

 IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20040) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20040,0,0,N'20040',N'20040',N'20040',N'������ɫ',N'����',N'HGJS',N'HGJS',1,20040,N'2020-04-03 14:25:03',5);
 
 IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20050) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20050,0,0,N'20050',N'20050',N'20050',N'ͳ�ƺͱ���',N'ͳ��',N'TJHBB',N'TJHBB',1,20050,N'2020-04-03 14:27:42',8);
 
 IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20060) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20060,0,0,N'20060',N'20060',N'20060',N'��Ⱦ����',N'����',N'GRGLB',N'GRGLB',N'��Ⱦ����',1,20060,N'2020-11-11 10:25:28',6);
