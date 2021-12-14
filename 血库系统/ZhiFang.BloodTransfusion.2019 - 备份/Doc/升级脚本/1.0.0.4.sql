 IF COL_LENGTH('Blood_BReqForm', 'ReqTotal') IS NULL ALTER TABLE Blood_BReqForm ADD ReqTotal float;
                

                 IF COL_LENGTH('Blood_BreqForm', 'ObsoleteID') IS NULL ALTER TABLE Blood_BreqForm ADD ObsoleteID bigint;
                

                 IF COL_LENGTH('Blood_BreqForm', 'ObsoleteName') IS NULL ALTER TABLE Blood_BreqForm ADD ObsoleteName varchar(20);
                

                 IF COL_LENGTH('Blood_BreqForm', 'ObsoleteTime') IS NULL ALTER TABLE Blood_BreqForm ADD ObsoleteTime datetime;
                

                 IF COL_LENGTH('Blood_BreqForm', 'ObsoleteMemo') IS NULL ALTER TABLE Blood_BreqForm ADD ObsoleteMemo varchar(200);
                
 IF COL_LENGTH('Blood_BReqEditItem', 'ReqCode') IS NULL ALTER TABLE Blood_BReqEditItem ADD ReqCode varchar(20);