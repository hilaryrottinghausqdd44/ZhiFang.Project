 IF COL_LENGTH('B_Parameter', 'NodeID') IS NULL ALTER TABLE B_Parameter ADD NodeID bigint; 

 IF COL_LENGTH('B_Parameter', 'GroupNo') IS NULL ALTER TABLE B_Parameter ADD GroupNo bigint; 

 IF COL_LENGTH('Blood_BreqForm', 'LabID') IS NULL ALTER TABLE Blood_BreqForm ADD LabID bigint; 

 IF COL_LENGTH('Blood_BreqForm', 'DispOrder') IS NULL ALTER TABLE Blood_BreqForm ADD DispOrder int; 

 IF COL_LENGTH('Blood_BreqForm', 'DataAddTime') IS NULL ALTER TABLE Blood_BreqForm ADD DataAddTime datetime; 

 IF COL_LENGTH('Blood_BreqForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BreqForm ADD DataTimeStamp timestamp; 

 IF COL_LENGTH('Blood_BreqForm', 'Visible') IS NULL ALTER TABLE Blood_BreqForm ADD Visible bit; 

 IF COL_LENGTH('Blood_BreqForm', 'HisDeptID') IS NULL ALTER TABLE Blood_BreqForm ADD HisDeptID varchar(20); 

 IF COL_LENGTH('Blood_BreqForm', 'HisDoctorId') IS NULL ALTER TABLE Blood_BreqForm ADD HisDoctorId varchar(20); 

 IF COL_LENGTH('Blood_BreqForm', 'CheckCompleteFlag') IS NULL ALTER TABLE Blood_BreqForm ADD CheckCompleteFlag bit; 

IF COL_LENGTH('Blood_BreqForm', 'CheckCompleteTime') IS NULL ALTER TABLE Blood_BreqForm ADD CheckCompleteTime datetime; 

 IF COL_LENGTH('Blood_BreqForm', 'ApplyID') IS NULL ALTER TABLE Blood_BreqForm ADD ApplyID bigint; 

 IF COL_LENGTH('Blood_BreqForm', 'ApplyName') IS NULL ALTER TABLE Blood_BreqForm ADD ApplyName varchar(20); 

 IF COL_LENGTH('Blood_BreqForm', 'ApplyTime') IS NULL ALTER TABLE Blood_BreqForm ADD ApplyTime datetime; 

IF COL_LENGTH('Blood_BreqForm', 'ApplyMemo') IS NULL ALTER TABLE Blood_BreqForm ADD ApplyMemo varchar(200); 

 IF COL_LENGTH('Blood_BreqForm', 'SeniorID') IS NULL ALTER TABLE Blood_BreqForm ADD SeniorID bigint; 

 IF COL_LENGTH('Blood_BreqForm', 'SeniorName') IS NULL ALTER TABLE Blood_BreqForm ADD SeniorName varchar(20); 

 IF COL_LENGTH('Blood_BreqForm', 'SeniorTime') IS NULL ALTER TABLE Blood_BreqForm ADD SeniorTime datetime; 

 IF COL_LENGTH('Blood_BreqForm', 'SeniorMemo') IS NULL ALTER TABLE Blood_BreqForm ADD SeniorMemo varchar(200); 

 IF COL_LENGTH('Blood_BreqForm', 'DirectorID') IS NULL ALTER TABLE Blood_BreqForm ADD DirectorID bigint; 

 IF COL_LENGTH('Blood_BreqForm', 'DirectorName') IS NULL ALTER TABLE Blood_BreqForm ADD DirectorName varchar(20); 

 IF COL_LENGTH('Blood_BreqForm', 'DirectorTime') IS NULL ALTER TABLE Blood_BreqForm ADD DirectorTime datetime; 

 IF COL_LENGTH('Blood_BreqForm', 'DirectorMemo') IS NULL ALTER TABLE Blood_BreqForm ADD DirectorMemo varchar(200); 

 IF COL_LENGTH('Blood_BreqForm', 'MedicalID') IS NULL ALTER TABLE Blood_BreqForm ADD MedicalID bigint; 

 IF COL_LENGTH('Blood_BreqForm', 'MedicalName') IS NULL ALTER TABLE Blood_BreqForm ADD MedicalName varchar(20); 

 IF COL_LENGTH('Blood_BreqForm', 'MedicalTime') IS NULL ALTER TABLE Blood_BreqForm ADD MedicalTime datetime;

 IF COL_LENGTH('Blood_BreqForm', 'MedicalMemo') IS NULL ALTER TABLE Blood_BreqForm ADD MedicalMemo varchar(200); 

 IF COL_LENGTH('Blood_BreqForm', 'BreqStatusID') IS NULL ALTER TABLE Blood_BreqForm ADD BreqStatusID bigint; 

 IF COL_LENGTH('Blood_BreqForm', 'BreqStatusName') IS NULL ALTER TABLE Blood_BreqForm ADD BreqStatusName varchar(20); 

 Update Blood_BreqForm set DispOrder=0 where DispOrder is null; 

 Update Blood_BreqForm set Visible=1 where Visible is null; 

 Update Blood_BreqForm set LabID=0 where LabID is null; 

 Update Blood_BreqForm set DataAddTime=getDate() where DataAddTime is null; 
 IF COL_LENGTH('Blood_BReqItem', 'LabID') IS NULL ALTER TABLE Blood_BReqItem ADD LabID bigint; 
 IF COL_LENGTH('Blood_BReqItem', 'DispOrder') IS NULL ALTER TABLE Blood_BReqItem ADD DispOrder int; 
 IF COL_LENGTH('Blood_BReqItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BReqItem ADD DataAddTime datetime; 
 IF COL_LENGTH('Blood_BReqItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BReqItem ADD DataTimeStamp timestamp; 
 IF COL_LENGTH('Blood_BReqItem', 'Visible') IS NULL ALTER TABLE Blood_BReqItem ADD Visible bit; 
 Update Blood_BReqItem set DispOrder=0 where DispOrder is null; 
 Update Blood_BReqItem set Visible=1 where Visible is null; 
 Update Blood_BReqItem set LabID=0 where LabID is null; 
 Update Blood_BReqItem set DataAddTime=getDate() where DataAddTime is null;
 IF COL_LENGTH('Blood_LargeUseitem', 'LabID') IS NULL ALTER TABLE Blood_LargeUseitem ADD LabID bigint; 
 IF COL_LENGTH('Blood_LargeUseitem', 'DispOrder') IS NULL ALTER TABLE Blood_LargeUseitem ADD DispOrder int; 
 IF COL_LENGTH('Blood_LargeUseitem', 'DataAddTime') IS NULL ALTER TABLE Blood_LargeUseitem ADD DataAddTime datetime; 
 IF COL_LENGTH('Blood_LargeUseitem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_LargeUseitem ADD DataTimeStamp timestamp; 
 IF COL_LENGTH('Blood_LargeUseitem', 'Visible') IS NULL ALTER TABLE Blood_LargeUseitem ADD Visible bit; 
 Update Blood_LargeUseitem set DispOrder=0 where DispOrder is null; 
 Update Blood_LargeUseitem set Visible=1 where Visible is null; 
 Update Blood_LargeUseitem set LabID=0 where LabID is null; 
Update Blood_LargeUseitem set DataAddTime=getDate() where DataAddTime is null; 

 IF COL_LENGTH('Blood_BreqItemResult', 'LabID') IS NULL ALTER TABLE Blood_BreqItemResult ADD LabID bigint; 
  IF COL_LENGTH('Blood_BreqItemResult', 'DispOrder') IS NULL ALTER TABLE Blood_BreqItemResult ADD DispOrder int; 
  IF COL_LENGTH('Blood_BreqItemResult', 'DataAddTime') IS NULL ALTER TABLE Blood_BreqItemResult ADD DataAddTime datetime; 
  IF COL_LENGTH('Blood_BreqItemResult', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BreqItemResult ADD DataTimeStamp timestamp; 
  IF COL_LENGTH('Blood_BreqItemResult', 'Visible') IS NULL ALTER TABLE Blood_BreqItemResult ADD Visible bit; 
  Update Blood_BreqItemResult set DispOrder=0 where DispOrder is null; 
   Update Blood_BreqItemResult set Visible=1 where Visible is null; 
   Update Blood_BreqItemResult set LabID=0 where LabID is null; 
   Update Blood_BreqItemResult set DataAddTime=getDate() where DataAddTime is null; 

 IF COL_LENGTH('Blood_LargeUseForm', 'LabID') IS NULL ALTER TABLE Blood_LargeUseForm ADD LabID bigint; 
IF COL_LENGTH('Blood_LargeUseForm', 'DispOrder') IS NULL ALTER TABLE Blood_LargeUseForm ADD DispOrder int; 
IF COL_LENGTH('Blood_LargeUseForm', 'DataAddTime') IS NULL ALTER TABLE Blood_LargeUseForm ADD DataAddTime datetime; 
IF COL_LENGTH('Blood_LargeUseForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_LargeUseForm ADD DataTimeStamp timestamp; 
IF COL_LENGTH('Blood_LargeUseForm', 'Visible') IS NULL ALTER TABLE Blood_LargeUseForm ADD Visible bit; 
Update Blood_LargeUseForm set DispOrder=0 where DispOrder is null; 
Update Blood_LargeUseForm set Visible=1 where Visible is null; 
Update Blood_LargeUseForm set LabID=0 where LabID is null; 
Update Blood_LargeUseForm set DataAddTime=getDate() where DataAddTime is null; 

  IF COL_LENGTH('Blood_LargeUseItem', 'Id') IS NULL ALTER TABLE Blood_LargeUseItem ADD Id bigint; 
IF COL_LENGTH('Blood_LargeUseItem', 'LabID') IS NULL ALTER TABLE Blood_LargeUseItem ADD LabID bigint; 
IF COL_LENGTH('Blood_LargeUseItem', 'DispOrder') IS NULL ALTER TABLE Blood_LargeUseItem ADD DispOrder int; 
IF COL_LENGTH('Blood_LargeUseItem', 'DataAddTime') IS NULL ALTER TABLE Blood_LargeUseItem ADD DataAddTime datetime; 
IF COL_LENGTH('Blood_LargeUseItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_LargeUseItem ADD DataTimeStamp timestamp; 
IF COL_LENGTH('Blood_LargeUseItem', 'Visible') IS NULL ALTER TABLE Blood_LargeUseItem ADD Visible bit; 
Update Blood_LargeUseItem set DispOrder=0 where DispOrder is null; 
Update Blood_LargeUseItem set Visible=1 where Visible is null; 
Update Blood_LargeUseItem set LabID=0 where LabID is null; 
Update Blood_LargeUseItem set DataAddTime=getDate() where DataAddTime is null; 
                

  IF COL_LENGTH('blood_useType', 'LabID') IS NULL ALTER TABLE blood_useType ADD LabID bigint; 
IF COL_LENGTH('blood_useType', 'DispOrder') IS NULL ALTER TABLE blood_useType ADD DispOrder int; 
IF COL_LENGTH('blood_useType', 'DataAddTime') IS NULL ALTER TABLE blood_useType ADD DataAddTime datetime; 
IF COL_LENGTH('blood_useType', 'DataTimeStamp') IS NULL ALTER TABLE blood_useType ADD DataTimeStamp timestamp; 
IF COL_LENGTH('blood_useType', 'Visible') IS NULL ALTER TABLE blood_useType ADD Visible bit; 
Update blood_useType set DispOrder=0 where DispOrder is null; 
Update blood_useType set Visible=1 where Visible is null; 
Update blood_useType set LabID=0 where LabID is null; 
Update blood_useType set DataAddTime=getDate() where DataAddTime is null; 
                

 IF COL_LENGTH('Blood_BReqEditItem', 'LabID') IS NULL ALTER TABLE Blood_BReqEditItem ADD LabID bigint; 
 IF COL_LENGTH('Blood_BReqEditItem', 'DispOrder') IS NULL ALTER TABLE Blood_BReqEditItem ADD DispOrder int; 
IF COL_LENGTH('Blood_BReqEditItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BReqEditItem ADD DataAddTime datetime; 
IF COL_LENGTH('Blood_BReqEditItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BReqEditItem ADD DataTimeStamp timestamp; 
IF COL_LENGTH('Blood_BReqEditItem', 'Visible') IS NULL ALTER TABLE Blood_BReqEditItem ADD Visible bit; 
Update Blood_BReqEditItem set DispOrder=0 where DispOrder is null; 
Update Blood_BReqEditItem set Visible=1 where Visible is null; 
Update Blood_BReqEditItem set LabID=0 where LabID is null; 
Update Blood_BReqEditItem set DataAddTime=getDate() where DataAddTime is null; 

 IF COL_LENGTH('Blood_style', 'LabID') IS NULL ALTER TABLE Blood_style ADD LabID bigint; 
IF COL_LENGTH('Blood_style', 'DispOrder') IS NULL ALTER TABLE Blood_style ADD DispOrder int; 
IF COL_LENGTH('Blood_style', 'DataAddTime') IS NULL ALTER TABLE Blood_style ADD DataAddTime datetime; 
IF COL_LENGTH('Blood_style', 'DataTimeStamp') IS NULL ALTER TABLE Blood_style ADD DataTimeStamp timestamp; 
IF COL_LENGTH('Blood_style', 'Visible') IS NULL ALTER TABLE Blood_style ADD Visible bit; 
Update Blood_style set DispOrder=0 where DispOrder is null; 
Update Blood_style set Visible=1 where Visible is null; 
Update Blood_style set LabID=0 where LabID is null;
Update Blood_style set DataAddTime=getDate() where DataAddTime is null; 
                

  IF COL_LENGTH('blood_btestItem', 'LabID') IS NULL ALTER TABLE blood_btestItem ADD LabID bigint; 
IF COL_LENGTH('blood_btestItem', 'DataAddTime') IS NULL ALTER TABLE blood_btestItem ADD DataAddTime datetime; 
IF COL_LENGTH('blood_btestItem', 'DataTimeStamp') IS NULL ALTER TABLE blood_btestItem ADD DataTimeStamp timestamp; 
Update blood_btestItem set LabID=0 where LabID is null; 
Update blood_btestItem set DataAddTime=getDate() where DataAddTime is null; 

 IF COL_LENGTH('blood_unit', 'LabID') IS NULL ALTER TABLE blood_unit ADD LabID bigint; 
IF COL_LENGTH('blood_unit', 'DataAddTime') IS NULL ALTER TABLE blood_unit ADD DataAddTime datetime; 
IF COL_LENGTH('blood_unit', 'DataTimeStamp') IS NULL ALTER TABLE blood_unit ADD DataTimeStamp timestamp; 
Update blood_unit set LabID=0 where LabID is null; 
Update blood_unit set DataAddTime=getDate() where DataAddTime is null; 

 IF COL_LENGTH('blood_class', 'LabID') IS NULL ALTER TABLE blood_class ADD LabID bigint; 
 IF COL_LENGTH('blood_class', 'DispOrder') IS NULL ALTER TABLE blood_class ADD DispOrder int; 
IF COL_LENGTH('blood_class', 'DataAddTime') IS NULL ALTER TABLE blood_class ADD DataAddTime datetime; 
IF COL_LENGTH('blood_class', 'DataTimeStamp') IS NULL ALTER TABLE blood_class ADD DataTimeStamp timestamp; 
IF COL_LENGTH('blood_class', 'Visible') IS NULL ALTER TABLE blood_class ADD Visible bit; 
Update blood_class set DispOrder=0 where DispOrder is null; 
Update blood_class set Visible=1 where Visible is null; 
Update blood_class set LabID=0 where LabID is null; 
Update blood_class set DataAddTime=getDate() where DataAddTime is null; 
                

 IF COL_LENGTH('blood_useType', 'LabID') IS NULL ALTER TABLE blood_useType ADD LabID bigint; 
IF COL_LENGTH('blood_useType', 'DataAddTime') IS NULL ALTER TABLE blood_useType ADD DataAddTime datetime; 
IF COL_LENGTH('blood_useType', 'DataTimeStamp') IS NULL ALTER TABLE blood_useType ADD DataTimeStamp timestamp; 
IF COL_LENGTH('blood_useType', 'Visible') IS NULL ALTER TABLE blood_useType ADD Visible bit; 
Update blood_useType set Visible=1 where Visible is null; 
Update blood_useType set LabID=0 where LabID is null; 
Update blood_useType set DataAddTime=getDate() where DataAddTime is null; 
                

 IF COL_LENGTH('Department', 'LabID') IS NULL ALTER TABLE Department ADD LabID bigint; 
 IF COL_LENGTH('Department', 'DataTimeStamp') IS NULL ALTER TABLE Department ADD DataTimeStamp timestamp; 
Update Department set LabID=0 where LabID is null; 
                

 IF COL_LENGTH('PUser', 'LabID') IS NULL ALTER TABLE PUser ADD LabID bigint; 
 IF COL_LENGTH('PUser', 'DataTimeStamp') IS NULL ALTER TABLE PUser ADD DataTimeStamp timestamp; 
 Update PUser set LabID=0 where LabID is null; 

  IF COL_LENGTH('doctor', 'LabID') IS NULL ALTER TABLE doctor ADD LabID bigint; 
IF COL_LENGTH('doctor', 'DataTimeStamp') IS NULL ALTER TABLE doctor ADD DataTimeStamp timestamp; 
IF COL_LENGTH('blood_useType', 'DataAddTime') IS NULL ALTER TABLE blood_useType ADD DataAddTime datetime; 
Update doctor set LabID=0 where LabID is null; 
Update blood_useType set DataAddTime=getDate() where DataAddTime is null; 
                

 IF COL_LENGTH('Blood_BReqType', 'LabID') IS NULL ALTER TABLE Blood_BReqType ADD LabID bigint; 
 IF COL_LENGTH('Blood_BReqType', 'DispOrder') IS NULL ALTER TABLE Blood_BReqType ADD DispOrder int; 
IF COL_LENGTH('Blood_BReqType', 'DataAddTime') IS NULL ALTER TABLE Blood_BReqType ADD DataAddTime datetime; 
 IF COL_LENGTH('Blood_BReqType', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BReqType ADD DataTimeStamp timestamp; 
 IF COL_LENGTH('Blood_BReqType', 'Visible') IS NULL ALTER TABLE Blood_BReqType ADD Visible bit; 
 Update Blood_BReqType set DispOrder=0 where DispOrder is null; 
Update Blood_BReqType set Visible=1 where Visible is null; 
Update Blood_BReqType set LabID=0 where LabID is null; 
 Update Blood_BReqType set DataAddTime=getDate() where DataAddTime is null; 
                

 IF COL_LENGTH('Blood_BReqTypeItem', 'LabID') IS NULL ALTER TABLE Blood_BReqTypeItem ADD LabID bigint; 
IF COL_LENGTH('Blood_BReqTypeItem', 'DispOrder') IS NULL ALTER TABLE Blood_BReqTypeItem ADD DispOrder int; 
IF COL_LENGTH('Blood_BReqTypeItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BReqTypeItem ADD DataAddTime datetime; 
 IF COL_LENGTH('Blood_BReqTypeItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BReqTypeItem ADD DataTimeStamp timestamp; 
 IF COL_LENGTH('Blood_BReqTypeItem', 'Visible') IS NULL ALTER TABLE Blood_BReqTypeItem ADD Visible bit; 
Update Blood_BReqTypeItem set DispOrder=0 where DispOrder is null; 
 Update Blood_BReqTypeItem set Visible=1 where Visible is null; 
 Update Blood_BReqTypeItem set LabID=0 where LabID is null; 
Update Blood_BReqTypeItem set DataAddTime=getDate() where DataAddTime is null; 
                

 IF COL_LENGTH('Blood_BReqEditItem', 'LisCode') IS NULL ALTER TABLE Blood_BReqEditItem ADD LisCode varchar(20); 
 Update Blood_BReqEditItem set LabID=0 where LabID is null; 
                

  IF COL_LENGTH('Blood_BreqForm', 'AeniorID') IS NOT NULL exec sp_rename 'Blood_BreqForm.AeniorID','SeniorID';
 IF COL_LENGTH('Blood_BreqForm', 'AeniorName') IS NOT NULL exec sp_rename 'Blood_BreqForm.AeniorName','SeniorName'; 
IF COL_LENGTH('Blood_BreqForm', 'AeniorTime') IS NOT NULL exec sp_rename 'Blood_BreqForm.AeniorTime','SeniorTime'; 
 IF COL_LENGTH('Blood_BreqForm', 'AeniorMemo') IS NOT NULL exec sp_rename 'Blood_BreqForm.AeniorMemo','SeniorMemo'; 
                

 IF COL_LENGTH('Blood_LargeUseForm', 'breqformIDLast') IS NULL ALTER TABLE Blood_LargeUseForm ADD breqformIDLast nvarchar(40);              
 IF COL_LENGTH('Blood_BreqForm', 'CheckDoctorNo') IS NOT NULL ALTER TABLE Blood_BreqForm DROP COLUMN CheckDoctorNo; 
 IF COL_LENGTH('Blood_BreqForm', 'Checktime') IS NOT NULL ALTER TABLE Blood_BreqForm DROP COLUMN Checktime; 
IF COL_LENGTH('Blood_LargeUseForm', 'breqformIDLast') IS NULL ALTER TABLE Blood_LargeUseForm ADD breqformIDLast varchar(20); 
                

  IF COL_LENGTH('Blood_BReqForm', 'ComNoIndex') IS not NULL ALTER TABLE Blood_BReqForm   DROP COLUMN ComNoIndex ; 
 IF COL_LENGTH('Blood_BreqForm', 'PatABO') IS NULL ALTER TABLE Blood_BreqForm ADD PatABO varchar(10); 
 IF COL_LENGTH('Blood_BreqForm', 'PatRh') IS NULL ALTER TABLE Blood_BreqForm ADD PatRh varchar(10); 
 IF COL_LENGTH('Blood_BreqForm', 'BloodWay') IS NULL ALTER TABLE Blood_BreqForm ADD BloodWay varchar(20); 
         
 IF COL_LENGTH('Blood_docGrade', 'LabID') IS NULL ALTER TABLE Blood_docGrade ADD LabID bigint; 
 IF COL_LENGTH('Blood_docGrade', 'DataAddTime') IS NULL ALTER TABLE Blood_docGrade ADD DataAddTime datetime; 
 IF COL_LENGTH('Blood_docGrade', 'DataTimeStamp') IS NULL ALTER TABLE Blood_docGrade ADD DataTimeStamp timestamp; 
 Update Blood_docGrade set DataAddTime=getDate() where DataAddTime is null; 




















