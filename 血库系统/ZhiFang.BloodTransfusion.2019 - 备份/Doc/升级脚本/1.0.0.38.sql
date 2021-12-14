

IF COL_LENGTH('Blood_BUnit', 'LabID') IS NULL ALTER TABLE Blood_BUnit ADD LabID bigint; 
IF COL_LENGTH('Blood_BUnit', 'PinYinZiTou') IS NULL ALTER TABLE Blood_BUnit ADD PinYinZiTou varchar(50); 
IF COL_LENGTH('Blood_BUnit', 'SName') IS NULL ALTER TABLE Blood_BUnit ADD SName varchar(50); 
IF COL_LENGTH('Blood_BUnit', 'DispOrder') IS NULL ALTER TABLE Blood_BUnit ADD DispOrder int; 
IF COL_LENGTH('Blood_BUnit', 'DataAddTime') IS NULL ALTER TABLE Blood_BUnit ADD DataAddTime datetime; 
IF COL_LENGTH('Blood_BUnit', 'Visible') IS NULL ALTER TABLE Blood_BUnit ADD Visible bit;
IF COL_LENGTH('Blood_BUnit', 'Visible') IS NOT NULL ALTER TABLE Blood_BUnit ALTER COLUMN Visible bit;
update Blood_BUnit set Visible=1 where Visible is null;


IF COL_LENGTH('Blood_Unit', 'LabID') IS NULL ALTER TABLE Blood_Unit ADD LabID bigint; 
IF COL_LENGTH('Blood_Unit', 'PinYinZiTou') IS NULL ALTER TABLE Blood_Unit ADD PinYinZiTou varchar(50); 
IF COL_LENGTH('Blood_Unit', 'SName') IS NULL ALTER TABLE Blood_Unit ADD SName varchar(50); 
IF COL_LENGTH('Blood_Unit', 'DispOrder') IS NULL ALTER TABLE Blood_Unit ADD DispOrder int; 
IF COL_LENGTH('Blood_Unit', 'DataAddTime') IS NULL ALTER TABLE Blood_Unit ADD DataAddTime datetime; 
IF COL_LENGTH('Blood_Unit', 'Visible') IS NULL ALTER TABLE Blood_Unit ADD Visible bit;
IF COL_LENGTH('Blood_Unit', 'Visible') IS NOT NULL ALTER TABLE Blood_Unit ALTER COLUMN Visible bit;
update Blood_Unit set Visible=1 where Visible is null;

IF COL_LENGTH('Blood_Class', 'PinYinZiTou') IS NULL ALTER TABLE Blood_Class ADD PinYinZiTou varchar(50); 
IF COL_LENGTH('Blood_Class', 'SName') IS NULL ALTER TABLE Blood_Class ADD SName varchar(50); 

IF COL_LENGTH('Blood_BReqType', 'PinYinZiTou') IS NULL ALTER TABLE Blood_BReqType ADD PinYinZiTou varchar(50); 
IF COL_LENGTH('Blood_BReqType', 'SName') IS NULL ALTER TABLE Blood_BReqType ADD SName varchar(50); 
              
IF COL_LENGTH('Blood_UseType', 'PinYinZiTou') IS NULL ALTER TABLE Blood_UseType ADD PinYinZiTou varchar(50); 
IF COL_LENGTH('Blood_UseType', 'SName') IS NULL ALTER TABLE Blood_UseType ADD SName varchar(50); 

                
IF COL_LENGTH('blood_style', 'PinYinZiTou') IS NULL ALTER TABLE blood_style ADD PinYinZiTou varchar(50); 
IF COL_LENGTH('blood_style', 'SName') IS NULL ALTER TABLE blood_style ADD SName varchar(50); 

 IF COL_LENGTH('Blood_BTestItem', 'PinYinZiTou') IS NULL ALTER TABLE Blood_BTestItem ADD PinYinZiTou varchar(50); 
 IF COL_LENGTH('Blood_BTestItem', 'SName') IS NULL ALTER TABLE Blood_BTestItem ADD SName varchar(50); 
 
update RBAC_Module set URL='#Shell.class.sysbase.bloodbreqtype.App' where URL is null and [ModuleID]=9120;
update RBAC_Module set URL='#Shell.class.sysbase.bloodusetype.App' where URL is null and [ModuleID]=9130;

update RBAC_Module set URL='#Shell.class.sysbase.bloodclass.App' where URL is null and [ModuleID]=9340;
update RBAC_Module set URL='#Shell.class.sysbase.bloodunit.App' where URL is null and [ModuleID]=9310;  

update RBAC_Module set URL='#Shell.class.sysbase.bloodstyle.App' where URL is null and [ModuleID]=9320;
update RBAC_Module set URL='#Shell.class.sysbase.bloodbtestitem.App' where URL is null and [ModuleID]=9330;     

IF COL_LENGTH('blood_style', 'BloodUnitNo') IS NOT NULL ALTER TABLE blood_style ALTER COLUMN BloodUnitNo int;


   