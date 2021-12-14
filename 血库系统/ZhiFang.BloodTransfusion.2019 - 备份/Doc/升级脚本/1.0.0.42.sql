 IF COL_LENGTH('Blood_BOutForm', 'EndBloodOperId') IS NULL ALTER TABLE Blood_BOutForm ADD EndBloodOperId bigint; 
                
IF COL_LENGTH('Blood_BOutForm', 'EndBloodOperName') IS NULL ALTER TABLE Blood_BOutForm ADD EndBloodOperName varchar(50); 
                
IF COL_LENGTH('Blood_BOutForm', 'EndBloodOperTime') IS NULL ALTER TABLE Blood_BOutForm ADD EndBloodOperTime datetime; 
                
 IF COL_LENGTH('Blood_BOutForm', 'EndBloodReason') IS NULL ALTER TABLE Blood_BOutForm ADD EndBloodReason varchar(500); 