 IF COL_LENGTH('Blood_Patinfo', 'isagree') IS NULL ALTER TABLE Blood_Patinfo ADD isagree varchar(20); 

 IF COL_LENGTH('Blood_Patinfo', 'WardNo') IS NULL ALTER TABLE Blood_Patinfo ADD WardNo varchar(50); 

IF COL_LENGTH('Blood_BReqForm', 'OrganTransplant') IS NULL ALTER TABLE Blood_BReqForm ADD OrganTransplant varchar(10);
IF COL_LENGTH('Blood_BReqForm', 'MarrowTransplantation') IS NULL ALTER TABLE Blood_BReqForm ADD MarrowTransplantation varchar(10);
IF COL_LENGTH('Blood_BReqForm', 'WardNo') IS NULL ALTER TABLE Blood_BReqForm ADD WardNo varchar(50);
IF COL_LENGTH('Blood_BReqForm', 'HisWardNo') IS NULL ALTER TABLE Blood_BReqForm ADD HisWardNo varchar(50);
IF COL_LENGTH('Blood_BReqForm', 'BLPreEvaluation') IS NULL ALTER TABLE Blood_BReqForm ADD BLPreEvaluation varchar(5000);
               