
--1.�������͵���
--Blood_BOutItem(������ϸ)��BloodNo��ԭ����nvarchar(20)����Ϊint;
if Exists(Select * from SysColumns where [Name]='BloodNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodNo int;

--Blood_BOutItem(������ϸ)��BloodABONo��ԭ����int����Ϊnvarchar(20);
if Exists(Select * from SysColumns where [Name]='BloodABONo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodABONo nvarchar(20);

--Blood_BOutItem(������ϸ)��BloodUnitNo��ԭ����nvarchar(20)����Ϊint;
if Exists(Select * from SysColumns where [Name]='BloodUnitNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodUnitNo int;

--Blood_BInItem(�����ϸ)��BloodABONo��ԭ����int����Ϊnvarchar(20);
if Exists(Select * from SysColumns where [Name]='BloodABONo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BInItem')) alter table Blood_BInItem ALTER COLUMN BloodABONo nvarchar(20);