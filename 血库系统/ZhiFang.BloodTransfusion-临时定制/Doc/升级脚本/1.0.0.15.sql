IF COL_LENGTH('Blood_BReqForm', 'PrintTotal') IS NULL ALTER TABLE Blood_BReqForm ADD PrintTotal int; 

update Blood_BReqForm set PrintTotal=0 where PrintTotal<0 or PrintTotal is null; 