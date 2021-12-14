if exists(select * from sysobjects where name='AK_Lis_OrderForm_OrderFormNo')
alter table dbo.Lis_OrderForm drop constraint AK_Lis_OrderForm_OrderFormNo;
go

alter table dbo.Lis_OrderForm
   add constraint AK_Lis_OrderForm_OrderFormNo unique (OrderFormNo)
go
