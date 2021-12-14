 IF COL_LENGTH('Blood_BOutItem', 'toHisFlag') IS NULL ALTER TABLE Blood_BOutItem ADD toHisFlag int;
                
IF COL_LENGTH('Blood_BOutItem', 'OrderFlag') IS NULL ALTER TABLE Blood_BOutItem ADD OrderFlag nvarchar(20);
                
IF COL_LENGTH('Blood_BOutItem', 'BloodAdverseCheckNo') IS NULL ALTER TABLE Blood_BOutItem ADD BloodAdverseCheckNo nvarchar(20);
                
IF COL_LENGTH('Blood_BOutItem', 'BloodAdverseCheckName') IS NULL ALTER TABLE Blood_BOutItem ADD BloodAdverseCheckName nvarchar(20);
                
IF COL_LENGTH('Blood_BOutItem', 'BloodAdverseCheckTime') IS NULL ALTER TABLE Blood_BOutItem ADD BloodAdverseCheckTime nvarchar(20);
                
IF COL_LENGTH('Blood_BOutItem', 'Output_no') IS NULL ALTER TABLE Blood_BOutItem ADD Output_no nvarchar(100);
                
IF COL_LENGTH('Blood_BOutItem', 'BloodAdverseDemo') IS NULL ALTER TABLE Blood_BOutItem ADD BloodAdverseDemo nvarchar(500);
                
IF COL_LENGTH('Blood_BOutItem', 'BnrNo') IS NULL ALTER TABLE Blood_BOutItem ADD BnrNo nvarchar(20);
                
IF COL_LENGTH('Blood_BOutItem', 'ScanID') IS NULL ALTER TABLE Blood_BOutItem ADD ScanID nvarchar(20);
                
IF COL_LENGTH('Blood_BOutItem', 'ScanTime') IS NULL ALTER TABLE Blood_BOutItem ADD ScanTime nvarchar(20);