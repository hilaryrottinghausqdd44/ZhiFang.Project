
IF COL_LENGTH('Blood_TransItem', 'NumberItemResult') IS NULL ALTER TABLE Blood_TransItem ADD NumberItemResult float;

update Blood_BOutForm set ConfirmCompletion=1 where ConfirmCompletion is null;

update Blood_BOutForm set HandoverCompletion=1 where HandoverCompletion is null; 

update Blood_BOutForm set CourseCompletion=1 where CourseCompletion is null;

 update Blood_BOutForm set RecoverCompletion=1 where RecoverCompletion is null;

 update Blood_BOutItem set ConfirmCompletion=1 where ConfirmCompletion is null;

update Blood_BOutItem set HandoverCompletion=1 where HandoverCompletion is null;

update Blood_BOutItem set CourseCompletion=1 where CourseCompletion is null;

update Blood_BOutItem set RecoverCompletion=1 where RecoverCompletion is null;