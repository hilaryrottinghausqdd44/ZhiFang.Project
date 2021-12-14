 IF COL_LENGTH('Blood_Patinfo', 'AdmID') IS NULL ALTER TABLE Blood_Patinfo ADD AdmID varchar(20); 

 IF COL_LENGTH('Blood_BReqForm', 'AdmID') IS NULL ALTER TABLE Blood_BReqForm ADD AdmID varchar(20); 

 update Blood_BReqForm set BReqFormFlag=0 where BReqFormFlag is null; 

 update Blood_BReqForm set BReqFormFlag=0 where BReqFormFlag is null; 

 update Blood_BReqForm set ToHisFlag=0 where ToHisFlag is null and DataAddTime>'2019-08-01 00:00:00.000'; 