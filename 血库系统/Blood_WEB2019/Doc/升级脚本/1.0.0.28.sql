
IF COL_LENGTH('Blood_TransForm', 'HasAdverseReactions') IS NULL ALTER TABLE Blood_TransForm ADD HasAdverseReactions bit;

IF COL_LENGTH('Blood_TransForm', 'AdverseReactionsTime') IS NULL ALTER TABLE Blood_TransForm ADD AdverseReactionsTime datetime;

IF COL_LENGTH('Blood_TransForm', 'AdverseReactionsHP') IS NULL ALTER TABLE Blood_TransForm ADD AdverseReactionsHP float;

if Exists(Select * from SysColumns where [Name]='BloodUnitNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodUnitNo int; 