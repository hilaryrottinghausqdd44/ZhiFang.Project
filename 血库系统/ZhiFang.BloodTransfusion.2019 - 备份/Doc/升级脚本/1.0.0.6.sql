 IF COL_LENGTH('B_Parameter', 'NodeID') IS NULL ALTER TABLE B_Parameter ADD NodeID bigint; 
IF COL_LENGTH('B_Parameter', 'GroupNo') IS NULL ALTER TABLE B_Parameter ADD GroupNo bigint; 
                

IF NOT EXISTS (SELECT * FROM PUser WHERE [UserNo] = 19999) INSERT INTO [dbo].[PUser]([UserNo] ,[CName] ,[Password] ,[ShortCode] ,[Gender] ,[Birthday] ,[Role] ,[Resume] ,[Visible] ,[DispOrder] ,[HisOrderCode] ,[userimage] ,[usertype] ,[SectorTypeNo] ,[UserImeName] ,[IsManager] ,[PassWordS] ,[CAUserName] ,[CAContainerName] ,[CAUserID] ,[CAkeysn] ,[DeptNo] ,[PWDateTime] ,[LoginDateTime] ,[code_1] ,[code_2] ,[code_3] ,[code_4] ,[code_5] ,[CAUserAuthorised] ,[UserDataRights] ,[Email] ,[Imgsignature] ,[LabID]) VALUES (19999 ,'血库免登录' ,'=er5flP5' ,'19999' ,1 ,'1900-01-01 00:00:00.000' ,'医生站' ,1 ,1 ,19999  ,NULL ,NULL ,'医生' ,NULL ,NULL ,0 ,'=er5flP5' ,NULL ,NULL ,NULL ,NULL ,NULL ,'2019-07-08 13:26:18.243' ,'2019-07-08 13:26:18.243' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,0);

IF COL_LENGTH('B_Parameter', 'LowLimit') IS NULL ALTER TABLE Blood_docGrade ADD LowLimit float; 
 IF COL_LENGTH('Blood_docGrade', 'UpperLimit') IS NULL ALTER TABLE Blood_docGrade ADD UpperLimit float; 
IF COL_LENGTH('Blood_docGrade', 'LabID') IS NOT NULL Update Blood_docGrade set LabID=0 where LabID is null; 
Update Blood_docGrade set LowLimit=0,UpperLimit=800 where GradeNo=2 and LowLimit is null and UpperLimit is null;
 Update Blood_docGrade set LowLimit=801,UpperLimit=1600 where GradeNo=3 and LowLimit is null and UpperLimit is null;
Update Blood_docGrade set LowLimit=1600,UpperLimit=100000000 where GradeNo=4 and LowLimit is null and UpperLimit is null;