
--1.数据类型调整
--Blood_BOutItem(出库明细)的BloodNo由原来的nvarchar(20)调整为int;
if Exists(Select * from SysColumns where [Name]='BloodNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodNo int;

--Blood_BOutItem(出库明细)的BloodABONo由原来的int调整为nvarchar(20);
if Exists(Select * from SysColumns where [Name]='BloodABONo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodABONo nvarchar(20);

--Blood_BOutItem(出库明细)的BloodUnitNo由原来的nvarchar(20)调整为int;
if Exists(Select * from SysColumns where [Name]='BloodUnitNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodUnitNo int;

--Blood_BInItem(入库明细)的BloodABONo由原来的int调整为nvarchar(20);
if Exists(Select * from SysColumns where [Name]='BloodABONo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BInItem')) alter table Blood_BInItem ALTER COLUMN BloodABONo nvarchar(20);