  CREATE TABLE SendOrder(
	OrderNo varchar2(30) NOT NULL,
	CreateMan varchar2(20) NULL,
	CreateDate date NULL,
	SampleNum number NULL,
	Status number NULL,
	Note varchar2(50) NULL,
	LabCode varchar2(25) NULL
  )
  
  
  
alter table barcodeform add OrderNo varchar(30)--barcodeform��������"�������"�ֶ�

alter table testitem add CheckMethodName varchar(30) --testitem������"��ⷽ��"�ֶ�

alter table SendOrder add IsConfirm int default 0 --����������"�Ƿ�ȷ��" 

alter table NRequestItem add price float  --����¼����Ŀ��
