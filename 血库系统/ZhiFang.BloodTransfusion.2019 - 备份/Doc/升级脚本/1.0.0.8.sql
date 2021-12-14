IF COL_LENGTH('Blood_BReqFormResult', 'ItemLisResult') IS NULL ALTER TABLE Blood_BReqFormResult ADD ItemLisResult varchar(500);

 IF COL_LENGTH('Blood_BReqFormResult', 'BTestItemEName') IS NULL ALTER TABLE Blood_BReqFormResult ADD BTestItemEName varchar(150); 

IF COL_LENGTH('Blood_BTestItem', 'IsResultItem') IS NULL ALTER TABLE Blood_BTestItem ADD IsResultItem bit; 

 update Blood_BTestItem set IsResultItem=1 where IsResultItem is null; 

 IF COL_LENGTH('Blood_BReqForm', 'ToHisFlag') IS NULL ALTER TABLE Blood_BReqForm ADD ToHisFlag int; 