
 IF COL_LENGTH('Lis_TestForm', 'ESendStatus') IS NULL  
  alter table Lis_TestForm Add ESendStatus int null

IF COL_LENGTH('Lis_TestForm', 'RedoStatus') IS NULL  
  alter table Lis_TestForm Add RedoStatus int null

IF COL_LENGTH('Lis_TestForm', 'TestAllStatus') IS NULL  
  alter table Lis_TestForm Add TestAllStatus int null

IF COL_LENGTH('Lis_TestForm', 'ZFSysCheckStatus') IS NULL  
  alter table Lis_TestForm Add ZFSysCheckStatus int null

IF COL_LENGTH('Lis_TestForm', 'OldTestFormID') IS NULL  
  alter table Lis_TestForm Add OldTestFormID bigint null

