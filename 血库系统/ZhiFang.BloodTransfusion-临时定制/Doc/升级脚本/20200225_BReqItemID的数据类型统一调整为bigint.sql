
/***
	一.数据库升级脚本调整;
	BReqItemID的数据类型统一调整为bigint;
	1.Blood_BOutItem(出库明细)的BReqItemID为nvarchar(20);
	2.Blood_BPreForm(配血主单)的BReqItemID为nvarchar(20);
	3.Blood_AnesthesiaMsg(麻醉消息字典)的BReqItemID为int;
*/

--1.Blood_BOutItem(出库明细)的BReqItemID由原来的nvarchar(20)调整为bigint;
if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BReqItemID bigint;

--2.Blood_BPreForm(配血主单)的BReqItemID由原来的nvarchar(20)调整为bigint;
if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BPreForm')) alter table Blood_BPreForm ALTER COLUMN BReqItemID bigint;

--2.Blood_BPreForm(麻醉消息字典)的BReqItemID由原来的int调整为bigint;
if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_AnesthesiaMsg')) alter table Blood_AnesthesiaMsg ALTER COLUMN BReqItemID bigint;