
IF COL_LENGTH('Lis_OperateAuthorize', 'OperateUser') IS NULL 
  ALTER TABLE Lis_OperateAuthorize ADD OperateUser nvarchar(200)

IF COL_LENGTH('Lis_OperateAuthorize', 'AuthorizeUser') IS NULL 
  ALTER TABLE Lis_OperateAuthorize ADD AuthorizeUser nvarchar(200)

IF COL_LENGTH('Lis_OperateAuthorize', 'Day0') IS NULL 
  alter  table  Lis_OperateAuthorize  Add  Day0  bit  null

IF COL_LENGTH('Lis_OperateAuthorize', 'Day1') IS NULL 
  alter  table  Lis_OperateAuthorize  Add  Day1  bit  null

IF COL_LENGTH('Lis_OperateAuthorize', 'Day2') IS NULL 
  alter  table  Lis_OperateAuthorize  Add  Day2  bit  null

IF COL_LENGTH('Lis_OperateAuthorize', 'Day3') IS NULL 
  alter  table  Lis_OperateAuthorize  Add  Day3  bit  null

IF COL_LENGTH('Lis_OperateAuthorize', 'Day4') IS NULL 
  alter  table  Lis_OperateAuthorize  Add  Day4  bit  null

IF COL_LENGTH('Lis_OperateAuthorize', 'Day5') IS NULL 
  alter  table  Lis_OperateAuthorize  Add  Day5  bit  null

IF COL_LENGTH('Lis_OperateAuthorize', 'Day6') IS NULL 
  alter  table  Lis_OperateAuthorize  Add  Day6  bit  null
