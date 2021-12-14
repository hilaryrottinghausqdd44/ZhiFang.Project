 
UPDATE [dbo].[Blood_docGrade] SET [BCount] =800,[LowLimit] =0,[UpperLimit] =800 WHERE [GradeName]='申请医生' and UpperLimit is null;

 IF COL_LENGTH('Blood_Patinfo', 'IsOrder') IS NULL ALTER TABLE Blood_Patinfo ADD IsOrder varchar(20); 
  
IF COL_LENGTH('Blood_Patinfo', 'IsLabNoC') IS NULL ALTER TABLE Blood_Patinfo ADD IsLabNoC varchar(20); 

update Blood_Patinfo set IsOrder='有' where  IsOrder is null; 

update Blood_Patinfo set IsLabNoC='有' where  IsLabNoC is null; 
