  CREATE TABLE SendOrder(
	OrderNo varchar2(30) NOT NULL,
	CreateMan varchar2(20) NULL,
	CreateDate date NULL,
	SampleNum number NULL,
	Status number NULL,
	Note varchar2(50) NULL,
	LabCode varchar2(25) NULL
  )
  
  
  
alter table barcodeform add OrderNo varchar(30)--barcodeform表中增加"订单编号"字段

alter table testitem add CheckMethodName varchar(30) --testitem中增加"检测方法"字段

alter table SendOrder add IsConfirm int default 0 --订单表增加"是否确认" 

alter table NRequestItem add price float  --申请录入项目表
