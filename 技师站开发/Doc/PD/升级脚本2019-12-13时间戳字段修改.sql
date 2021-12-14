IF COL_LENGTH('Lis_OrderForm', 'DateTimeStamp') IS Not NULL  
  alter table Lis_OrderForm drop column DateTimeStamp 

IF COL_LENGTH('Lis_OrderItem', 'DateTimeStamp') IS Not NULL  
  alter table Lis_OrderItem drop column DateTimeStamp 

IF COL_LENGTH('Lis_BarCodeForm', 'DateTimeStamp') IS Not NULL  
  alter table Lis_BarCodeForm drop column DateTimeStamp 

IF COL_LENGTH('Lis_Patient', 'DateTimeStamp') IS Not NULL  
  alter table Lis_Patient drop column DateTimeStamp 

IF COL_LENGTH('Lis_Operate', 'DateTimeStamp') IS Not NULL  
  alter table Lis_Operate drop column DateTimeStamp 

IF COL_LENGTH('Lis_OperateAuthorize', 'DateTimeStamp') IS Not NULL  
  alter table Lis_OperateAuthorize drop column DateTimeStamp 

IF COL_LENGTH('Lis_OrderForm', 'DataTimeStamp') IS NULL  
  alter table Lis_OrderForm Add DataTimeStamp timestamp	null

IF COL_LENGTH('Lis_OrderItem', 'DataTimeStamp') IS NULL  
  alter table Lis_OrderItem Add DataTimeStamp timestamp	null

IF COL_LENGTH('Lis_BarCodeForm', 'DataTimeStamp') IS NULL  
  alter table Lis_BarCodeForm Add DataTimeStamp timestamp null

 IF COL_LENGTH('Lis_Patient', 'DataTimeStamp') IS NULL  
  alter table Lis_Patient Add DataTimeStamp timestamp null

IF COL_LENGTH('Lis_Operate', 'DataTimeStamp') IS NULL  
  alter table Lis_Operate Add DataTimeStamp timestamp null

IF COL_LENGTH('Lis_OperateAuthorize', 'DataTimeStamp') IS NULL  
  alter table Lis_OperateAuthorize Add DataTimeStamp timestamp null